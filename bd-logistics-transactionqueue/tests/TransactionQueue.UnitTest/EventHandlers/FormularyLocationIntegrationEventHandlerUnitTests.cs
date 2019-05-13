//using Microsoft.Extensions.Logging;
//using Moq;
//using System.Collections.Generic;
//using TransactionQueue.API.Application.Entities;
//using TransactionQueue.API.BussinessLayer.Abstraction;
//using TransactionQueue.API.IntegrationEvents.EventHandling;
//using TransactionQueue.API.IntegrationEvents.Events;
//using Xunit;

//namespace TransactionQueue.UnitTest.EventHandlers
//{
//    /// <summary>
//    /// This class contains unit-tests for FormularyLocation Event Handler </summary>
//    /// </summary>
//    public class FormularyLocationIntegrationEventHandlerUnitTests
//    {
//        /// <summary>
//        /// This event should get data from event bus and pass to be formulary location service to update location.
//        /// </summary>
//        [Fact]
//        public async void UpdateTransactionWithStorageLocation()
//        {
//            var formularyLocationManager = new Mock<IFormularyLocationManager>();
//            var logger = new Mock<ILogger<FormularyLocationIntegrationEventHandler>>();
//            var handler = new FormularyLocationIntegrationEventHandler(formularyLocationManager.Object, logger.Object);
//            var storageSpace = new Device()
//            {
//                Type = "xyz",
//                DeviceId = 123,
//                IsDefault = true,
//            };
//            List<Device> list = new List<Device>();
//            list.Add(storageSpace);
//            var @event = new FormularyLocationIntegrationEvent()
//            {
//                TransactionQueueId = "xyz",
//                Devices = list
//            };

//            await handler.Handle(@event);
//            formularyLocationManager.Verify(x => x.UpdateTransactionWithStorageDetails(It.IsAny<string>(), It.IsAny<List<Device>>()), Times.Once);
//        }
//    }
//}
