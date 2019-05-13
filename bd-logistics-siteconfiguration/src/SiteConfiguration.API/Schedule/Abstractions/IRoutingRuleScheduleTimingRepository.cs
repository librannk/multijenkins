using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Schedule.Abstractions
{
    public interface IRoutingRuleScheduleTimingRepository
    {
        Task<bool> GetRoutingRuleScheduleTiming(Guid key);
    }
}
