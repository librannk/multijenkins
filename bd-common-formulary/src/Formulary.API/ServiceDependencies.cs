using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BD.Core.EventBusKafka.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Formulary.API.Configuration;
using Caching;
using BD.Core.Extension;
using BD.Core.EventBus.Abstractions;
using AutoMapper;
using Formulary.API.Registration;
using Formulary.API.AutoMapper;

namespace Formulary.API
{
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
                
                .AddHttpContextAccessor()
                .Configure<Formulary.API.Configuration.Configuration>(configuration.GetSection("KafkaConfiguration"))
                .AddOptions();

            services
                .AddCaching(configuration)
                .AddAutoMapper();



            services
                .RegisterSqlRepository(configuration)
                .RegisterManager();


            services
                 .RegisterSwagger();
                

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
                .UseEventBusServices(configuration);

            return app;
        }

        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<TemplateAddedIntegrationEvent, TemplateAddedEventHandler>(configuration["MessageBusTopics:DemoTopic"], configuration["MessageBusTopics:DemoTopic"]);

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
