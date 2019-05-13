using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.Printers.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Abstractions
{
    /// <summary>
    /// Printer repository
    /// </summary>
    public interface IPrinterRepository : IRepository<Printer>
    {
        /// <summary>
        /// Get Printers by the facilityId.
        /// </summary>
        /// <param name="facilityKey">facilityKey</param>
        /// <returns></returns>
        Task<IEnumerable<Printer>> GetPrintersByFacility(Guid facilityKey);
    }
}
