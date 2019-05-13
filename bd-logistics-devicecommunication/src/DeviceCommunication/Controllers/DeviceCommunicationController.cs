using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;
using BD.Core.EventBus.Abstractions;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events;
using Logistics.Services.DeviceCommunication.API.Application.Strategy;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using System.Threading.Tasks;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Common;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Newtonsoft.Json;
using static Logistics.Services.DeviceCommunicationAPI.Application.Common.Constants;

namespace Logistics.Services.DeviceCommunication.API.Controllers
{
    /// <summary>
    /// Device communication controller
    /// To mock the publishing of Event for dev and testing purpose
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class DeviceCommunicationController : ControllerBase
    {
        #region Fields

        private readonly IEventBus _eventBus;
        private readonly ILogger<DeviceCommunicationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICarouselConnection _carouselConnection;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize Event bus and logger
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="logger"></param>
        public DeviceCommunicationController(IConfiguration config, IEventBus eventBus, ILogger<DeviceCommunicationController> logger, IMediator mediator, ICarouselConnection carouselConnection)
        {
            _mediator = mediator;
            _configuration = config;

            _eventBus = eventBus;
            _logger = logger;
            _carouselConnection = carouselConnection;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Event publishing method for publishing Transaction Queue data to test subscriber.
        /// For development and testing purpose
        /// Will be removed
        /// Not part of the application
        /// </summary>
        /// <param name="messageEvent"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PublishMessageEvent(ProcessTransactionQueueIntegrationEvent messageEvent)
        {
            try
            {
                _logger.LogInformation("Demo Data recieved from postman or swagger in device communication API");
                var headers = Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key, item => item.Value);

                var kafkaSettings = _configuration.GetSection("MessageBusTopics").GetChildren()
                    .Select(item => new KeyValuePair<string, string>(item.Key, item.Value))
                    .ToDictionary(x => x.Key, x => x.Value);

                _eventBus.Publish(kafkaSettings["KafkaTopic"], messageEvent, headers);
                //Handle(messageEvent);
                _logger.LogInformation("Demo Data published from device communication API");
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <param name="carouselData"></param>
        [HttpPut]
        public IActionResult ConnectionCheck(IEnumerable<CarouselData> carouselData)
        {
            _logger.LogDebug(string.Format(LoggingMessage.RequestReceived, JsonConvert.SerializeObject(carouselData)));
            try
            {
                IEnumerable<CarouselData> response = _carouselConnection.Check(carouselData);
                _logger.LogDebug(string.Format(LoggingMessage.ResponseGenerated, JsonConvert.SerializeObject(response)));
                return Ok(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }


        #endregion

        #region Fields

        private readonly IMediator _mediator;

        #endregion

        /// <summary>
        /// Subscribing event from publisher.
        /// </summary>
        /// <param name="event">This event comming from Transaction Queue</param>
        /// <returns></returns>
        private void Handle(ProcessTransactionQueueIntegrationEvent @event)
        {
            try
            {
                _mediator.Execute(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}

