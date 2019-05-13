using BD.Core.EventBus.Events;
using Logistics.Services.DeviceCommunication.API.Application.Models;

namespace Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events
{
    /// <summary>
    ///ProcessTransactionQueueIntegrationEvent: This is Transaction Queue type event comming from 
    ///Transaction Queue service.
    /// </summary>
    public class ProcessTransactionQueueIntegrationEvent: Event
    {
        /// <summary>
        /// read write TransactionData
        /// </summary>
        public TransactionData TransactionData { get; set; }

    }
}
