//using Microsoft.Extensions.Logging;
//using Moq;
//using Xunit;
//using System.Threading.Tasks;
//using TransactionQueue.API.Application.Entities;
//using TransactionQueue.API.BussinessLayer.Concrete;
//using TransactionQueue.API.DataLayer.Abstraction;

//namespace TransactionQueue.UnitTest.BussinessLayer
//{
//    public class TransactionPriorityManagerTest
//    {
//        #region Private Fields
//        private readonly Mock<ILogger<TransactionPriorityManager>> _logger;
//        private readonly Mock<ITransactionPriorityDAL> _mockTransactionPriorityDAL;
//        private TransactionPriorityManager _transactionPriorityManager;
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Initializes the private fields
//        /// </summary>
//        public TransactionPriorityManagerTest()
//        {
//            _logger = new Mock<ILogger<TransactionPriorityManager>>();
//            _mockTransactionPriorityDAL = new Mock<ITransactionPriorityDAL>();
//        }

//        #endregion

//        #region Test Methods

//        [Fact]
//        public async Task ProcessTransactionPriorityRequest_ShouldCallUpdatePriority()
//        {
//            //Arrange
//            var data = CreateRequest();
//            _mockTransactionPriorityDAL.Setup(x => x.GetTransactionPriorityByFacilityIdAndPriorityCode(data.FacilityId, data.TransactionPriorityCode)).ReturnsAsync(data);
//            _mockTransactionPriorityDAL.Setup(x => x.UpdateTransactionPriority(data));
//            _transactionPriorityManager = new TransactionPriorityManager(_mockTransactionPriorityDAL.Object, _logger.Object);

//            //Act
//            _transactionPriorityManager.ProcessTransactionPriorityRequest(data);

//            //Assert
//            _mockTransactionPriorityDAL.Verify(x => x.UpdateTransactionPriority(data), Times.Once);
//        }

//        /// <summary>
//        /// This event should get data from data bus and store that in DB
//        /// </summary>

//        [Fact]
//        public void ProcessPriorityRequest_ShouldCallInsertPriority()
//        {
//            //Arrange
//            var fakeLog = new Mock<ILogger<TransactionPriorityManager>>();
//            var fakePriorityDAL = new Mock<ITransactionPriorityDAL>();
//            fakePriorityDAL.Setup(x => x.InsertTransactionPriority(It.IsAny<TransactionPriority>()));
//            TransactionPriorityManager manager = new TransactionPriorityManager(fakePriorityDAL.Object, fakeLog.Object);

//            //Act
//            manager.ProcessTransactionPriorityRequest(CreateRequest());

//            //Assert
//            fakePriorityDAL.Verify(x => x.InsertTransactionPriority(It.IsAny<TransactionPriority>()), Times.Once);
//        }

//        #endregion

//        #region private method

//        /// <summary>
//        /// Mock TransactionPriority request
//        /// </summary>
//        /// <returns></returns>
//        private TransactionPriority CreateRequest()
//        {
//            return new TransactionPriority
//            {
//                TransactionPriorityCode = "PATIENTPICK",
//                IsActive = true,
//                FacilityId = 1,
//                IsAdu = true
//            };
//        }
//        #endregion
//    }
//}
