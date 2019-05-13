using BD.Core.EventBus.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.Events
{
    /// <summary> TransactionPriorityAddedIntegrationEvent </summary>
    public class TransactionPriorityAddedIntegrationEvent : Event
    {
        #region Auto-Properties
        /// <summary> TransactionPriorityId </summary>
        public int TransactionPriorityId { get; set; }
        /// <summary> FacilityId </summary>
        public int FacilityId { get; set; }
        /// <summary> IsActive </summary>
        public bool IsActive { get; set; }
        /// <summary> UseInterfaceName </summary>
        public bool UseInterfaceName { get; set; }
        /// <summary> IsAdu </summary>
        public bool IsAdu { get; set; }
        /// <summary> PriorityCode </summary>
        public string PriorityCode { get; set; }
        #endregion
    }
}
