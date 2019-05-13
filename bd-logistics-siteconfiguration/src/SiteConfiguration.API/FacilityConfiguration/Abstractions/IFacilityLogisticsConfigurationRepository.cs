using SiteConfiguration.API.FacilityConfiguration.Models;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Threading.Tasks;

namespace SiteConfiguration.API.FacilityConfiguration.Abstractions
{
    /// <summary>
    /// base interface for facility logistic configuration
    /// </summary>
    public interface IFacilityLogisticsConfigurationRepository : IRepository<FacilityLogisticsConfig>
    {
        /// <summary>
        /// async call to validate facility key
        /// </summary>
        /// <param name="facilityKey"></param>
        /// <returns></returns>
        Task<FacilityLogisticsConfig> GetAsync(Guid facilityKey);
    }
}
