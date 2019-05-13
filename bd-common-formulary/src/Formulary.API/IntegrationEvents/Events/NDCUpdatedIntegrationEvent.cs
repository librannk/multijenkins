using BD.Core.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.IntegrationEvents.Events
{
    /// <summary>
    /// NDCUpdatedIntegrationEvent
    /// </summary>
    public class NDCUpdatedIntegrationEvent:Event
    {
        /// <summary>
        /// NDC Identifier
        /// </summary>
        public int NDCId { get; set; }
        /// <summary>
        /// NDC Cost
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// NDC Generic Name
        /// </summary>
        public string GenericName { get; set; }
        /// <summary>
        /// NDC Trade Name
        /// </summary>
        public string TradName { get; set; }
    }
}
