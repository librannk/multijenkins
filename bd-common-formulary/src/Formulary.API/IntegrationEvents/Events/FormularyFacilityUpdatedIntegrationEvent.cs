using BD.Core.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.IntegrationEvents.Events
{
    public class FormularyFacilityUpdatedIntegrationEvent : Event
    {               
        /// <summary>
        /// FormularyId
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Approved
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

    }
}
