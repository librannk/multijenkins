using BD.Core.EventBus.Events;
using CCEProxy.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCEProxy.API.IntegrationEvents.Events
{
    /// <summary>
    ///TransactionPriorityAddedIntegrationEvent: This is Transaction Priority type event 
    ///</summary>
    public class TransactionPriorityAddedIntegrationEvent : Event
    {
        /// <summary>
        /// FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// TransactionPriorityId
        /// </summary>
        public int TransactionPriorityId { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        public string PriorityCode { get; set; }

    }
}
