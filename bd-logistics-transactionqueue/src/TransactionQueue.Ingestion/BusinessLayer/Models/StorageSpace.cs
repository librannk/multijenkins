using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;

namespace TransactionQueue.Ingestion.BusinessLayer.Models
{
    /// <summary> StorageSpace </summary>
    public class StorageSpace
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for ItemType
        /// </summary>
        public StorageSpaceItemType ItemType { get; set; }
        /// <summary>
        /// To hold value for Number
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// To hold value for StorageSpaceItemAttr
        /// </summary>
        public StorageSpaceAttribute Attribute { get; set; }
        #endregion
    }
}
