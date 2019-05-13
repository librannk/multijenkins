using BD.Core.EventBus.Events;
using TransactionQueue.Ingestion.BusinessLayer.Models;

namespace TransactionQueue.Ingestion.IntegrationEvents.Events
{
    /// <summary>  </summary>
    public class TransactionQueueAddedIntegrationEvent : Event
    {
        #region Auto-Properties
        /// <summary> Message </summary>
        public TransactionRequest Message { get; set; }
        #endregion
    }
}
