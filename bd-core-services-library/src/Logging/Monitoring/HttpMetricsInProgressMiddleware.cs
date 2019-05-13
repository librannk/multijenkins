using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Prometheus;
using System.Text.RegularExpressions;

namespace BD.Core.Logging.Monitoring.Middlewares
{
    public sealed class HttpMetricsInProgressMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IGauge _inProgressGauge;
        private readonly string _pattern;

        public HttpMetricsInProgressMiddleware(RequestDelegate next, IGauge gauge, string pattern)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _inProgressGauge = gauge ?? throw new ArgumentNullException(nameof(gauge));
            _pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var patternNotMatching = string.IsNullOrWhiteSpace(_pattern) ||
                                     !Regex.IsMatch(context.Request.Path, _pattern, RegexOptions.IgnoreCase);

            using (patternNotMatching ? _inProgressGauge.TrackInProgress() : null)
            {
                await _next(context);
            }
        }
    }
}