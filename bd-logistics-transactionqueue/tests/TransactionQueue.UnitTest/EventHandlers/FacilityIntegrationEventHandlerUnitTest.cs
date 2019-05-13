//using Microsoft.Extensions.Logging;
//using Moq;
//using TransactionQueue.API.Application.DataLayer.Abstraction;
//using TransactionQueue.API.BussinessLayer.Concrete;
//using Xunit;

//namespace TransactionQueue.UnitTest.EventHandlers
//{
//    /// <summary>
//    /// This class contains unit test for FacilityIntegrationEventHandler
//    /// </summary>

//    public class FacilityIntegrationEventHandlerUnitTest
//    {
//        /// <summary>
//        /// This event should get data from data bus and store that in DB
//        /// </summary>

//        [Fact]
//        public void ProcessFacilityRequest_facilityRequest_ShouldCallInsertFacility()
//        {
//            //Arrange
//            var fakeLog = new Mock<ILogger<FacilityManager>>();
//            var fakeFacilityDAL = new Mock<IFacilityDAL>();
//            fakeFacilityDAL.Setup(x => x.InsertFacility(It.IsAny<TransactionQueue.API.Application.Entities.Facility>()));
//            FacilityManager facilityManager = new FacilityManager(fakeFacilityDAL.Object, fakeLog.Object);
//            TransactionQueue.API.Application.Entities.Facility facilityRequest = new TransactionQueue.API.Application.Entities.Facility();
            
//            //Act
//            facilityManager.ProcessFacilityRequest(facilityRequest);

//            //Assert
//            fakeFacilityDAL.Verify(x => x.InsertFacility(It.IsAny<TransactionQueue.API.Application.Entities.Facility>()), Times.Once);
//        }
//    }
//}
