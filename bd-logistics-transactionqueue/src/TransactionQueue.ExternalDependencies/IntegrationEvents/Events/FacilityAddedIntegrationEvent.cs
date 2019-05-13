using BD.Core.EventBus.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.Events
{
    /// <summary>FacilityAddedIntegrationEvent </summary>
    public class FacilityAddedIntegrationEvent : Event
    {
        #region Properties
        /// <summary> FacilityId </summary>
        public int FacilityId { get; set; }

        /// <summary> ProcessInactiveAsException </summary>
        public bool ProcessInactiveAsException { get; set; }

        /// <summary> AduIgnoreCritLow </summary>
        public bool AduIgnoreCritLow { get; set; }
        /// <summary> AduIgnoreStockout </summary>
        public bool AduIgnoreStockout { get; set; }
        #endregion
    }
}
