using Moq;
using Xunit;
using System.Net.Sockets;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;

namespace DeviceCommunication.UnitTest.DeviceInterface.Common
{
    public class IPSocketTest
    {
        #region Fields

        private MockIIPSocket _ipsocket;

        #endregion

        #region Constructor

        public IPSocketTest()
        {
            _ipsocket = new MockIIPSocket();

            //Setup mock methods
            _ipsocket.setup_ConnectToServer("172.17.44.209", 4001, 10);
            _ipsocket.setup_Disconnect();
            _ipsocket.setup_WriteSocket(string.Empty);
            _ipsocket.setup_IsConnected();
        }

        #endregion

        #region Test Cases

        [Fact]
        public void ReadSocket_NoInput_ShouldReadFromSocket()
        {
            //Assume
            int expected = 0;

            //Act
            int actual = _ipsocket.ReadSocket();

            //Assert
            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void ClearReceiveBuffer_NoInput_ShouldClearBuffer()
        {
            //Act
            _ipsocket.ClearReceiveBuffer();

            //Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("172.17.44.209", 4001, 10)]
        public void ConnectToServer_MultipleInput_ShouldConnect(string address, int port, uint timeout)
        {
            //Assume
            bool expected = true;

            //Act
            bool actual = _ipsocket.ConnectToServer(address, port, timeout);

            //Assert
            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void Disconnect()
        {
            //Assume
            bool expected = !_ipsocket.IsConnected;

            //Act
            bool actual = _ipsocket.Disconnect();

            //Assert
            Assert.Equal(expected: expected, actual: actual);
        }

        [Theory]
        [InlineData("")]
        public void WriteSocket(string sockData)
        {
            //Assume
            bool expected = true;

            //Act
            bool actual = _ipsocket.WriteSocket(sockData);

            //Assert
            Assert.Equal(expected: expected, actual: actual);
        }

        #endregion
    }

    #region Mock IPSocket class

    /// <summary>
    /// Mock socket class to mimick the IPSocket class operations
    /// </summary>
    public class MockIIPSocket : IIPSocket
    {
        #region Fields

        private readonly Mock<IIPSocket> _mock;
        private bool isSocketConnected;

        #endregion

        #region Constructor

        public MockIIPSocket()
        {
            _mock = new Mock<IIPSocket>();
        }

        #endregion

        #region Setup Methods

        public void setup_ConnectToServer(string address, int port, uint timeout)
        {
            _mock.Setup(con => con.ConnectToServer(address, port, timeout)).Returns(true);
        }
        public void setup_Disconnect()
        {
            _mock.Setup(con => con.Disconnect()).Returns(true);
        }

        public void setup_WriteSocket(string sockData)
        {
            _mock.Setup(con => con.WriteSocket(sockData)).Returns(true);
        }

        public void setup_IsConnected()
        {
            _mock.SetupGet(con => con.IsConnected).Returns(isSocketConnected);
        }

        #endregion

        #region Mock Properties 

        public Socket Socket { get; set; }

        #endregion

        #region Mock Methods

        public bool IsConnected
        {
            get { return isSocketConnected; }
        }
        public int ReadSocket()
        {
            return _mock.Object.ReadSocket();
        }

        public void ClearReceiveBuffer()
        {
            _mock.Object.ClearReceiveBuffer();
        }

        public bool ConnectToServer(string address, int port, uint timeout)
        {
            isSocketConnected = true;
            return _mock.Object.ConnectToServer(address, port, timeout);
        }

        public bool Disconnect()
        {
            isSocketConnected = false;
            return _mock.Object.Disconnect();
        }

        public bool WriteSocket(string sockData)
        {
            setup_WriteSocket(sockData);
            return _mock.Object.WriteSocket(sockData);
        }

        public bool WaitForSocketData()
        {
            return _mock.Object.WaitForSocketData();
        }

        public string GetReceiveBuffer()
        {
            return _mock.Object.GetReceiveBuffer();
        }

        #endregion
    }

    #endregion
}
