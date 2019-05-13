using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.RequestReponceModel
{
    [ExcludeFromCodeCoverage]
    public class ErrorMessage
    {
        public string ErrorCode { get; set; }

        public string ErrorDescription { get; set; }
    }
}
