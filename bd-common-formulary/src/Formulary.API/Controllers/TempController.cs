//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BD.Core.EventBus.Abstractions;
//using Formulary.API.BusinessLayer.Contract;
//using Formulary.API.Common;
//using Formulary.API.IntegrationEvents.Events;
//using Formulary.API.Model;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Primitives;
//using Newtonsoft.Json;
//using Swashbuckle.AspNetCore.Annotations;

//namespace Formulary.API.Controllers
//{
//    /// <summary>
//    /// This is TEMPORARY API only for Backward Compatiblity. Once the actual APIs are created this would be removed.
//    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
//    /// </summary>
//    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
//    [Route("formulary/api/v{version:apiVersion}/Temp")]
//    [Authorize]
//    [ApiController]
//    [ApiVersion("1.0")]
//    public class TempController : ControllerBase
//    {
//        #region Private Fields
//        private readonly IFormularyManager _formularyManager;
//        private readonly IEventBus _eventBus;
//        private readonly ILogger<FormularyController> _logger;
//        private readonly IConfiguration _configuration;
//        private readonly ISystemItemSetUpManager _systemItemSetUpManager;
//        private readonly IItemSetupManager _itemSetupManager;

//        #endregion
//        #region Constructor

//        /// <summary>
//        /// Initializes a new instance of the <see cref="FormularyController"/> class.
//        /// </summary>
//        /// <param name="configuration">The configuration.</param>
//        /// <param name="logger">The logger.</param>
//        /// <param name="formularyManager">The formulary manager.</param>
//        /// <param name="eventBus">The event bus.</param>
//        /// <param name="systemItemSetUpManager">The system item set up manager.</param>
//        /// <param name="itemSetupManager">The item setup manager.</param>
//        public TempController(IConfiguration configuration, ILogger<FormularyController> logger,
//            IFormularyManager formularyManager, IEventBus eventBus, ISystemItemSetUpManager systemItemSetUpManager,
//            IItemSetupManager itemSetupManager)
//        {
//            _configuration = configuration;
//            _formularyManager = formularyManager;
//            _eventBus = eventBus;
//            _logger = logger;
//            _systemItemSetUpManager = systemItemSetUpManager;
//            _itemSetupManager = itemSetupManager;
//        }

//        #endregion

//        #region Public Methods

//        /// <summary>
//        ///  Method to save formulary details in to sql database and for the publishing messages to Event Bus
//        /// </summary>
//        /// <param name="request">Any String message</param>
//        [HttpPost]
//        [SwaggerResponse(200, "Success")]
//        [SwaggerResponse(400, "Bad Request")]
//        [SwaggerResponse(500, "Internal error")]
//        [SwaggerResponse(401, "Unauthorized")]
//        public async Task<IActionResult> Formulary([FromBody] FormularyRequest request)
//        {
//            try
//            {
//                _logger.LogInformation(string.Format(Constants.IncomingRequest, JsonConvert.SerializeObject(request)));

//                var errorList = (from item in ModelState
//                                 where item.Value.Errors.Any()
//                                 select item.Value.Errors[0].ErrorMessage).ToList();
//                if (ModelState.IsValid)
//                {
//                    var formularyEntity = await _formularyManager.SaveFormulary(request);
//                    _logger.LogInformation(string.Format(Constants.Entity_Saved, JsonConvert.SerializeObject(formularyEntity)));
//                    if (formularyEntity != null)
//                    {
//                        var eventMessage = new FormularyUpdatedIntegrationEvent()
//                        {
//                            IsActive = formularyEntity.Active,
//                            FormularyId = formularyEntity.Id,
//                            ItemId = formularyEntity.ItemId,
//                            Description = formularyEntity.Description
//                        };

//                        var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
//                        _eventBus.Publish(_configuration[Constants.Topic_Publish_Formulary_Update], eventMessage, headers);
//                        _logger.LogInformation(string.Format(Constants.Data_Published, _configuration[Constants.Topic_Publish_Formulary_Update], JsonConvert.SerializeObject(eventMessage)));

//                        await Task.CompletedTask;

//                        return Ok(formularyEntity);
//                    }
//                    return BadRequest(NotSuccess.RecordNotInserted);
//                }
//                else
//                {
//                    return BadRequest(errorList);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(string.Format(Constants.Formulary_Error_Msg, ex.Message));
//                return StatusCode(500, ex.InnerException);
//            }
//        }

//        /// <summary>
//        /// Method to save facility details in to sql database and for the publishing messages to Event Bus
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [SwaggerResponse(200, "Success")]
//        [SwaggerResponse(400, "Bad Request")]
//        [SwaggerResponse(500, "Internal error")]
//        [Route("MapFormularyFacility")]
//        [SwaggerResponse(401, "Unauthorized")]
//        public async Task<IActionResult> MapFormularyFacility([FromBody] FacilityRequest request)
//        {
//            var errorList = (from item in ModelState
//                             where item.Value.Errors.Any()
//                             select item.Value.Errors[0].ErrorMessage).ToList();
//            try
//            {
//                _logger.LogInformation(string.Format(Constants.IncomingRequest, JsonConvert.SerializeObject(request)));

//                if (ModelState.IsValid)
//                {

//                    var facilityEntity = await _formularyManager.SaveFacility(request);
//                    _logger.LogInformation(string.Format(Constants.Entity_Saved, facilityEntity));
//                    if (facilityEntity != null)
//                    {
//                        var eventMessage = new FormularyFacilityUpdatedIntegrationEvent()
//                        {
//                            FormularyId = facilityEntity.FormularyId,
//                            FacilityId = facilityEntity.Id,
//                            Active = facilityEntity.Active,
//                            Approved = facilityEntity.Approved,
//                            AduIgnoreCritLow = facilityEntity.ADUIgnoreCritLow,
//                            AduIgnoreStockout = facilityEntity.ADUIgnoreStockOut
//                        };

//                        var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
//                        _eventBus.Publish(_configuration[Constants.TOPIC_PUBLISH_FACILITY_UPDATE], eventMessage, headers);
//                        _logger.LogInformation(string.Format(Constants.Data_Published, _configuration[Constants.TOPIC_PUBLISH_FACILITY_UPDATE], JsonConvert.SerializeObject(eventMessage)));

//                        await Task.CompletedTask;
//                        return Ok(facilityEntity);
//                    }
//                    return BadRequest(NotSuccess.DbInsertionIssue);
//                }

//                else
//                {
//                    return BadRequest(errorList);

//                }

//            }

//            catch (Exception ex)
//            {
//                _logger.LogError(string.Format(Constants.Formulary_Error_Msg, ex.Message));
//                return StatusCode(500, ex.InnerException);
//            }
//        }


//        /// <summary>
//        /// Method to save NDC details in to sql database and for the publishing messages to Event Bus
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [SwaggerResponse(200, "Success")]
//        [SwaggerResponse(400, "Bad Request")]
//        [SwaggerResponse(500, "Internal error")]
//        [SwaggerResponse(401, "Unauthorized")]
//        [Route("MapFormularyNDC")]
//        public async Task<IActionResult> MapFormularyNDC([FromBody] NDCRequest request)
//        {
//            var errorList = (from item in ModelState
//                             where item.Value.Errors.Any()
//                             select item.Value.Errors[0].ErrorMessage).ToList();
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    var nDCEntity = await _formularyManager.SaveNDC(request);
//                    _logger.LogInformation(string.Format(Constants.Entity_Saved, nDCEntity));

//                    var facilityNDCAssocEntity = await _formularyManager.SaveFacilityNDCAssoc(request.FacilityNDCDetails);

//                    _logger.LogInformation(string.Format(Constants.Entity_Saved, facilityNDCAssocEntity));
//                    if (nDCEntity != null && facilityNDCAssocEntity != null)
//                    {
//                        var eventMessage = new NDCUpdatedIntegrationEvent()
//                        {
//                            Cost = facilityNDCAssocEntity.Cost,
//                            NDCId = facilityNDCAssocEntity.NDCId,
//                            GenericName = nDCEntity.GenericName,
//                            TradName = nDCEntity.TradeName,
//                        };

//                        var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
//                        _eventBus.Publish(_configuration[Constants.TOPIC_PUBLISH_FACILITY_UPDATE], eventMessage, headers);
//                        _logger.LogInformation(string.Format(Constants.Data_Published, _configuration[Constants.TOPIC_PUBLISH_FACILITY_UPDATE], eventMessage));

//                        await Task.CompletedTask;
//                        request.Id = nDCEntity.Id;
//                        return Ok(request);
//                    }
//                    return BadRequest(NotSuccess.DbInsertionIssue);
//                }
//                else
//                {
//                    return BadRequest(errorList);
//                }
//            }

//            catch (Exception ex)
//            {
//                _logger.LogError(string.Format(Constants.NDC_ERROR_MSG, ex.Message));
//                return StatusCode(500, ex.Message);
//            }
//        }

//        /// <summary>
//        /// For Update Item ID
//        /// </summary>
//        /// <param name="Id"></param>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPut]
//        [SwaggerResponse(200, "Success")]
//        [SwaggerResponse(400, "Bad Request")]
//        [SwaggerResponse(500, "Internal error")]
//        [SwaggerResponse(401, "Unauthorized")]
//        [Route("FormularyItemUpdate/{Id}")]
//        public async Task<IActionResult> FormularyUpdate(int Id, FormularyRequest request)
//        {
//            var errorList = (from item in ModelState
//                             where item.Value.Errors.Any()
//                             select item.Value.Errors[0].ErrorMessage).ToList();

//            _logger.LogInformation(string.Format(Constants.IncomingRequest, JsonConvert.SerializeObject(request)));

//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    var formualryEntity = await _formularyManager.UpdateFormulary(Id, request);
//                    if (formualryEntity != null)
//                    {
//                        _logger.LogInformation(string.Format(Constants.Entity_Saved, JsonConvert.SerializeObject(formualryEntity)));
//                        return Ok(formualryEntity);
//                    }
//                    return BadRequest(NotSuccess.UpdateDbIssue);
//                }
//                else
//                {
//                    return BadRequest(errorList);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(string.Format(Constants.Formulary_Error_Msg, ex.Message));
//                return StatusCode(500, ex.InnerException);
//            }
//        }

//        #endregion
//    }
//}