using BD.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Polly.Registry;
using BD.Core.ResiliencePolicy;
using BD.Core.Logging.HeaderHandlers;

namespace Formulary.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        // METHODS
        #region IServiceCollection Extension Methods
        /// <summary> Registers all the services(such as HttpClient) </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddTransient<CorrelationHeaderHandler>();
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
