//using BD.Core.EventBus.Events;
//using Microsoft.Extensions.Logging;
//using Moq;
//using TransactionQueue.API.Application.Models;
//using TransactionQueue.API.IntegrationEvents.EventHandling;
//using TransactionQueue.API.IntegrationEvents.Events;
//using Xunit;

//namespace TransactionQueue.UnitTest.EventHandlers
//{
//    /// <summary>
//    /// This class contains unit-tests for TransactionQueueAddedIntegration Event Handler </summary>
//    /// </summary>
//    public class TransactionQueueAddedIntegrationEventHandlerUnitTests
//    {
//        /// <summary>
//        /// This event should get data from event bus and execute corresponding method of event class.
//        /// </summary>
//        [Fact]
//        public async void TransactionQueueAddedIntegratedEventHandlerUnitTest()
//        {
//            var mediator = new Mock<IMediator>();
//            var logger = new Mock<ILogger<TransactionQueueMediator>>();

//            var handler = new TransactionQueueAddedIntegrationEventHandler(mediator.Object, logger.Object);
//            var @event = new TransactionQueueAddedIntegrationEvent
//            {
//                Message = new TransactionRequest()
//            };
//            await handler.Handle(@event);
//            mediator.Verify(x => x.Execute(It.IsAny<Event>()), Times.Once);
//        }
//    }
//}