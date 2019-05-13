using System;
using System.Runtime.Serialization;

namespace Facility.API.Model
{
    /// <summary>
    /// Update Facility request model.
    /// Implements the <see>
    ///     <cref>Facility.API.Model.BaseFacility</cref>
    /// </see>
    /// </summary>
    /// <seealso>
    ///     <cref>Facility.API.Model.BaseFacility</cref>
    /// </seealso>
    public class UpdateFacilityRequest : BaseFacility
    {
        /// <summary>
        /// Facility Active Status
        /// </summary>
        /// <value><c>true</c> if facility is active; otherwise, <c>false</c>.</value>
        [DataMember(Name = "ActiveFlag")]
        public bool ActiveFlag { get; set; }
    }
}
