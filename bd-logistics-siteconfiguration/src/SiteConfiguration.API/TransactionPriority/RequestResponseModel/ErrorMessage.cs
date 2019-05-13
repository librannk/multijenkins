using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.TransactionPriority.RequestResponseModel
{
    [ExcludeFromCodeCoverage]
    public class ErrorMessage
    {
        public decimal ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

    }
}
