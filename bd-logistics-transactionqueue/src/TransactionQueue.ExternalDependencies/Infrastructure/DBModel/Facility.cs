using System.Collections.Generic;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ExternalDependencies.Infrastructure.DBModel
{
    /// <summary> It contains facility information of the transaction </summary>
    public class Facility : Entity
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To store the value for ProcessInactiveAsException
        /// </summary>
        public bool ProcessInactiveAsException { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreCritLow
        /// </summary>
        public bool AduIgnoreCritLow { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreStockout
        /// </summary>
        public bool AduIgnoreStockout { get; set; }
        /// <summary>
        /// To store the value for StorageSpaces
        /// </summary>
        public List<FacilityStorageSpace> StorageSpaces { get; set; }

        /// <summary>
        /// To store the value for AduDupeTimeDelay
        /// </summary>
        public int? AduDupeTimeDelay { get; set; }

        /// <summary>
        /// To store the value for AduQtyRounding
        /// </summary>
        public bool? AduQtyRounding { get; set; }
        #endregion
    }
}
