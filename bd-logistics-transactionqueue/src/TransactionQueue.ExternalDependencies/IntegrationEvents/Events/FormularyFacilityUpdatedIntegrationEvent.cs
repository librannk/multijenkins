using BD.Core.EventBus.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.Events
{
    /// <summary> It contains Formulary Facility information of the transaction </summary>
    public class FormularyFacilityUpdatedIntegrationEvent : Event
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for FormularyId
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// To store the value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To store the value for Approved
        /// </summary>
        public bool Approved { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreCritLow
        /// </summary>
        public bool? AduIgnoreCritLow { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreStockout
        /// </summary>
        public bool? AduIgnoreStockout { get; set; }
        #endregion
    }
}
