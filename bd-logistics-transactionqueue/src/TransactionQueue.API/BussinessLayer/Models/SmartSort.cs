using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Services.TransactionQueue.API.BussinessLayer.Models
{
    /// <summary>
    /// To fetch SmartSort columns
    /// </summary>
    public class SmartSort
    {
        /// <summary>
        /// TranPriorityId
        /// </summary>
        public int TranPriorityId { get; set; }
        /// <summary>
        /// SmartSortColumnId
        /// </summary>
        public int SmartSortColumnId { get; set; }
        /// <summary>
        /// SmartSortOrder
        /// </summary>
        public int SmartSortOrder { get; set; }
        /// <summary>
        /// LastModifiedBy
        /// </summary>
        public int LastModifiedBy { get; set; }
        /// <summary>
        /// LastModifiedDate
        /// </summary>
        public DateTime LastModifiedDate { get; set; }
        /// <summary>
        /// LastModifiedBinaryValue
        /// </summary>
        public byte[] LastModifiedBinaryValue { get; set; }
        /// <summary>
        /// LastModifiedUTCDateTime
        /// </summary>
        public DateTime LastModifiedUTCDateTime { get; set; }
        /// <summary>
        /// SmartSortColumn
        /// </summary>
        public SmartSortColumn SmartSortColumn { get; set; }

    }
}
