using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Models;

namespace SiteConfiguration.API.Schedule.Repository
{
    /// <summary>
    /// ScheduleRepository class implements the all member of IScheduleRepository of type ScheduleTiming.
    /// </summary>
    public class ScheduleRepository : BaseRepository<ScheduleTiming>, IScheduleRepository
    {
        /// <param name="context"></param>
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Get Schedule by the facilityKey and tenantKey which is previously stored on DB.
        /// </summary>
        /// <param name="facilityKey">facilityKey field of schedule</param>
        /// <returns></returns>
        public async Task<IEnumerable<ScheduleTiming>> GetSchedules(Guid facilityKey)
        {
            var result = DbSet.Where(x => x.FacilityKey == facilityKey).OrderByDescending(sch => sch.LastModifiedUTCDateTime);
            return await Task.Run(() => result);
        }

        /// <summary>
        /// Get Schedule by the facilityKey, tenantKey and ScheduleTimingName which is previously stored on DB.
        /// </summary>
        /// <param name="facilityKey">facilityKey field of schedule</param>
        /// <param name="ScheduleTimingName">name of the schedule</param>
        /// <returns></returns>
        public bool GetScheduleByName(Guid facilityKey, string ScheduleTimingName)
        {
            return DbSet.Any(x => x.FacilityKey == facilityKey && x.ScheduleTimingName == ScheduleTimingName);
        }

        /// <summary>
        /// Get Schedule by the facilityKey, tenantId and ScheduleTimingName which is previously stored on DB.
        /// </summary>
        /// <param name="ScheduleTimingName">name of the schedule</param>
        /// <returns></returns>
        public ScheduleTiming GetScheduleByName(string ScheduleTimingName)
        {
            return DbSet.Where(x => x.ScheduleTimingName == ScheduleTimingName).FirstOrDefault();
        }
    }
}
