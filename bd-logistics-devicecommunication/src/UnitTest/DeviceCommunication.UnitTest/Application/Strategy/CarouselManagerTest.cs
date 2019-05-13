using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Logistics.Services.DeviceCommunication.API.DeviceInterface;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Factory;
using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using Logistics.Services.DeviceCommunication.API.Application.Strategy;
using Logistics.Services.DeviceCommunication.API.Utilities;
using System.Threading.Tasks;

namespace DeviceCommunication.UnitTest.Application.Strategy
{
    public class CarouselManagerTest
    {
        #region Field

        public Mock<ICarouselFactory> _carouselFactory = null;
        public Mock<ILogger<CarouselManager>> _logger;
        public Mock<IIPSocket> _socket;
        public ICarousel _carousel = null;
        public Mock<IDeviceResponse> _deviceResponse;
        public IConfiguration _configuration;

        #endregion

        #region Constructor

        public CarouselManagerTest()
        {
            _carouselFactory = new Mock<ICarouselFactory>();
            _logger = new Mock<ILogger<CarouselManager>>();
            _socket = new Mock<IIPSocket>();
            _deviceResponse = new Mock<IDeviceResponse>();
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        #endregion

        #region TestCases

        [Fact]
        public async Task CreateCarousel_TransactionQueue_shouldCreateWhiteIPCDualAccessCarousel_Positive()
        {
            //Arrange
            TransactionData objTransactionData = CreateTransactionQueueData();
            Device _carouselData = objTransactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            _carousel = new CarWhiteIpcDualAccess(controlType: ControllerType.WhiteIPCDualAccess, ipAddr: _carouselData.Attribute.IPAddress, carAddr: _carouselData.Attribute.DeviceNumber,
                                                               port: _carouselData.Attribute.Port, timeOut: 10, returnsStatus: true, deviceResponse: _deviceResponse.Object, socket: _socket.Object);

            _carouselFactory.Setup(x => x.GetCarouselType(_carouselData, It.IsAny<IDeviceResponse>(), It.IsAny<IIPSocket>(), It.IsAny<string>())).Returns(Task.FromResult(_carousel));
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, _carouselFactory.Object, _socket.Object, _deviceResponse.Object);

            //Act
            IDeviceResponse deviceResponse = await objCarouselManager.CreateCarousel(objTransactionData);

            //Assert
            Assert.False(deviceResponse.HasError);
        }

        [Fact]
        public async Task CreateCarousel_TransactionQueue_shouldCreateWhiteIPCDualAccessCarousel_Negative()
        {
            //Arrange
            TransactionData objTransactionData = CreateTransactionQueueData();
            Device _carouselData = objTransactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            _carousel = null;
            IDeviceResponse divDeviceResponse = new DeviceResponse();
            _carouselFactory.Setup(x => x.GetCarouselType(_carouselData, It.IsAny<IDeviceResponse>(), It.IsAny<IIPSocket>(), It.IsAny<string>())).Returns(Task.FromResult(_carousel));
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, _carouselFactory.Object, _socket.Object, divDeviceResponse);

            //Act
            IDeviceResponse deviceResponse = await objCarouselManager.CreateCarousel(objTransactionData);

            //Assert
            Assert.True(deviceResponse.HasError);
        }

        [Fact]
        public async Task CreateCarousel_TransactionQueue_shouldCreateCarWhiteTB1470Carousel_Positive()
        {
            //Arrange
            TransactionData objTransactionData = CreateTransactionQueueData();
            Device _carouselData = objTransactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            _carousel = new CarWhiteTB1470H(controlType: ControllerType.WhiteTB1470, ipAddr: _carouselData.Attribute.IPAddress, carAddr: _carouselData.Attribute.DeviceNumber,
                                                               port: _carouselData.Attribute.Port, TimeOut: 10, returnsStatus: true, deviceResponse: _deviceResponse.Object, socket: _socket.Object);

            _carouselFactory.Setup(x => x.GetCarouselType(_carouselData, It.IsAny<IDeviceResponse>(), It.IsAny<IIPSocket>(), It.IsAny<string>())).Returns(Task.FromResult(_carousel));
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, _carouselFactory.Object, _socket.Object, _deviceResponse.Object);

            //Act
            IDeviceResponse deviceResponse = await objCarouselManager.CreateCarousel(objTransactionData);

            //Assert
            Assert.False(deviceResponse.HasError);
        }

        [Fact]
        public async Task CreateCarousel_TransactionQueue_shouldCreateCarWhiteTB1470Carousel_Negative()
        {
            //Arrange
            TransactionData objTransactionData = CreateTransactionQueueData();
            Device _carouselData = objTransactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            _carousel = null;
            IDeviceResponse divDeviceResponse = new DeviceResponse();
            _carouselFactory.Setup(x => x.GetCarouselType(_carouselData, It.IsAny<IDeviceResponse>(), It.IsAny<IIPSocket>(), It.IsAny<string>())).Returns(Task.FromResult(_carousel));
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, _carouselFactory.Object, _socket.Object, divDeviceResponse);

            //Act
            IDeviceResponse deviceResponse = await objCarouselManager.CreateCarousel(objTransactionData);

            //Assert
            Assert.True(deviceResponse.HasError);
        }

        [Fact]
        public async Task MoveCarouse__TransactionQueue_DeviceResponse_Positive()
        {
            //Assume
            IDeviceResponse expected = new DeviceResponse() { HasError = false };
            CarouselFactory carouselFactory = new CarouselFactory();
            Utility utility = new Utility();
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, carouselFactory, _socket.Object, _deviceResponse.Object);

            TransactionData data = CreateTransactionQueueData();
            await objCarouselManager.CreateCarousel(data);
            Slot slot = await utility.BuildStorageSpaceItem(data.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"))?.StorageSpaces);

            //Act
            IDeviceResponse actual = await objCarouselManager.MoveCarousel(data, slot);

            //Assert
            Assert.Equal(expected.HasError, actual.HasError);
        }

        [Fact]
        public async Task MoveCarouse__TransactionQueue_DeviceResponse_WhiteTB1470_2300_Positive()
        {
            //Assume
            IDeviceResponse expected = new DeviceResponse() { HasError = false };
            CarouselFactory carouselFactory = new CarouselFactory();
            Utility utility = new Utility();
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, carouselFactory, _socket.Object, _deviceResponse.Object);

            TransactionData data = CreateTransactionQueueData();
            Device device = data.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            device.Attribute.DeviceClass = ControllerType.WhiteTB1470_2300.ToString().ToUpper();
            await objCarouselManager.CreateCarousel(data);
            Slot slot = await utility.BuildStorageSpaceItem(device.StorageSpaces);

            //Act
            IDeviceResponse actual = await objCarouselManager.MoveCarousel(data, slot);

            //Assert
            Assert.Equal(expected.HasError, actual.HasError);
        }

        [Fact]
        public async Task MoveCarouse__TransactionQueue_DeviceResponse_CarWhiteTB1470_Positive()
        {
            //Assume
            IDeviceResponse expected = new DeviceResponse() { HasError = false };
            CarouselFactory carouselFactory = new CarouselFactory();
            Utility utility = new Utility();
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, carouselFactory, _socket.Object, _deviceResponse.Object);

            TransactionData data = CreateTransactionQueueData();
            Device device = data.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            device.Attribute.DeviceClass = ControllerType.WhiteTB1470.ToString().ToUpper();
            await objCarouselManager.CreateCarousel(data);
            Slot slot = await utility.BuildStorageSpaceItem(device.StorageSpaces);

            //Act
            IDeviceResponse actual = await objCarouselManager.MoveCarousel(data, slot);

            //Assert
            Assert.Equal(expected.HasError, actual.HasError);
        }

        [Fact]
        public async Task MoveCarouse__TransactionQueue_DeviceResponse_CarWhiteTB1470H_Positive()
        {
            //Assume
            IDeviceResponse expected = new DeviceResponse() { HasError = false };
            CarouselFactory carouselFactory = new CarouselFactory();
            Utility utility = new Utility();
            CarouselManager objCarouselManager = new CarouselManager(_configuration, _logger.Object, carouselFactory, _socket.Object, _deviceResponse.Object);

            TransactionData data = CreateTransactionQueueData();
            Device device = data.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            device.Attribute.DeviceClass = ControllerType.WhiteTB1470H.ToString().ToUpper();
            await objCarouselManager.CreateCarousel(data);
            Slot slot = await utility.BuildStorageSpaceItem(device.StorageSpaces);

            //Act
            IDeviceResponse actual = await objCarouselManager.MoveCarousel(data, slot);

            //Assert
            Assert.Equal(expected.HasError, actual.HasError);
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






