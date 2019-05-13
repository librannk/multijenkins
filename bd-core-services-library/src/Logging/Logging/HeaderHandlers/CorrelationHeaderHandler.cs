
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BD.Core.Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Propagation;

namespace BD.Core.Logging.HeaderHandlers
{
    public class CorrelationHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITracer _tracer;
        private readonly ILogger<CorrelationHeaderHandler> _logger;

        private const string TRACE_ID = "X-B3-TraceId";
        private const string SPAN_ID = "X-B3-SpanId";
        private const string PARENT_SPAN_ID = "X-B3-ParentSpanId";
        private const string SAMPLE_FLAG = "X-B3-Sampled";

        public CorrelationHeaderHandler(IHttpContextAccessor httpContextAccessor, ITracer tracer, ILogger<CorrelationHeaderHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _tracer = tracer;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            SetHeaders(request);

            return await base.SendAsync(request, cancellationToken);
        }

        private void SetHeaders(HttpRequestMessage request)
        {
            var headers = _httpContextAccessor.HttpContext.Request.Headers;

            //Parent Correlation ID will be set with Current Correlation ID, 
            //when another micro service will be hit during request
            if (!request.Headers.Contains(Model.CORRELATION_PARENT_ID))
            {
                request.Headers.Add(Model.CORRELATION_PARENT_ID,
                    headers[Model.CORRELATION_CURRENT_ID].ToString());
            }

            if (!request.Headers.Contains(Model.CORRELATION_ORIGIN_ID))
            {
                request.Headers.Add(Model.CORRELATION_ORIGIN_ID,
                    headers[Model.CORRELATION_ORIGIN_ID].ToString());
            }

            if (!request.Headers.Contains(Model.USER))
            {
                request.Headers.Add(Model.USER, headers[Model.USER].ToString());
            }

            SetTracingHeaders(request, headers);
        }

        private void SetTracingHeaders(HttpRequestMessage request, IHeaderDictionary headers)
        {
            try
            {

                Dictionary<string, string> activeSpanHeaders = null;
                if (_tracer?.ActiveSpan != null)
                {
                    activeSpanHeaders = new Dictionary<string, string>();
                    _tracer.Inject(_tracer?.ActiveSpan.Context, BuiltinFormats.HttpHeaders, new TextMapInjectAdapter(activeSpanHeaders));
                }
                AddHeaderIfNotNull(request, Model.REQUEST_ID, headers[Model.REQUEST_ID].ToString(), activeSpanHeaders);
                AddHeaderIfNotNull(request, Model.B3_TRACE_ID, headers[Model.B3_TRACE_ID].ToString(), activeSpanHeaders);
                AddHeaderIfNotNull(request, Model.B3_SPAN_ID, headers[Model.B3_SPAN_ID].ToString(), activeSpanHeaders);
                AddHeaderIfNotNull(request, Model.B3_PARENT_SPAN_ID, headers[Model.B3_PARENT_SPAN_ID].ToString(), activeSpanHeaders);
                AddHeaderIfNotNull(request, Model.B3_SAMPLED, headers[Model.B3_SAMPLED].ToString(), activeSpanHeaders);
                AddHeaderIfNotNull(request, Model.B3_FLAGS, headers[Model.B3_FLAGS].ToString(), activeSpanHeaders);
                AddHeaderIfNotNull(request, Model.OT_SPAN_CONTEXT, headers[Model.OT_SPAN_CONTEXT].ToString(), activeSpanHeaders);
                AddHeaderIfNotNull(request, Model.UBER, headers[Model.UBER].ToString(), activeSpanHeaders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"OpenTracing header modification exception : {ex.Message}");
            }
        }

        private void AddHeaderIfNotNull(HttpRequestMessage request, string headerName, string headerValue, Dictionary<string, string> activeSpanHeaders)
        {
            if (!string.IsNullOrWhiteSpace(headerValue))
            {
                request.Headers.TryAddWithoutValidation(headerName, headerValue);
            }
            else if (activeSpanHeaders != null)
            {
                switch (headerName)
                {
                    case Model.B3_TRACE_ID:
                        if (activeSpanHeaders.ContainsKey(TRACE_ID))
                            request.Headers.TryAddWithoutValidation(headerName, activeSpanHeaders[TRACE_ID]);
                        break;
                    case Model.B3_SPAN_ID:
                        if (activeSpanHeaders.ContainsKey(SPAN_ID))
                            request.Headers.TryAddWithoutValidation(headerName, activeSpanHeaders[SPAN_ID]);
                        break;
                    case Model.B3_PARENT_SPAN_ID:
                        if (activeSpanHeaders.ContainsKey(PARENT_SPAN_ID))
                            request.Headers.TryAddWithoutValidation(headerName, activeSpanHeaders[PARENT_SPAN_ID]);
                        break;
                    case Model.B3_SAMPLED:
                        if (activeSpanHeaders.ContainsKey(SAMPLE_FLAG))
                            request.Headers.TryAddWithoutValidation(headerName, activeSpanHeaders[SAMPLE_FLAG]);
                        break;
                    case Model.UBER:
                        if (activeSpanHeaders.ContainsKey(Model.UBER))
                            request.Headers.TryAddWithoutValidation(headerName, activeSpanHeaders[Model.UBER]);
                        break;
                }

            }
            else
            {
                _logger.LogTrace($"Not adding header {headerName} to the client. It is null or empty.");
            }
        }
    }
}