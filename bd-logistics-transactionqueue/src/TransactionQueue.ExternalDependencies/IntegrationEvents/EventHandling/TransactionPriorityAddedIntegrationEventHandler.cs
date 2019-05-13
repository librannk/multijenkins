using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.Common.Constants;
using TransactionQueue.ExternalDependencies.IntegrationEvents.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.EventHandling
{
    /// <summary> Eventhandler to store transaction priority </summary>
    public class TransactionPriorityAddedIntegrationEventHandler : IEventHandler<TransactionPriorityAddedIntegrationEvent>
    {
        #region Private Fields
        private readonly ILogger<TransactionPriorityAddedIntegrationEventHandler> _logger;
        private readonly ITransactionPriorityManager _manager;
        #endregion

        #region Constructors
        /// <summary> Initializes an instace </summary>
        public TransactionPriorityAddedIntegrationEventHandler()
        {
        }

        /// <summary>
        /// Initializes instances - ITransactionPriorityManager and logger
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="manager"></param>
        public TransactionPriorityAddedIntegrationEventHandler(ILogger<TransactionPriorityAddedIntegrationEventHandler> logger, ITransactionPriorityManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus </summary>
        /// <param name="event"> data received from event bus</param>
        /// <returns></returns>
        public async Task Handle(TransactionPriorityAddedIntegrationEvent @event)
        {
            try
            {
                if (@event != null && @event.FacilityId > 0 && !string.IsNullOrEmpty(@event.PriorityCode))
                {
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.TransactionPriorityDataReceivedFromFacility, JsonConvert.SerializeObject(@event)));
                    var model = new ExternalDependencies.BusinessLayer.Models.TransactionPriority
                    {
                        TransactionPriorityId = @event.TransactionPriorityId,
                        FacilityId = @event.FacilityId,
                        TransactionPriorityCode = @event.PriorityCode,
                        IsActive = @event.IsActive,
                        IsAdu = @event.IsAdu,
                        UseInterfaceItemName = @event.UseInterfaceName
                    };
                    await _manager.ProcessTransactionPriorityRequest(model);
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
