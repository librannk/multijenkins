using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteConfiguration.API.Printers.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Common.Constants;

namespace SiteConfiguration.API.Printers.Controllers
{
    /// <summary>
    /// PrinterModelController
    /// </summary>
    [Route("api/v{version:apiVersion}/siteconfiguration/")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class PrinterModelController : Controller
    {
        private readonly IPrinterBusiness _printerBusiness;
        private readonly ILogger<PrinterModelController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="printerBusiness"></param>
        /// <param name="logger"></param>
        public PrinterModelController(IPrinterBusiness printerBusiness, ILogger<PrinterModelController> logger)
        {
            _printerBusiness = printerBusiness;
            _logger = logger;
        }

        /// <summary>
        /// HttpGet method to fetch printermodels
        /// </summary>
        /// <returns>List of PrinterModels</returns>
        [HttpGet("printermodels")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> GetAllPrinterModels()
        {
            try
            {

                _logger.LogInformation(Constants.GetPrinterModelRequestReceived);
                var printerModels = await _printerBusiness.GetPrinterModels();
                if (printerModels.Any())
                {
                    return Ok(printerModels);
                }
                _logger.LogError(Constants.Empty_List);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
