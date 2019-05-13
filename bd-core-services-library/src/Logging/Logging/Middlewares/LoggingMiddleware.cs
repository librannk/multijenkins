
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Serilog;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using BD.Core.Logging.Models;
using OpenTracing;
using BD.Core.Logging.Configuration;

namespace BD.Core.Logging.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITracer _tracer;
        private readonly TraceConfiguration _traceConfiguration;

        public LoggingMiddleware(RequestDelegate next, TraceConfiguration traceConfiguration, ITracer tracer = null)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _traceConfiguration = traceConfiguration ?? throw new ArgumentNullException(nameof(traceConfiguration));
            _tracer = tracer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            SetHeaders(context);

            var curCorrelationId = context.Request.Headers[Model.CORRELATION_CURRENT_ID].ToString();
            var orgCorrelationId = context.Request.Headers[Model.CORRELATION_ORIGIN_ID].ToString();
            var parCorrelationId = string.IsNullOrWhiteSpace(context.Request.Headers[Model.CORRELATION_PARENT_ID])
                                    ? null : context.Request.Headers[Model.CORRELATION_PARENT_ID].ToString();
            var user = context.Request.Headers[Model.USER].ToString();
            var traceId = string.IsNullOrWhiteSpace(context.Request.Headers[Model.B3_TRACE_ID]) ? _tracer?.ActiveSpan?.Context.TraceId : context.Request.Headers[Model.B3_TRACE_ID].ToString();

            try
            {
                //Add using for adding more properties

                using (LogContext.PushProperty(Model.CORRELATION_CURRENT_ID, curCorrelationId, true))
                using (LogContext.PushProperty(Model.CORRELATION_ORIGIN_ID, orgCorrelationId, true))
                using (LogContext.PushProperty(Model.CORRELATION_PARENT_ID, parCorrelationId, true))
                using (LogContext.PushProperty(Model.METHOD, context.Request.Method, true))
                using (LogContext.PushProperty(Model.USER, user, true))
                using (traceId != null ? LogContext.PushProperty(Model.Trace_Id, traceId, true) : null)
                {
                    //if trace is on then delegate will be invoked before 
                    //response headers will be sent to the client
                    if (_traceConfiguration.Enabled)
                    {
                        //trace if there is no endpoint to exclude OR not matching request path
                        if (string.IsNullOrWhiteSpace(_traceConfiguration.ExcludePattern) ||
                            !Regex.IsMatch(context.Request.Path, _traceConfiguration.ExcludePattern, RegexOptions.IgnoreCase))
                        {
                            // Start the Timer using Stopwatch  
                            var watch = new Stopwatch();
                            var logEventEnricher = LogContext.Clone();

                            watch.Start();

                            context.Response.OnStarting(() =>
                            {
                                // Stop the timer information and calculate the time
                                watch.Stop();

                                var responseTimeNS = watch.ElapsedMilliseconds * 1000000;

                                using (LogContext.Push(logEventEnricher))
                                using (LogContext.PushProperty(Model.DURATION_NS, responseTimeNS, true))
                                using (LogContext.PushProperty(Model.STATUS_CODE, context.Response.StatusCode, true))
                                {
                                    Log.Information("TRACING");
                                }

                                return Task.CompletedTask;
                            });
                        }
                    }

                    await _next(context);
                }
            }
            //To make sure that we don't loose the scope in case of an unexpected error
            catch (Exception ex) when (LogOnUnexpectedError(ex))
            {
                return;
            }
        }

        private void SetHeaders(HttpContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Request.Headers[Model.CORRELATION_CURRENT_ID]))
                context.Request.Headers[Model.CORRELATION_CURRENT_ID] = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(context.Request.Headers[Model.CORRELATION_PARENT_ID]))
                context.Request.Headers[Model.CORRELATION_PARENT_ID] =
                    context.Request.Headers[Model.CORRELATION_CURRENT_ID];

            //Origin Correlation ID will always be set with first current Correlation ID within the request
            if (string.IsNullOrWhiteSpace(context.Request.Headers[Model.CORRELATION_ORIGIN_ID]))
            {
                context.Request.Headers[Model.CORRELATION_ORIGIN_ID] =
                    context.Request.Headers[Model.CORRELATION_CURRENT_ID];
            }

            if (string.IsNullOrWhiteSpace(context.Request.Headers[Model.USER]))
                context.Request.Headers[Model.USER] = "system";

            //Response Header
            context.Response.Headers.Add(Model.CORRELATION_ORIGIN_ID, context.Request.Headers[Model.CORRELATION_ORIGIN_ID]);
        }

        private bool LogOnUnexpectedError(Exception ex)
        {
            Log.Error(ex, "An unexpected exception occured!");
            return true;
        }
    }
}