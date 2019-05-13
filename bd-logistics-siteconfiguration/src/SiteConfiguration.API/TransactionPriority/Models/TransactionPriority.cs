using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.RoutingRules.Models;

namespace SiteConfiguration.API.TransactionPriority.Models
{
    [ExcludeFromCodeCoverage]
    [Table("TransactionPriority", Schema = "dbo")]
    public class TransactionPriority : BaseEntity
    {
        public TransactionPriority()
        {           
            SmartSort = new HashSet<SmartSort>();
            RoutingRuleTranPriority = new HashSet<RoutingRuleTranPriority>();
        }

        [Key]
        [Column("TranPriorityKey")]
        public Guid Id { get; set; }
        public Guid TenantKey { get; set; }
        public Guid FacilityKey { get; set; }
        public Guid PrintLabelKey { get; set; }
        public string PriorityCode { get; set; }
        public string PriorityName { get; set; }
        public int PriorityOrder { get; set; }
        public string LegendForeColor { get; set; }
        public string LegendBackColor { get; set; }
        public int MaxOnHoldLength { get; set; }
        public bool ForManualPickFlag { get; set; }
        public bool ForManualRestockFlag { get; set; }
        public bool ADUFlag { get; set; }
        public bool AutoReceiveFlag { get; set; }
        public bool SystemFlag { get; set; }
        public bool ActiveFlag { get; set; }
        public bool UseInterfaceMedNameFlag { get; set; }
        public ICollection<SmartSort> SmartSort { get; set; } = new List<SmartSort>();
        public virtual ICollection<RoutingRuleTranPriority> RoutingRuleTranPriority { get; set; }

    }
}
