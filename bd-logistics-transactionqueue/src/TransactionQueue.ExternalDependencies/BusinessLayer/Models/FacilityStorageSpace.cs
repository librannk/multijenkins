using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Models
{
    /// <summary>
    /// FacilityStorageSpace
    /// </summary>
    public class FacilityStorageSpace
    {
        /// <summary>
        /// To hold value for Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// To hold value for IsDefault
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
