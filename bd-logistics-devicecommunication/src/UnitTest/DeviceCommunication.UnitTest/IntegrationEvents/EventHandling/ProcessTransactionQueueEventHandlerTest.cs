using Moq;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.EventHandling;

namespace DeviceCommunication.UnitTest
{
    public class ProcessTransactionQueueEventHandlerTest
    {
        #region Private Fields
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<ProcessTransactionQueueEventHandler>> _logger;
        #endregion

        #region Contructor
        /// <summary>
        /// Initialize new instance
        /// </summary>
        public ProcessTransactionQueueEventHandlerTest()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<ProcessTransactionQueueEventHandler>>();
        }
        #endregion

        #region Test cases
        /// <summary>
        /// verify event handler executed successfully 
        /// </summary>
        [Fact]
        public void HandleExecuteSuccess()
        {
            ProcessTransactionQueueEventHandler processTransactionQueueEventHandler = new ProcessTransactionQueueEventHandler(_mediator.Object, _logger.Object);
            ProcessTransactionQueueIntegrationEvent processTransactionQueueIntegrationEvent = new ProcessTransactionQueueIntegrationEvent();
            var device = new Device { DeviceId = 1234, Type = "Carousel", Attribute = new DeviceAttribute(), StorageSpaces = new List<StorageSpace>() };           
            var listDevices = new List<Device>(); listDevices.Add(device);
            processTransactionQueueIntegrationEvent.TransactionData = new TransactionData { Devices = listDevices, Quantity = 1, Type = "R" };
            processTransactionQueueIntegrationEvent.Headers = new Dictionary<string, string>();
            var result = processTransactionQueueEventHandler.Handle(processTransactionQueueIntegrationEvent);
            Assert.NotNull(result);
        }

        #endregion
    }
}
