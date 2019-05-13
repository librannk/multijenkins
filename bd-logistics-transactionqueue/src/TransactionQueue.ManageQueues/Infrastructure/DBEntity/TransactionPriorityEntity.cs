using TransactionQueue.Shared.DataAccess.Mongo.Entities;
using System.Collections.Generic;

namespace TransactionQueue.ManageQueues.Infrastructure.DBEntity
{
    public class TransactionPriorityEntity : Entity
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

        public List<SmartSortEntity> SmartSortData { get; set; }
        #endregion
    }
}
