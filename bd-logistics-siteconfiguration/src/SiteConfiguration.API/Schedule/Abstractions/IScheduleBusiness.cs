using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiteConfiguration.API.Schedule.Models;

namespace SiteConfiguration.API.Schedule.Abstractions
{
    /// <summary>
    /// Schedule Business interface
    /// </summary>
    public interface IScheduleBusiness
    {
        /// <summary>
        /// Get Schedules from database
        /// </summary>
        /// <param name="facilityKey"></param>
        Task<IEnumerable<ScheduleResponse>>  GetSchedules(Guid facilityKey);

        /// <summary>
        /// Get Schedule from database
        /// </summary>
        /// <param name="key"></param>
        Task<ScheduleResponseByKey> GetScheduleByKey(Guid key);

        /// <summary>
        /// Add Schedule to the database
        /// </summary>
        /// <param name="facilityKey"></param>
        /// <param name="schedule"></param>
        Task AddSchedule(Guid facilityKey, ScheduleRequest schedule);

        /// <summary>
        /// Add Schedule to the database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="facilityKey"></param>
        /// <param name="schedule"></param>
        Task UpdateSchedule(Guid key, Guid facilityKey, ScheduleRequest schedule);

        /// <summary>
        /// Delete Schedule from database
        /// </summary>
        /// <param name="key"></param>
        Task DeleteSchedule(Guid key);
    }
}
