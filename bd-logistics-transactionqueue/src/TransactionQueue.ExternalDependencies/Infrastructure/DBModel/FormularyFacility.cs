
namespace TransactionQueue.ExternalDependencies.Infrastructure.DBModel
{
    /// <summary> It contains Formulary Facility information of the transaction </summary>
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
