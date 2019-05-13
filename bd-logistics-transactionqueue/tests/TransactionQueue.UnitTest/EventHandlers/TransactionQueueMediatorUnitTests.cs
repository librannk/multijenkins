//using Microsoft.Extensions.Logging;
//using Moq;
//using System.Collections.Generic;
//using TransactionQueue.API.Application.BussinessLayer.Abstraction;
//using TransactionQueue.API.Application.Models;
//using TransactionQueue.API.IntegrationEvents.EventHandling;
//using TransactionQueue.API.IntegrationEvents.Events;
//using Xunit;

//namespace TransactionQueue.UnitTest.EventHandlers
//{
//    /// <summary>
//    /// This class contains unit-tests for TransactionQueueMediator Event Handler </summary>
//    /// </summary>
//    public class TransactionQueueMediatorUnitTests
//    {
//        /// <summary>
//        /// This event should get data from event bus and 
//        /// validates whether ProcessTransactionRequest method executed or not.
//        /// </summary>
//        [Fact]
//        public void TransactionQueueMediatorUnitTest()
//        {
//            var transactionQueueManager = new Mock<ITransactionQueueManager>();
//            var logger = new Mock<ILogger<TransactionQueueMediator>>();

//            TransactionQueueMediator transactionQueueMediator = new TransactionQueueMediator(transactionQueueManager.Object, logger.Object);
//            var @event = new TransactionQueueAddedIntegrationEvent
//            {
//                Message = new TransactionRequest()
//            };

//            transactionQueueMediator.Execute(@event);
//            transactionQueueManager.Verify(x => x.ProcessTransactionRequest(It.IsAny<TransactionRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
//        }
//    }
//}