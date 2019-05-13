using SiteConfiguration.API.FacilityConfiguration.Abstractions;
using SiteConfiguration.API.FacilityConfiguration.Models;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SiteConfiguration.API.FacilityConfiguration.Repository
{
    /// <summary>
    /// Repository for facility configuration
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FacilityLogisticsConfigurationRepository : BaseRepository<FacilityLogisticsConfig>, IFacilityLogisticsConfigurationRepository
    {
        /// <summary>
        /// constructor for auto initialization
        /// </summary>
        /// <param name="context"></param>
        public FacilityLogisticsConfigurationRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        /// <summary>
        /// method to validate facility key
        /// </summary>
        /// <param name="facilityKey"></param>
        /// <returns></returns>
        public async Task<FacilityLogisticsConfig> GetAsync(Guid facilityKey)
        {
            return await DbSet.FindAsync(facilityKey);
        }
    }
}
