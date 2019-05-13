using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BD.Core.BaseModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.Business;
using SiteConfiguration.API.TransactionPriority.Models;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiteConfiguration.API.TransactionPriority.Controllers
{

    [Route("api/v{version:apiVersion}/siteconfiguration/facilities/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SmartSortController : Controller
    {

        private readonly ISmartSortManager _manager;
        private readonly ILogger<SmartSortController> _logger;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingRulesRepository"></param>
        public SmartSortController(ILogger<SmartSortController> logger, ISmartSortManager manager)
        {
            _logger = logger;
            _manager = manager;
            
        }


        [HttpGet("smartsorts")]
        public async Task<ActionResult<List<RequestResponseModel.SmartSortResponse>>> Get(bool isActive)
        {
            try
            {
                List<RequestResponseModel.SmartSortResponse> listSmartSort = await _manager.GetAllSmartSort(isActive);
                if(listSmartSort==null)
                {
                    _logger.LogInformation("No Smart Sort Found");
                    return BadRequest(new ErrorResponse("No Smart Sort Found", (int)HttpStatusCode.BadRequest, ResponsePayloadType.BusinessException));
                }
                return listSmartSort;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
           
        }

     
    }
}
