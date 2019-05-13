using SiteConfiguration.API.Registration;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka.Extensions;
using BD.Core.Extension;
using Caching;
using HateoasFilter.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BD.Core.Context.Http.Middlewares;
using BD.Core.ElasticClient.Extensions;
using BD.Core.ElasticClient.SQL;


namespace SiteConfiguration.API
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceDependencies
    {
        /// <summary> Add services to the IServiceCollection instance </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration,
            IHostingEnvironment env, ILogger logger)
        {
            services
                .AddEventBusServices(configuration, logger)
                .AddHttpContextAccessor()
                .Configure<Configuration.Configuration>(configuration.GetSection("MessageBusTopics"))
                .AddOptions();

            services
                .AddCaching(configuration);

            services
                .RegisterSqlRepository(configuration);

            services.AddElasticDBClient(configuration, logger);

            services
                .AddHateoasServices()
                .RegisterSwagger()
                .AddAutoMapper();

            services
                .AddMvcCoreServices(configuration, logger, env);

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
                .UseEventBusServices(configuration)
                .UseMiddleware<ExecutionContextMiddleware>()
                .UseElasticClient(configuration, logger);

            return app;
        }

        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<TemplateAddedIntegrationEvent, TemplateAddedEventHandler>(configuration["MessageBusTopics:DemoTopic"], configuration["MessageBusTopics:DemoTopic"]);

            return app;
        }
    }
}

