using BD.Core.EventBus.Events;

namespace TransactionQueue.Ingestion.IntegrationEvents.Events
{
    /// <summary> FormularyLocationRequestEvent </summary>
    public class FormularyLocationRequestEvent : Event
    {
        #region Auto-Properties
        /// <summary> TransactionQueueId </summary>
        public string TransactionQueueId { get; set; }
        /// <summary> FormularyId </summary>
        public int FormularyId { get; set; }
        /// <summary> FacilityId </summary>
        public int FacilityId { get; set; }
        /// <summary> ISAId </summary>
        public int? ISAId { get; set; }
        #endregion
    }
}
