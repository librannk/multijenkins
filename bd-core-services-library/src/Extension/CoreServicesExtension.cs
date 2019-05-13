using BD.Core.ExceptionHandling;
using BD.Core.Logging.Extensions;
using BD.Core.Context.Http.Extensions;
using BD.Core.ResiliencePolicy.Extensions;
using BD.Core.Security.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using BD.Core.Extension.AppSettings;
using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.Options;

namespace BD.Core.Extension
{
    /// <summary> The Core service extensions class </summary>
    public static class CoreServicesExtension
    {
        /// <summary> The system error code </summary>
        private const int SystemErrorCode = 50000;

        private const string HealthCheckUrl = "Uris:HealthCheckUri";

        /// <summary> The production error message </summary>
        private const string ProductionErrorMessage = "System Error Occured.";

        /// <summary> The global exception handler </summary>
        private const string GlobalExceptionHandler = "GlobalExceptionHandler";

        /// <summary> The Content Type </summary>
        private const string ContentType = "application/json";

        /// <summary> The Adding Core Services </summary>
        private const string AddingCoreServices = "Adding Core Services";

        /// <summary> The Core Services Added </summary>
        private const string CoreServicesAdded = "Core Services Added";

        /// <summary> The Configuring Core Services </summary>
        private const string ConfiguringCoreServices = "Configuring Core Services";

        /// <summary> The Core Services Configured </summary>
        private const string CoreServicesConfigured = "Core Services Configured";

        /// <summary> The Adding Jaeger Service </summary>
        private const string AddingJaegerService = "Adding tracing - Initiating JAEGER client.";

        /// <summary> The Jaeger Service Added </summary>
        private const string JaegerServiceAdded = "Tracing added - JAEGER client initiated.";

        /// <summary>        
        private const string enUSCulture = "en-US";

        /// <summary>
        private const string pathForResourceFile = "Resources";

        /// <summary>
        /// This method is used as an extension to use the common core services method
        /// and applies across all micro-services.
        /// Sequence first Logging, MVC, Authorization, Resilience etc.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IServiceCollection AddMvcCoreServices(this IServiceCollection services, IConfiguration configuration, ILogger logger, IHostingEnvironment hostingEnvironment)
        {
            logger.LogInformation(AddingCoreServices);

            services
                .AddHttpContextAccessor()
                .AddHealthChecks();

            services
                .AddSecurityPolicy(hostingEnvironment, configuration, logger)
                .AddExecutionContext(logger)
                .AddLoggingServices()
                .AddPollyServices()
                .AddMvc(config =>
                {
                    config.Filters.Add(typeof(GlobalExceptionFilter));
                    config.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddVersionedApiExplorer(options =>
                {
                    //The format of the version added to the route URL  
                    options.GroupNameFormat = "'v'VVV";
                    //Tells swagger to replace the version in the controller route  
                    options.SubstituteApiVersionInUrl = true;
                });

            services
                .AddApiVersioning(options => options.ReportApiVersions = true);

            logger.LogInformation(AddingJaegerService);
            services.AddTracing();
            logger.LogInformation(JaegerServiceAdded);


            // service for Internationalization
            if (pathForResourceFile.Equals(SupportedLocalizeCulture.ResourcePath))
            {
                services.AddLocalization(s => s.ResourcesPath = pathForResourceFile);
            }
            else
            {
                logger.LogInformation(SupportedLocalizeCulture.ResourcePathNotFound);
            }
            List<CultureInfo> cinfo = new List<CultureInfo>();
            var SupportedCultureConfig = configuration.GetSection(SupportedLocalizeCulture.SupportedCultures).Get<Dictionary<string, string>>();
            if (SupportedCultureConfig != null)
            {
                foreach (var item in SupportedCultureConfig)
                {
                    cinfo.Add(new CultureInfo(item.Value));
                }

                services.Configure<RequestLocalizationOptions>(s =>
                {
                    s.SupportedCultures = cinfo;
                    s.SupportedUICultures = cinfo;
                    s.DefaultRequestCulture = new RequestCulture(culture: enUSCulture, uiCulture: enUSCulture);
                });
            }
            else
            {
                logger.LogInformation(SupportedLocalizeCulture.SupportedCultureNotFound);
            }
            logger.LogInformation(CoreServicesAdded);

            return services;
        }

        /// <summary> Configure Core services </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMvcCoreServices(this IApplicationBuilder app, IConfiguration configuration, ILogger logger, IHostingEnvironment hostingEnvironment)
        {
            logger.LogInformation(ConfiguringCoreServices);

            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app
                .UseHealthChecks(configuration[HealthCheckUrl], new HealthCheckOptions
                {
                    Predicate = check => false,
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(new
                        {
                            status = report.Status.ToString(),
                            errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value) })
                        });
                        context.Response.ContentType = MediaTypeNames.Application.Json;

                        await context.Response.WriteAsync(result);
                    }
                });

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app
                .UseSecurityPolicy(hostingEnvironment, configuration, logger)
                .UseEndpointRouting()
                .UseExecutionContext(configuration, logger) //Context Middleware
                .UseMonitoringService(configuration,logger)
                .UseLoggingServices(hostingEnvironment, configuration, logger)
                .UseMvc();

            logger.LogInformation(CoreServicesConfigured);
            
            return app;
        }

    }
}
