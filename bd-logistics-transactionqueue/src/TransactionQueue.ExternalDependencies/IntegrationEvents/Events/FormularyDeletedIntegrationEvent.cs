using BD.Core.EventBus.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.Events
{
    /// <summary>FormularyDeletedIntegrationEvent </summary>
    public class FormularyDeletedIntegrationEvent : Event
    {
        #region Properties

        /// <summary>
        /// Formulary Identifier
        /// </summary>
        public int FormularyId { get; set; }

        #endregion
    }
}