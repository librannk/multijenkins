using BD.Core.EventBus.Abstractions;
using BD.Core.ResiliencePolicy;
using BD.Template.API.Infrastructure.DBModel;
using BD.Template.API.Infrastructure.Repository.Interfaces;
using BD.Template.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.Template.API.Infrastructure.Repository.Interfaces;

namespace BD.Template.API.Controllers
{
    /// <summary>
    /// Creating a controller for Template
    /// </summary>
    [Route("/api/v1/template/[controller]")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class TemplateController : ControllerBase
    {        
        private readonly IEventBus _eventBus;
        private readonly HttpClientFactory _identityService;
        private readonly IKafkaResponseRepository _kafkaResponseRepository;
        private readonly ILogger<TemplateController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="identityService"></param>
        /// <param name="kafkaResponseRepository"></param>
        /// <param name="logger"></param>
        public TemplateController(IEventBus eventBus,  HttpClientFactory identityService, IKafkaResponseRepository kafkaResponseRepository,
            ILogger<TemplateController> logger)     
        {            
            _eventBus = eventBus;
            _identityService = identityService;
            _kafkaResponseRepository = kafkaResponseRepository;
            _logger = logger;
        }
        
        /// <summary>
        /// Sample Method for the publishing messages to Event Bus
        /// </summary>
        /// <param name="value">Any String message</param>
        [HttpPost("publishevent")]
        public void Post([FromBody] string value)
        {
            try
            {
                var eventMessage = new TemplateAddedIntegrationEvent()
                {
                    Message = value,
                    Names = new List<string> { "BDName1", "BDName2" },
                    Quantity = 1,
                    PrePosition = false,
                    TranQType = "TQ",
                    ConnectionResetMinutes = null

                };

                //Before publish, saving the response into DB
                if (ModelState.IsValid)
                {
                    var kafkaResponse = new KafkaResponse
                    {
                        ResponseType = "Publisher",
                        EventMessage = eventMessage.Message,
                        Names = eventMessage.Names.ToArray(),
                        Quantity = eventMessage.Quantity,
                        TranQType = eventMessage.TranQType,
                        ConnectionResetMinutes = eventMessage.ConnectionResetMinutes,
                        CreationDate = DateTime.Now,
                        Topic = "bddev"
                    };
                    _kafkaResponseRepository.InsertAsync(kafkaResponse);
                }

                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);
                _eventBus.Publish("bddev", eventMessage, headers);
                //Before publish, saving the response into DB
                if (ModelState.IsValid)
                {
                    var kafkaResponse = new KafkaResponse
                    {
                        ResponseType = "Publisher After",
                        EventMessage = eventMessage.Message,
                        Names = eventMessage.Names.ToArray(),
                        Quantity = eventMessage.Quantity,
                        TranQType = eventMessage.TranQType,
                        ConnectionResetMinutes = eventMessage.ConnectionResetMinutes,
                        CreationDate = DateTime.Now,
                        Topic = "bddev"
                    };
                    _kafkaResponseRepository.InsertAsync(kafkaResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("polly")]
        public async Task<ActionResult> GetPolly()
        {
            var result = await _identityService.Client.GetStringAsync("/");
            return Ok(result);
        }
    }
}