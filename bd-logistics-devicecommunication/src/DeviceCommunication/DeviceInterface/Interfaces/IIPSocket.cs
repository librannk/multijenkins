using System.Net.Sockets;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces
{
    /// <summary>
    /// Interface for ip socket
    /// </summary>
    public interface IIPSocket
    {
        #region Properties

        /// <summary>Socket </summary>
        Socket Socket { get; set; }

        /// <summary> Property to check socket is connected or not</summary>
        bool IsConnected { get; }

        #endregion

        #region Methods

        /// <summary> Read available socket </summary>
        int ReadSocket();

        /// <summary> clear the buffer</summary>
        void ClearReceiveBuffer();

        /// <summary> make connect at port and ipaddress</summary>
        bool ConnectToServer(string address, int port, uint timeout);

        /// <summary>diconnect connection</summary>
        bool Disconnect();

        /// <summary> write on connected socket</summary>
        bool WriteSocket(string sockData);

        /// <summary>write on connected socket</summary>
        bool WaitForSocketData();

        /// <summary> Get receive buffer</summary>
        string GetReceiveBuffer();

        #endregion
    }
}

