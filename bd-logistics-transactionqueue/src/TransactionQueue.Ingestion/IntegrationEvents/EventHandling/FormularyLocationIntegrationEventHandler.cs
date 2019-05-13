using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Abstraction;
using TransactionQueue.Ingestion.Common.Constants;
using TransactionQueue.Ingestion.IntegrationEvents.Events;

namespace TransactionQueue.Ingestion.IntegrationEvents.EventHandling
{
    /// <summary> Event handler to update storage location</summary>
    public class FormularyLocationIntegrationEventHandler : IEventHandler<FormularyLocationIntegrationEvent>
    {
        #region Private Fields
        private ITransactionManager _transactionQueueManager;
        private readonly ILogger _logger;
        #endregion

        #region Constructors
        /// <summary> Initializes an instance </summary>
        public FormularyLocationIntegrationEventHandler()
        {
        }

        /// <summary>
        /// Initializes an instances - transactionQueueManager and logger
        /// </summary>
        /// <param name="formularyLocationManager">Contains formularyLocationManager logics</param>
        /// <param name="logger"></param>
        public FormularyLocationIntegrationEventHandler(ITransactionManager transactionQueueManager,
            ILogger<FormularyLocationIntegrationEventHandler> logger)
        {
            _transactionQueueManager = transactionQueueManager;
            _logger = logger;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus</summary>
        /// <param name="event"> data received from Event-Bus </param>
        /// <returns></returns>
        public async Task Handle(FormularyLocationIntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation(String.Format(CommonConstants.LoggingMessage.DataPublishedForFormularyLocation, JsonConvert.SerializeObject(@event)));
                await _transactionQueueManager.UpdateTransactionWithStorageDetails(@event.TransactionQueueId, @event.Devices);
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
