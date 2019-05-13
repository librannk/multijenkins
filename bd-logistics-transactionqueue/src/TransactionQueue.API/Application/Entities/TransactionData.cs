using System.Collections.Generic;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Ingestion.BusinessLayer.Models;

namespace TransactionQueue.API.Application.Entities
{
    /// <summary>
    /// This class is used for publishing the data to the device communication service
    /// </summary>
    public class TransactionData
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for Type
        /// </summary>
        public TransactionType Type { get; set; }
        /// <summary>
        /// To hold value for Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// To hold value for Devices
        /// </summary>
        public List<Device> Devices { get; set; }
        #endregion
    }
}