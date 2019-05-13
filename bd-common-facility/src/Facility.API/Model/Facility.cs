using System;
using System.Runtime.Serialization;

namespace Facility.API.Model
{
    /// <summary>
    /// Facility 
    /// </summary>
    public class Facility : NewFacilityRequest
    {
        /// <summary>
        /// Facility system primary Id.
        /// </summary>
        /// <value>Facility system primary Id.</value>
        [DataMember(Name = "FacilityKey")]
        public Guid FacilityKey { get; set; }

        /// <summary>
        /// Facility Active Status
        /// </summary>
        /// <value><c>true</c> if facility is active; otherwise, <c>false</c>.</value>
        [DataMember(Name = "ActiveFlag")]
        public bool ActiveFlag { get; set; }
    }
}