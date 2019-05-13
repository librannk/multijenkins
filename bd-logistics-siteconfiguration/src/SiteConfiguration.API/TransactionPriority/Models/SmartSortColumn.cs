using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;


namespace SiteConfiguration.API.TransactionPriority.Models
{
    [ExcludeFromCodeCoverage]
    public partial class SmartSortColumn :BaseEntity
    {
        public SmartSortColumn()
        {
            SmartSort = new HashSet<SmartSort>();
        }

        [Key]
        public Guid SmartSortColumnKey { get; set; }
        public Guid TenantKey { get; set; }
        public string ColumnNameText { get; set; }
        public string FriendlyColumnNameText { get; set; }
        public bool ActiveFlag { get; set; }

        public ICollection<SmartSort> SmartSort { get; set; } = new List<SmartSort>();
    }
}
