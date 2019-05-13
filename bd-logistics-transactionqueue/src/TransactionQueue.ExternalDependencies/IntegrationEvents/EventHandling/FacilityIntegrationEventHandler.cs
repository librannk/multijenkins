using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.Common.Constants;
using TransactionQueue.ExternalDependencies.IntegrationEvents.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.EventHandling
{
    /// <summary> Eventhandler to store facility </summary>
    public class FacilityIntegrationEventHandler : IEventHandler<FacilityAddedIntegrationEvent>
    {
        #region Private Fields
        private readonly ILogger<FacilityIntegrationEventHandler> _logger;
        private readonly BusinessLayer.Abstraction.IFacilityManager _facilityManager;
        #endregion

        #region Constructors
        /// <summary> Initializes an instace </summary>
        public FacilityIntegrationEventHandler()
        {
        }

        /// <summary>
        /// Initializes instances - IFacilityManager and logger
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="facilityManager"></param>
        public FacilityIntegrationEventHandler(ILogger<FacilityIntegrationEventHandler> logger, BusinessLayer.Abstraction.IFacilityManager facilityManager)
        {
            _logger = logger;
            _facilityManager = facilityManager;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus </summary>
        /// <param name="event"> data received from event bus</param>
        /// <returns></returns>
        public async Task Handle(FacilityAddedIntegrationEvent @event)
        {
            try
            {
                if (@event != null)
                {
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.DataReceivedFromFacility, JsonConvert.SerializeObject(@event)));
                    var facilityRequestModel = new BusinessLayer.Models.Facility
                    {
                        Id = @event.FacilityId,
                        ProcessInactiveAsException = @event.ProcessInactiveAsException,
                        AduIgnoreCritLow = @event.AduIgnoreCritLow,
                        AduIgnoreStockout = @event.AduIgnoreStockout
                    };
                    await _facilityManager.ProcessFacilityRequest(facilityRequestModel);
                }
                else
                {
                    _logger.LogError(CommonConstants.LoggingMessage.InvalidRequest);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(CommonConstants.LoggingMessage.ErrorWhileProcessingRequest, ex.Message));
            }
        }
        #endregion
    }
}
