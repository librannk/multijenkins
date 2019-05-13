using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;


namespace SiteConfiguration.API.TransactionPriority.Models
{
    [ExcludeFromCodeCoverage]
    public partial class LabelCatagory:BaseEntity
    {
        public LabelCatagory()
        {
        }

        public Guid LabelCatagoryKey { get; set; }
        public Guid Description { get; set; }
        public bool? ActiveFlag { get; set; }
    
    }
}
