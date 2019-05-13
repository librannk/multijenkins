//using AutoMapper;
//using Moq;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TransactionQueue.API.DataLayer;
//using TransactionQueue.API.Infrastructure.Repository.Interfaces;
//using Xunit;

//namespace TransactionQueue.UnitTest.DataLayer
//{
//    /// <summary>
//    /// Unit Tests for FacilityDAL class methods.
//    /// </summary>
//    public class FacilityDALTest
//    {
//        #region Private Fields
//        private readonly Mock<IFacilityMongoRepository> fakeFacilityMongoRepository;
//        private FacilityDAL facilityDAL;
//        private readonly IMapper _mapper;
//        #endregion

//        #region Constructor
//        public FacilityDALTest()
//        {
//            var mockMapper = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new TransactionQueue.API.AutoMapper.MapProfile());
//            });
//            _mapper = mockMapper.CreateMapper();

//            fakeFacilityMongoRepository = new Mock<IFacilityMongoRepository>();
//            facilityDAL = new FacilityDAL(fakeFacilityMongoRepository.Object, _mapper);
//        }
//        #endregion

//        #region Test Cases
//        [Fact]
//        public async Task InsertFacility_facility_ShouldCallMongoRepository()
//        {
//            //Arrange
//            TransactionQueue.API.Application.Entities.Facility fakeFacility = new TransactionQueue.API.Application.Entities.Facility();

//            //Act
//            await facilityDAL.InsertFacility(fakeFacility);

//            //Assert
//            fakeFacilityMongoRepository.Verify(x => x.InsertFacility(It.IsAny<TransactionQueue.API.Application.Entities.Facility>()), Times.Once);
//        }

//        [Fact]
//        public async Task GetFacilityById_facilityId_ShouldReturnFacility()
//        {
//            //Arrange
//            int mockFacilityId = 1;
//            TransactionQueue.API.Infrastructure.DBModel.Facility fakeFacility = new TransactionQueue.API.Infrastructure.DBModel.Facility
//            {
//                FacilityId = mockFacilityId,
//                StorageSpaces = new List<TransactionQueue.API.Infrastructure.DBModel.FacilityStorageSpace>()
//            };
//            fakeFacilityMongoRepository.Setup(x => x.GetFacilityById(It.IsAny<int>())).ReturnsAsync(fakeFacility);

//            //Act
//            TransactionQueue.API.Application.Entities.Facility mockResponse = await facilityDAL.GetFacilityById(mockFacilityId);

//            //Assert
//            Assert.Equal(mockFacilityId, mockResponse.Id);
//        }

//        [Fact]
//        public async Task GetFacilityById_facilityNotFound_ShouldReturnNull()
//        {
//            //Arrange
//            int fakeFacilityId = 1;

//            //Act
//            TransactionQueue.API.Application.Entities.Facility mockResponse = await facilityDAL.GetFacilityById(fakeFacilityId);

//            //Assert
//            Assert.Null(mockResponse);
//        }
//        #endregion
//    }
//}
