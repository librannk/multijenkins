using CCEProxy.API.BusinessLayer.Contracts;
using static CCEProxy.API.Common.Constants.Constants;
using CCEProxy.API.Entity;
using CCEProxy.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CCEProxy.API.BusinessLayer.Concrete
{
    /// <summary> This class is responsible for handling the Transaction Queue operations </summary>
    public class FacilityManager : IFacilityManager
    {
        #region Private Fields
        private readonly IRequestRepository _requestRepository;
        private readonly ILogger _logger;
        #endregion

        #region Constructors
        /// <summary> Initialize the private fields </summary>
        public FacilityManager(IRequestRepository requestRepository, ILogger<FacilityManager> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
          
        }
        #endregion
        /// <summary>
        /// This method processes the facility request from Facility Service and insert the data into database.
        /// <param name="facilityRequest">facilityRequest</param>
        /// </summary>
        public async Task ProcessFacilityRequest(Facility facilityRequest)
        {
            _logger.LogInformation(LoggingMessage.ProcessFacilityRequest, JsonConvert.SerializeObject(facilityRequest));
            await _requestRepository.AddFacilityRequest(facilityRequest);
        }
    }
}
