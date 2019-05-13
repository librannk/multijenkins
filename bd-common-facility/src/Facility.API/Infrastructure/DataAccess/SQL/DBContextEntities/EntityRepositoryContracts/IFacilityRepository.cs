using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Facility.API.Infrastructure.DataAccess.SQL.EFRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// IFacilityRepository interface represent the all member of IRepository of type Facility.
    /// IFacilityRepository provide the extensibility for new operation other than IRepository.
    /// </summary> 
    public interface IFacilityRepository : IRepository<FacilityEntity>
    {
        /// <summary>
        /// Gets all facilities asynchronous.
        /// </summary>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <param name="searchTerm">Search term for facility</param>
        /// <returns>Task&lt;List&lt;FacilityEntity&gt;&gt;.</returns>
        Task<List<FacilityEntity>> GetAllFacilitiesAsync(bool showInactive, string searchTerm);

        /// <summary>
        /// Adds the facility.
        /// </summary>
        /// <param name="facilityEntity">The facility entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;. if facility is inserted</returns>
        Task<bool> AddFacility(FacilityEntity facilityEntity);

        /// <summary>
        /// Searches the facilities by name asynchronously.
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns>Task&lt;List&lt;FacilityEntity&gt;&gt;.</returns>
        Task<List<FacilityEntity>> SearchFacilitiesByNameAsync(string facilityName, bool showInactive);

        /// <summary>
        /// Finds facility by facility code asynchronously.
        /// </summary>
        /// <param name="facilityCode">The facility code.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<FacilityEntity> GetFacilityByCode(string facilityCode);

        /// <summary>
        /// Updates the facility.
        /// </summary>
        /// <param name="facilityEntity">The facility entity to be updated.</param>
        /// <returns>Updation Result</returns>
        Task<bool> UpdateFacility(FacilityEntity facilityEntity);

        /// <summary>
        /// Gets all facilities asynchronously.
        /// </summary>
        /// <param name="showInactive">if set to <c>true</c> returns inactive facilities.</param>
        /// <param name="searchTerm">Search term for facility</param>
        /// <param name="offset">Number of records to skip</param>
        /// <param name="limit">Number of records to include in page result.</param>
        /// <returns>Task&lt;List&lt;FacilityEntity&gt;&gt;.</returns>
        Task<(List<FacilityEntity> entities, int totalCount)> GetAllFacilitiesWithPaginationAsync(
            bool showInactive, string searchTerm, int offset, int limit);
    }
}
