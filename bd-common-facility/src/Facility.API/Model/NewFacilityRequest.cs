using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Facility.API.Model
{
    /// <summary>
    /// New Facility Request.
    /// </summary>
    public class NewFacilityRequest : BaseFacility
    {
        /// <summary>
        /// Facility Code.
        /// </summary>
        /// <value>Facility Code.</value>
        [Required(ErrorMessage = "Please enter Facility Code")]
        [MaxLength(20)]
        [DataMember(Name = "FacilityCode")]
        public string FacilityCode { get; set; }
    }
}
