using CCEProxy.API.BusinessLayer.Concrete;
using CCEProxy.API.Entity;
using CCEProxy.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CCEProxyUnitTests.BusinessLayer
{
    /// <summary>
    ///  This class contains unit tests for FacilityManager
    /// </summary>
    public class FacilityManagerUnitTest
    {
        #region PrivateFields
        private readonly Mock<IRequestRepository> _requestRepository;
        private readonly Mock<ILogger<FacilityManager>> _logger;
        private readonly FacilityManager _facilityManager;
        private readonly Facility _facilityRequest;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the private fields
        /// </summary>

        public FacilityManagerUnitTest()
        {
            _requestRepository = new Mock<IRequestRepository>();
            _logger = new Mock<ILogger<FacilityManager>>();
            _facilityRequest = new Facility
            {
                FacilityCode = "23"
            };

            _facilityManager = new FacilityManager( _requestRepository.Object, _logger.Object);

        }

        #endregion

        #region Test case for InsertFacilityRequestUnitTest
        [Fact]
        public async Task InsertFacilityRequest__ShouldInsertFacility()
        {
            //Act
            await _facilityManager.ProcessFacilityRequest(_facilityRequest);
           
            //Arrange
            _requestRepository.Verify(x => x.AddFacilityRequest(_facilityRequest));

            

        }
        #endregion
    }
}
