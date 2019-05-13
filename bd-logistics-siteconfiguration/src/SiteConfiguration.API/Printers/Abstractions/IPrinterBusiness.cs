using SiteConfiguration.API.Printers.Models.BuisnessContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Abstractions
{
    /// <summary>
    /// Printer Service interface
    /// </summary>
    public interface IPrinterBusiness
    {

        /// <summary>
        /// Get printers from database as per facility id.
        /// </summary>
        /// <param name="facilityKey"></param>
        Task<IEnumerable<PrinterResponse>> GetPrintersByFacility(Guid facilityKey);

        /// <summary>
        /// Get printerModels from database
        /// </summary>
        Task<IEnumerable<PrinterModel>> GetPrinterModels();

        /// <summary>
        /// Get Printer by the printerKey.
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        Task<PrinterResponse> GetPrinterByKey(Guid key);

        /// <summary>
        /// Add printer with given FacilityId
        /// </summary>
        /// <param name="printerRequest">printerRequest</param>
        /// <param name="facilityKey">facilityKey</param>
        /// <returns></returns>
        Task AddPrinter(PrinterRequest printerRequest, Guid facilityKey);

        /// <summary>
        /// Updates Printer
        /// </summary>
        /// <param name="key"></param>
        /// <param name="facilityKey"></param>
        /// <param name="printer"></param>
        /// <returns></returns>
        Task UpdatePrinter(Guid key, Guid facilityKey, PrinterRequest printer);



    }
}
