using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.BusinessLayer;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DeviceCommunication.UnitTest.BusinessLayer
{
    /// <summary>
    /// This class contains unit test for CarouselConnection Class
    /// </summary>
    public class CarouselConnectionUnitTest
    {
        #region Private Fields
        private readonly IConfiguration _configuration;
        private readonly Mock<IIPSocket> _socket;
        private readonly CarouselConnection carouselConnection;
        #endregion

        #region Constructor
        public CarouselConnectionUnitTest()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _socket = new Mock<IIPSocket>();
            carouselConnection = new CarouselConnection(_configuration, _socket.Object);
        }
        #endregion

        #region Test Cases
        [Fact]
        public void Check_CarouselDisplayOnlineAtSameAddress_ShouldReturnIsOnlineTrue()
        {
            //Arrange
            _socket.Setup(x => x.ConnectToServer(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<uint>())).Returns(true);
            var mockCarouselData = new CarouselData
            {
                ISAId = "xyz",
                IPAddress = "ip",
                Port = 0,
                isOnlineFlag = false,
                Display = new Display
                {
                    DisplayAttachedFlag = true,
                    DisplayIPAddress = "ip",
                    DisplayPort = 0,
                    isOnlineFlag = false
                }
            };
            var list = new List<CarouselData>();
            list.Add(mockCarouselData);

            //Act
            var response = carouselConnection.Check(list);

            //Assert
            Assert.True(response.First().isOnlineFlag&&response.First().Display.isOnlineFlag);

        }

        [Fact]
        public void Check_CarouselDisplayOnlineAtDifferntAddress_ShouldReturnIsOnlineTrue()
        {
            //Arrange
            _socket.Setup(x => x.ConnectToServer(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<uint>())).Returns(true);
            var mockCarouselData = new CarouselData
            {
                ISAId = "xyz",
                IPAddress = "ip",
                Port = 0,
                isOnlineFlag = false,
                Display = new Display
                {
                    DisplayAttachedFlag = true,
                    DisplayIPAddress = "ip1",
                    DisplayPort = 0,
                    isOnlineFlag = false
                }
            };
            var list = new List<CarouselData>();
            list.Add(mockCarouselData);

            //Act
            var response = carouselConnection.Check(list);

            //Assert
            Assert.True(response.First().isOnlineFlag && response.First().Display.isOnlineFlag);

        }

        [Fact]
        public void Check_CarouselOnlineDisplayOffline_ShouldReturnCarouselOnlineTrueDisplayOnlineFalse()
        {
            //Arrange
            _socket.Setup(x => x.ConnectToServer("ip", 0, It.IsAny<uint>())).Returns(true);
            _socket.Setup(x => x.ConnectToServer("ip1", 0, It.IsAny<uint>())).Returns(false);
            var mockCarouselData = new CarouselData
            {
                ISAId = "xyz",
                IPAddress = "ip",
                Port = 0,
                isOnlineFlag = false,
                Display = new Display
                {
                    DisplayAttachedFlag = true,
                    DisplayIPAddress = "ip1",
                    DisplayPort = 0,
                    isOnlineFlag = false
                }
            };
            var list = new List<CarouselData>();
            list.Add(mockCarouselData);

            //Act
            var response = carouselConnection.Check(list);

            //Assert
            Assert.True(response.First().isOnlineFlag);
            Assert.False(response.First().Display.isOnlineFlag);

        }

        [Fact]
        public void Check_CarouselOnlineDisplayNotAttached_ShouldReturnCarouselOnlineTrueDisplayOnlineFalse()
        {
            //Arrange
            _socket.Setup(x => x.ConnectToServer(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<uint>())).Returns(true);
            var mockCarouselData = new CarouselData
            {
                ISAId = "xyz",
                IPAddress = "ip",
                Port = 0,
                isOnlineFlag = false,
                Display = new Display
                {
                    DisplayAttachedFlag = false
                }
            };
            var list = new List<CarouselData>();
            list.Add(mockCarouselData);

            //Act
            var response = carouselConnection.Check(list);

            //Assert
            Assert.True(response.First().isOnlineFlag);
            Assert.False(response.First().Display.isOnlineFlag);

        }

        [Fact]
        public void Check_CarouselOffline_ShouldReturnCarouselOnlineFalseDisplayOnlineFalse()
        {
            //Arrange
            _socket.Setup(x => x.ConnectToServer(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<uint>())).Returns(false);
            var mockCarouselData = new CarouselData
            {
                ISAId = "xyz",
                IPAddress = "ip",
                Port = 0,
                isOnlineFlag = false,
                Display = new Display
                {
                    DisplayAttachedFlag = true,
                    DisplayIPAddress = "ip1",
                    DisplayPort = 0,
                    isOnlineFlag = false
                }
            };
            var list = new List<CarouselData>();
            list.Add(mockCarouselData);

            //Act
            var response = carouselConnection.Check(list);

            //Assert
            Assert.False(response.First().isOnlineFlag);
            Assert.False(response.First().Display.isOnlineFlag);

        }
        #endregion
    }
}
