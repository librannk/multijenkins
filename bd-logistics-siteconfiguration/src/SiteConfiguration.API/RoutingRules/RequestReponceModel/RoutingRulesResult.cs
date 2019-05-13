using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.RequestReponceModel
{
    [ExcludeFromCodeCoverage]
    public class RoutingRulesResult
    {
        public string RoutingRuleName { get; set; }

        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$")]
        [Required(ErrorMessage = "Please enter a valid GUID")]
        public Guid RoutingRuleKey { get; set; }

        public string Schedules { get; set; }
        public string TranPriorities { get; set; }
        public string Destinations { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class RoutingRulesById
    {        
        public Guid RoutingRuleKey { get; set; }       
        public string RoutingRuleName { get; set; }
        public short SearchCriteriaGranularityLevel { get; set; }
        public  List<Guid> RoutingRuleDestinations { get; set; }
        public List<Guid>  RoutingRuleScheduleTimings { get; set; }
        public List<Guid> RoutingRuleTranPriorities { get; set; }
    }
}
