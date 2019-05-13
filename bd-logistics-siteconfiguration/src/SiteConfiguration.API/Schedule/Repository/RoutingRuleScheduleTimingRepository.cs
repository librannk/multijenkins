using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.RoutingRules.Models;
using SiteConfiguration.API.Schedule.Abstractions;

namespace SiteConfiguration.API.Schedule.Repository
{
    /// <summary>
    /// RoutingRuleScheduleTimingRepository class implements the all member of IRoutingRuleScheduleTimingRepository of type RoutingRuleScheduleTiming.
    /// </summary>
    public class RoutingRuleScheduleTimingRepository : BaseRepository<RoutingRuleScheduleTiming>, IRoutingRuleScheduleTimingRepository
    {
        /// <param name="context"></param>
        public RoutingRuleScheduleTimingRepository(ApplicationDbContext context) : base(context)
        {

        }
        /// <summary>
        /// Get RoutingRuleScheduleTiming by the scheduletimingkey which is previously stored on DB.
        /// </summary>
        /// <param name="key">facilityId field of schedule</param>
        /// <returns></returns>
        public async Task<bool> GetRoutingRuleScheduleTiming(Guid key)
        {
            var result = DbSet.Where(x => x.ScheduleTimingKey == key);
            if (result.Any())
            {
                return await Task.Run(() => true);
            }
            return await Task.Run(() => false);
        }
    }
}
