////using Microsoft.Extensions.Logging;
////using Moq;
////using System.Threading.Tasks;
////using TransactionQueue.API.Application.Entities;
////using TransactionQueue.API.BussinessLayer.Concrete;
////using TransactionQueue.API.DataLayer.Abstraction;
////using Xunit;

////namespace TransactionQueue.UnitTest.EventHandlers
////{
////    /// <summary>
////    /// This class contains unit test for TransactionPriority
////    /// </summary>

////    public class TransactionPriorityIntegrationEventHandlerUnitTest
////    {
////        /// <summary>
////        /// This event should get data from data bus and store that in DB
////        /// </summary>

////        [Fact]
////        public void ProcessPriorityRequest_ShouldCallInsertPriority()
////        {
////            //Arrange
////            var fakeLog = new Mock<ILogger<TransactionPriorityManager>>();
////            var fakePriorityDAL = new Mock<ITransactionPriorityDAL>();
////            fakePriorityDAL.Setup(x => x.InsertTransactionPriority(It.IsAny<TransactionPriority>()));
////            TransactionPriorityManager manager = new TransactionPriorityManager(fakePriorityDAL.Object, fakeLog.Object);
////            TransactionPriority request = new TransactionPriority();

////            //Act
////            manager.ProcessTransactionPriorityRequest(request);

////            //Assert
////            fakePriorityDAL.Verify(x => x.InsertTransactionPriority(It.IsAny<TransactionPriority>()), Times.Once);
////        }

////        [Fact]
////        public async Task ProcessPriorityRequest_ShouldCallUpdatePriority()
////        {
////            //Arrange
////            var fakeLog = new Mock<ILogger<TransactionPriorityManager>>();
////            var fakePriorityDAL = new Mock<ITransactionPriorityDAL>();
////            TransactionPriority data = new TransactionPriority
////            {
////                TransactionPriorityCode = "PATIENTPICK",
////                IsActive = true,
////                FacilityId = 1,
////                IsAdu = true
////            };
////            fakePriorityDAL.Setup(x => x.GetTransactionPriorityByFacilityIdAndPriorityCode(data.FacilityId, data.TransactionPriorityCode)).ReturnsAsync(data);
////            fakePriorityDAL.Setup(x => x.UpdateTransactionPriority(data));
////            TransactionPriorityManager manager = new TransactionPriorityManager(fakePriorityDAL.Object, fakeLog.Object);

////            //Act
////            manager.ProcessTransactionPriorityRequest(data);

////            //Assert
////            fakePriorityDAL.Verify(x => x.UpdateTransactionPriority(It.IsAny<TransactionPriority>()), Times.Once);
//        }
//    }
//}
