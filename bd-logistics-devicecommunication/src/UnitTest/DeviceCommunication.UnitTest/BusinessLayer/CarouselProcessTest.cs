using Moq;
using Xunit;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Logistics.Services.DeviceCommunication.API.BusinessLayer;
using Logistics.Services.DeviceCommunication.API.DeviceInterface;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.Application.Strategy;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using System.Threading.Tasks;

namespace DeviceCommunication.UnitTest
{
    public class CarouselProcessTest
    {
        #region Private Fields
        private readonly Mock<ICarouselManager> _carouselManager;
        private readonly Mock<ILogger<CarouselProcess>> _logger;
        private readonly Mock<IDeviceResponse> _deviceResponse;
        private readonly Mock<IUtility> _utility;

        #endregion

        #region Contructor
        /// <summary>
        /// Initialize new instance
        /// </summary>
        public CarouselProcessTest()
        {
            _logger = new Mock<ILogger<CarouselProcess>>();
            _deviceResponse = new Mock<IDeviceResponse>();
            _carouselManager = new Mock<ICarouselManager>();
            _utility = new Mock<IUtility>();
        }

        #endregion

        #region Test cases

        /// <summary>
        /// Verify Error message found at CreateCarousel
        /// </summary>
        [Fact]
        public async Task MoveCarousel_ErrorFound_At_CreateCarousel_DeviceResponse()
        {
            IDeviceResponse deviceResponseAtCreateCarousel = new DeviceResponse() { HasError = true, Message = "Error Found at CreateCarousel." };
            IDeviceResponse deviceResponseMoveCarousel = new DeviceResponse();
            var transactionData = CreateTransactionQueueData();
            _carouselManager.Setup(x => x.CreateCarousel(transactionData)).Returns(Task.FromResult(deviceResponseAtCreateCarousel));
            _carouselManager.Setup(x => x.MoveCarousel(transactionData, new Slot())).Returns(Task.FromResult(deviceResponseMoveCarousel));
            CarouselProcess carouselProcess = new CarouselProcess(_logger.Object, _deviceResponse.Object, _carouselManager.Object, _utility.Object);
            IDeviceResponse deviceResponse = await carouselProcess.MoveCarousel(transactionData);
            Assert.Equal(deviceResponseAtCreateCarousel.HasError, deviceResponse.HasError);
            Assert.Equal(deviceResponseAtCreateCarousel.Message, deviceResponse.Message);
        }

        /// <summary>
        /// Verify Error message found at Move carosuel
        /// </summary>
        [Fact]
        public async Task MoveCarousel_ErrorFound_At_MoveCarousel_DeviceResponse()
        {

            IDeviceResponse deviceResponseAtMoveCarousel = new DeviceResponse() { HasError = true, Message = "Erro Found at MoveCarousel." };
            IDeviceResponse deviceResponseMoveCarousel = new DeviceResponse();
            var transactionData = CreateTransactionQueueData();
            Device device = transactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));

            Slot slot = new Slot();
            _utility.Setup(x => x.BuildStorageSpaceItem(device.StorageSpaces)).Returns(Task.FromResult(slot));
            _carouselManager.Setup(x => x.CreateCarousel(transactionData)).Returns(Task.FromResult(deviceResponseMoveCarousel));
            _carouselManager.Setup(x => x.MoveCarousel(transactionData, slot)).Returns(Task.FromResult(deviceResponseAtMoveCarousel));
            CarouselProcess carouselProcess = new CarouselProcess(_logger.Object, _deviceResponse.Object, _carouselManager.Object, _utility.Object);
            IDeviceResponse deviceResponse = await carouselProcess.MoveCarousel(transactionData);
            Assert.Equal(deviceResponseAtMoveCarousel.HasError, deviceResponse.HasError);
            Assert.Equal(deviceResponseAtMoveCarousel.Message, deviceResponse.Message);
        }

        /// <summary>
        /// Verify expected ConnectionResfused Exception
        /// </summary>
        [Fact]
        public async Task CreateCarousel_ThrowSocketException_ConnectionRefused()
        {
            string expectedErrorMessage = "No connection could be made because the target machine actively refused it";
            int expectedErrorCode = 10061;
            var transactionData = CreateTransactionQueueData();
            Device device = transactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));

            _utility.Setup(x => x.BuildStorageSpaceItem(device.StorageSpaces)).Returns(Task.FromResult(new Slot()));
            _carouselManager.Setup(x => x.CreateCarousel(transactionData)).Throws(new SocketException(expectedErrorCode));
            CarouselProcess carouselProcess = new CarouselProcess(_logger.Object, _deviceResponse.Object, _carouselManager.Object, _utility.Object);
            await carouselProcess.MoveCarousel(transactionData);
            _deviceResponse.VerifySet(x => x.HasError = true);
            _deviceResponse.VerifySet(x => x.ErrorCode = expectedErrorCode);
            _deviceResponse.VerifySet(x => x.Message = expectedErrorMessage);
        }

        /// <summary>
        /// Verify expected Timeout Exception
        /// </summary>
        [Fact]
        public async Task CreateCarousel_ThrowSocketException_Timeout()
        {
            string expectedErrorMessage = "A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond";
            int expectedErrorCode = 10060;
            var transactionData = CreateTransactionQueueData();
            Device device = transactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));

            _utility.Setup(x => x.BuildStorageSpaceItem(device.StorageSpaces)).Returns(Task.FromResult(new Slot()));
            _carouselManager.Setup(x => x.CreateCarousel(transactionData)).Throws(new SocketException(expectedErrorCode));
            CarouselProcess carouselProcess = new CarouselProcess(_logger.Object, _deviceResponse.Object, _carouselManager.Object, _utility.Object);
            await carouselProcess.MoveCarousel(transactionData);
            _deviceResponse.VerifySet(x => x.HasError = true);
            _deviceResponse.VerifySet(x => x.ErrorCode = expectedErrorCode);
            _deviceResponse.VerifySet(x => x.Message = expectedErrorMessage);
        }

        /// <summary>
        /// MoveCarousel_ThrowSocketException_Timeout
        /// </summary>
        [Fact]
        public async Task MoveCarousel_ThrowException()
        {
            //Arrange
            string expectedErrorMessage = "Controller for this storage space is not found";
            var transactionData = CreateTransactionQueueData();
            Device device = transactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            IDeviceResponse devResponse;
            IDeviceResponse deviceResponseCreateCarousel = new DeviceResponse() { HasError = true, Message = expectedErrorMessage };
            _utility.Setup(x => x.BuildStorageSpaceItem(device.StorageSpaces)).Returns(Task.FromResult(new Slot()));
            _carouselManager.Setup(x => x.CreateCarousel(transactionData)).Returns(Task.FromResult(deviceResponseCreateCarousel));
            CarouselProcess carouselProcess = new CarouselProcess(_logger.Object, _deviceResponse.Object, _carouselManager.Object, _utility.Object);

            //Act
            devResponse = await carouselProcess.MoveCarousel(transactionData);

            //Assert
            Assert.True(devResponse.HasError);
            Assert.Equal(devResponse.Message, expectedErrorMessage);
        }

        #endregion

        #region Private Method

        private TransactionData CreateTransactionQueueData()
        {
            StorageSpace spRack = new StorageSpace()
            {
                ItemType = "1",
                Number = 123,
                Attribute = new StorageSpaceAttribute()
                {
                    DispenseForm = "asas",
                    LeftOffset = (decimal)2.4,
                    OverideBaseAddress = 22
                }
            };
            StorageSpace spShelf = new StorageSpace()
            {
                ItemType = "2",
                Number = 123,
                Attribute = new StorageSpaceAttribute()
                {
                    DispenseForm = "asas",
                    LeftOffset = (decimal)2.4,
                    OverideBaseAddress = 22
                }
            };
            StorageSpace spBin = new StorageSpace()
            {
                ItemType = "3",
                Number = 123,
                Attribute = new StorageSpaceAttribute()
                {
                    DispenseForm = "asas",
                    LeftOffset = (decimal)2.4,
                    OverideBaseAddress = 22
                }
            };
            StorageSpace spSlot = new StorageSpace()
            {
                ItemType = "4",
                Number = 123,
                Attribute = new StorageSpaceAttribute()
                {
                    DispenseForm = "asas",
                    LeftOffset = (decimal)2.4,
                    OverideBaseAddress = 22
                }
            };
            Device spCarousel = new Device()
            {
                Type = "Carousel",
                DeviceId = 2234,
                Attribute = new DeviceAttribute()
                {
                    DeviceClass = ControllerType.WhiteIPCDualAccess.ToString().ToUpper(),
                    IPAddress = "172.17.44.209",
                    DeviceNumber = "123",
                    Port = 4001,
                    RestrictControl = false,
                    IsDualAccess = false,
                    MaxRack = 99,
                    ReturnStatus = false,
                    ConnectionResetMinutes = 12,
                },
                StorageSpaces = new List<StorageSpace>()
            };

            spCarousel.StorageSpaces.Add(spSlot);
            spCarousel.StorageSpaces.Add(spBin);
            spCarousel.StorageSpaces.Add(spShelf);
            spCarousel.StorageSpaces.Add(spRack);

            var transactionData = new TransactionData();
            transactionData.Devices = new List<Device>();
            transactionData.Devices.Add(spCarousel);
            transactionData.Devices.Add(new Device()
            {
                Type = "Display",
                DeviceId = 2237,
                Attribute = null,
                StorageSpaces = null
            });
            transactionData.Quantity = 1;
            return transactionData;
        }

        #endregion
    }
}
