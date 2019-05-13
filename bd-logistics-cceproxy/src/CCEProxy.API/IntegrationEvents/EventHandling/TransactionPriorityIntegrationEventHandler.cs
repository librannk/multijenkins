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
    /// <summary> Event handler to pass request to ITransactionPriorityManager to process TransactionPriority request. </summary>
    public class TransactionPriorityIntegrationEventHandler : IEventHandler<TransactionPriorityAddedIntegrationEvent>
    {
        #region Private Fields
        private readonly ILogger _logger;
        private readonly ITransactionPriorityManager _transactionPriorityManager;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        /// <summary> Initializes an instance </summary>
        public TransactionPriorityIntegrationEventHandler()
        {
        }

        /// <summary>
        /// Initializes an instances - ITransactionPriorityManager and logger
        /// </summary>
        /// <param name="transactionPriorityManager"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public TransactionPriorityIntegrationEventHandler(ITransactionPriorityManager transactionPriorityManager,
            ILogger<TransactionPriorityIntegrationEventHandler> logger,
            IMapper mapper)
        {
            _transactionPriorityManager = transactionPriorityManager;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus</summary>
        /// <param name="event"> data received from Event-Bus </param>
        /// <returns></returns>
        public async Task Handle(TransactionPriorityAddedIntegrationEvent @event)
        {
            try
            {
                if (@event != null)
                {
                    _logger.LogInformation(string.Format(LoggingMessage.DataReceivedFromTransactionPriority, JsonConvert.SerializeObject(@event)));
                   
                    var transactionPriorityRequest = _mapper.Map<TransactionPriority>(@event);
                    await _transactionPriorityManager.ProcessTransactionPriorityRequest(transactionPriorityRequest);
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
