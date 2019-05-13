using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using BD.Core.EventBus.Abstractions;
using TransactionQueue.Ingestion.Common.Constants;
using TransactionQueue.Ingestion.IntegrationEvents.Events;

namespace TransactionQueue.Ingestion.IntegrationEvents.EventHandling
{
    /// <summary> Event handler to pass request to ITransactionQueueManager to process incoming request. </summary>
    public class TransactionQueueAddedIntegrationEventHandler : IEventHandler<TransactionQueueAddedIntegrationEvent>
    {
        #region Private Fields
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionQueueAddedIntegrationEventHandler> _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize the private fields
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public TransactionQueueAddedIntegrationEventHandler(IMediator mediator,
            ILogger<TransactionQueueAddedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus </summary>
        /// <param name="event"> data received from Event-Bus </param>
        /// <returns></returns>
        public async Task Handle(TransactionQueueAddedIntegrationEvent @event)
        {
            try
            {
                if (@event != null && @event.Message != null)
                {
                    _logger.LogInformation(String.Format(CommonConstants.LoggingMessage.DataReceivedFromCCEProxy, JsonConvert.SerializeObject(@event)));
                    await _mediator.Execute(@event);
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
