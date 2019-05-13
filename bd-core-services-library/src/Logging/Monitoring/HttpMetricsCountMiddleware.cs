using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Prometheus.HttpMetrics;
using Prometheus;
using System.Text.RegularExpressions;

namespace BD.Core.Logging.Monitoring.Middlewares
{
    public sealed class HttpMetricsCountMiddleware : HttpRequestMiddlewareBase<Counter>
    {
        private readonly RequestDelegate _next;
        private readonly Counter _requestCount;
        private readonly string _pattern;

        public HttpMetricsCountMiddleware(RequestDelegate next, Counter counter, string pattern)
            : base(counter)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _requestCount = counter ?? throw new ArgumentNullException(nameof(counter));
            _pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            try
            {
                await _next(context);
            }
            finally
            {
                if (string.IsNullOrWhiteSpace(_pattern) ||
                    !Regex.IsMatch(context.Request.Path, _pattern, RegexOptions.IgnoreCase))
                {
                    // GetLabelData() route data is only available *after* invoking the next request delegate.
                    _requestCount
                        .WithLabels(GetLabelData(context))
                        .Inc();
                }
            }
        }
    }
}