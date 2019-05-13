using Facility.API.Model;
using Facility.API.Model.InternalModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facility.API.BusinessLayer.Contracts
{
    /// <summary>
    /// Business Logic for Facility API.
    /// </summary>
    public interface IFacilityManager
    {
        /// <summary>
        /// Sends facility to Facility Repository for insertion into database.
        /// </summary>
        /// <param name="facilityRequest">The facility request.</param>
        /// <returns>Task&lt;FacilityEntity&gt;.</returns>
        Task<BusinessResult<Model.Facility>> Add(NewFacilityRequest facilityRequest);

        /// <summary>
        /// Send Updates  to Facility Repository.
        /// </summary>
        /// <param name="facilityKey">Facility Identifier.</param>
        /// <param name="updateFacilityRequest">The update Facility request.</param>
        /// <returns>Task&lt;FacilityEntity&gt;.</returns>
        Task<BusinessResult<Model.Facility>> Update(Guid facilityKey, UpdateFacilityRequest updateFacilityRequest);

        /// <summary>
        /// Provides the list of all facilities.
        /// Optionally provides support to filter Inactive Facilities, Search facilities partially by facility name,
        /// and support for pagination options.
        /// </summary>
        /// <param name="showInactive">true if inactive facilities needs to be selected. Default is false.</param>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>List of Facilities</returns>
        Task<BusinessResult<IList<FacilityList>>> GetAllFacilities(bool showInactive = false, string searchTerm = "",
            int offset = 0, int limit = 0);

        /// <summary>
        /// Searches the facility by facility name and flag if search should be done in inactive facilities as well.
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <param name="showInactive">if set to <c>true</c> include inactive in result.</param>
        /// <returns>Task&lt;IList&lt;FacilityList&gt;&gt;.</returns>
        Task<IList<FacilityList>> SearchFacility(string facilityName, bool showInactive);

        /// <summary>
        /// Gets the facility by key asynchronously.
        /// </summary>
        /// <param name="facilityKey">The facility key.</param>
        /// <returns>Facility</returns>
        Task<Model.Facility> GetFacilityByKeyAsync(Guid facilityKey);
    }
}