using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Facility.API.Infrastructure.DataAccess.SQL.EFRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{
    /// <summary>
    /// CustomerRepository class implements the all member of IFacilityRepository of type Facility.
    /// </summary> 
    public class FacilityRepository : BaseRepository<FacilityEntity>, IFacilityRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public FacilityRepository(FacilityDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all facilities asynchronously.
        /// </summary>
        /// <param name="showInactive">if set to <c>true</c> returns inactive facilities.</param>
        /// <param name="searchTerm">Search term for facility</param>
        /// <returns>Task&lt;List&lt;FacilityEntity&gt;&gt;.</returns>
        public Task<List<FacilityEntity>> GetAllFacilitiesAsync(bool showInactive, string searchTerm)
        {
            var entities = GetFacilitiesWithInactiveOptionQueryable(showInactive);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                entities = entities.Where(b => b.FacilityName.Contains(searchTerm));
            }
            return entities.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Gets all facilities asynchronously.
        /// </summary>
        /// <param name="showInactive">if set to <c>true</c> returns inactive facilities.</param>
        /// <param name="searchTerm">Search term for facility</param>
        /// <param name="offset">Number of records to skip</param>
        /// <param name="limit">Number of records to include in page result.</param>
        /// <returns>Task&lt;List&lt;FacilityEntity&gt;&gt;.</returns>
        public async Task<(List<FacilityEntity> entities, int totalCount)>
            GetAllFacilitiesWithPaginationAsync(bool showInactive, string searchTerm, int offset, int limit)
        {
            var entities = GetFacilitiesWithInactiveOptionQueryable(showInactive);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                entities = entities.Where(b => b.FacilityName.Contains(searchTerm));
            }

            var filteredResult = entities.AsQueryable();
            if (limit > 0)
            {
                filteredResult = entities.Skip(offset).Take(limit);
            }
            return (await filteredResult.AsNoTracking().ToListAsync(), await entities.CountAsync());
        }

        /// <summary>
        /// Searches the facilities by name asynchronously.
        /// </summary>
        /// <param name="facilityName">Name of the facility to be searched.</param>
        /// <param name="showInactive">if set to <c>true</c> includes inactive in result.</param>
        /// <returns>Task&lt;List&lt;FacilityEntity&gt;&gt;.</returns>
        public Task<List<FacilityEntity>> SearchFacilitiesByNameAsync(string facilityName, bool showInactive)
        {
            var result = DbSet.Where(b => b.FacilityName.Contains(facilityName));
            if (!showInactive)
            {
                result = result.Where(b => b.ActiveFlag);
            }

            return result.AsNoTracking().ToListAsync();
        }


        /// <summary>
        /// Finds facility by facility code asynchronously.
        /// </summary>
        /// <param name="facilityCode">The facility code.</param>
        /// <returns>Facility</returns>
        public async Task<FacilityEntity> GetFacilityByCode(string facilityCode)
        {
            return await DbSet.FirstOrDefaultAsync(b => b.FacilityCode.Equals(facilityCode));
        }

        /// <summary>
        /// Adds the facility.
        /// </summary>
        /// <param name="facilityEntity">The facility entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;. if facility is inserted</returns>
        public async Task<bool> AddFacility(FacilityEntity facilityEntity)
        {
            await AddAsync(facilityEntity);
            facilityEntity.CreatedDateTime = DateTimeOffset.Now;
            //TODO: Actor
            return await _unitOfWork.CommitChangesAsync() > 0;
        }

        /// <summary>
        /// Updates the facility.
        /// </summary>
        /// <param name="facilityEntity">The facility entity to be updated.</param>
        /// <returns>Updation Result</returns>
        public async Task<bool> UpdateFacility(FacilityEntity facilityEntity)
        {
            Update(facilityEntity);
            facilityEntity.LastModifiedDateTime = DateTimeOffset.Now;
            //TODO: Actor
            return await _unitOfWork.CommitChangesAsync() > 0;
        }

        /// <summary>
        /// Gets the facilities dataset as Queryable.
        /// </summary>
        /// <param name="getInactive">if set to <c>true</c> if filter should include inactive option.</param>
        /// <returns>IQueryable of FacilityEntities</returns>
        private IQueryable<FacilityEntity> GetFacilitiesWithInactiveOptionQueryable(bool getInactive)
        {
            var result = DbSet.AsQueryable();
            if (!getInactive)
            {
                result = result.Where(b => b.ActiveFlag);
            }

            return result;
        }
    }
}
