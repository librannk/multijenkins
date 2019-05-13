using BD.Core.EventBus.Events;
using System.Collections.Generic;


namespace BD.Template.API.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateAddedIntegrationEvent : Event
    {
        /// <summary>
        /// property to read and write the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// property to read and write the quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// property to read and write the proposition
        /// </summary>
        public bool PrePosition { get; set; }

        /// <summary>
        /// property to read and write the TranQType
        /// </summary>
        public string TranQType { get; set; }

        /// <summary>
        /// property to read and write the ConnectionResetMinutes
        /// </summary>
        public int? ConnectionResetMinutes { get; set; }

        /// <summary>
        /// property to read and write the Names
        /// </summary>
        public List<string> Names { get; set; }

    }
}

