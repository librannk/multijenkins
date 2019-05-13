using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.Printers.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Abstractions
{
    /// <summary>
    /// Contract for PrinterModelRepository
    /// </summary>
    public interface IPrinterModelRepository : IRepository<PrinterModel>
    {
    }
}
