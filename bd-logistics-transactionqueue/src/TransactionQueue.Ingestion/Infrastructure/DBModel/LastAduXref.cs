using System;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.Ingestion.Infrastructure.DBModel
{
    /// <summary>
    /// <summary> It contains LastAduXref information of the transaction  </summary>
    /// </summary>
    public class LastAduXref : Entity
    {
        #region Auto-Properties
        /// <summary>
        /// To store the value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To store the value for FormularyId
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// To store the value for Destination
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// To store the value for LastAduTransUtcDateTime
        /// </summary>
        public DateTime? LastAduTransUtcDateTime { get; set; }
        #endregion
    }
}
