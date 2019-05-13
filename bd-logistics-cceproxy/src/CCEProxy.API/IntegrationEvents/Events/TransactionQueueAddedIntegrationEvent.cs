using BD.Core.EventBus.Events;
using CCEProxy.API.Entity;

namespace CCEProxy.API.IntegrationEvents.Events
{
    /// <summary>
    ///TransactionQueueAddedIntegrationEvent: This is Transaction Queue type event 
    ///to pass the Aggregator Model to Transaction Queue.
    /// </summary>
    public class TransactionQueueAddedIntegrationEvent : Event
    {
        /// <summary>
        /// Message property
        /// </summary>
        public IncomingRequest Message { get; set; }
    }
}
