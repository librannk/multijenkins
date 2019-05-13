using BD.Core.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.IntegrationEvents.Events
{
    [ExcludeFromCodeCoverage]
    public class TransactionPriorityEvent : Event
    {
        public TransactionPriority.Models.TransactionPriority Message { get; set; }

        public string EventType { get; set; }
    }
}
