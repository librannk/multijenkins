//using Microsoft.Extensions.Logging;
//using Moq;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TransactionQueue.API.Application.Models;
//using TransactionQueue.UnitTest.Common;
//using Xunit;

//namespace TransactionQueue.UnitTest.BussinessLayer
//{
//    /// <summary>
//    /// This class contains  unit-tests for Adu Transaction service </summary>
//    /// </summary>
//    public class AduTransactionManagerTest
//    {
//        #region Private Fields
//        private readonly AduTransactionManager _aduTransactionManager;
//        private readonly Mock<ILogger<AduTransactionManager>> _logger;
//        private readonly Mock<ITransactionQueueDAL> _mockTransactionQueueDAL;
//        private readonly Mock<ITransactionPriorityDAL> _mockTransactionPriorityDAL;
//        private readonly Mock<ILastAduXrefDal> _mockLastAduXrefDal;

//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Initializes the private fields
//        /// </summary>
//        public AduTransactionManagerTest()
//        {
//            _logger = new Mock<ILogger<AduTransactionManager>>();
//            _mockTransactionQueueDAL = new Mock<ITransactionQueueDAL>();
//            _mockTransactionPriorityDAL = new Mock<ITransactionPriorityDAL>();
//            _mockLastAduXrefDal = new Mock<ILastAduXrefDal>();

//            _aduTransactionManager = new AduTransactionManager(
//                 _mockTransactionQueueDAL.Object, _logger.Object,
//                 _mockTransactionPriorityDAL.Object, _mockLastAduXrefDal.Object);
//        }

//        #endregion

//        #region Test Methods

//        /// <summary>
//        /// PyxisLoad priority incoming request should return false.
//        /// </summary>
//        [Fact]
//        public async Task PyxisLoadPriorityIncomingRequest_Should_Return_False()
//        {
//            var request = CreateRequest();
//            var newTransaction = TransactionQueueChildObject.TransactionWithPendingStatusAndRefillPriority;
//            var priority = TransactionPriorityChildObject.PyxisLoadPriority;
//            var item = ItemChildObject.Norepinephrine;
//            _mockTransactionQueueDAL.Setup(x => x.GetAllTransactions())
//                .ReturnsAsync(new List<TransactionQueueModel> {
//                    TransactionQueueChildObject.TransactionWithPendingStatusAndCritalLowPriority
//                });

//            var result = await _aduTransactionManager.ProcessAduTransaction(request, newTransaction, item, priority, new API.Application.Entities.Facility());
//            Assert.False(result);
//        }

//        /// <summary>
//        /// Adu Refill priority incomingrequest should return true for duplicate transanction 
//        /// if critical low or stockout transaction exists in database  for same Facility, Item and destination
//        /// </summary>
//        [Fact]
//        public async Task AduRefillIncomingRequest_Should_Return_True_ForDuplicateTransanction()
//        {
//            var request = CreateRequest();
//            var newTransaction = TransactionQueueChildObject.TransactionWithPendingStatusAndCritalLowPriority;
//            var priority = TransactionPriorityChildObject.RefillPriority;
//            var item = ItemChildObject.Norepinephrine;
//            var facility = FacilityChildObject.Fortis;

//            _mockTransactionQueueDAL.Setup(x => x.GetAllTransactions())
//                .ReturnsAsync(new List<TransactionQueueModel> {
//                    TransactionQueueChildObject.TransactionWithPendingStatusAndCritalLowPriority
//                });

//            _mockTransactionQueueDAL.Setup(x => x.UpdateTransaction(newTransaction));
//            _mockTransactionPriorityDAL.Setup(x => x.GetTransactionPriority(newTransaction.TranPriorityId)).
//             ReturnsAsync(TransactionPriorityChildObject.PyxisCritLowPriority);

//            var result = await _aduTransactionManager.ProcessAduTransaction(request, newTransaction, item, priority, facility);

//            Assert.True(result);
//        }

//        /// <summary>
//        /// Adu CriticalLow priority incomingrequest should return true for duplicate transanction 
//        /// if refill transaction exists in database  for same Facility, Item and destination
//        /// </summary>
//        [Fact]
//        public async Task AduCriticalLowIncomingRequest_Should_Return_True_ForDuplicateTransanction()
//        {
//            var request = CreateRequest();
//            var newTransaction = TransactionQueueChildObject.TransactionWithPendingStatusAndRefillPriority;
//            var priority = TransactionPriorityChildObject.PyxisCritLowPriority;
//            var item = ItemChildObject.Norepinephrine;
//            var facility = FacilityChildObject.Fortis;

//            _mockTransactionQueueDAL.Setup(x => x.GetAllTransactions())
//                .ReturnsAsync(new List<TransactionQueueModel> {
//                    TransactionQueueChildObject.TransactionWithPendingStatusAndRefillPriority
//                });

//            _mockTransactionQueueDAL.Setup(x => x.UpdateTransaction(newTransaction));
//            _mockTransactionPriorityDAL.Setup(x => x.GetTransactionPriority(newTransaction.TranPriorityId)).
//             ReturnsAsync(TransactionPriorityChildObject.RefillPriority);

//            var result = await _aduTransactionManager.ProcessAduTransaction(request, newTransaction, item, priority, facility);

//            Assert.True(result);
//        }

//        /// <summary>
//        /// Adu Stockout priority incomingrequest should return true for duplicate transanction 
//        /// if refill transaction exists in database  for same Facility, Item and destination
//        /// </summary>
//        [Fact]
//        public async Task AduStockOutIncomingRequest_Should_Return_True_ForDuplicateTransanction()
//        {
//            var request = CreateRequest();
//            var newTransaction = TransactionQueueChildObject.TransactionWithPendingStatusAndRefillPriority;
//            var priority = TransactionPriorityChildObject.PyxisStockOutPriority;
//            var item = ItemChildObject.Norepinephrine;
//            var facility = FacilityChildObject.Fortis;

//            _mockTransactionQueueDAL.Setup(x => x.GetAllTransactions())
//                .ReturnsAsync(new List<TransactionQueueModel> {
//                    TransactionQueueChildObject.TransactionWithPendingStatusAndRefillPriority
//                });

//            _mockTransactionQueueDAL.Setup(x => x.UpdateTransaction(newTransaction));
//            _mockTransactionPriorityDAL.Setup(x => x.GetTransactionPriority(newTransaction.TranPriorityId)).
//             ReturnsAsync(TransactionPriorityChildObject.RefillPriority);

//            var result = await _aduTransactionManager.ProcessAduTransaction(request, newTransaction, item, priority, facility);

//            Assert.True(result);
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
//            model.Status = TransactionStatus.Active.ToString();
//            model.Priority = Priority.PYXISREFILL.ToString();
//            model.Facility = new API.Application.Models.Facility()
//            {
//                FacilityId = 1,
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