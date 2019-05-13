using System.Collections.Generic;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Models
{
    /// <summary> Formulary details of the transection </summary>
    public class Formulary
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for FormularyId
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// To store the value for Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// To store the value for IsActive
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// To store the value for ItemId
        /// </summary>
        public int ItemId { get; set; }
        
        /// <summary>
        /// To store the value for FacilityFormulary
        /// </summary>
        public FacilityFormulary FacilityFormulary { get; set; }
        #endregion
    }
}
