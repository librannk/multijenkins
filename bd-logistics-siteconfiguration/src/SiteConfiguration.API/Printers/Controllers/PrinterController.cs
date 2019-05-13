using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteConfiguration.API.Printers.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using SiteConfiguration.API.Common.Constants;
using SiteConfiguration.API.Printers.Models.BuisnessContract;
using BD.Core.Context;
using BD.Core.BaseModels;
using Microsoft.EntityFrameworkCore;
using SiteConfiguration.API.Printers.Exceptions;

namespace SiteConfiguration.API.Printers.Controllers
{
    /// <summary>
    /// Controller for printers
    /// </summary>
    [Route("api/v{version:apiVersion}/siteconfiguration/facilities/{FacilityKey:guid}/")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class PrinterController : Controller
    {
        private readonly IPrinterBusiness _printerBusiness;
        private readonly ILogger<PrinterController> _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        /// <summary>
        /// Only constructor, params are automatically injected
        /// </summary>
        /// <param name="printerBusiness"></param>
        /// <param name="logger"></param>
        public PrinterController(IPrinterBusiness printerBusiness, ILogger<PrinterController> logger, IExecutionContextAccessor executionContextAccessor)
        {
            _printerBusiness = printerBusiness;
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;
        }

        /// <summary>
        /// HttpGet method to fetch printers specific to a facility
        /// </summary>
        /// <returns>List of Printers</returns>
        [HttpGet("printers")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> GetAllPrinters()
        {
            try
            {

                var facilityKey = Guid.Parse(_executionContextAccessor.Current.Facility.FacilityKey); 
                _logger.LogInformation(String.Format(Constants.GetPrinterRequestReceived, facilityKey));
                var printers = await _printerBusiness.GetPrintersByFacility(facilityKey);
                if (printers.Any())
                {
                    return Ok(printers);
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

        /// <summary>
        /// HttpGet method to fetch a printer
        /// </summary>
        /// <returns>Printer</returns>
        [HttpGet("printers/{key:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> GetPrinterByKey()
        {
            try
            {
                var key = Guid.Parse(RouteData.Values["key"].ToString());
                _logger.LogInformation(string.Format(Constants.GetPrinterByKeyRequestReceived, key));
                var printer = await _printerBusiness.GetPrinterByKey(key);
                if (printer != null)
                {
                    return Ok(printer);
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

        /// <summary>
        /// HttpPost method to add a printer
        /// </summary>
        /// <returns>PrinterId</returns>
        [HttpPost("printers")]
        [SwaggerResponse(201, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> AddPrinter([FromBody]PrinterRequest printer)
        {               
            try
            {         
                var facilityKey = Guid.Parse(_executionContextAccessor.Current.Facility.FacilityKey);
                _logger.LogInformation(string.Format(Constants.PostPrinterRequestReceived, facilityKey));
                await _printerBusiness.AddPrinter(printer, facilityKey);
                return StatusCode(201);
            }
            catch (InvalidPrinterException invalidprinterexception)
            {
                _logger.LogError(invalidprinterexception.Message);
                return BadRequest(new ErrorResponse(invalidprinterexception.Message, invalidprinterexception.HResult,
                    ResponsePayloadType.BusinessException));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// HttpPut method to Update existing printers
        /// </summary>
        /// <returns>httpstatuscode</returns>
        [HttpPut("printers/{key:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> UpdatePrinter(PrinterRequest printer)
        {
            try
            {
                var key = Guid.Parse(RouteData.Values["key"].ToString());
                var facilityKey = Guid.Parse(_executionContextAccessor.Current.Facility.FacilityKey);
                _logger.LogInformation(string.Format(Constants.PutPrinterRequestReceived, key));
                await _printerBusiness.UpdatePrinter(key, facilityKey, printer);

                return Ok();
            }
            catch (InvalidPrinterException printerException)
            {
                _logger.LogError(printerException.Message);
                return BadRequest(new ErrorResponse(printerException.Message, printerException.HResult,
                    ResponsePayloadType.BusinessException));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
