using AutoMapper;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka.Extensions;
using BD.Core.Extension;
using BD.Core.HealthCheck.Extensions;
using Caching;
using CCEProxy.API.Infrastructure.DataAccess.Mongo.Clients;
using CCEProxy.API.IntegrationEvents.EventHandling;
using CCEProxy.API.IntegrationEvents.Events;
using CCEProxy.API.Registration;
using HateoasFilter.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BD.Core.ElasticClient.Extensions.Mongo;
using BD.Core.Context.Http.Extensions;

namespace CCEProxy.API
{
    /// <summary> Adds and Configure all Services for the Startup.cs file </summary>
    public static class ServiceDependencies
    {
        /// <summary> Adds services required in the Startup.cs file by the API </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {
            services
                .AddEventBusServices(configuration, logger)
                .AddHttpContextAccessor()
                .Configure<Configuration.Configuration>(configuration.GetSection("MessageBusTopics"))
                .AddOptions();

            #region Register MongoDbClient to perform Mongo DB operations
            services.AddExecutionContext(logger).AddElasticMongoClient(configuration, logger);
            services.AddCaching(configuration);
            services.AddHateoasServices();

            //This line adds Swagger generation services to our container.
            services.RegisterSwagger();

            //To disable the Automatic modelState invalid behavior
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddAutoMapper();
            #endregion
            services
                .AddMvcCoreServices(configuration, logger, env);

            return services;
        }

        /// <summary> Configure Services added in the AddServices method </summary>
        /// <param name="app">IApplicationBuilderparam></param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        /// <returns>Instance of type IApplicationBuilder</returns>
        public static IApplicationBuilder UseServices(this IApplicationBuilder app, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {
            app.UseMvcCoreServices(configuration, logger, env)
                .UseEventBusServices(configuration);

            return app;
        }

        /// <summary> Adds event bus subscriptions </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>Instance of type IApplicationBuilder</returns>
        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TransactionPriorityAddedIntegrationEvent, TransactionPriorityIntegrationEventHandler>(
                configuration["MessageBusTopics:KafkaTransactionPriorityResponseTopic"],
                configuration["MessageBusTopics:KafkaTransactionPriorityConsumerGroupId"]);
            eventBus.Subscribe<FacilityAddedIntegrationEvent, FacilityIntegrationEventHandler>(
                configuration["MessageBusTopics:KafkaFacilityResponseTopic"],
                configuration["MessageBusTopics:KafkaFacilityConsumerGroupId"]);

            return app;
        }
    }
}
