using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.TransactionPriority.RequestResponseModel
{
    [ExcludeFromCodeCoverage]
    public class TransactionPriorityBaseEntity
    {
        public Guid ActorKey { get; set; }
    }
}
