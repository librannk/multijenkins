using AutoMapper;
using CCEProxy.API.AutoMapper;
using CCEProxy.API.DataLayer.Concrete;
using CCEProxy.API.Entity;
using CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces;
using CCEProxy.Repository.Contracts;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CCEProxyUnitTests.DataLayer
{
    /// <summary>
    /// This class contains  unit-tests for RequestRepository </summary>
    /// </summary>
    public class RequestRepositoryUnitTest
    {
        #region Private Fields
        private readonly Mock<IIncomingRequestRepository> _mockIncomingRepository;
        private readonly Mock<IFacilityRepository> _mockFacilityRepository;
        private readonly Mock<ITransactionPriorityRepository> _mockTransactionPriorityRepository;
        private readonly IMapper _mapper;
        private IRequestRepository _requestRepository;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the private fields - TransactionQueueMongoRepository
        /// </summary>
        public RequestRepositoryUnitTest()
        {
            _mockIncomingRepository = new Mock<IIncomingRequestRepository>();
            _mockFacilityRepository = new Mock<IFacilityRepository>();
            _mockTransactionPriorityRepository = new Mock<ITransactionPriorityRepository>();
            
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }
       
        [Fact]
        public async Task InsertFacility_facility_ShouldCallRepository()
        {
            //Arrange
            Facility facility = new Facility
            {
                FacilityCode = "23",
                Id = 1
            };
            //Act
            IRequestRepository requestRepository = new RequestRepository(_mockIncomingRepository.Object, _mockTransactionPriorityRepository.Object, _mockFacilityRepository.Object, _mapper);
            await requestRepository.AddFacilityRequest(facility);

            //verify
            _mockFacilityRepository.Verify(x => x.InsertAsync(It.IsAny<CCEProxy.API.Infrastructure.DataAccess.DBModel.Facility>()), Times.Once);
        }

        [Fact]
        public async Task GetFacility_ShouldReturnFacility()
        {
            //Arrange
            string mockFacilityCode = "23";
            //Arrange
            CCEProxy.API.Infrastructure.DataAccess.DBModel.Facility facility = new CCEProxy.API.Infrastructure.DataAccess.DBModel.Facility
            {
                FacilityCode = mockFacilityCode,
                FacilityId = 1
            };

            _mockFacilityRepository.Setup(x => x.GetFacilityByCode(mockFacilityCode))
                .Returns(Task.FromResult(facility));

            //Act
            IRequestRepository requestRepository = new RequestRepository(_mockIncomingRepository.Object, _mockTransactionPriorityRepository.Object, _mockFacilityRepository.Object, _mapper);
            Facility mockResponse = await requestRepository.GetFacility(mockFacilityCode);

            //Assert
            Assert.Equal(mockFacilityCode, mockResponse.FacilityCode);
        }
        [Fact]
        public async Task GetTransactionPriority_ShouldReturnTransactionPriority()
        {
            //Arrange
            string mockPriorityCode = "PATIENTPICK";
            int mockfacilityId = 1;
            //Arrange
            CCEProxy.API.Infrastructure.DataAccess.DBModel.TransactionPriority transactionPriority = new CCEProxy.API.Infrastructure.DataAccess.DBModel.TransactionPriority()
            {
                FacilityId = mockfacilityId,
                PriorityCode = mockPriorityCode
            };

            _mockTransactionPriorityRepository.Setup(x => x.GetTransactionPriority(mockfacilityId, mockPriorityCode))
                .Returns(Task.FromResult(transactionPriority));

            //Act
            IRequestRepository requestRepository = new RequestRepository(_mockIncomingRepository.Object, _mockTransactionPriorityRepository.Object, _mockFacilityRepository.Object, _mapper);
            TransactionPriority mockResponse = await requestRepository.GetTransactionPriority(mockfacilityId, mockPriorityCode);

            //Assert
            Assert.Equal(mockPriorityCode, mockResponse.PriorityCode);
        }
        #endregion
    }
}
