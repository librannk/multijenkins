using System.Collections.Generic;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Models
{
    /// <summary> It contains facility information of the transaction </summary>
    public class Facility
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for Id
        /// </summary>
        public int Id { get; set; }
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
