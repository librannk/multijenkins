using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Common;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Factory;
using System.Threading.Tasks;

namespace DeviceCommunication.UnitTest.DeviceInterface.Factory
{
    public class CarouselFactoryTest
    {
        #region Field

        private CarouselFactory _carouselFactory;
        private Mock<DeviceResponse> _deviceResponse;
        private Mock<IPSocket> _socket;
        private string _timeOut;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize the common fields to be used in all the test cases
        /// </summary>
        public CarouselFactoryTest()
        {
            //Default initialization
            _carouselFactory = new CarouselFactory();
            _deviceResponse = new Mock<DeviceResponse>();
            _socket = new Mock<IPSocket>();
            _timeOut = "10";
        }

        #endregion

        #region Test cases

        /// <summary>
        /// To test the GetCarouselType functionality
        /// Should be able to deliver specific carousel based on the controllerType provided
        /// </summary>
        [Fact]
        public async Task GetCarouselType_WhiteIPCDualAccessCarouselData_ShouldGetWhiteIPCDualAccessCarousel()
        {
            //Assume
            Type expected = typeof(CarWhiteIpcDualAccess);

            //Act
            Type actual = default(Type);
            ICarousel carousel = await _carouselFactory.GetCarouselType(
                carouselData: CreateTransactionQueueData(ControllerType.WhiteIPCDualAccess).Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel")),
                deviceResponse: _deviceResponse.Object, socket: _socket.Object, timeOut: _timeOut);

            if (carousel is CarWhiteIpcDualAccess)
                actual = typeof(CarWhiteIpcDualAccess);

            //Assert
            Assert.True(expected.Equals(actual));
        }

        /// <summary>
        /// To test the GetCarouselType functionality
        /// Should be able to deliver specific carousel based on the controllerType provided
        /// </summary>
        [Fact]
        public async Task GetCarouselType_CarWhiteTb14702300CarouselData_ShouldGetCarWhiteTb14702300Carousel()
        {
            //Assume
            Type expected = typeof(CarWhiteTb14702300);


            //Act
            Type actual = default(Type);
            ICarousel carousel = await _carouselFactory.GetCarouselType(
                carouselData: CreateTransactionQueueData(ControllerType.WhiteTB1470_2300).Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel")),
                deviceResponse: _deviceResponse.Object, socket: _socket.Object, timeOut: _timeOut);

            if (carousel is CarWhiteTb14702300)
                actual = typeof(CarWhiteTb14702300);

            //Assert
            Assert.True(expected.Equals(actual));
        }

        /// <summary>
        /// To test the GetCarouselType functionality
        /// Should be able to deliver specific carousel based on the controllerType provided
        /// </summary>
        [Fact]
        public async Task GetCarouselType_CarWhiteTB1470HCarouselData_ShouldGetCarWhiteTB1470HCarousel()
        {
            //Assume
            Type expected = typeof(CarWhiteTB1470H);

            //Act
            Type actual = default(Type);
            ICarousel carousel = await _carouselFactory.GetCarouselType(
                carouselData: CreateTransactionQueueData(ControllerType.WhiteTB1470H).Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel")),
                deviceResponse: _deviceResponse.Object, socket: _socket.Object, timeOut: _timeOut);

            if (carousel is CarWhiteTB1470H)
                actual = typeof(CarWhiteTB1470H);

            //Assert
            Assert.True(expected.Equals(actual));
        }

        /// <summary>
        /// To test the GetCarouselType functionality
        /// Should be able to deliver specific carousel based on the controllerType provided
        /// </summary>
        [Fact]
        public async Task GetCarouselType_CarWhiteTB1470CarouselData_ShouldGetCarWhiteTB1470Carousel()
        {
            //Assume
            Type expected = typeof(CarWhiteTB1470);


            //Act
            Type actual = default(Type);
            ICarousel carousel = await _carouselFactory.GetCarouselType(
                carouselData: CreateTransactionQueueData(ControllerType.WhiteTB1470).Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel")),
                deviceResponse: _deviceResponse.Object, socket: _socket.Object, timeOut: _timeOut);

            if (carousel is CarWhiteTB1470)
                actual = typeof(CarWhiteTB1470);

            //Assert
            Assert.True(expected.Equals(actual));
        }

        #endregion

        #region Private Method

        private TransactionData CreateTransactionQueueData(ControllerType controllerType)
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
                    DeviceClass = controllerType.ToString().ToUpper(),
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

