using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCEProxy.API.Entity
{
    /// <summary>
    /// Class Incoming Facility
    /// </summary>
    public class IncomingFacility
    {
        /// <summary>
        /// To hold the FacilityCode
        /// </summary>
        [Required(ErrorMessage = "Facility is null")]
        public virtual string FacilityCode { get; set; }

        /// <summary>
        /// To hold the OrderingFacility
        /// </summary>
        public string OrderingFacility { get; set; }

        /// <summary>
        /// To hold the FacilityId
        /// </summary>
        public int? FacilityId { get; set; }
    }
}
