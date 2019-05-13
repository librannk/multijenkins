using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;
using TransactionQueue.ExternalDependencies.Common.Constants;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Concrete
{
    /// <summary>
    /// This class is responsible for handling facility related operations.
    /// </summary>
    public class FacilityManager : IFacilityManager
    {
        #region Private Fields
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<FacilityManager> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        /// <summary>
        /// Intializes the private fields.
        /// </summary>
        /// <param name="facilityRepository">facilityDal</param>
        /// <param name="logger">logger</param>
        public FacilityManager(IFacilityRepository facilityRepository,
            ILogger<FacilityManager> logger,
            IMapper mapper)
        {
            _facilityRepository = facilityRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// This method is used to store a facility in DB.
        /// </summary>
        /// <param name="request">Facility to be stored.</param>
        public async Task<bool> ProcessFacilityRequest(Facility request)
        {
            _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.DataReceivedFromFacility, JsonConvert.SerializeObject(request)));

            var result = await _facilityRepository.GetFacilityById(request.Id);
            if (result == null)
            {
                return await _facilityRepository.InsertFacility(request);
            }
            else
            {
                return await _facilityRepository.UpdateFacility(request);
            }
        }

        /// <summary>
        /// This method is used to validate facility.
        /// </summary>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        public async Task<Facility> ValidateFacility(int facilityId)
        {
            var result = await _facilityRepository.GetFacilityById(facilityId);

            if (result != null)
            {
                return _mapper.Map<Facility>(result);
            }

            return null;
        }
        #endregion
    }
}
