using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.Ingestion.BusinessLayer.Models
{
    /// <summary> StorageSpaceItemAttr </summary>
    public class StorageSpaceAttribute
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for LeftOffset
        /// </summary>
        public decimal? LeftOffset { get; set; }
        /// <summary>
        /// To hold value for DispenseForm
        /// </summary>
        public string DispenseForm { get; set; }
        /// <summary>
        /// To hold value for OverideBaseAddress
        /// </summary>
        public int? OverideBaseAddress { get; set; }
        #endregion
    }
}
