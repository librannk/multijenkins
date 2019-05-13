using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Prometheus.HttpMetrics;
using Prometheus;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BD.Core.Logging.Monitoring.Middlewares
{
    public sealed class HttpMetricsDurationMiddleware : HttpRequestMiddlewareBase<Histogram>
    {
        private readonly RequestDelegate _next;
        private readonly Histogram _requestDuration;
        private readonly string _pattern;

        public HttpMetricsDurationMiddleware(RequestDelegate next, Histogram histogram, string pattern)
            : base(histogram)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _requestDuration = histogram ?? throw new ArgumentNullException(nameof(histogram));
            _pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var stopWatch = Stopwatch.StartNew();

            // We need to write this out in long form instead of using a timer because
            // GetLabelData() can only return values *after* executing the next request delegate.
            try
            {
                await _next(context);
            }
            finally
            {
                stopWatch.Stop();

                if (string.IsNullOrWhiteSpace(_pattern) ||
                    !Regex.IsMatch(context.Request.Path, _pattern, RegexOptions.IgnoreCase))
                {
                    // GetLabelData() route data is only available *after* invoking the next request delegate.
                    _requestDuration
                        .WithLabels(GetLabelData(context))
                        .Observe(stopWatch.Elapsed.TotalSeconds);
                }
            }
        }
    }
}