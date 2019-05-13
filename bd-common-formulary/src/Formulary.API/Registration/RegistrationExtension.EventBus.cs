using Autofac;
using BD.Core.EventBus;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka;
using BD.Core.EventBusKafka.Extensions;
using Formulary.API.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Formulary.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        #region Private Fields

        private static Configuration.Configuration config;

        #endregion

        #region IServiceCollection Extension Methods

        /// <summary> Register all Event-Buses details </summary>
        /// <param name="services"> Of type IServiceCollection </param>
        /// <param name="configuration"> Of type IConfiguration </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Configuration.Configuration>(configuration.GetSection(Constants.KAFKA_CONFIGURATION));
            services.AddSingleton<IEventBus, EventBusKafka>(sp =>
            {
                var eventBudConfiguration = GetConfigurationSettingBySection(configuration, "EventBusConfiguration");
                SetConfig(eventBudConfiguration, configuration);
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                return new EventBusKafka(
                    config,
                    eventBusSubcriptionsManager,
                    iLifetimeScope,
                    sp.GetRequiredService<Microsoft.Extensions.Logging.ILogger<EventBusKafka>>(),
                    configuration.GetSection("Serilog:TraceEndpoints"));
            });
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            return services;
        }

        #endregion

        #region Private Method
        private static void SetConfig(Dictionary<string, string> eventBudConfiguration, IConfiguration configuration)
        {
            var kafka = GetConfigurationSettingBySection(configuration, "EventBusConfiguration:KafkaConfiguration");
            var EventHub = GetConfigurationSettingBySection(configuration, "EventBusConfiguration:EventHubConfiguration");

            config = new Configuration.Configuration
            {
                IsEventHub = Convert.ToBoolean(eventBudConfiguration["IsEventHub"]),
                ValidateToken = Convert.ToBoolean(eventBudConfiguration["ValidateToken"]),
                TokenServerUri = eventBudConfiguration["TokenServerURL"],
                AutoCommit = Convert.ToBoolean(eventBudConfiguration["EnableAutoCommit"]),
                KafkaConfiguration = new KafkaConfiguration
                {
                    Endpoint = kafka["Endpoint"],
                },
                EventHubConfiguration = new EventHubConfiguration
                {
                    Endpoint = EventHub["Endpoint"],
                    Username = EventHub["Username"],
                    Password = EventHub["Password"],
                    BrokerVersionFallback = EventHub["BrokerVersionFallback"]
                }
            };
        }

        #endregion

        #region IConfiguration Extension Methods

        /// <summary> Gets a Dictionary of children's Key-Value pair, based on the section "name" passed in the param </summary>
        /// <param name="configuration"> Of type IConfiguration </param>
        /// <param name="name"> Name of the section (speficially for Kafka) </param>
        /// <returns></returns>
        public static Dictionary<string, string> GetConfigurationSettingBySection(this IConfiguration configuration, string name)
        {
            return configuration.GetSection(name).GetChildren()
                                .Select(item => new KeyValuePair<string, string>(item.Key, item.Value))
                                .ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion
    }
}
