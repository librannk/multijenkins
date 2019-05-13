using System.Collections.Generic;
using System.Linq;
using BD.Core.HealthCheck.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BD.Core.Extension;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.EventHandling;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.BusinessLayer;
using Logistics.Services.DeviceCommunication.API.Application.Strategy;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Common;
using Logistics.Services.DeviceCommunication.API.DeviceInterface;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Factory;
using Logistics.Services.DeviceCommunication.API.Utilities;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka;
using BD.Core.EventBusKafka.Extensions;

namespace Logistics.Services.DeviceCommunication.API
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceDependencies
    {
        #region Public Extension Methods

        /// <summary>
        /// Adds the required global and common services needed in application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IServiceCollection AddGlobalServices(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {
            services
                .AddEventBusServices(configuration, logger)
                .Configure<EventBusConfiguration>(configuration.GetSection("MessageBusTopics"))
                .AddOptions();

            services
                .AddMvcCoreServices(configuration, logger, env)
                .AddHttpHealthCheckServices()
                .AddHttpContextAccessor();
            return services;
        }
        
        /// <summary>
        /// Adds the required internal services needed in application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IMediator, ProcessTransactionMediator>()
                .AddSingleton<ICarouselProcess, CarouselProcess>()
                .AddSingleton<ICarouselConnection, CarouselConnection>()
                .AddSingleton<ICarouselManager, CarouselManager>()
                .AddSingleton<IIPSocket, IPSocket>()
                .AddSingleton<IDeviceResponse, DeviceResponse>()
                .AddSingleton<ICarouselFactory, CarouselFactory>()
                .AddSingleton<IUtility, Utility>();

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
            app
                .UseEventBusServices(configuration)
                .UseHttpHealthCheckServices(configuration)
                .UseMvcCoreServices(configuration, logger, env);

            return app;
        }

        #endregion

        #region Private Extension Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            var KafkaSettings = configuration.GetSectionSetting("MessageBusTopics");

            var topicName = KafkaSettings["KafkaTopic"];
            var consumerGroup = KafkaSettings["KafkaTopic"];
            eventBus.Subscribe<ProcessTransactionQueueIntegrationEvent, ProcessTransactionQueueEventHandler>(topicName: topicName, groupId:consumerGroup);

            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="sectionname"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetSectionSetting(this IConfiguration configuration, string sectionname)
        {
            return configuration.GetSection(sectionname).GetChildren()
                .Select(item => new KeyValuePair<string, string>(item.Key, item.Value))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion
    }
}

