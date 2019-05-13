using BD.Core.BaseModels;
using BD.Core.EventBus.Abstractions;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.Common;
using Formulary.API.IntegrationEvents.Events;
using Formulary.API.Model;
using Formulary.API.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Controllers
{
    /// <summary>
    ///  Formulary controller is used to save formulary detials in the database and for pushing these detials to event bus
    /// </summary>
    [Route("/api/v{version:apiVersion}/formularies")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class FormularyController : ControllerBase
    {
        #region Private Fields
        private readonly IFormularyManager _formularyManager;
        private readonly IEventBus _eventBus;
        private readonly ILogger<FormularyController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISystemItemSetUpManager _systemItemSetUpManager;
        private readonly IItemSetupManager _itemSetupManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FormularyController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="formularyManager">The formulary manager.</param>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="systemItemSetUpManager">The system item set up manager.</param>
        /// <param name="itemSetupManager">The item setup manager.</param>
        /// <param name="httpContextAccessor">Http context Accessor</param>
        public FormularyController(IConfiguration configuration, ILogger<FormularyController> logger,
            IFormularyManager formularyManager, IEventBus eventBus, ISystemItemSetUpManager systemItemSetUpManager,
            IItemSetupManager itemSetupManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _formularyManager = formularyManager;
            _eventBus = eventBus;
            _logger = logger;
            _systemItemSetUpManager = systemItemSetUpManager;
            _itemSetupManager = itemSetupManager;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get Items List 
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>List of System Items</returns>
        [HttpGet]
        [SwaggerOperation("GetFacilitiesList")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<MedicationItemList>), description: "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> GetItems()
        {
            _logger.LogDebug("Get all system items list Request");
            var result = await _itemSetupManager.GetMedicationItems();
            return Ok(result);
        }


        /// <summary>
        /// Add System Item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(201, "Created")]
        public async Task<IActionResult> AddSystemItem(SystemItemSetupRequest request)
        {

            _logger.LogDebug("Add Item request received, data: {@NewSystemItemRequest}", request);
            if (ModelState.IsValid)
            {
                var itemSetupCreationResult = await _formularyManager.AddSystemItem(request);
                if (itemSetupCreationResult.OperationResult == CreateUpdateResultEnum.ErrorAlreadyExists)
                {
                    _logger.LogError("Error in saving Item with Item ID {ItemID}:",
                        request.ItemId);
                    return BadRequest($"Record already exists with Item ID {request.ItemId}");
                }

                if (itemSetupCreationResult.OperationResult == CreateUpdateResultEnum.ValidationFailed ||
                   itemSetupCreationResult.OperationResult == CreateUpdateResultEnum.UnknownError)
                {
                    _logger.LogError(
                        "Error in saving ItemSetup due to validation failure for data {@Facility}",
                        request);
                    return BadRequest($"Error in saving ItemSetup.");
                }
                _logger.LogInformation("Entity has been saved and entity object  {@Itemsetup}:", itemSetupCreationResult.Object);

                //todo: to be updated for Guid
                var timeStamp = (DateTime.Now.Ticks >> 20); // lose smallest 20 bits
                var autoValue = Convert.ToInt32(timeStamp % 1000000000);       // lost left digits to bring in integer range and convert to int

                var eventMessage = new FormularyUpdatedIntegrationEvent()
                {
                    IsActive = true,
                    FormularyId = autoValue,
                    ItemId = autoValue,
                    Description = request.Description
                };
                var headers =
                    _httpContextAccessor.HttpContext.Request.Headers
                        .ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key,
                            item => item.Value);
                _eventBus.Publish(_configuration[Constants.Topic_Publish_Formulary_Update], eventMessage, headers);
                return new CreatedResult(
                    itemSetupCreationResult.Object.ToString(),
                    itemSetupCreationResult.Object);
            }
            _logger.LogError("ModelState validation failed :{@Data}", request);
            return BadRequest(new ModelStateRequestValidationAdaptor(ModelState));

        }

        /// <summary>
        /// Update an existing system item setup
        /// </summary>
        /// <param name="itemkey">itemkey</param>
        /// <param name="medicationItem">To update medicationItem</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{itemkey}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal error")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> UpdateItemSetUpById(Guid itemkey, [FromBody] SystemItemSetupRequest medicationItem)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Update Item SetUp called with {@EditSyatemItemSetUp}", medicationItem);
                var systemItemUpdate = await _systemItemSetUpManager.UpdateSystemItemSetUp(itemkey, medicationItem);

                if (systemItemUpdate.OperationResult == CreateUpdateResultEnum.NotFound)
                {
                    _logger.LogError("No Result Found for item  {itemkey}.", itemkey);
                    return NotFound($"Item with itemkey key {itemkey} not found.");
                }
                if (systemItemUpdate.OperationResult != CreateUpdateResultEnum.Success)
                {
                    _logger.LogError("There was an error updating System item setup.");
                    return BadRequest("There was an error updating System item setup.");
                }

                _logger.LogInformation("Update Item SetUp successfully done with result : {EditSyatemItemSetUp}", systemItemUpdate);
                return Ok(systemItemUpdate.Object);
            }
            return BadRequest(new ModelStateRequestValidationAdaptor(ModelState));
        }


        #endregion

    }
}
