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
    /// 
    /// </summary>
    public class PrinterModelRepository : BaseRepository<PrinterModel>, IPrinterModelRepository
    {
        /// <summary>
        /// PrinterModelRepository
        /// </summary>
        /// <param name="context"></param>
        public PrinterModelRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
