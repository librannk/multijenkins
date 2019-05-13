using BD.Core.EventBus.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.Events
{
    /// <summary>FormularyUpdatedIntegrationEvent </summary>
    public class FormularyUpdatedIntegrationEvent : Event
    {
        #region Properties

        /// <summary>
        /// Formulary Identifier
        /// </summary>
        public int FormularyId { get; set; }

        /// <summary>
        /// ItemId Identifier
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Formulary IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Formulary Description
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}