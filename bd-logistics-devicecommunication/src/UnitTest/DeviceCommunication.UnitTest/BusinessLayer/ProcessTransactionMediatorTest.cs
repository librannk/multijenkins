using Moq;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.BusinessLayer;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events;

namespace DeviceCommunication.UnitTest
{
    public class ProcessTransactionMediatorTest
    {
        #region Private Fields

        private readonly Mock<ICarouselProcess> _carouselProcess;
        private readonly Mock<ILogger<ProcessTransactionMediator>> _logger;

        #endregion

        #region Contructor

        /// <summary>
        /// Initialize new instance
        /// </summary>
        public ProcessTransactionMediatorTest()
        {
            _carouselProcess = new Mock<ICarouselProcess>();
            _logger = new Mock<ILogger<ProcessTransactionMediator>>();
        }

        #endregion

        #region TestMethods

        /// <summary>
        /// Execute method should execute successfully
        /// </summary>
        [Fact]
        public void ProcessTransactionMediatorExecuteSuccess()
        {
            //Arrange
            ProcessTransactionQueueIntegrationEvent transQueueEvent = new ProcessTransactionQueueIntegrationEvent();
            var device = new Device() { DeviceId = 123, Type = "Carousel", Attribute = new DeviceAttribute(), StorageSpaces = new List<StorageSpace>() };
            var listDevices = new List<Device>(); listDevices.Add(device);
            transQueueEvent.TransactionData = new TransactionData { Devices = listDevices, Quantity = 1, Type = "R" };
            ProcessTransactionMediator processTransactionMediator = new ProcessTransactionMediator(_logger.Object, _carouselProcess.Object);

            //Act
            processTransactionMediator.Execute(transQueueEvent);

            //Assert
            Assert.True(true);
        }

        #endregion
    }
}
