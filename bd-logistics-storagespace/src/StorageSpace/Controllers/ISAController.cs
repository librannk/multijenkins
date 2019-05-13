using BD.Core.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StorageSpace.API.BusinessLayer.Contracts;
using StorageSpace.API.Common;
using StorageSpace.API.Common.Helpers;
using StorageSpace.API.Model;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageSpace.API.Controllers
{

    /// <summary>
    /// Class ISAController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("/api/v{version:apiVersion}/storagespaces/facilities/{facilitykey:Guid}/isas/")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ISAController : Controller
    {
        private readonly IISAManager _isaManager;
        private readonly ILogger<ISAController> _logger;
        private readonly IExecutionContextAccessor executionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ISAController"/> class.
        /// </summary>
        /// <param name="isaManager">The isa manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="executionContextAccessor">Context accessor</param>
        public ISAController(IISAManager isaManager, ILogger<ISAController> logger, IExecutionContextAccessor executionContextAccessor)
        {
            this._isaManager = isaManager;
            _logger = logger;
            this.executionContextAccessor = executionContextAccessor;
        }

        /// <summary>
        /// Gets the isa.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> GetISA()
        {
            try
            {
                var facilityKey = Guid.Parse(executionContextAccessor.Current.Facility.FacilityKey);
                _logger.LogInformation(LoggingConstants.GetISA, facilityKey);
                List<ISARequest> isaList = await _isaManager.GetISA(facilityKey);
            
                return Ok(isaList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// To get isa Data by Id
        /// </summary>
        /// <param name="isaId"></param>
        /// <returns></returns>
        [Route("isaId")]
        [HttpGet]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> GetISAById(string isaId)
        {
            try
            {
                if (!ObjectIdValidator.Validate(isaId))
                {
                    return BadRequest("Please enter valid ISA Id.");
                }
                var facilityKey = Guid.Parse(executionContextAccessor.Current.Facility.FacilityKey);
                _logger.LogInformation(LoggingConstants.GetISAById, facilityKey);
                ISA isa = await _isaManager.GetISAById(isaId, facilityKey);
                if (isa == null)
                {
                    _logger.LogError(LoggingConstants.Empty_List);
                    return NotFound($"ISA with ISA ID {isaId} not found");
                }
                return Ok(isa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}