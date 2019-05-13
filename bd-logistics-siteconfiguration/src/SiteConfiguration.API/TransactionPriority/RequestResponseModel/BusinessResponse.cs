using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.TransactionPriority.RequestResponseModel
{
    [ExcludeFromCodeCoverage]
    public class BusinessResponse
    {
        public bool IsSuccesss{get;set;}
        public string Message { get; set; }
    }
}
