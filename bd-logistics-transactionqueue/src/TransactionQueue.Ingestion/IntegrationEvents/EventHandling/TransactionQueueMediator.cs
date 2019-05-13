using BD.Core.EventBus.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Abstraction;
using TransactionQueue.Ingestion.Common.Constants;
using TransactionQueue.Ingestion.IntegrationEvents.Events;

namespace TransactionQueue.Ingestion.IntegrationEvents.EventHandling
{
    /// <summary>
    /// Event handler to pass request to ITransactionQueueManager to process incoming request.
    /// </summary>
    public class TransactionQueueMediator : IMediator
    {
        #region Fields
        private ITransactionManager _transactionQueueManager;
        private readonly ILogger<TransactionQueueMediator> _logger;
        #endregion

        #region Constructors
        /// <summary> Initialize the private fields  </summary>
        public TransactionQueueMediator(ITransactionManager transactionQueueManager,
            ILogger<TransactionQueueMediator> logger)
        {
            _transactionQueueManager = transactionQueueManager;
            _logger = logger;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Execute the ProcessTransactionRequest method of TransactionQueueManager 
        /// </summary>
        /// <param name="event"> data received from Event-Bus</param>
        public async Task Execute(Event @event)
        {
            try
            {
                var incomingRequestModel = ((TransactionQueueAddedIntegrationEvent)@event).Message;

                if (incomingRequestModel != null)
                {
                    await _transactionQueueManager.ProcessTransactionRequest(incomingRequestModel, @event.Headers);
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

        #endregion Public Methods
    }
}
