using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using static CCEProxy.API.Common.Constants.Constants;
using CCEProxy.API.BusinessLayer.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CCEProxy.API.Entity;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CCEProxy.API.Controllers
{
    /// <summary>
    /// Handling Incoming Request into ProcessRequestController
    /// </summary>
    [Route("api/v{version:apiVersion}/cceproxy")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class IncomingRequestController : ControllerBase
    {
        #region properties declaration
        private readonly IRequestManager _requestManager;
        private readonly ILogger<IncomingRequestController> _logger;

        #endregion

        #region Constructor
        /// <param name="logger"></param>
        /// <param name="requestManager"></param>
        public IncomingRequestController(IRequestManager requestManager,
            ILogger<IncomingRequestController> logger)
        {
            _requestManager = requestManager;
            _logger = logger;

        }
        #endregion

        #region Public Action Methods
        /// <summary>
        /// HttpPost method processing the incoming request
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("incomingrequest")]
        [SwaggerResponse(202, "Accepted")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "UnAuthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> ProcessIncomingRequest(IncomingRequest incomingRequest)
        {
            try
            {
                _logger.LogInformation(string.Format(LoggingMessage.RequestReceived, JsonConvert.SerializeObject(incomingRequest)));

                if (ModelState.IsValid)
                {
                    _logger.LogDebug(LoggingMessage.ModelStateValid);

                    incomingRequest.Status = IncomingRequestStatus.Received;

                    string incomingRequestId =  await _requestManager.InsertIncomingRequest(incomingRequest);

                    var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);

                    var response =  await _requestManager.ProcessIncomingRequest(incomingRequest, incomingRequestId, headers);

                    if(string.IsNullOrEmpty(response))
                    {
                        return Accepted();
                    }
                    return BadRequest(response);
                }
                else
                {
                    _logger.LogDebug(LoggingMessage.ModelStateInvalid);

                    incomingRequest.Status = IncomingRequestStatus.Rejected;

                    if (string.IsNullOrEmpty(incomingRequest.Priority) ||
                        string.IsNullOrEmpty(incomingRequest.Facility.FacilityCode)|| 
                        incomingRequest.Items == null)
                    {
                        incomingRequest.StatusMessage = string.Join("; ", ModelState.Values
                                              .SelectMany(x => x.Errors)
                                              .Select(x => x.ErrorMessage));
                    }

                    string incomingRequestId = await _requestManager.InsertIncomingRequest(incomingRequest);

                    _logger.LogInformation($"{incomingRequestId}"+ IncomingRequestStatus.Rejected);

                    return BadRequest(incomingRequest.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}
