//using Microsoft.Extensions.Logging;
//using Moq;
//using System.Collections.Generic;
//using TransactionQueue.UnitTest.Common;
//using Xunit;
//using TransactionQueue.API.Application.Models.Enums;
//using TransactionQueue.API.BussinessLayer.Concrete;
//using TransactionQueue.API.Application.DataLayer.Abstraction;
//using TransactionQueue.API.Application.Entities;

//namespace TransactionQueue.UnitTest.BussinessLayer
//{
//    /// <summary>
//    /// This class contains  unit-tests for FormularyLocationManager service </summary>
//    /// </summary>
//    public class FormularyLocationManagerTest
//    {
//        #region Private Fields
//        private readonly Mock<ILogger<FormularyLocationManager>> _logger;
//        private readonly Mock<ITransactionQueueDAL> _mockTransactionQueueDal;
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Initializes the private fields
//        /// </summary>
//        public FormularyLocationManagerTest()
//        {
//            _logger = new Mock<ILogger<FormularyLocationManager>>();
//            _mockTransactionQueueDal = new Mock<ITransactionQueueDAL>();
//        }

//        #endregion

//        #region Test Methods

//        /// <summary>
//        /// Validates whether TransactionQueueDetail updated with storage location detail or not.
//        /// </summary>
//        [Fact]
//        public void UpdateTransactionQueueDetailWithStorageDetail()
//        {
//            var transactionQueueId = "ac12ds324o2901234";
//            var storageLocation = StorageLocationChildObject.StorageLocations;
//            var location = "-02";

//            _mockTransactionQueueDal.Setup(x => x.UpdateTransactionWithStorageDetails(transactionQueueId, It.IsAny<TransactionStatus>(), location, It.IsAny<List<Device>>()));

//            var formularyLocationManager = new FormularyLocationManager(_mockTransactionQueueDal.Object, _logger.Object);
//            formularyLocationManager.UpdateTransactionWithStorageDetails(transactionQueueId, storageLocation);
//            _mockTransactionQueueDal.Verify(x => x.UpdateTransactionWithStorageDetails(transactionQueueId, It.IsAny<TransactionStatus>(), location, It.IsAny<List<Device>>()));
//        }

//        #endregion
//    }
//}

