using BD.Core.Logging.Configuration;
using BD.Core.Logging.Monitoring.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prometheus;
using Prometheus.HttpMetrics;

namespace BD.Core.Logging.Extensions
{
    /// <summary> MonitoringExtension class that contains extension methods USE prometheus monitoring </summary>
    public static class MonitoringExtension
    {

        #region Constants

        /// <summary> SerilogTrace </summary>
        private const string SerilogTrace = "Serilog:TraceEndpoints";

        /// <summary> AddingMonitoringMiddleware </summary>
        private const string AddingMonitoringMiddleware = "Adding Monitoring Middleware.";

        /// <summary> MonitoringMiddlewareAdded </summary>
        private const string MonitoringMiddlewareAdded = "Monitoring Middleware added.";

        #endregion

        #region UseServices in Configure method of Startup.cs

        /// <summary>
        /// Application Monitoring - Configures (prometheus) monitoring middleware to IApplicationBuilder instance
        /// </summary>
        /// <param name="app">IApplicationBuilder instance.</param>
        /// <param name="configuration">IConfiguration instance.</param>
        /// <param name="logger">ILogger instance.</param>
        /// <returns>Same IApplicationBuilder instance after configuring MonitoringMiddleware</returns>
        public static IApplicationBuilder UseMonitoringService(this IApplicationBuilder app, IConfiguration configuration, Microsoft.Extensions.Logging.ILogger logger)
        {
            var traceConfiguration = new TraceConfiguration();
            configuration.Bind(SerilogTrace, traceConfiguration);

            Metrics.SuppressDefaultMetrics();

            var options = new HttpMiddlewareExporterOptions();

            options.RequestDuration.Histogram = Metrics.CreateHistogram("http_request_duration",
                "The duration of HTTP requests processed by an ASP.NET Core application",
                new HistogramConfiguration()
                {
                    // 500 ms to 2000 ms and infinite buckets
                    Buckets = Histogram.ExponentialBuckets(0.500, 2, 3),
                    LabelNames = new[] { "code", "method", "controller", "action" },
                    SuppressInitialValue = true,
                });

            logger.LogInformation(AddingMonitoringMiddleware);

            var pattern = traceConfiguration.ExcludePattern;

            if (options.InProgress.Enabled)
                app.UseMiddleware<HttpMetricsInProgressMiddleware>(options.InProgress.Gauge, pattern);
            if (options.RequestCount.Enabled)
                app.UseMiddleware<HttpMetricsCountMiddleware>(options.RequestCount.Counter, pattern);
            if (options.RequestDuration.Enabled)
                app.UseMiddleware<HttpMetricsDurationMiddleware>(options.RequestDuration.Histogram, pattern);

            
            app.UseMetricServer();

            logger.LogInformation(MonitoringMiddlewareAdded);

            return app;
        }

        #endregion
    }
}
