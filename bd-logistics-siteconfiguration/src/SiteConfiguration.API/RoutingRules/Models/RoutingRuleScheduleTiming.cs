using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using SiteConfiguration.API.Schedule.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.RoutingRules.Models
{
    [ExcludeFromCodeCoverage]
    public partial class RoutingRuleScheduleTiming : BaseEntity
    {
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid RoutingRuleScheduleTimingKey { get; set; }
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid TenantKey { get; set; }
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid ScheduleTimingKey { get; set; }
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid RoutingRuleKey { get; set; }
     
        public virtual RoutingRule RoutingRuleKeyNavigation { get; set; }
        public virtual ICollection<ScheduleTiming> ScheduleTimingKeyNavigation { get; set; }
    }
}
