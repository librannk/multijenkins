using Autofac;
using BD.Core.Context;
using BD.Core.EventBus;
using BD.Core.EventBus.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace BD.Core.EventBusKafka.Extensions
{
    /// <summary> Extension class for EventBus. Contains extension methods to be included in ConfigureServices and Configure methods of Startup.cs class of various services(APIs) </summary>
    public static class EventBusExtension
    {
        private const string EventBusConfiguration = "EventBusConfiguration";
        private const string SerilogTraceEndpoints = "Serilog:TraceEndpoints";
        private const string AddedEventBusServices = "Added Event Bus Services";

        /// <summary> AddEventBusServices </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddEventBusServices(this IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
            logger.LogInformation(AddedEventBusServices);
            //For Injecting Tenant Context
            services.AddScoped<IContextHeaders, MiddlewareContextHeaders>();
            services.AddSingleton<IEventBus, EventBusKafka>(sp =>
            {
                return new EventBusKafka(
                    configuration.GetSection(EventBusConfiguration).Get<EventBusConfiguration>(),
                    sp.GetRequiredService<IEventBusSubscriptionsManager>(),
                    sp.GetRequiredService<ILifetimeScope>(),
                    sp.GetRequiredService<ILogger<EventBusKafka>>(), 
                    configuration.GetSection(SerilogTraceEndpoints), 
                    sp.GetRequiredService<ITracer>(), 
                    sp.GetRequiredService<IHttpContextAccessor>(),
                    sp.GetRequiredService<IExecutionContextFactory>(),
                    sp.GetRequiredService<IContextHeaders>()
                );
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}
