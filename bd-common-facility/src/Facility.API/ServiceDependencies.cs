using AutoMapper;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka;
using BD.Core.EventBusKafka.Extensions;
using BD.Core.Extension;
using Caching;
using Facility.API.Automapper;
using Facility.API.Configuration;
using Facility.API.Constants;
using Facility.API.Registration;
using HateoasFilter.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Facility.API
{
    /// <summary> Adds and configures all the service, which are specific to the API. </summary>
    public static class ServiceDependencies
    {
        /// <summary> Add services to the IServiceCollection instance </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {
            services
                .AddCaching(configuration);
            services
                .AddHateoasServices()
                .RegisterSwagger()
                .AddAutoMapper();

            services.RegisterManager();

            services.RegisterSqlRepository(configuration);

            services.Configure<EventBusConfiguration>(configuration.GetSection(ConfigurationConstants.EventbusConfigurationSectionName));
            services.Configure<MessageBusTopics>(configuration.GetSection(ConfigurationConstants.FacilityKafkaConfiguration));

            services
                .AddMvcCoreServices(configuration, logger, env)
                 .AddEventBusServices(configuration, logger);
            RegisterAutoMapperProfiles(services);
            return services;
        }

        /// <summary> Configures services </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseServices(this IApplicationBuilder app, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {
            app
                .UseMvcCoreServices(configuration, logger, env)
                .UseEventBusServices();
            return app;
        }

        /// <summary> Adds event bus subscriptions </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>Instance of type IApplicationBuilder</returns>
        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<IEventBus>();
            return app;
        }

        /// <summary>
        /// Registers the automatic mapper profiles.
        /// </summary>
        /// <param name="services">Registraion Service Collection</param>
        private static void RegisterAutoMapperProfiles(IServiceCollection services)
        {
            AutomapperRegistrations.ConfigureFacilityMapping();
            services.AddAutoMapper();
        }
    }
}
