using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Services.TransactionQueue.API.BussinessLayer.Models
{
    /// <summary>
    /// Class name SmartSortColumn
    /// </summary>
    public class SmartSortColumn
    {
        /// <summary>
        /// ColumnName
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// FriendlyName
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// LastModifiedBy
        /// </summary>
        public int LastModifiedBy { get; set; }
        /// <summary>
        /// LastModifiedDate
        /// </summary>
        public DateTime LastModifiedDate { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// LastModifiedBinaryValue
        /// </summary>
        public byte[] LastModifiedBinaryValue { get; set; }
        /// <summary>
        /// LastModifiedUTCDateTime
        /// </summary>
        public DateTime LastModifiedUTCDateTime { get; set; }
        /// <summary>
        /// CreateUTCDateTime
        /// </summary>
        public DateTime CreateUTCDateTime { get; set; }
    }
}
