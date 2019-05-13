using BD.Core.EventBus.Events;
using SiteConfiguration.API.TransactionPriority.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.IntegrationEvents.Events
{
    [ExcludeFromCodeCoverage]
    public class TransactionPrioritySmartSortEvent : Event
    {
        public List<SmartSort> Message { get; set; }

        public string EventType { get; set; }
    }
}
