using SiteConfiguration.API.FacilityConfiguration.RequestResponseModel;
using System.Threading.Tasks;

namespace SiteConfiguration.API.FacilityConfiguration.Abstractions
{
    /// <summary>
    /// interface defining Facility Logistic abstraction
    /// </summary>
    public interface IFacilityLogisticsConfiguration
    {
        /// <summary>
        /// defining inetrface for facility Configuration
        /// </summary>
        /// <param name="facilityConfiguration"></param>
        /// <returns></returns>
        Task CreateFacilitySpecificConfigurationAsync(TransactionQueueConfigurationRequest facilityConfiguration);

        /// <summary>
        /// defining inetrface for facility Extension
        /// </summary>
        /// <param name="facilityLogisticsConfigurationExtension"></param>
        /// <returns></returns>
        Task<BusinessResponse> CreateFacilityExtensionAsync(FacilityLogisticsConfigurationExtension facilityLogisticsConfigurationExtension);

        /// <summary>
        /// defining inetrface for updating existing facility Configuration
        /// </summary>
        /// <param name="facilityConfiguration"></param>
        /// <returns></returns>
        Task<BusinessResponse> UpdateFacilityConfigAsync(TransactionQueueConfigurationRequest facilityConfiguration);

        /// <summary>
        /// defining interface for for updating existing facility Extension
        /// </summary>
        /// <param name="facilityLogisticsConfigurationExtension"></param>
        /// <returns></returns>
        Task<BusinessResponse> UpdateFacilityExtensionAsync(FacilityLogisticsConfigurationExtension facilityLogisticsConfigurationExtension);
    }
}