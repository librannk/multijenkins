using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.Printers.Abstractions;
using SiteConfiguration.API.Printers.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Repository
{

    /// <summary>
    /// PrinterRepository class implements the all member of IScheduleRepository of type ScheduleTiming.
    /// </summary>
    public class PrinterRepository : BaseRepository<Printer>, IPrinterRepository
    {
        /// <summary>
        /// Printer repository.
        /// </summary>
        /// <param name="context"></param>
        public PrinterRepository(ApplicationDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Get Printers by the facilityKey.
        /// </summary>
        /// <param name="facilityKey">facilityKey field of schedule</param>
        /// <returns></returns>
        public async Task<IEnumerable<Printer>> GetPrintersByFacility(Guid facilityKey)
        {
            var result = DbSet.Where(x => x.FacilityKey == facilityKey).OrderByDescending(printer => printer.LastModifiedUTCDateTime);
            return await Task.Run(() => result);
        }
    }
}
