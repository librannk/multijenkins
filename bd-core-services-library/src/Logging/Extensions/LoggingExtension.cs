using Serilog;
using BD.Core.Logging.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BD.Core.Logging.Configuration;

namespace BD.Core.Logging.Extensions
{
    /// <summary> LoggingExtension class that contains extension methods to ADD & USE logging </summary>
    public static class LoggingExtension
    {

        #region Constants
        /// <summary> AddingLoggingMiddleware </summary>
        private const string AddingLoggingMiddleware = "Adding Logging Middleware";

        /// <summary> LoggingMiddlewareAdded </summary>
        private const string LoggingMiddlewareAdded = "Logging Middleware Added";

        /// <summary> SerilogTrace </summary>
        private const string SerilogTrace = "Serilog:TraceEndpoints";

        #endregion

        #region AddServices in ConfigureServices method of Startup.cs
        /// <summary> Adds logging to IServiceCollection instance </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>Same IServiceCollection instance after adding logging</returns>
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            services.AddLogging(builder => { builder.AddSerilog(dispose: true); });

            return services;
        }
        #endregion

        #region UseServices in Configure method of Startup.cs
        /// <summary> Configures logging middleware to IApplicationBuilder instance </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>Same IApplicationBuilder instance after configuring LoggingMiddleware</returns>
        public static IApplicationBuilder UseLoggingServices(this IApplicationBuilder app, IHostingEnvironment environment, IConfiguration configuration,
            Microsoft.Extensions.Logging.ILogger logger)
        {

            var traceConfiguration = new TraceConfiguration();
            configuration.Bind(SerilogTrace, traceConfiguration);

            logger.LogInformation(AddingLoggingMiddleware);
            app.UseMiddleware<LoggingMiddleware>(traceConfiguration);
            logger.LogInformation(LoggingMiddlewareAdded);

            return app;
        }
        #endregion
        
    }
}