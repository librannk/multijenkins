//using Moq;
//using TransactionQueue.UnitTest.Common;
//using Xunit;
//using System.Threading.Tasks;
//using AutoMapper;
//using TransactionQueue.API.Infrastructure.Repository.Interfaces;
//using TransactionQueue.API.Application.Models.Enums;
//using TransactionQueue.API.Application.DataLayer;
//using TransactionQueue.API.Application.DataLayer.Abstraction;
//using TransactionQueue.API.Application.Entities;

//namespace TransactionQueue.UnitTest.DataLayer
//{
//    /// <summary>
//    /// This class contains  unit-tests for TransactionQueueDal layer </summary>
//    /// </summary>
//    public class TransactionQueueDalTest
//    {
//        #region Private Fields
//        private readonly Mock<ITransactionQueueMongoRepository> _mockMongoRepository;
//        private readonly IMapper _mapper;
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Initializes the private fields - TransactionQueueMongoRepository
//        /// </summary>
//        public TransactionQueueDalTest()
//        {
//            _mockMongoRepository = new Mock<ITransactionQueueMongoRepository>();
//            var mockMapper = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new TransactionQueue.API.AutoMapper.MapProfile());
//            });
//            _mapper = mockMapper.CreateMapper();
//        }

//        #endregion

//        #region Test Methods

//        /// <summary>
//        /// Validates the storage location of the activate transaction
//        /// </summary>
//        [Fact]
//        public async Task ActivateTransaction_Should_ActivateTransactionAndReturnStorageLocation()
//        {
//            var transactionQueueId = "507f191e810c19729de860ea";
//            var expectedResult = TransactionQueueChildObject.TransactionQueueModel;
//            var mockTransactionQueueMongoCollection = new TransactionQueue.API.Infrastructure.DBModel.TransactionQueue
//            {
//                Devices = expectedResult.Devices,
//                Status = TransactionStatus.Active.ToString(),
//                Type = TransactionType.Pick.ToString()
//            };

//            _mockMongoRepository.Setup(x => x.GetByIdAsync(transactionQueueId)).ReturnsAsync(mockTransactionQueueMongoCollection);
//            _mockMongoRepository.Setup(x => x.UpdateAsync(mockTransactionQueueMongoCollection)).Returns(Task.CompletedTask);

//            ITransactionQueueDAL transactionQueueDal = new TransactionQueueDAL(_mockMongoRepository.Object, _mapper);
//            var transactionQueueModel = await transactionQueueDal.UpdateTransactionStatus(transactionQueueId, TransactionStatus.Active);
//            Assert.Equal(expectedResult.Devices, transactionQueueModel.Devices);
//        }

//        /// <summary>
//        ///  Validates the transaction queue details against transactionqueue id
//        /// </summary>
//        [Fact]
//        public async Task GetTransactionDetail_ShouldReturnTransactionQueueDetails()
//        {
//            var transactionQueueId = "507f191e810c19729de860ea";
//            var expectedStorageLocation = StorageLocationChildObject.StorageLocations;
//            var mockTransactionQueueMongoCollection = new TransactionQueue.API.Infrastructure.DBModel.TransactionQueue
//            {
//                Id = transactionQueueId,
//                Devices = expectedStorageLocation,
//                Status = TransactionStatus.Pending.ToString(),
//                Type = TransactionType.Pick.ToString()
//            };

//            _mockMongoRepository.Setup(x => x.GetByIdAsync(transactionQueueId)).ReturnsAsync(mockTransactionQueueMongoCollection);

//            ITransactionQueueDAL transactionQueueDal = new TransactionQueueDAL(_mockMongoRepository.Object,_mapper);
//            var transactionQueueModel = await transactionQueueDal.GetTransactionDetails(transactionQueueId);
//            Assert.Equal(transactionQueueId, transactionQueueModel.TransactionQueueId);
//            Assert.Equal(expectedStorageLocation, transactionQueueModel.Devices);
//        }

//        /// <summary>
//        /// This method should return transaction queue id
//        /// </summary>
//        [Fact]
//        public async Task CreateTransaction_ShouldReturnTransactionQueueId()
//        {
//            var transactionQueueId = "507f191e810c19729de860ea";
//            var request = new TransactionQueueModel
//            {
//                TransactionQueueId = transactionQueueId,
//                Status = TransactionStatus.Pending,
//                Devices = StorageLocationChildObject.StorageLocations
//            };

//            ITransactionQueueDAL transactionQueueDal = new TransactionQueueDAL(_mockMongoRepository.Object, _mapper);
//            var resultTransactionQueueId = await transactionQueueDal.CreateTransaction(request);
//            Assert.Equal(transactionQueueId,resultTransactionQueueId);
//        }

//        /// <summary>
//        /// Validates whether storage data is updated with storage location or not.
//        /// </summary>
//        [Fact]
//        public void UpdateTransactionQueueDataWithStorageDetails()
//        {
//            var transactionQueueId = "507f191e810c19729de860ea";
//            var storageLocation = StorageLocationChildObject.StorageLocations;

//            var mockTransactionQueueMongoCollection = new TransactionQueue.API.Infrastructure.DBModel.TransactionQueue
//            {
//                Devices = storageLocation
//            };

//            _mockMongoRepository.Setup(x => x.GetByIdAsync(transactionQueueId)).ReturnsAsync(mockTransactionQueueMongoCollection);
//            _mockMongoRepository.Setup(x => x.UpdateAsync(mockTransactionQueueMongoCollection));

//            ITransactionQueueDAL transactionQueueDal = new TransactionQueueDAL(_mockMongoRepository.Object, _mapper);
//            transactionQueueDal.UpdateTransactionWithStorageDetails(transactionQueueId, TransactionStatus.Pending, "-02", storageLocation);

//            _mockMongoRepository.Verify(x => x.UpdateAsync(mockTransactionQueueMongoCollection), Times.Once);
//        }

//        #endregion
//    }
//}

