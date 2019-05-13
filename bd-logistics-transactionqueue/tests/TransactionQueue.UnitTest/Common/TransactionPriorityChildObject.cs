//using System;
//using System.Collections.Generic;
//using System.Text;
//using TransactionQueue.API.Application.Entities;
//using TransactionQueue.API.Application.Models.Enums;

//namespace TransactionQueue.UnitTest.Common
//{
//    /// <summary>
//    /// This class is responsible for providing transaction priority mock data to test cases
//    /// </summary>
//    public class TransactionPriorityChildObject
//    {

//        /// <summary>
//        /// Return refill priority.
//        /// </summary>
//        public static TransactionPriority RefillPriority
//        {
//            get
//            {
//                return new TransactionPriority
//                {
//                    IsAdu = true,
//                    TransactionPriorityCode = Priority.PYXISREFILL.ToString(),
//                    TransactionPriorityId = 100
//                };
//            }
//        }

//        /// <summary>
//        /// Return PyxisLoad priority.
//        /// </summary>
//        public static TransactionPriority PyxisLoadPriority
//        {
//            get
//            {
//                return new TransactionPriority
//                {
//                    IsAdu = true,
//                    TransactionPriorityCode = Priority.PYXISLOAD.ToString(),
//                    TransactionPriorityId = 99
//                };
//            }
//        }
//        /// <summary>
//        /// Return PyxisCritLow priority.
//        /// </summary>
//        public static TransactionPriority PyxisCritLowPriority
//        {
//            get
//            {
//                return new TransactionPriority
//                {
//                    IsAdu = true,
//                    TransactionPriorityCode = Priority.PYXISCRITLOW.ToString(),
//                    TransactionPriorityId = 101
//                };
//            }
//        }

//        /// <summary>
//        /// Return PyxisStockOut priority.
//        /// </summary>
//        public static TransactionPriority PyxisStockOutPriority
//        {
//            get
//            {
//                return new TransactionPriority
//                {
//                    IsAdu = true,
//                    TransactionPriorityCode = Priority.PYXISSTOCKOUT.ToString(),
//                    TransactionPriorityId = 102
//                };
//            }
//        }

//    }
//}
