using AutoMapper;
using Facility.API.BusinessLayer.Contracts;
using Facility.API.Constants;
using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Facility.API.Model;
using Facility.API.Model.InternalModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facility.API.BusinessLayer.Concrete
{
    /// <summary>
    /// Business Logic for Facility API.
    /// Implements the <see cref="IFacilityManager" />
    /// </summary>
    /// <seealso cref="IFacilityManager" />
    public class FacilityManager : IFacilityManager
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<FacilityManager> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityManager"/> class.
        /// </summary>
        /// <param name="facilityRepository">The facility repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">Automapper</param>
        public FacilityManager(IFacilityRepository facilityRepository, ILogger<FacilityManager> logger, IMapper mapper)
        {
            _facilityRepository = facilityRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Sends facility to Facility Repository for insertion into database.
        /// </summary>
        /// <param name="facilityRequest">The facility request.</param>
        /// <returns>Task&lt;FacilityEntity&gt;.</returns>
        public async Task<BusinessResult<Model.Facility>> Add(NewFacilityRequest facilityRequest)
        {
            var existingFacility = await _facilityRepository.GetFacilityByCode(facilityRequest.FacilityCode);
            if (existingFacility != null)
            {
                return new BusinessResult<Model.Facility>(null, CreateUpdateResultEnum.ErrorAlreadyExists);
            }

            var facility = _mapper.Map<NewFacilityRequest, FacilityEntity>(facilityRequest);

            if (await _facilityRepository.AddFacility(facility))
            {
                return new BusinessResult<Model.Facility>(_mapper.Map<FacilityEntity, Model.Facility>(facility),
                    CreateUpdateResultEnum.Success);
            }

            return new BusinessResult<Model.Facility>(null, CreateUpdateResultEnum.ValidationFailed);
        }

        /// <summary>
        /// Send Updates  to Facility Repository.
        /// </summary>
        /// <param name="facilityKey">Facility Identifier.</param>
        /// <param name="updateFacilityRequest">The update Facility Request.</param>
        /// <returns>Task&lt;FacilityEntity&gt;.</returns>
        public async Task<BusinessResult<Model.Facility>> Update(Guid facilityKey, UpdateFacilityRequest updateFacilityRequest)
        {
            var facilityEntity = await _facilityRepository.GetAsync(facilityKey);
            if (facilityEntity == null)
            {
                return new BusinessResult<Model.Facility>(null, CreateUpdateResultEnum.NotFound);
            }
            var updatedFacility =
                _mapper.Map(updateFacilityRequest, facilityEntity);
            var updateResult = await _facilityRepository.UpdateFacility(updatedFacility);
            if (updateResult)
            {
                return new BusinessResult<Model.Facility>(
                    _mapper.Map<FacilityEntity, Model.Facility>(updatedFacility), CreateUpdateResultEnum.Success);
            }

            return new BusinessResult<Model.Facility>(null, CreateUpdateResultEnum.ValidationFailed);
        }

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
        public async Task<BusinessResult<IList<FacilityList>>> GetAllFacilities(bool showInactive = false, string searchTerm = "",
            int offset = 0, int limit = 0)
        {
            _logger.LogDebug(
                $"Request received from Service to fetch GetAllFacilities with Inactive Flag: {showInactive}");

            if (limit > 0)
            {
                var (facilityResult, totalCount) =
                    await _facilityRepository.GetAllFacilitiesWithPaginationAsync(showInactive, searchTerm, offset, limit);
                var facilities = _mapper.Map<IList<FacilityEntity>, IList<FacilityList>>(facilityResult);
                return new BusinessResult<IList<FacilityList>>(facilities, CreateUpdateResultEnum.Success,
                    totalCount);
            }
            else
            {
                var facilities = await _facilityRepository.GetAllFacilitiesAsync(showInactive, searchTerm);

                return new BusinessResult<IList<FacilityList>>(
                    _mapper.Map<IList<FacilityEntity>, IList<FacilityList>>(facilities), CreateUpdateResultEnum.Success,
                    facilities.Count);
            }
        }

        /// <summary>
        /// Searches the facilities by facility name and flag if search should be done in inactive facilities as well.
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <param name="showInactive">if set to <c>true</c> include inactive in result.</param>
        /// <returns>Task&lt;IList&lt;FacilityList&gt;&gt;.</returns>
        public async Task<IList<FacilityList>> SearchFacility(string facilityName, bool showInactive)
        {
            _logger.LogDebug(
                $"Request received from Service to fetch GetAllFacilities with Inactive Flag: {showInactive} and search term {facilityName}");
            var facilities = await _facilityRepository.SearchFacilitiesByNameAsync(facilityName, showInactive);
            return _mapper.Map<IList<FacilityEntity>, IList<FacilityList>>(facilities);
        }

        /// <summary>
        /// Gets the facility by key asynchronously.
        /// </summary>
        /// <param name="facilityKey">The facility key.</param>
        /// <returns>Facility</returns>
        public async Task<Model.Facility> GetFacilityByKeyAsync(Guid facilityKey)
        {
            _logger.LogDebug(
                $"Request received from Service to fetch Facility with Facility Key: {facilityKey}");
            var facility = await _facilityRepository.GetAsync(facilityKey);
            if (facility != null)
            {
                return _mapper.Map<FacilityEntity, Model.Facility>(facility);
            }
            _logger.LogDebug($"Facility with facilityKey {facilityKey} not found.");
            return null;
        }
    }
}
