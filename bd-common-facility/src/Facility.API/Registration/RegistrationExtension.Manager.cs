using Facility.API.BusinessLayer.Concrete;
using Facility.API.BusinessLayer.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Facility.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        #region IServiceCollection Extension Methods
        /// <summary> Register all the handlers </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterManager(this IServiceCollection services)
        {
            services.AddScoped<IFacilityManager, FacilityManager>();

            return services;
        }
        #endregion
    }
}
