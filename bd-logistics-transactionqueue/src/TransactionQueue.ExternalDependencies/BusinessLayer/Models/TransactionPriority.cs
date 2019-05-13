
namespace TransactionQueue.ExternalDependencies.BusinessLayer.Models
{
    /// <summary> It contains TransactionPriority information of the transaction </summary>
    public class TransactionPriority
    {
        #region Auto-Properties
        /// <summary>
        /// To store value for Id
        /// </summary>
        public int TransactionPriorityId { get; set; }
        /// <summary>
        /// To store value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To store value for TransactionPriorityCode
        /// </summary>
        public string TransactionPriorityCode { get; set; }
        /// <summary>
        /// To store value for IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// To store value for IsAdu
        /// </summary>
        public bool? IsAdu { get; set; }
        /// <summary>
        /// To store value for UseInterfaceItemName
        /// </summary>
        public bool? UseInterfaceItemName { get; set; }
        #endregion
    }
}
