using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.RoutingRules.Models
{
    [ExcludeFromCodeCoverage]
    public partial class RoutingRuleDestination : BaseEntity
    {
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid RoutingRuleDestinationKey { get; set; }

        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid TenantKey { get; set; }

        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid RoutingRuleKey { get; set; }

        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid DestinationKey { get; set; }
       

       // public virtual Destination DestinationKeyNavigation { get; set; }
        public virtual RoutingRule RoutingRuleKeyNavigation { get; set; }
    }
}
