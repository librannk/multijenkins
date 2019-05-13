using BD.Core.EventBus.Events;
using TransactionQueue.API.Application.Entities;

namespace TransactionQueue.API.IntegrationEvents.Events
{
    /// <summary>
    /// This class is used for publishing the data to the device communication service
    /// </summary>
    public class ProcessTransactionQueueIntegrationEvent : Event
    {
        #region Auto-Properties
        /// <summary> TransactionsQueueData </summary>
        public TransactionData TransactionData { get; set; }
        #endregion
    }
}
