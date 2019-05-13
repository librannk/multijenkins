using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Models;
using Swashbuckle.AspNetCore.Annotations;
using SwashBuckle;
using SiteConfiguration.API.Common.Constants;
using BD.Core.BaseModels;
using SiteConfiguration.API.Schedule.Exceptions;
using BD.Core.ResiliencePolicy;

namespace SiteConfiguration.API.Schedule.Controllers
{
    /// <summary>
    /// Controller for schedules
    /// </summary>
    [Route("api/v{version:apiVersion}/siteconfiguration/facilities/{FacilityKey:guid}/")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleBusiness _scheduleBusiness;
        private readonly ILogger<ScheduleController> _logger;
        private readonly HttpClientFactory _factory;
        /// <summary>
        /// Only constructor, params are automatically injected
        /// </summary>
        /// <param name="scheduleBusiness"></param>
        /// <param name="logger"></param>
        /// <param name="factory"></param>
        public ScheduleController(IScheduleBusiness scheduleBusiness, ILogger<ScheduleController> logger, HttpClientFactory factory)
        {
            _scheduleBusiness = scheduleBusiness;
            _logger = logger;
            _factory = factory;
        }

        #region Public Action Methods
        /// <summary>
        /// HttpGet method to fetch schedules specific to a facility
        /// </summary>
        /// <returns>List of Schedules</returns>
        [HttpGet("pickschedules")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult>  GetAllSchedules()
        {
            try
            {
                _logger.LogInformation(string.Format(Constants.GetScheduleRequestReceived, Guid.Parse(RouteData.Values["FacilityKey"].ToString())));
                var schedules = await _scheduleBusiness.GetSchedules(Guid.Parse(RouteData.Values["FacilityKey"].ToString()));
                if (schedules.Any())  
                {
                    return Ok(schedules);
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
        /// HttpGet method to fetch schedules by its Id
        /// </summary>
        /// <returns>Schedule</returns>
        [HttpGet("pickschedules/{key:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> GetScheduleByKey()
        {
            try
            {
                _logger.LogInformation(Constants.GetScheduleRequestReceived, Guid.Parse(RouteData.Values["key"].ToString()));
                var schedule = await _scheduleBusiness.GetScheduleByKey(Guid.Parse(RouteData.Values["key"].ToString()));
                if (schedule != null)
                {
                    return Ok(schedule);
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
        /// HttpPost method to enter a new schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        [HttpPost("pickschedules")]
        [SwaggerResponse(201, "Request Accepted")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<IActionResult> AddSchedule(ScheduleRequest schedule)
        {
            try
            {
                _logger.LogInformation(string.Format(Constants.PostScheduleRequestReceived,
                    Guid.Parse(RouteData.Values["FacilityKey"].ToString())));
                if (ModelState.IsValid)
                {
                    await _scheduleBusiness
                        .AddSchedule(Guid.Parse(RouteData.Values["FacilityKey"].ToString()), schedule)
                        .ConfigureAwait(false);
                    return StatusCode(201);
                }
                else
                {
                    _logger.LogInformation(Constants.ModelValidationFailed);
                    return BadRequest(new ModelStateRequestValidationAdaptor(ModelState));
                }
            }
            catch (InvalidScheduleException scheduleException)
            {
                return BadRequest(new ErrorResponse(scheduleException.Message, scheduleException.HResult, 
                    ResponsePayloadType.BusinessException));
            }
            catch (ArgumentNullException argumentNullException)
            {
                return BadRequest(argumentNullException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// HttpPut method to Update existing schedules
        /// </summary>
        /// <returns>httpstatuscode</returns>
        [HttpPut("pickschedules/{key:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> UpdateSchedule(ScheduleRequest schedule)
        {
            try
            {
                var key = Guid.Parse(RouteData.Values["key"].ToString());
                var facilityKey = Guid.Parse(RouteData.Values["FacilityKey"].ToString());
                _logger.LogInformation(Constants.DeleteScheduleRequestReceived, key);

                await _scheduleBusiness.UpdateSchedule(key, facilityKey, schedule);

                return Ok();
            }
            catch (InvalidScheduleException scheduleException)
            {
                return BadRequest(new ErrorResponse(scheduleException.Message, scheduleException.HResult,
                    ResponsePayloadType.BusinessException));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// HttpDelete method to delete schedules specific to a facility
        /// </summary>
        /// <returns>httpstatuscode</returns>
        [HttpDelete("pickschedules/{key:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> DeleteSchedule()
        {
            try
            {
                var key = Guid.Parse(RouteData.Values["key"].ToString());
                _logger.LogInformation(Constants.DeleteScheduleRequestReceived, key);

                await _scheduleBusiness.DeleteSchedule(key);
                
                return Ok();
            }
            catch (InvalidScheduleException scheduleException)
            {
                return BadRequest(new ErrorResponse(scheduleException.Message, scheduleException.HResult,
                    ResponsePayloadType.BusinessException));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// HttpGet method to test sync flow for tracing.
        /// </summary>
        /// <returns>List of Printers</returns>
        [HttpGet("DemoSyncTracing")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> DemoSyncTracing()
        {
            _factory.Client.DefaultRequestHeaders.Add("Authorization", Request.Headers["Authorization"].ToString());
            var result = await _factory.Client.GetStringAsync($"http://qabd.westus.cloudapp.azure.com/api/v1/siteconfiguration/facilities/{Guid.Parse(RouteData.Values["FacilityKey"].ToString())}/printers");
            return Ok(result);
        }
        #endregion
    }
}