using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.RoutingRules.Models
{
    [ExcludeFromCodeCoverage]
    public partial class RoutingRule : BaseEntity
    {
        public RoutingRule()
        {
            RoutingRuleDestination = new HashSet<RoutingRuleDestination>();
            RoutingRuleScheduleTiming = new HashSet<RoutingRuleScheduleTiming>();
            RoutingRuleTranPriority = new HashSet<RoutingRuleTranPriority>();
        }

        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid RoutingRuleKey { get; set; }

        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid TenantKey { get; set; }

        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid FacilityKey { get; set; }
        public string RoutingRuleName { get; set; }
        public short SearchCriteriaGranularityLevel { get; set; }    
        public virtual ICollection<RoutingRuleDestination> RoutingRuleDestination { get; set; }
        public virtual ICollection<RoutingRuleScheduleTiming> RoutingRuleScheduleTiming { get; set; }
        public virtual ICollection<RoutingRuleTranPriority> RoutingRuleTranPriority { get; set; }
    }
}
