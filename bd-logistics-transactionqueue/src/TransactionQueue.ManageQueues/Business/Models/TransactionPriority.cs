
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ManageQueues.Business.Models
{
    [BsonIgnoreExtraElements]
    public class TransactionPriority : Entity
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for TransactionPriorityId
        /// </summary>
        public int TransactionPriorityId { get; set; }
        /// <summary>
        /// To hold value for TransactionPriorityCode
        /// </summary>
        public string TransactionPriorityCode { get; set; }
        /// <summary>
        /// To hold value for TransactionPriorityName
        /// </summary>
        public string TransactionPriorityName { get; set; }
        /// <summary>
        /// To hold value for TransactionPriorityOrder
        /// </summary>
        public int TransactionPriorityOrder { get; set; }
        /// <summary>
        /// To hold value for IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// To hold value for IsAdu
        /// </summary>
        public bool? IsAdu { get; set; }
        /// <summary>
        /// To hold value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To store value for UseInterfaceItemName
        /// </summary>
        public bool? UseInterfaceItemName { get; set; }
        /// <summary>
        /// To capture Legend ForeColor
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Data of SmartSortData
        /// </summary>
        public List<SmartSort> SmartSortData { get; set; }
        #endregion
    }
}
