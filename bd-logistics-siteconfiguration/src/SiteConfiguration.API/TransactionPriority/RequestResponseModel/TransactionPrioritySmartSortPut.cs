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
    /// <summary>
    /// 
    /// </summary>

    public class TransactionPrioritySmartSortPut
    {

        /// <summary>
        /// Gets or Sets SmartSortId
        /// </summary>

        public string SmartSortColumnKey { get; set; }

        /// <summary>
        /// Gets or Sets TPSortOrder
        /// </summary>

        public int SmartSortOrder { get; set; }
    }
}
