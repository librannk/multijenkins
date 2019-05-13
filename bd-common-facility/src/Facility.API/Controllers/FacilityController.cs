using BD.Core.BaseModels;
using BD.Core.EventBus.Abstractions;
using Facility.API.BusinessLayer.Contracts;
using Facility.API.Configuration;
using Facility.API.Constants;
using Facility.API.Infrastructure.Extensions;
using Facility.API.Infrastructure.Filters;
using Facility.API.IntegrationEvents.Events;
using Facility.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facility.API.Controllers
{
    /// <summary>
    /// Facility controller is used to save facility details in the database and for pushing these details to event bus
    /// </summary>
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/facilities")]
    [Authorize]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        #region Private Fields
        private readonly IFacilityManager _facilityManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventBus _eventBus;
        private readonly ILogger<FacilityController> _logger;
        private readonly MessageBusTopics _messageBusTopicsConfiguration;
        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="facilityManager">The facility manager.</param>
        /// <param name="messageBusTopicsConfiguration">The message bus topics configuration.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public FacilityController(ILogger<FacilityController> logger,
            IEventBus eventBus,
            IFacilityManager facilityManager,
            IOptions<MessageBusTopics> messageBusTopicsConfiguration,
            IHttpContextAccessor httpContextAccessor)
        {
            _facilityManager = facilityManager;
            _httpContextAccessor = httpContextAccessor;
            _eventBus = eventBus;
            _logger = logger;
            _messageBusTopicsConfiguration = messageBusTopicsConfiguration?.Value;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Gets list of Facilities.
        /// </summary>
        /// <remarks>Provides the list of all facilities.
        /// Optionally provides support to filter Inactive Facilities, Search facilities partially by facility name,
        /// and support for pagination options.</remarks>
        /// <param name="showInactive">Should the result include inactive results. Default value is false</param>
        /// <param name="offset">Start index or number of records to skip.</param>
        /// <param name="limit">Page size or number of records to be returned.</param>
        /// <param name="searchTerm">Facility name that should be searched. Partial string can be entered</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [SwaggerOperation("GetFacilitiesList")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<FacilityList>), description: "OK")]
        [TotalCountFilter]
        public async Task<IActionResult> GetFacilitiesList([FromQuery] bool showInactive = false,
            [FromQuery] int offset = 0, [FromQuery] int limit = 0, [FromQuery]string searchTerm = "")
        {
            _logger.LogDebug("Get all facilities Request with search in {ShowInActive} flag", showInactive);
            var facilitiesBusinessResult = await _facilityManager.GetAllFacilities(showInactive, searchTerm, offset, limit);
            _httpContextAccessor?.HttpContext.SetTotalCount(facilitiesBusinessResult.ResultCount);
            return Ok(facilitiesBusinessResult.Object);
        }

        /// <summary>
        /// Gets the Facility by facilityKey
        /// </summary>
        /// <remarks>Gets the complete Facility by the Facility Id.</remarks>
        /// <param name="facilityKey">Facility Id for which system for which data should be fetched.</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{facilityKey}")]
        [SwaggerOperation("GetFacilityBykey")]
        [SwaggerResponse(statusCode: 200, type: typeof(Model.Facility), description: "success")]
        public async Task<IActionResult> GetFacilityByKey(Guid facilityKey)
        {
            _logger.LogDebug("Get Facility by FacilityKey called with {FacilityKey}", facilityKey);
            if (facilityKey.Equals(Guid.Empty))
            {
                ModelState.AddModelError("facilitykey", "Empty or null Facility Key");
                _logger.LogError("Empty or null facility key passed {FacilityKey}", facilityKey);
                return BadRequest(new ModelStateRequestValidationAdaptor(ModelState));
            }

            var facility = await _facilityManager.GetFacilityByKeyAsync(facilityKey);
            if (facility == null)
            {
                _logger.LogError("No facility Key found with Facility key {FacilityKey}", facilityKey);
                ModelState.AddModelError("facilitykey", "Facility Key");
                return NotFound($"No facility Key found with Facility key {facilityKey}");
            }

            return Ok(facility);
        }

        /// <summary>
        ///  Method to save facility details in to SQL database and for the publishing messages to Event Bus
        /// </summary>
        /// <param name="request">Any String message</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [SwaggerResponse(201, type: typeof(Model.Facility))]
        [SwaggerOperation("AddFacility")]
        public async Task<IActionResult> AddFacility([FromBody] NewFacilityRequest request)
        {
            _logger.LogDebug("Add facility request received, data: {@NewFacilityRequest}", request);
            if (ModelState.IsValid)
            {
                var facilityCreationResult = await _facilityManager.Add(request);

                if (facilityCreationResult.OperationResult == CreateUpdateResultEnum.Success)
                {
                    _logger.LogDebug("Entity has been saved with facility {Facility}:",
                        facilityCreationResult.Object.FacilityKey);
                    //var eventMessage = new FacilityAddedIntegrationEvent()
                    //{
                    //    //TODO: following code needs to be updated.
                    //    IsActive = facilityCreationResult.Object.ActiveFlag,
                    //    ProcessInactiveAsException = false,// facilityEntity.ProcessInactiveAsException,
                    //    FacilityId = request.Id,//ID
                    //    FacilityCode = facilityCreationResult.Object.FacilityCode,
                    //    ADUIgnoreStockOut = false,//facilityEntity.ADUIgnoreStockOut,
                    //    AduIgnoreCritLow = false,//facilityEntity.AduIgnoreCritLow

                    //};
                    //TODO: This code is to be removed. commented temporarily to test out event bus.
                    //var headers =
                    //    _httpContextAccessor.HttpContext.Request.Headers
                    //        .ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key,
                    //            item => item.Value);
                    //_eventBus.Publish(_messageBusTopicsConfiguration.KafkaFacilityDetailsTopic, eventMessage, headers);
                    //_logger.LogDebug("data published to {EventBus} and data object :{@Data}",
                    //    _messageBusTopicsConfiguration.KafkaFacilityDetailsTopic, eventMessage);
                    return new CreatedResult(
                        facilityCreationResult.Object.FacilityKey.ToString(), facilityCreationResult.Object);
                }

                if (facilityCreationResult.OperationResult == CreateUpdateResultEnum.ErrorAlreadyExists)
                {
                    _logger.LogError("Record already exists with facility code {FacilityCode}:",
                        request.FacilityCode);
                    ModelState.AddModelError(nameof(request.FacilityCode), "Record already exists with facility code");
                }
                if (facilityCreationResult.OperationResult == CreateUpdateResultEnum.ValidationFailed ||
                    facilityCreationResult.OperationResult == CreateUpdateResultEnum.UnknownError)
                {
                    _logger.LogError(
                        "Error in saving facility due to validation failure for data {@Facility}",
                        request);
                    ModelState.AddModelError(string.Empty,
                        "Error in saving facility due to validation failure for data.");
                }
            }


            _logger.LogDebug("ModelState validation failed :{@Data}", request);
            return BadRequest(new ModelStateRequestValidationAdaptor(ModelState));
        }

        /// <summary>
        /// Updates the facility.
        /// </summary>
        /// <param name="facilitykey"></param>
        /// <param name="request">Any String message</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{facilitykey}")]
        [SwaggerResponse(200, "Success", type: typeof(Model.Facility))]
        [SwaggerOperation("UpdateFacility")]
        public async Task<IActionResult> UpdateFacility(Guid facilitykey, [FromBody]UpdateFacilityRequest request)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Update Facility Request called with {@UpdateRequest}", request);
                var facilityUpdate = await _facilityManager.Update(facilitykey, request);
                if (facilityUpdate.OperationResult == CreateUpdateResultEnum.Success)
                {
                    _logger.LogInformation("Facility successfully saved updated result : {UpdateResult}", facilityUpdate);
                    return Ok(facilityUpdate.Object);
                }

                if (facilityUpdate.OperationResult == CreateUpdateResultEnum.NotFound)
                {
                    _logger.LogError("No Result Found for facility  {FacilityKey}.", facilitykey);
                    ModelState.AddModelError("facilitykey", "Error Processing Request");
                }
                if (facilityUpdate.OperationResult == CreateUpdateResultEnum.ValidationFailed)
                {
                    _logger.LogError("Error updating facility  {FacilityKey} as validation failed from database.", facilitykey);
                    ModelState.AddModelError("facilitykey", "Error Processing Request");
                }
            }
            _logger.LogDebug("ModelState validation failed :{@Data}", request);
            return BadRequest(new ModelStateRequestValidationAdaptor(ModelState));
        }
        #endregion
    }
}