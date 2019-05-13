using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ExternalDependencies.Infrastructure.DBModel
{
    /// <summary> 
    /// It contains Formulary information of the transaction 
    /// </summary>
    public class Formulary : Entity
    {
        // #region Auto-Properties
        /// <summary>
        /// To store value for ItemId
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// To store value for FormularyId
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// To store value for IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// To store value for Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// To store value for FacilityFormulary
        /// </summary>
        public FacilityFormulary FacilityFormulary { get; set; }
        //    #endregion
    }
}
