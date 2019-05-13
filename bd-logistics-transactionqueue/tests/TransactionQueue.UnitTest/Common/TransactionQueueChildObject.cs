//using System;
//using TransactionQueue.API.Application.Entities;
//using TransactionQueue.API.Application.Models.Enums;

//namespace TransactionQueue.UnitTest.Common
//{
//    /// <summary>
//    /// This class is responsible for providing transaction queue mock data to test cases
//    /// </summary>
//    public class TransactionQueueChildObject
//    {
//        /// <summary>
//        /// return storage location for transaction
//        /// </summary>
//        public static TransactionQueueModel TransactionQueueModel
//        {
//            get
//            {
//                return new TransactionQueueModel
//                {
//                    TransactionQueueId = Guid.NewGuid().ToString(),
//                    Status = TransactionStatus.Active,
//                    Type = TransactionType.Pick,
//                    Devices = StorageLocationChildObject.StorageLocations
//                };
//            }
//        }

//        /// <summary>
//        /// return Transaction with pending status and refill priority.
//        /// </summary>
//        public static TransactionQueueModel TransactionWithPendingStatusAndRefillPriority
//        {
//            get
//            {
//                return new TransactionQueueModel
//                {
//                    Status = TransactionStatus.Pending,
//                    ItemId = 48130,
//                    FormularyId = 1,
//                    Destination = "Fortis",
//                    FacilityId = 1,
//                    TranPriorityId = 100,
//                    Quantity = 15,
//                    Type = TransactionType.Pick,
//                    ReceivedDt = DateTime.Now
//                };
//            }
//        }

//        /// <summary>
//        /// return Transaction with pending status and critical low priority.
//        /// </summary>
//        public static TransactionQueueModel TransactionWithPendingStatusAndCritalLowPriority
//        {
//            get
//            {
//                return new TransactionQueueModel
//                {
//                    Status = TransactionStatus.Pending,
//                    ItemId = 48130,
//                    FormularyId = 1,
//                    Destination = "Fortis",
//                    FacilityId = 1,
//                    TranPriorityId = 101,
//                    Quantity = 15,
//                    ReceivedDt = DateTime.Now
//                };
//            }
//        }


//        /// <summary>
//        /// return Transaction with pending status and c priority.
//        /// </summary>
//        public static TransactionQueueModel TransactionWithPendingStatusAndStockOutPriority
//        {
//            get
//            {
//                return new TransactionQueueModel
//                {
//                    Status = TransactionStatus.Pending,
//                    ItemId = 48130,
//                    FormularyId = 1,
//                    Destination = "Fortis",
//                    FacilityId = 1,
//                    TranPriorityId = 102,
//                    Quantity = 20,
//                    ReceivedDt = DateTime.Now
//                };
//            }
//        }
//    }
//}