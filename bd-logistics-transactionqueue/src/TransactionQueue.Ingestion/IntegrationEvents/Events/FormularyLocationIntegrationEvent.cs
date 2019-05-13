using BD.Core.EventBus.Events;
using System.Collections.Generic;
using TransactionQueue.Ingestion.BusinessLayer.Models;

namespace TransactionQueue.Ingestion.IntegrationEvents.Events
{
    /// <summary> FormularyLocationIntegrationEvent </summary>
    public class FormularyLocationIntegrationEvent : Event
    {
        #region Auto-Properties
        /// <summary>TransactionQueueId</summary>
        public string TransactionQueueId { get; set; }
        /// <summary> StorageSpaces </summary>
        public List<Device> Devices { get; set; }
        #endregion
    }
}
