using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteConfiguration.API.TransactionPriority.Business;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;
using SiteConfiguration.API.TransactionPriority.Models;
using System.Text;
using FluentValidation;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.Common;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using BD.Core.BaseModels;
using System.Net;

namespace SiteConfiguration.API.TransactionPriority.Controllers
{

    [Route("api/v{version:apiVersion}/siteconfiguration/facilities/{facilitykey}/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TransactionPriorityController : ControllerBase
    {
        #region PrivateFields
        private readonly ITransactionPriorityManager _manager;
        private readonly ILogger<TransactionPriorityController> _logger;
        private readonly IValidator<TransactionPriorityPost> _entityValidator;
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingRulesRepository"></param>
        public TransactionPriorityController(ILogger<TransactionPriorityController> logger, ITransactionPriorityManager manager, IValidator<TransactionPriorityPost> entityValidator)
        {
            _logger = logger;
            _manager = manager;
            _entityValidator = entityValidator;
        }
        #endregion

        #region ActionMethods

        #region TransactionPriority_Create
        /// <summary>
        /// To Post Transaction Priority
        /// </summary>
        /// <param name="transactionPriorityPost">TransactionPriorityPost Object</param>
        /// <returns></returns>
        [HttpPost("transactionpriority")]
        public async Task<ActionResult<TransactionPriorityPost>> PostTransactionPriority([FromBody]TransactionPriorityPost transactionPriorityPost)
        {
            Models.TransactionPriority objTransactionPriority = new Models.TransactionPriority();
            try
            {
                var validationResult = _entityValidator.Validate(transactionPriorityPost);
                if (!validationResult.IsValid)
                {
                    StringBuilder validationMessages = new StringBuilder();
                    foreach (var error in validationResult.Errors)
                    {
                        validationMessages.Append(error + " ");
                    }
                    ErrorMessage objErrorMessage = new ErrorMessage() { ErrorCode = 400, ErrorDescription = validationMessages.ToString() };
                    _logger.LogInformation(validationMessages.ToString());
                    return BadRequest(new ErrorResponse(validationMessages.ToString(), (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                var facilityKey = RouteData.Values["facilitykey"].ToString();
                BusinessResponse objBusinessResponse = await _manager.AddTransactionPriority(transactionPriorityPost, Utility.ParseStringToGuid(facilityKey), headers);
                if (objBusinessResponse.IsSuccesss)
                {
                    return CreatedAtAction(nameof(GetTransactionPriority), new { tranPriorityKey = objBusinessResponse.Message }, transactionPriorityPost);
                }
                else
                {
                    _logger.LogInformation(objBusinessResponse.Message);
                    return BadRequest(new ErrorResponse(objBusinessResponse.Message, (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);

            }

        }

        #endregion

        #region TransactionPriority_GetById
        /// <summary>
        /// Get TransactionPriority by TransactionPriorityId
        /// </summary>
        /// <param name="tranPriorityKey">tranPriorityKey</param>
        /// <returns></returns>
        [HttpGet("transactionpriority/{tranPriorityKey:guid}")]
        public async Task<ActionResult<Models.TransactionPriority>> GetTransactionPriority(string tranPriorityKey)
        {
            try
            {
                return _manager.GetTransactionPriorityById(tranPriorityKey.ToUpper());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }


        }


        #endregion

        #region TransactionPriority_Update

        /// <summary>
        /// To Update Transaction Priority
        /// </summary>
        /// <param name="tranPriorityKey"></param>
        /// <param name="transactionPriorityPut">TransactionPriorityPut Object</param>
        /// <returns></returns>
        [HttpPut("transactionpriority/{tranPriorityKey:guid}")]
        public async Task<IActionResult> PutTransactionPriority(string tranPriorityKey, TransactionPriorityPut transactionPriorityPut)
        {
            Models.TransactionPriority objTransactionPriority = new Models.TransactionPriority();
            try
            {

                var facilityKey = RouteData.Values["facilitykey"].ToString();
                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                BusinessResponse objBusinessResponse = await _manager.UpdateTransactionPriorityAsync(tranPriorityKey, transactionPriorityPut, Utility.ParseStringToGuid(facilityKey),headers);
                if (objBusinessResponse.IsSuccesss)
                {
                    _logger.LogInformation(objBusinessResponse.Message);
                   return Ok(objBusinessResponse.Message);
                }
                else
                {
                    _logger.LogInformation(objBusinessResponse.Message);
                    return BadRequest(new ErrorResponse(objBusinessResponse.Message, (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
           
        }

        #endregion

        #region TransactionPriority_Get
        /// <summary>
        /// Get Active In-Active Transaction Priorities
        /// </summary>
        /// <param name="offset">Leave the offset Count</param>
        /// <param name="limit">Expected Transaction Priorities</param>
        /// <param name="isActive">Active and In-Active Transaction Priority.</param>
        /// <returns></returns>
        [HttpGet("transactionpriorities")]
        public async Task<ActionResult<IEnumerable<TransactionPriorityGet>>> GetTransactionPriority(int offset = 0, int limit = 0, bool isActive = true)
        {

            try
            {
                var facilityKey = RouteData.Values["facilitykey"].ToString();
               

                IEnumerable<TransactionPriorityGet> listTransactionPriorityGet = await _manager.GetAllTransactionPriorityASync(offset, limit, isActive, facilityKey);
                if (listTransactionPriorityGet == null || listTransactionPriorityGet?.ToList().Count==0)
                {
                    _logger.LogInformation("No TransactionPriority Found");
                    return NotFound(new ErrorResponse("No TransactionPriority Found", (int)HttpStatusCode.NotFound, ResponsePayloadType.BusinessException));
                }

                return listTransactionPriorityGet.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        #endregion


        #region TransactionPriority_GetSearch

        /// <summary>
        /// Get Searched Transaction Priority 
        /// </summary>
        /// <param name="priorityName">Transaction Priority description(name)</param>
        /// <param name="offset">Leave the offset Count</param>
        /// <param name="limit">Expected Transaction Priorities</param>
        /// <returns></returns>
        [HttpGet("transactionpriority/{priorityName}/Search")]
        public async Task<ActionResult<IEnumerable<TransactionPriorityGet>>> GetTransactionPrioritySearch(string priorityName, int offset = 0, int limit = 0)
        {

            try
            {
                var facilityKey = RouteData.Values["facilitykey"].ToString();
                IEnumerable<TransactionPriorityGet> listTransactionPriorityGet = await _manager.GetAllSerachedTransactionPriorityAsync(priorityName, offset, limit, facilityKey);
                if (listTransactionPriorityGet == null || listTransactionPriorityGet?.ToList().Count == 0)
                {
                    
                    _logger.LogInformation("No TransactionPriority Found");
                    return NotFound(new ErrorResponse("No TransactionPriority Found", (int)HttpStatusCode.NotFound, ResponsePayloadType.BusinessException));
                }

                return listTransactionPriorityGet.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        #endregion


        #region TransactionPrioritySmartSort_Get
        /// <summary>
        /// Get Smart Sorts Related to Transaction Priority
        /// </summary>
        /// <param name="tranPriorityKey">Transaction Priority Key</param>
        /// <returns></returns>
        [HttpGet("transactionpriority/{tranPriorityKey:guid}/SmartSort")]
        public async Task<ActionResult<IEnumerable<TransactionPrioritySmartSort>>> GetTransactionPrioritySmartSorts(string tranPriorityKey)
        {

            try
            {
                var facilityKey = RouteData.Values["facilitykey"].ToString();
                var listTransactionPrioritySmartSort = await _manager.GetSmartSortForTransactionPriority(tranPriorityKey, facilityKey);
                if (listTransactionPrioritySmartSort == null)
                {
                   
                    _logger.LogInformation("No TransactionPriority Found");
                    return NotFound(new ErrorResponse("No TransactionPriority Found", (int)HttpStatusCode.NotFound, ResponsePayloadType.BusinessException));
                }

                return listTransactionPrioritySmartSort.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        #endregion


        #region TransactionPrioritySmartSort_Put
        /// <summary>
        /// Update Transaction Priority with respect to transaction Priority Id 
        /// </summary>
        /// <param name="tranPriorityKey">Transaction Priority key</param>
        /// <param name="listTransactionPrioritySmartSortPut">Transaction Priority SmartSorts to Update</param>
        /// <returns></returns>
        [HttpPut("transactionpriority/{tranPriorityKey:guid}/SmartSort")]
        public async Task<ActionResult> UpdateTransactionPrioritySmartSorts(string tranPriorityKey, List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut)
        {

            try
            {
                var facilityKey = RouteData.Values["facilitykey"].ToString();
                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                BusinessResponse objBusinessResponse  = await _manager.PutSmartSortForTransactionPriorityAsync(tranPriorityKey, facilityKey,listTransactionPrioritySmartSortPut,headers);
                if (objBusinessResponse.IsSuccesss)
                {
                    _logger.LogInformation(objBusinessResponse.Message);
                    return Ok(objBusinessResponse.Message);
                }
                _logger.LogInformation(objBusinessResponse.Message);
                return NotFound(new ErrorResponse(objBusinessResponse.Message, (int)HttpStatusCode.NotFound, ResponsePayloadType.BusinessException));

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        #endregion

        #region TransactionPrioritySmartSort_Post

        /// <summary>
        /// Insert new SmartSort to Transaction Priority
        /// </summary>
        /// <param name="tranPriorityKey">Transaction PriorityId</param>
        /// <param name="listTransactionPrioritySmartSortPost">List of SmartSort to Post</param>
        /// <returns></returns>
        [HttpPost("transactionpriority/{tranPriorityKey:guid}/SmartSort")]
        public async Task<ActionResult> InsertTransactionPrioritySmartSorts(string tranPriorityKey, List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPost)
        {

            try
            {
                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                var facilityKey = RouteData.Values["facilitykey"].ToString();
                BusinessResponse objBusinessResponse = await _manager.PostSmartSortForTransactionPriorityAsync(tranPriorityKey, facilityKey, listTransactionPrioritySmartSortPost, headers);
                if(objBusinessResponse.IsSuccesss)
                {
                    _logger.LogInformation(objBusinessResponse.Message);
                    return Ok(objBusinessResponse.Message);
                }
                _logger.LogInformation(objBusinessResponse.Message);
                return NotFound(new ErrorResponse(objBusinessResponse.Message, (int)HttpStatusCode.NotFound, ResponsePayloadType.BusinessException));
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        #endregion

        #endregion

    }


}