using BD.Core.EventBus.Events;
//using BD.Core.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.IntegrationEvents.Events
{
    public class FormularyUpdatedIntegrationEvent:Event
    {
        /// <summary>
        /// Medicine Identifier
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Formulary IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Formulary Identifier
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

    }
}
