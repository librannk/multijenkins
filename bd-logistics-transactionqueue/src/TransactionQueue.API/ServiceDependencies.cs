using AutoMapper;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka.Extensions;
using BD.Core.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TransactionQueue.API.Registration;
using TransactionQueue.ExternalDependencies.IntegrationEvents.EventHandling;
using TransactionQueue.ExternalDependencies.IntegrationEvents.Events;
using TransactionQueue.Ingestion.IntegrationEvents.EventHandling;
using TransactionQueue.Ingestion.IntegrationEvents.Events;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.SocketManager;
using static TransactionQueue.API.Common.Constants.Constants;

namespace TransactionQueue.API
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceDependencies
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {

            services
                .AddEventBusServices(configuration, logger)
                .AddHttpContextAccessor()
                .Configure<Shared.Configuration.Configuration>(configuration.GetSection("MessageBusTopics"))
                .AddOptions();

            services.RegisterHandler();
            services.AddAutoMapper();
            services.AddSingleton<IMediator, TransactionQueueMediator>();

            #region Register MongoDbClient to perform Mongo DB operations
            services.AddSingleton(sp => new MongoDbClient(configuration.GetSection("MongoDb:Database").Value,
                connectionString: configuration.GetSection("MongoDb:ConnectionString").Value));
            #endregion

            services.AddWebSocketManager();

            services
                .AddMvcCoreServices(configuration, logger, env);

            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseServices(this IApplicationBuilder app, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {
            app.UseMvcCoreServices(configuration, logger, env)
                .UseEventBusServices(configuration);

            return app;
        }

        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            var KafkaSettings = GetSectionSettingConfig(configuration, MessageBus.Topic);
            var KafkaConsumerGroupSettings = GetSectionSettingConfig(configuration, MessageBus.Groups);
            eventBus.Subscribe<TransactionQueueAddedIntegrationEvent, TransactionQueueAddedIntegrationEventHandler>(KafkaSettings["KafkaAggregatorTopic"], KafkaSettings["KafkaAggregatorTopic"]);
            eventBus.Subscribe<FormularyLocationIntegrationEvent, FormularyLocationIntegrationEventHandler>(KafkaSettings["KafkaFormularyLocationResponseTopic"], KafkaSettings["KafkaFormularyLocationResponseTopic"]);
            
            //TODO: Subscribers are obsolete
            //eventBus.Subscribe<FacilityAddedIntegrationEvent, FacilityIntegrationEventHandler>(KafkaSettings["KafkaFacilityResponseTopic"], KafkaConsumerGroupSettings["KafkaFacilityConsumerGroupId"]);
            //eventBus.Subscribe<FormularyUpdatedIntegrationEvent, FormularyUpdatedIntegrationEventHandler>(KafkaSettings["KafkaFormularyUpdateResponseTopic"], KafkaSettings["KafkaFormularyUpdateResponseTopic"]);
            //eventBus.Subscribe<TransactionPriorityAddedIntegrationEvent, TransactionPriorityAddedIntegrationEventHandler>(KafkaSettings["KafkaTransactionPriorityResponseTopic"], KafkaConsumerGroupSettings["KafkaTransactionPriorityConsumerGroupId"]);
            //eventBus.Subscribe<FormularyFacilityUpdatedIntegrationEvent, FormularyFacilityUpdatedIntegrationEventHandler>(KafkaSettings["KafkaFormularyFacilityUpdateResponseTopic"], KafkaSettings["KafkaFormularyFacilityUpdateResponseTopic"]);

            return app;
        }

        private static Dictionary<string, string> GetSectionSettingConfig(IConfiguration configuration, string sectionname)
        {
            return configuration.GetSection(sectionname).GetChildren()
                                .Select(item => new KeyValuePair<string, string>(item.Key, item.Value))
                                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
