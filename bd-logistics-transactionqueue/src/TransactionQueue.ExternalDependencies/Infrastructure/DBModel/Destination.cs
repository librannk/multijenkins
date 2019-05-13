using System.Collections.Generic;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ExternalDependencies.Infrastructure.DBModel
{
    /// <summary> 
    /// It contains Destination information of the transaction 
    /// </summary>
    public class Destination : Entity
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for DestinationCode
        /// </summary>
        public int DestinationId { get; set; }
        /// <summary>
        /// To store the value for DestinationCode
        /// </summary>
        public string DestinationCode { get; set; }
        /// <summary>
        /// To store the value for AduQtyRounding
        /// </summary>
        public bool? AduQtyRounding { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreStockOut
        /// </summary>
        public bool? AduIgnoreStockOut { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreCritLow
        /// </summary>
        public bool? AduIgnoreCritLow { get; set; }
        #endregion
    }
}
