
namespace TransactionQueue.ExternalDependencies.BusinessLayer.Models
{
    /// <summary> It contains facility information of the formulary  </summary>
    public class FacilityFormulary
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To store the value for Approved
        /// </summary>
        public bool Approved { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreCritLow
        /// </summary>
        public bool? AduIgnoreCritLow { get; set; }
        /// <summary>
        /// To store the value for AduIgnoreStockout
        /// </summary>
        public bool? AduIgnoreStockout { get; set; }

        /// <summary>
        /// To store the value for AduQtyRounding
        /// </summary>
        public bool? AduQtyRounding { get; set; }
        #endregion
    }
}
