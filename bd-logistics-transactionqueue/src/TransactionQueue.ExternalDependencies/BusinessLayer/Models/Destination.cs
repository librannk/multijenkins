
namespace TransactionQueue.ExternalDependencies.BusinessLayer.Models
{
    /// <summary> It contains destination information of the transaction </summary>
    public class Destination
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for Destinationid
        /// </summary>
        public int DestinationId { get; set; }

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
