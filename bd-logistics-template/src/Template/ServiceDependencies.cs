using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka.Extensions;
using BD.Core.Extension;
using BD.Template.API.Configuration;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Clients;
using BD.Template.API.IntegrationEvents.Events;
using BD.Template.API.Registration;
using Caching;
using HateoasFilter.Extentions;
using Logistics.Services.Template.API.IntegrationEvents.EventHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Template.API.Registration;

namespace Template.API
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
                .AddEventBusServices(configuration, logger)
                .AddTransient<TemplateAddedEventHandler>()
                .AddHttpContextAccessor()
                .Configure<Configuration>(configuration.GetSection("MessageBusTopics"))
                .AddOptions();

            services
                .AddCaching(configuration)
                .BlobFiles(configuration, logger);

            services
                .AddSingleton(sp => new MongoDbClient(configuration.GetSection("MongoDb:Database").Value,
                    connectionString: configuration.GetSection("MongoDb:ConnectionString").Value));

            services
                .AddHateoasServices()
                .RegisterSwagger();

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
                .UseEventBusServices(configuration);

            return app;
        }

        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TemplateAddedIntegrationEvent, TemplateAddedEventHandler>(configuration["MessageBusTopics:DemoTopic"], configuration["MessageBusTopics:DemoTopic"]);

            return app;
        }
    }
}
