using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SiteConfiguration.API.TransactionPriority.RequestResponseModel
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// 
    /// </summary>

    public class TransactionPrioritySmartSort
    {
       

        /// <summary>
        /// Gets or Sets SmartSortName
        /// </summary>

        public string SmartSortName { get; set; }

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
