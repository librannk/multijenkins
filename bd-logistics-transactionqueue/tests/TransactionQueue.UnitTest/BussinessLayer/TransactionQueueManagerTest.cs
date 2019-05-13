//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Moq;
//using System;
//using System.Collections.Generic;
//using TransactionQueue.UnitTest.Common;
//using Xunit;
//using System.Threading.Tasks;
//using TransactionQueue.API.Application.Models;
//using TransactionQueue.API.Application.Entities;
//using BD.Core.EventBus.Events;
//using TransactionQueue.API.Application.BussinessLayer.Concrete;
//using TransactionQueue.API.Application.DataLayer.Abstraction;
//using TransactionQueue.API.DataLayer.Abstraction;
//using BD.Core.EventBus.Abstractions;
//using TransactionQueue.API.Configuration;
//using TransactionQueue.API.Application.Models.Enums;
//using TransactionQueue.API.BussinessLayer.Abstraction;
//using Microsoft.Extensions.Configuration;

//namespace TransactionQueue.UnitTest.BussinessLayer
//{
//    /// <summary>
//    /// This class contains  unit-tests for TransactionQueueManager service </summary>
//    /// </summary>
//    public class TransactionQueueManagerTest
//    {
//        #region Private Fields
//        private readonly Mock<ILogger<TransactionQueueManager>> _logger;
//        private readonly Mock<ITransactionQueueDAL> _mockTransactionQueueDAL;
//        private readonly Mock<IFacilityDAL> _mockFacilityDAL;
//        private readonly Mock<ITransactionPriorityDAL> _mockTransactionPriorityDAL;
//        private readonly Mock<IFormularyDAL> _mockFormularyDAL;
//        private readonly Mock<IAduTransactionManager> _mockAduTransactionManager;
//        private readonly Mock<IDestinationDal> _mockDestinationDal;
//        private readonly Mock<IEventBus> _eventBus;
//        private readonly Mock<IOptions<Configuration>> _option;
//        private readonly Configuration _configuration;
//        private readonly TransactionQueueManager _transactionQueueManager;
//        private readonly IConfiguration configuration;
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Initializes the private fields
//        /// </summary>
//        public TransactionQueueManagerTest()
//        {
//            _eventBus = new Mock<IEventBus>();
//            _logger = new Mock<ILogger<TransactionQueueManager>>();
//            _mockTransactionQueueDAL = new Mock<ITransactionQueueDAL>();
//            _mockFacilityDAL = new Mock<IFacilityDAL>();
//            _mockTransactionPriorityDAL = new Mock<ITransactionPriorityDAL>();
//            _mockFormularyDAL = new Mock<IFormularyDAL>();
//            _mockAduTransactionManager = new Mock<IAduTransactionManager>();
//            _mockDestinationDal = new Mock<IDestinationDal>();
//            _option = new Mock<IOptions<Configuration>>();
//             configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

//            _option.SetupGet(x => x.Value).Returns(new Configuration()
//            {
//                KafkaDeviceTopic = "xyz",
//                KafkaAggregatorTopic = "xyz",
//                KafkaFormularyLocationRequestTopic = "xyz",
//                KafkaFormularyLocationResponseTopic = "xyz"
//            });
//            _transactionQueueManager = new TransactionQueueManager(
//                _eventBus.Object, _mockTransactionQueueDAL.Object,
//                _option.Object, _logger.Object, _mockFacilityDAL.Object,
//                _mockTransactionPriorityDAL.Object, _mockFormularyDAL.Object,
//                _mockAduTransactionManager.Object, _mockDestinationDal.Object,
//                configuration);
//        }

//        #endregion

//        #region Test Methods

//        /// <summary>
//        /// Validates whether TransactionQueueId generated while inseting data into database.
//        /// </summary>
//        [Fact]
//        public async Task TransactionQueueIdCreated()
//        {
//            var request = CreateRequest();

//            _mockTransactionQueueDAL.Setup(x => x.CreateTransaction(It.IsAny<TransactionQueueModel>())).ReturnsAsync("111");
//            _mockTransactionPriorityDAL.Setup(x => x.GetTransactionPriorityByFacilityIdAndPriorityCode(It.IsAny<Int32>(), It.IsAny<string>())).
//                ReturnsAsync(new TransactionPriority()
//                {
//                    TransactionPriorityId = 123,
//                    TransactionPriorityCode = "STAT",
//                    IsActive = true,
//                    IsAdu = false
//                });
//            _mockDestinationDal.Setup(x => x.GetDestinationByCode(It.IsAny<string>())).ReturnsAsync(new Destination()
//                {
//                    ADUIgnoreCritLow = true,
//                    ADUIgnoreStockOut = true,
//                    AduQtyRounding = true
//                });
//            _mockFacilityDAL.Setup(x => x.GetFacilityById(It.IsAny<int>()))
//                .ReturnsAsync(new TransactionQueue.API.Application.Entities.Facility()
//                {
//                    Id = 123,
//                    ProcessInactiveAsException = true
//                });
//            _eventBus.Setup(x => x.Publish(It.IsAny<string>(), It.IsAny<Event>(), It.IsAny<Dictionary<string, string>>()));
//            _option.SetupGet(x => x.Value).Returns(_configuration);
//            await _transactionQueueManager.ProcessTransactionRequest(request, new Dictionary<string, string>());
//            _mockTransactionQueueDAL.Verify(x => x.CreateTransaction(It.IsAny<TransactionQueueModel>()), Times.AtLeast(2));
//        }

//        /// <summary>
//        /// Should publish storage location request to formulary location service.
//        /// </summary>
//        [Fact]
//        public async Task TransactionQueuePublishData()
//        {
//            var request = CreateRequest();

//            _mockTransactionQueueDAL.Setup(x => x.CreateTransaction(It.IsAny<TransactionQueueModel>())).ReturnsAsync("111");
//            _mockTransactionPriorityDAL.Setup(x => x.GetTransactionPriorityByFacilityIdAndPriorityCode(It.IsAny<Int32>(), It.IsAny<string>()))
//                .ReturnsAsync(new TransactionPriority()
//                {
//                    TransactionPriorityId = 123,
//                    TransactionPriorityCode = "Stat",
//                    IsActive = true,
//                    IsAdu = false
//                });
//            _mockFacilityDAL.Setup(x => x.GetFacilityById(It.IsAny<int>()))
//                .ReturnsAsync(new TransactionQueue.API.Application.Entities.Facility()
//                {
//                    Id = 123,
//                    ProcessInactiveAsException = true,
//                    AduIgnoreCritLow = true,
//                    AduIgnoreStockout = true,
//                    StorageSpaces = new List<FacilityStorageSpace>{ new FacilityStorageSpace
//                    {
//                        IsDefault = true
//                    } }
//                });
//            _mockFormularyDAL.Setup(x => x.GetFormularyByItemId(It.IsAny<int>()))
//                .ReturnsAsync(new Formulary()
//                {
//                    FormularyId = 123,
//                    Description = "xyz",
//                    IsActive = true,
//                    FacilityFormulary = new FacilityFormulary()
//                    {
//                        FacilityId = 123,
//                        Approved = true,
//                        AduIgnoreCritLow = true,
//                        AduIgnoreStockout = true
//                    }
//                });

//            var storageSpace = new FacilityStorageSpace()
//            {
//                IsDefault = true
//            };
//            _eventBus.Setup(x => x.Publish(It.IsAny<string>(), It.IsAny<Event>(), It.IsAny<Dictionary<string, string>>()));
//            _option.SetupGet(x => x.Value).Returns(_configuration);
//            await _transactionQueueManager.ProcessTransactionRequest(request, new Dictionary<string, string>());
//            _eventBus.Verify(x => x.Publish(It.IsAny<string>(), It.IsAny<Event>(), It.IsAny<Dictionary<string, string>>()), Times.AtLeastOnce);
//        }

//        /// <summary>
//        /// Validates the storage location of the activate transaction
//        /// </summary>
//        [Fact]
//        public async Task ActivateTransaction_ActiveStatus_ShouldReturnStorageLocation()
//        {
//            string transactionQueueId = "ac12ds324o2901234";
//            var expectedResult = TransactionQueueChildObject.TransactionQueueModel;
//            _mockTransactionQueueDAL.Setup(x => x.GetTransactionDetails(transactionQueueId)).ReturnsAsync(expectedResult);
//            _option.SetupGet(x => x.Value).Returns(_configuration);

//            var transactionQueueModel = await _transactionQueueManager.UpdateTransactionStatus(transactionQueueId, TransactionStatus.Active, new Dictionary<string, string>());
//            Assert.Equal(expectedResult.Devices, transactionQueueModel.Devices);
//        }

//        #endregion

//        #region private method

//        /// <summary>
//        /// Mock Incoming request
//        /// </summary>
//        /// <returns></returns>
//        private TransactionRequest CreateRequest()
//        {
//            TransactionRequest model = new TransactionRequest();
//            model.RequestId = "13";
//            model.Status = "active";
//            model.Priority = "High";
//            model.Facility = new TransactionQueue.API.Application.Models.Facility()
//            {
//                FacilityId = 123,
//                OrderingFacility = "OrderingFacility"
//            };
//            model.Patient = new Patient()
//            {
//                FirstName = "PatientFirstname",
//                MiddleName = "PatientMiddlename",
//                LastName = "PatientLastname",
//                Suffix = "PatientSuffix",
//                Gender = "PatientSex",
//                DateOfBirth = "01-01-2019",
//                AccountNumber = "PatientAccountNumber",
//                ContactNo = "1234567890",
//                Bed = "100",
//                Room = "10",
//                VisitId = "VisitId",
//                Mrn = "PatientMrn",
//                PrescriptionNo = "1",
//                Dept = "dept",
//                DeliverToLocation = "Fortis"
//            };
//            model.Order = new Order()
//            {
//                OrderNo = "10",
//                CopeOrderNo = "21",
//                OrderControlId = "MedicationSystem",
//                IsStatOrder = "true",
//                OrderingPriority = "",
//                OrderingDrInstructions = "OrderingDrInstructions",
//                OrderingDueTime = ""
//            };
//            model.ADU = new ADU()
//            {
//                AduTransId = "10",
//                StationName = "StationName",
//                Drawer = "",
//                Subdrawer = "",
//                Pocket = ""
//            };
//            model.UserDef = new UserDef()
//            {
//                UsrDef1 = "",
//                UsrDef2 = "",
//                UsrDef3 = "",
//                UsrDef4 = "",
//                UsrDef5 = "",
//            };

//            var listItem = new List<Item>();
//            listItem.Add(new Item
//            {
//                ItemId = 45738,
//                ItemName = "oxcarbazepine (TRILEPTAL) 300 mg tab(s)",
//                ComponentType = "M",
//                ComponentStrength = "300",
//                ComponentStrengthUnits = "MG",
//                ComponentAmount = "1",
//                OrderAmount = "1",
//                DispenseUnits = "TAB",
//                SupplementaryCode = "TRILEPTAL",
//                TotalDose = "900 MG",
//                Volume = "100",
//                Strength = "1",
//                Concentration = "Concentration",
//                PharmacySpecialDispInstructions = "no",
//                DispenseAmount = "90",
//            });
//            listItem.Add(new Item
//            {
//                ItemId = 48130,
//                ItemName = "oxcarbazepine (TRILEPTAL) 300 mg tab(s)",
//                ComponentType = "M",
//                ComponentStrength = "300",
//                ComponentStrengthUnits = "MG",
//                ComponentAmount = "1",
//                OrderAmount = "1",
//                DispenseUnits = "TAB",
//                SupplementaryCode = "TRILEPTAL",
//                TotalDose = "900 MG",
//                Volume = "100",
//                Strength = "1",
//                Concentration = "Concentration",
//                PharmacySpecialDispInstructions = "no",
//                DispenseAmount = "90",
//            });
//            model.Items = listItem;
//            return model;
//        }
//        #endregion
//    }
//}
