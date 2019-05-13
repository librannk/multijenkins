using BD.Core.EventBus.Events;
using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.IntegrationEvents.Events
{
    [ExcludeFromCodeCoverage]
    public class RoutingRuleEvent : Event
    {
        public RoutingRulesById Message { get; set; }

        public string EventType { get; set; }
    }
}
