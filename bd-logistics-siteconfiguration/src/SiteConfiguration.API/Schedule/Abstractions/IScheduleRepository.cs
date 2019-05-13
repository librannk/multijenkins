using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.Schedule.Models;

namespace SiteConfiguration.API.Schedule.Abstractions
{
    /// <summary>
    /// Schedule Repository interface
    /// </summary>
    public interface IScheduleRepository : IRepository<ScheduleTiming>
    {
        /// <summary>
        /// Get Schedules from database
        /// </summary>
        /// <param name="facilityId"></param>
        Task<IEnumerable<ScheduleTiming>> GetSchedules(Guid facilityId);

        /// <summary>
        /// check wether schedule with the same namee exist in the database or not
        /// </summary>
        /// <param name="facilityId"></param>
        /// <param name="ScheduleTimingName"></param>
        bool GetScheduleByName(Guid facilityId, string ScheduleTimingName);

        /// <summary>
        /// check wether schedule with the same namee exist in the database or not
        /// </summary>
        /// <param name="ScheduleTimingName"></param>
        ScheduleTiming GetScheduleByName(string ScheduleTimingName);
    }
}
