using CCEProxy.API.BusinessLayer.Contracts;
using CCEProxy.API.IntegrationEvents.Events;
using static CCEProxy.API.Common.Constants.Constants;
using CCEProxy.API.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using AutoMapper;
using BD.Core.EventBus.Abstractions;

namespace CCEProxy.API.IntegrationEvents.EventHandling
{
    /// <summary> Event handler to pass request to IFacilityManager to process facility request. </summary>
    public class FacilityIntegrationEventHandler : IEventHandler<FacilityAddedIntegrationEvent>
    {
        #region Private Fields
        private readonly IFacilityManager _facilityManager;
        private readonly ILogger<FacilityIntegrationEventHandler> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes an instance 
        /// </summary>
        public FacilityIntegrationEventHandler()
        {
        }
        /// <summary>
        /// Initialize the private fields
        /// </summary>
        /// <param name="facilityManager"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public FacilityIntegrationEventHandler(IFacilityManager facilityManager,
            ILogger<FacilityIntegrationEventHandler> logger,
            IMapper mapper)
        {
            _facilityManager = facilityManager;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus</summary>
        /// <param name="event"> data received from Event-Bus </param>
        /// <returns></returns>
        public async Task Handle(FacilityAddedIntegrationEvent @event)
        {
            try
            {
                if (@event != null)
                {
                    _logger.LogInformation(String.Format(LoggingMessage.DataReceivedFromFacility, JsonConvert.SerializeObject(@event)));
                   
                    var facilityRequestModel = _mapper.Map<Facility>(@event);
                    await _facilityManager.ProcessFacilityRequest(facilityRequestModel);
                }
                else
                {
                    _logger.LogError(LoggingMessage.InvalidRequest);
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingMessage.ErrorWhileProcessingRequest, ex.Message);
            }
        }
        #endregion
    }
}
