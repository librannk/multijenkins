using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.RequestReponceModel
{
    [ExcludeFromCodeCoverage]
    public class RoutingRuleRequest : RoutingRuleBaseEntity
    {

        [Required]
        public string RoutingRuleName { get; set; }
        public Int16 SearchCriteriaGranularityLevel { get; set; }

        public List<RequestRoutingRuleSchedule> RoutingRuleSchedules { get; set; }
        public List<RequestRoutingRuleDestination> RoutingRuleDestinations { get; set; }
        public List<RequestRoutingRuleTranPriority> RoutingRuleTranPriority { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class RequestRoutingRuleSchedule
    {
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage= "Please enter a valid GUID")]        
        public Guid ScheduleKey { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class RequestRoutingRuleDestination
    {
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid DestinationKey { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class RequestRoutingRuleTranPriority
    {
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid TranPriorityKey { get; set; }
    }
}
