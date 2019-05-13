using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;


namespace SiteConfiguration.API.TransactionPriority.Models
{
    [ExcludeFromCodeCoverage]
    public partial class SmartSort : BaseEntity
    {
       
        
        public Guid TransPriorityKey { get; set; }
        public Guid SmartSortColumnKey { get; set; }
        public Guid TenantKey { get; set; }
        public int SmartSortOrder { get; set; }

        public  SmartSortColumn SmartSortColumnKeyNavigation { get; set; }
        
         public TransactionPriority TransPriorityKeyNavigation { get; set; }
}
}
