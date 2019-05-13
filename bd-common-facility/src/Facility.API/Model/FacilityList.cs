using System;
using System.Runtime.Serialization;

namespace Facility.API.Model
{
    /// <summary>
    /// Facility List Model
    /// </summary>
    public class FacilityList
    {
        /// <summary>
        /// Facility system primary Id.
        /// </summary>
        /// <value>Facility system primary Id.</value>
        [DataMember(Name = "FacilityKey")]
        public Guid FacilityKey { get; set; }

        /// <summary>
        /// Name of the Facility
        /// </summary>
        /// <value>Name of the Facility</value>
        [DataMember(Name = "FacilityName")]
        public string FacilityName { get; set; }

        /// <summary>
        /// Facility Code
        /// </summary>
        /// <value>Facility Code</value>
        [DataMember(Name = "FacilityCode")]
        public string FacilityCode { get; set; }

        /// <summary>
        /// Description of the facility.
        /// </summary>
        /// <value>Description of the facility.</value>
        [DataMember(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Indicates Active status for the facility.
        /// </summary>
        /// <value>Indicates Active status for the facility.</value>
        [DataMember(Name = "Active")]
        public bool Active { get; set; }
    }
}
