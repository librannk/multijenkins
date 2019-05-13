using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.RequestReponceModel
{
    [ExcludeFromCodeCoverage]
    public class BusinessResponse
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
