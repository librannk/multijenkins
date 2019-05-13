using BD.Core.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransactionQueue.API.Application.BussinessLayer.Abstraction;
using TransactionQueue.API.Application.Models;
using TransactionQueue.API.Common.Constants;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.Shared.Models;

namespace TransactionQueue.API.Controllers
{
    /// <summary> It contains the TransactionQueue related action methods</summary>
    [Route("/api/v{version:apiVersion}/transactionqueue")]
    [ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    public class TransactionQueueController : ControllerBase
    {
        #region Fields
        private readonly ILogger _logger;
        private readonly ITransactionQueueManager _transactionQueueManager;
        private readonly ITransactionQueueBussiness _transactionQueueBussiness;
        private readonly IQueueFilter _queueFilter;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="transactionQueueManager"></param>
        /// <param name="transactionQueueBussiness"></param>
        /// <param name="queueFilter"></param>

        public TransactionQueueController(
            ILogger<TransactionQueueController> logger,
            ITransactionQueueManager transactionQueueManager, ITransactionQueueBussiness transactionQueueBussiness, IQueueFilter queueFilter
            )
        {
            _logger = logger;
            _transactionQueueManager = transactionQueueManager;
            _transactionQueueBussiness = transactionQueueBussiness;
            _queueFilter = queueFilter;
        }
        #endregion

        #region Public Action-Methods
        ///// <summary>
        ///// Activates the transaction and publish data to request formulary location
        ///// </summary>
        ///// <param name="transactionQueueId"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //[Route("transaction/{transactionQueueId}")]
        //[HttpPatch]
        //[SwaggerResponse(200, "Success")]
        //[SwaggerResponse(400, "Bad Request")]
        //[SwaggerResponse(401, "Unauthorized")]
        //[SwaggerResponse(500, "Internal Server Error")]
        //public async Task<IActionResult> UpdateTransactionStatus(string transactionQueueId, [FromBody] Transaction transaction)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(transactionQueueId) || (transaction == null && transaction.Status == null))
        //        {
        //            _logger.LogError(Constants.LoggingMessage.TransactionIdOrStatusNull);
        //            return BadRequest(Constants.LoggingMessage.TransactionIdOrStatusNull);
        //        }

        //        if (transaction.Status != TransactionStatus.Active && transaction.Status != TransactionStatus.Complete)
        //        {
        //            var errorMessage = string.Format(Constants.LoggingMessage.InvalidStatus, transactionQueueId);
        //            _logger.LogError(errorMessage);
        //            return BadRequest(errorMessage);
        //        }

        //        _logger.LogInformation(string.Format(Constants.LoggingMessage.ActivateTransactionStarted, transactionQueueId));

        //        var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
        //        var response = await _transactionQueueManager.UpdateTransactionStatus(transactionQueueId, transaction.Status.Value, headers);

        //        if (response == null)
        //        {
        //            var errorMessage = string.Format(Constants.LoggingMessage.InvalidRequestForTransaction, transactionQueueId);
        //            _logger.LogError(errorMessage);
        //            return BadRequest(errorMessage);
        //        }

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return StatusCode((int)HttpStatusCode.InternalServerError);
        //    }
        //}

        /// <summary>
        /// Get All sorted Transaction (Active, Pending and Hold)
        /// </summary>
        /// <param name="actorKey"></param>
        /// <param name="activeTQId"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transactionType"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>returns All sorted Transaction (Active, Pending and Hold)</returns>

        [HttpGet]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetTransactions([FromQuery] int actorKey, [FromQuery] string activeTQId, [FromQuery] string sortColumn, [FromQuery] int sortDirection, [FromQuery] string transactionType, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                //Convert Transaction Type into Title case
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                transactionType = textInfo.ToTitleCase(transactionType.ToLower());

                if (actorKey > 0 && !string.IsNullOrEmpty(activeTQId) && !string.IsNullOrEmpty(transactionType))
                {
                    var activeISA = await _transactionQueueBussiness.GetActiveISA(actorKey);
                    if (activeISA != null)
                    {
                        var activeAndPendingTransactions = _transactionQueueBussiness.GetTransactions(activeTQId, activeISA, transactionType);

                        var holdTransactions = _transactionQueueBussiness.GetHoldTransactions(activeISA, transactionType);

                        await Task.WhenAll(activeAndPendingTransactions, holdTransactions);

                        var sortedTransactions = _queueFilter.GetSortedTransaction((await activeAndPendingTransactions).Item1, (await activeAndPendingTransactions).Item2, holdTransactions.Result, sortColumn, sortDirection, page, pageSize);
                        return Ok(sortedTransactions);
                    }
                    else
                    {
                        _logger.LogError("Invalid request parameters");
                        return BadRequest(new ErrorResponse("Invalid request parameters", (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                    }
                }
                else
                {
                    _logger.LogError("Invalid request parameters");
                    return BadRequest(new ErrorResponse("Invalid request parameters", (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                  
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// Pick Now to make tarnsaction active
        /// </summary>
        /// <param name="activeTransactionQueueKey">Transaction key which is active</param>
        /// <param name="actorKey"> actorkey</param>
        /// <param name="pickNow">PickNow object to activate</param>
        /// <returns></returns>
        [Route("{activeTransactionQueueKey}/user/{actorKey:int}/picknow")]
        [HttpPut]
        public async Task<ActionResult> PickNow(string activeTransactionQueueKey, int actorKey, PickNow pickNow)
        {
            try
            {
                if (string.IsNullOrEmpty(activeTransactionQueueKey) || !Regex.IsMatch(activeTransactionQueueKey, @"^[0-9a-fA-F]{24}$"))
                {
                    BusinessResponse objBadRequest = new BusinessResponse() { IsSuccess = false, Message = "ActiveTransactionQueueKey is not valid.", StatusCode = 400 };
                    _logger.LogError(objBadRequest.Message);
                    return BadRequest(objBadRequest);

                }
                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                BusinessResponse objBusinessResponse = await _transactionQueueBussiness.PickNow(activeTransactionQueueKey, actorKey, pickNow, headers);
                
                if (!objBusinessResponse.IsSuccess && objBusinessResponse.StatusCode == 400)
                {
                    return BadRequest(new ErrorResponse(objBusinessResponse.Message, objBusinessResponse.StatusCode, ResponsePayloadType.BusinessException));
                }
                else if (!objBusinessResponse.IsSuccess && objBusinessResponse.StatusCode == 404)
                {
                    return NotFound(new ErrorResponse(objBusinessResponse.Message, objBusinessResponse.StatusCode, ResponsePayloadType.BusinessException));
                }
               
                return Ok(pickNow.TransactionQueueKeyToActivate);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        ///<summary>
        /// Mark Active transaction to Complete
        /// </summary>
        /// <param name="activeTransactionQueueKey">Transaction key which is active</param>
        /// <param name="actorKey">Login User actorKey</param>
        /// <param name="tQRequestObjectForComplete">Request obejct</param>
        [Route("{activeTransactionQueueKey}/user/{actorKey}/complete")]
        [HttpPut]
        public async Task<ActionResult> MarkCompleteTransaction([FromBody] TQRequestObjectForComplete tQRequestObjectForComplete)
        {
            try
            {
                var activeTQKey = RouteData.Values["activeTransactionQueueKey"].ToString().Trim();

                if (string.IsNullOrEmpty(activeTQKey) || !Regex.IsMatch(activeTQKey, @"^[0-9a-fA-F]{24}$"))
                {
                    BusinessResponse objBadRequest = new BusinessResponse() { IsSuccess = false, Message = "ActiveTransactionQueueKey is not valid.", StatusCode = 400 };
                    _logger.LogError(objBadRequest.Message);
                    return BadRequest(objBadRequest);

                }              

                int activeActorKey;
                Int32.TryParse(RouteData.Values["actorKey"].ToString(), out activeActorKey);
                if (activeActorKey <= 0)
                {
                    _logger.LogError("Please provide valid actorKey");
                    return BadRequest(new ErrorResponse("Please provide valid actorKey", (int)HttpStatusCode.BadRequest,
                       ResponsePayloadType.BusinessException));
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model is not Valid");
                    return BadRequest(new ErrorResponse(new ModelStateRequestValidationAdaptor(ModelState).ToString(), (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }

                BusinessResponse response = _transactionQueueBussiness.MarkCompleteTransaction(activeTQKey, activeActorKey, tQRequestObjectForComplete);
                if (response.IsSuccess == false)
                {
                    _logger.LogError(response.Message);
                    return BadRequest(new ErrorResponse(response.Message, (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
                else
                {
                    _logger.LogInformation(response.Message);
                    return Ok(response.Message);
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        #endregion
    }
}