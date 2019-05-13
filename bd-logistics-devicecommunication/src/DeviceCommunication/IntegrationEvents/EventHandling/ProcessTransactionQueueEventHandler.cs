using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BD.Core.EventBus.Abstractions;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events;

namespace Logistics.Services.DeviceCommunication.API.IntegrationEvents.EventHandling
{
    /// <summary>
    /// EventHandler to process TransactionQueue data comming from transaction Queue service
    /// </summary>
    public class ProcessTransactionQueueEventHandler :
        IEventHandler<ProcessTransactionQueueIntegrationEvent>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProcessTransactionQueueEventHandler> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor to Initialize mediator and logger
        /// </summary>
        public ProcessTransactionQueueEventHandler(IMediator mediator, ILogger<ProcessTransactionQueueEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion

        #region Event Handler Method for Topic Subscription

        /// <summary>
        /// Subscribing event from publisher.
        /// </summary>
        /// <param name="event">This event comming from Transaction Queue</param>
        /// <returns></returns>
        public async Task Handle(ProcessTransactionQueueIntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation(String.Format(Constants.ProcessTransactionQueueEventHandler.DataReceivedFromTransactionQueue, JsonConvert.SerializeObject(@event)));
                await _mediator.Execute(@event);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        #endregion
    }
}
