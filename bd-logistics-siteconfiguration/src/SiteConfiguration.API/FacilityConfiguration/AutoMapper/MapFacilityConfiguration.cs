using AutoMapper;
using SiteConfiguration.API.FacilityConfiguration.Models;
using SiteConfiguration.API.FacilityConfiguration.RequestResponseModel;

namespace SiteConfiguration.API.AutoMapper
{
    /// <summary>
    /// mapper class for facility configuration
    /// </summary>
    public class MapFacilityConfiguration : Profile
    {
        /// <summary>
        ///  initialization taken place
        /// </summary>
        public MapFacilityConfiguration() 
        {
            CreateMap<TransactionQueueConfigurationRequest, FacilityLogisticsConfig>();
            CreateMap<FacilityLogisticsConfig, TransactionQueueConfigurationRequest>();
            CreateMap<FacilityLogisticsConfigurationExtension, FacilityLogisticsConfig>();
            CreateMap<FacilityLogisticsConfig, FacilityLogisticsConfigurationExtension>();
        }
    }
}
