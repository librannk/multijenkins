using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BD.Core.BaseModels;
using SiteConfiguration.API.RoutingRules.Abstractions;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using SiteConfiguration.API.Common;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace SiteConfiguration.API.RoutingRules.Controllers
{
    [Route("api/v{version:apiVersion}/siteconfiguration/facilities/{facilitykey:guid}/")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class RoutingRuleController : ControllerBase
    {
        private readonly IRoutingRuleManager _manager;
        private readonly ILogger<RoutingRuleController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingRulesRepository"></param>
        public RoutingRuleController(ILogger<RoutingRuleController> logger, IRoutingRuleManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        /// <summary>
        /// HttpGET method processingg the incoming request
        /// </summary>
        /// <returns></returns>
        [HttpGet("routingrules")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<IActionResult> GetRoutingRules(int page = 0, int pageSize = 0, string searchString = "")
        {
            try
            {               
                var result = _manager.GetAllRoutingRule(page, pageSize, searchString);
                if (result.Any())
                {
                    _logger.LogInformation("Get All Routing Rule Successfully");
                    return Ok(result);
                }
                _logger.LogInformation("No Record Found");
                return BadRequest(new ErrorResponse("No Record Found", (int)HttpStatusCode.BadRequest,ResponsePayloadType.BusinessException));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// to get routing rule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("routingrule/{rountingRuleKey:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<IActionResult> GetRoutingRule()
        {
            try
            {
                var ruleId = RouteData.Values["rountingRuleKey"].ToString();
                List<string> str = new List<string>() { ruleId };
                bool res = Utility.ValidateGUID(str);
                if (res == false)
                {
                    _logger.LogInformation("Please provide valid RoutingRuleKey.");
                    return BadRequest(new ErrorResponse("Please provide valid Guid", (int)HttpStatusCode.BadRequest,ResponsePayloadType.BusinessException));
                }
                
                var result = _manager.GetByID(Utility.ParseStringToGuid(ruleId));
                if (result == null)
                {
                    _logger.LogInformation("Routing Rule with RountingRuleKey :" + ruleId + " not found.");
                    return BadRequest(new ErrorResponse("Routing Rule with RountingRuleKey :" + ruleId + " not found.", (int)HttpStatusCode.BadRequest,ResponsePayloadType.BusinessException));
                }
                _logger.LogInformation("Routing Rule from routingRuleKey "+ ruleId+" Get Successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// to add a new routing rule in database
        /// </summary>
        /// <param name="routingRule"></param>
        /// <returns></returns>
        [HttpPost("routingrule")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<IActionResult> Post([FromBody] RoutingRuleRequest routingRule)
        {
            try
            {  
                if (ModelState.IsValid)
                {
                    if (routingRule.RoutingRuleDestinations.Count == 0 &&
                       routingRule.RoutingRuleTranPriority.Count == 0 &&
                       routingRule.RoutingRuleSchedules.Count == 0)
                    {
                        _logger.LogInformation("Any one of them is mandatory, Destination,Shedule,Transaction Priority");
                        return BadRequest(new ErrorResponse("Any one of them is mandatory, Destination,Shedule,Transaction Priority", (int)HttpStatusCode.BadRequest,ResponsePayloadType.BusinessException));
                    }

                    var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                    //to test routing rule name already exist in db or not..
                    BusinessResponse response = _manager.AddRoutingRule(routingRule, headers);

                    if (response.IsSuccess == false)
                    {
                        _logger.LogInformation(response.Message);
                        return BadRequest(new ErrorResponse(response.Message, (int)HttpStatusCode.BadRequest,ResponsePayloadType.BusinessException));
                    }
                    else
                    {
                        return CreatedAtAction(nameof(GetRoutingRule), new { rountingRuleKey = response.Id }, routingRule);
                    }
                }
                else
                {
                    _logger.LogInformation("Model is not valid");
                    return BadRequest(new ErrorResponse(new ModelStateRequestValidationAdaptor(ModelState).ToString(), (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// this method will update an exsiting rule in database..
        /// </summary>
        /// <param name="id"></param>
        /// <param name="routingRule"></param>
        /// <returns></returns>
        [HttpPut("routingrule/{rountingRuleKey:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<IActionResult> Put([FromBody] RoutingRuleRequest routingRule)
        {
            try
            {                
                var ruleId = RouteData.Values["rountingRuleKey"].ToString();

                List<string> str = new List<string>() { ruleId };
                bool res = Utility.ValidateGUID(str);
                if (res == false)
                {
                    _logger.LogInformation("Please provide valid RoutingRuleKey");
                    return BadRequest(new ErrorResponse("Please provide valid RoutingRuleKey", (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }

                if (ModelState.IsValid)
                {
                    if (routingRule.RoutingRuleDestinations.Count == 0 &&
                       routingRule.RoutingRuleTranPriority.Count == 0 &&
                       routingRule.RoutingRuleSchedules.Count == 0)
                    {
                        _logger.LogInformation("Any one of them is mandatory, Destination,Shedule,Transaction Priority");
                        return BadRequest(new ErrorResponse("Any one of them is mandatory, Destination,Shedule,Transaction Priority", (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                    }
                    
                    var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                    BusinessResponse response = _manager.UpdateRoutingRule(routingRule,Utility.ParseStringToGuid(ruleId), headers);
                    if (response.IsSuccess == false)
                    {
                        _logger.LogInformation(response.Message);
                        return BadRequest(new ErrorResponse(response.Message, (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                    }
                    else
                    {
                        _logger.LogInformation(response.Message);
                        return Ok(response.Message);
                    }
                }
                else
                {
                    _logger.LogInformation("Model is not Valid");
                    return BadRequest(new ErrorResponse(new ModelStateRequestValidationAdaptor(ModelState).ToString(), (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// to delete routing rule from database..
        /// </summary>        
        /// <returns></returns>
        [HttpDelete("routingrule/{rountingRuleKey:guid}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal error")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                string ruleId = RouteData.Values["rountingRuleKey"].ToString();

                List<string> str = new List<string>() { ruleId };
                bool res =  Utility.ValidateGUID(str);
                if(res == false)
                {
                    _logger.LogInformation("Please provide valid RoutingRuleKey");
                    return BadRequest(new ErrorResponse("Please provide valid RoutingRuleKey", (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                BusinessResponse response = _manager.DeleteRoutingRule(Utility.ParseStringToGuid(ruleId), headers);
                if (response.IsSuccess == false)
                {

                    _logger.LogInformation(response.Message);
                    return BadRequest(new ErrorResponse(response.Message, (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
                _logger.LogInformation(response.Message);
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}