using Xunit;
using System.Linq;
using System.Collections.Generic;
using DeviceCommunication.UnitTest.DeviceInterface.Common;
using Logistics.Services.DeviceCommunication.API.Utilities;
using Logistics.Services.DeviceCommunication.API.DeviceInterface;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using System.Threading.Tasks;

namespace DeviceCommunication.UnitTest
{
    public class CarWhiteIPCDualAccessTest
    {
        #region Field

        private CarWhiteIpcDualAccess _carousel;
        private Device _carouselData;
        private MockIIPSocket _socket;
        private DeviceResponse _deviceResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize the common fields to be used in all the test cases
        /// </summary>
        public CarWhiteIPCDualAccessTest()
        {
            //Default initialization
            _carouselData = CreateTransactionQueueData().Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            _socket = new MockIIPSocket();
            _deviceResponse = new DeviceResponse();

            //Setup mock methods
            _socket.setup_ConnectToServer(_carouselData.Attribute.IPAddress, _carouselData.Attribute.Port, 10);
            _socket.setup_Disconnect();
            _socket.setup_WriteSocket(string.Empty);
            _socket.setup_IsConnected();
        }

        #endregion

        #region Test cases

        /// <summary>
        /// To test the connect functionality
        /// </summary>
        [Fact]
        public void Connect_NoInput_CarouselShouldConnected()
        {
            _carousel = new CarWhiteIpcDualAccess(controlType: ControllerType.WhiteIPCDualAccess, ipAddr: _carouselData.Attribute.IPAddress, carAddr: _carouselData.Attribute.DeviceNumber,
                port: _carouselData.Attribute.Port, timeOut: 10, returnsStatus: true, deviceResponse: _deviceResponse, socket: _socket);

            //Assume
            bool expected = true;

            //Act
            _carousel.Connect();
            bool actual = _carousel.IsConnected;
            _carousel.Disconnect();

            //Assert
            Assert.Equal(expected: expected, actual: actual);
        }

        /// <summary>
        /// To test the move functionality
        /// </summary>
        [Fact]
        public async Task Move_ShelfInfo_CarouselShouldMove()
        {
            _carousel = new CarWhiteIpcDualAccess(controlType: ControllerType.WhiteIPCDualAccess, ipAddr: _carouselData.Attribute.IPAddress, carAddr: _carouselData.Attribute.DeviceNumber,
                port: _carouselData.Attribute.Port, timeOut: 10, returnsStatus: true, deviceResponse: _deviceResponse, socket: _socket);
            _carousel.Connect();

            //Assume
            IDeviceResponse expected = new DeviceResponse()
            {
                HasError = false,
                Message = "Data is written on device successfully.",
                CurrentCarrier = 0
            };

            //Act
            IDeviceResponse actual = await _carousel.Move(shelfNum: _carouselData.StorageSpaces.FirstOrDefault(space => space.ItemType.Equals("2")).Number);
            _carousel.Disconnect();

            //Assert
            Assert.Equal(expected: expected.HasError, actual: actual.HasError);
            Assert.Equal(expected: expected.Message, actual: actual.Message);
            Assert.Equal(expected: expected.CurrentCarrier, actual: actual.CurrentCarrier);
        }

        /// <summary>
        /// To test the move functionality
        /// </summary>
        [Fact]
        public async Task Move_SlotInfo_CarouselShouldMove()
        {
            IUtility utility = new Utility();
            _carousel = new CarWhiteIpcDualAccess(controlType: ControllerType.WhiteIPCDualAccess, ipAddr: _carouselData.Attribute.IPAddress, carAddr: _carouselData.Attribute.DeviceNumber,
                port: _carouselData.Attribute.Port, timeOut: 10, returnsStatus: true, deviceResponse: _deviceResponse, socket: _socket);
            _carousel.Connect();

            //Assume
            IDeviceResponse expected = new DeviceResponse()
            {
                HasError = false,
                Message = "Data is written on device successfully.",
                CurrentCarrier = 0
            };

            //Act
            IDeviceResponse actual = await _carousel.Move(slot: await utility.BuildStorageSpaceItem(_carouselData.StorageSpaces));
            _carousel.Disconnect();

            //Assert
            Assert.Equal(expected: expected.HasError, actual: actual.HasError);
            Assert.Equal(expected: expected.Message, actual: actual.Message);
            Assert.Equal(expected: expected.CurrentCarrier, actual: actual.CurrentCarrier);
        }

        /// <summary>
        /// To test the move functionality
        /// </summary>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Move_SlotInfoAndComputerInfo_CarouselShouldMove(bool isOutsideComputer)
        {
            IUtility utility = new Utility();
            _carousel = new CarWhiteIpcDualAccess(controlType: ControllerType.WhiteIPCDualAccess, ipAddr: _carouselData.Attribute.IPAddress, carAddr: _carouselData.Attribute.DeviceNumber,
                port: _carouselData.Attribute.Port, timeOut: 10, returnsStatus: true, deviceResponse: _deviceResponse, socket: _socket);
            _carousel.Connect();

            //Assume
            IDeviceResponse expected = new DeviceResponse()
            {
                HasError = false,
                Message = "Data is written on device successfully.",
                CurrentCarrier = 0
            };

            //Act
            IDeviceResponse actual = await _carousel.Move(slot: await utility.BuildStorageSpaceItem(_carouselData.StorageSpaces), isOutsideComputer: isOutsideComputer);
            _carousel.Disconnect();

            //Assert
            Assert.Equal(expected: expected.HasError, actual: actual.HasError);
            Assert.Equal(expected: expected.Message, actual: actual.Message);
            Assert.Equal(expected: expected.CurrentCarrier, actual: actual.CurrentCarrier);
        }

        /// <summary>
        /// To test the disconnect functionality
        /// </summary>
        [Fact]
        public void Disconnect_NoInput_CarouselShouldDisconnected()
        {
            _carousel = new CarWhiteIpcDualAccess(controlType: ControllerType.WhiteIPCDualAccess, ipAddr: _carouselData.Attribute.IPAddress, carAddr: _carouselData.Attribute.DeviceNumber,
                port: _carouselData.Attribute.Port, timeOut: 10, returnsStatus: true, deviceResponse: _deviceResponse, socket: _socket);
            _carousel.Connect();

            //Assume
            bool expected = false;

            //Act
            _carousel.Disconnect();
            bool actual = _carousel.IsConnected;

            //Assert
            Assert.Equal(expected: expected, actual: actual);
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Get TransactionQueueData
        /// </summary>
        /// <returns></returns>
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
