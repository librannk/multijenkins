using BD.Core.Logging.HeaderHandlers;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using System;
using System.Net.Http;

namespace BD.Core.ResiliencePolicy.Extensions
{
    public static class PollyExtension
    {
        // METHODS
        #region IServiceCollection Extension Methods
        /// <summary> Registers all the services(such as HttpClient) </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection AddPollyServices(this IServiceCollection services)
        {
            var registry = services.AddPolicyRegistry();
            InitRegistory(ref registry);

            services.AddTransient<CorrelationHeaderHandler>();

            services.AddHttpClient<HttpClientFactory>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            })
            .AddPolicyHandlerFromRegistry((policyRegistry, httpRequestMessage) =>
            {
                if (httpRequestMessage.Method == HttpMethod.Get || httpRequestMessage.Method == HttpMethod.Delete)
                {
                    return policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>("TimeOut");
                }
                else if (httpRequestMessage.Method == HttpMethod.Post)
                {
                    return registry.Get<IAsyncPolicy<HttpResponseMessage>>("WaitAndRetry");
                }

                return registry.Get<IAsyncPolicy<HttpResponseMessage>>("NoOp");
            })
            .AddHttpMessageHandler<CorrelationHeaderHandler>();

            return services;
        }
        #endregion

        private static void InitRegistory(ref IPolicyRegistry<string> registry)
        {
            NoOpPolicyClass.NoOpPolicy(ref registry);
            TimeoutPolicyClass.Timeout(ref registry, 10);
            WaitAndRetryPolicy.WaitAndRetry(ref registry);

        }
    }
}
