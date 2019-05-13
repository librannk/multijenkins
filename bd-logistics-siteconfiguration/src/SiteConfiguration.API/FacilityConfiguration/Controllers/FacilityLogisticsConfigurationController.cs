using BD.Core.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SiteConfiguration.API.FacilityConfiguration.Abstractions;
using SiteConfiguration.API.FacilityConfiguration.RequestResponseModel;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace SiteConfiguration.API.FacilityConfiguration.Controllers
{
    /// <summary>
    /// FacilityLogisticsConfigurationController
    /// </summary>
    [Route("api/v{version:apiVersion}/siteconfigurations/facilities/{facilitykey:Guid}/")]
    //[Route("siteconfiguration/api/v{version:apiVersion}/facilities/{facilitykey}/")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class FacilityLogisticsConfigurationController : FacilityLogisticsBaseController
    {
        private readonly IFacilityLogisticsConfiguration _business;
        /// <summary>
        /// logger key
        /// </summary>
        public ILogger<FacilityLogisticsConfigurationController> _logger;
        IExecutionContextAccessor _accessor;

        /// <summary>
        /// FacilityLogisticsConfigurationController 
        /// </summary>
        /// <param name="business"></param>
        /// <param name="accessor"></param>
        /// <param name="logger"></param>
        public FacilityLogisticsConfigurationController(IFacilityLogisticsConfiguration business, IExecutionContextAccessor accessor, ILogger<FacilityLogisticsConfigurationController> logger)
        {
            _accessor = accessor;
            _logger = logger;
            _business = business;
        }

        /// <summary>
        /// HttpPost method to save facility configuration info
        /// </summary>
        /// <param name="facilityConfiguration"></param>
        /// <returns></returns>
        [HttpPost("settings")]

        [SwaggerResponse(201, "Created Successfully")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<ActionResult<TransactionQueueConfigurationRequest>> CreateAsync(TransactionQueueConfigurationRequest facilityConfiguration)
        {
            try
            {
                facilityConfiguration.FacilityKey = Guid.Parse(_accessor.Current.Facility.FacilityKey);

                if (ModelState.IsValid)
                {
                    await _business.CreateFacilitySpecificConfigurationAsync(facilityConfiguration);
                    _logger.LogInformation("Created Successfully : {CreatedResult}", facilityConfiguration);
                    return Created(new Uri($"{Request.Path}", UriKind.Relative), new { Message = "Created Successfully.", Model = facilityConfiguration });
                }
                else
                {
                    _logger.LogError("Bad Request:", ModelState.ValidationState);
                    return BadRequestErrorMessage();
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
        }

        /// <summary>
        /// HttpPost method to save facility extension info
        /// </summary>
        /// <param name="facilityLogisticsConfigurationExtension"></param>
        /// <returns></returns>
        [HttpPost("extensions")]
        [SwaggerResponse(201, "Created Successfully")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<ActionResult<FacilityLogisticsConfigurationExtension>> PostFacilityExtensionAsync(FacilityLogisticsConfigurationExtension facilityLogisticsConfigurationExtension)
        {
            try
            {
                facilityLogisticsConfigurationExtension.FacilityKey = Guid.Parse(_accessor.Current.Facility.FacilityKey);

                if (ModelState.IsValid)
                {
                    BusinessResponse response = await _business.CreateFacilityExtensionAsync(facilityLogisticsConfigurationExtension);
                    if (response.IsSuccess == false)
                    {
                        _logger.LogError("Bad Request:", response.Message);
                        return BadRequest(response.Message);
                    }
                    else
                    {
                        _logger.LogInformation("Created Successfully : {CreatedResult}", facilityLogisticsConfigurationExtension);
                        return Created(new Uri($"{Request.Path}", UriKind.Relative), new { Message = "Created Successfully.", Model = facilityLogisticsConfigurationExtension });
                    }
                }
                else
                {
                    _logger.LogError("Bad Request:", ModelState.ValidationState);
                    return BadRequestErrorMessage();
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
        }

        /// <summary>
        /// Http Put method for updating existing facility configuration
        /// </summary>
        /// <param name="transactionQueueConfigurationRequest"></param>
        /// <returns></returns>
        [HttpPut("settings")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Error")]
        public async Task<ActionResult<TransactionQueueConfigurationRequest>> PutfacilityConfigurationAsync(TransactionQueueConfigurationRequest transactionQueueConfigurationRequest)
        {
            try
            {
                transactionQueueConfigurationRequest.FacilityKey = Guid.Parse(_accessor.Current.Facility.FacilityKey);

                if (ModelState.IsValid)
                {
                    BusinessResponse businessResponse = await _business.UpdateFacilityConfigAsync(transactionQueueConfigurationRequest);
                    if (businessResponse.IsSuccess == false)
                    {
                        _logger.LogError("Bad Request:", ModelState.ValidationState);
                        return BadRequest(businessResponse.Message);
                    }
                    else
                    {
                        _logger.LogInformation("Updated Successfully : {UpdateResult}", transactionQueueConfigurationRequest);
                        return Ok(businessResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError("Bad Request:", ModelState.ValidationState);
                    return BadRequestErrorMessage();
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
        }

        /// <summary>
        /// facilityLogisticsConfigurationExtension
        /// </summary>
        /// <param name="facilityLogisticsConfigurationExtension"></param>
        /// <returns></returns>
        [HttpPut("extensions")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<ActionResult<FacilityLogisticsConfigurationExtension>> PutFacilityExtensionAsync(FacilityLogisticsConfigurationExtension facilityLogisticsConfigurationExtension)
        {
            try
            {
                facilityLogisticsConfigurationExtension.FacilityKey = Guid.Parse(_accessor.Current.Facility.FacilityKey);

                if (ModelState.IsValid)
                {
                    BusinessResponse businessResponse = await _business.UpdateFacilityExtensionAsync(facilityLogisticsConfigurationExtension);
                    if (businessResponse.IsSuccess == false)
                    {
                        _logger.LogError("Bad Request:", businessResponse.Message);
                        return BadRequest(businessResponse.Message);
                    }
                    else
                    {
                        _logger.LogInformation("Updated Successfully : {UpdateResult}", facilityLogisticsConfigurationExtension);
                        return Ok(businessResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError("Bad Request:", ModelState.ValidationState);
                    return BadRequestErrorMessage();
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequestErrorMessage();
            }
        }
    }
}