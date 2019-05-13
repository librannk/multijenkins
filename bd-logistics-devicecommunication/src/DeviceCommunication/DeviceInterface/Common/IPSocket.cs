using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Common
{
    /// <summary>
    /// Socket Class
    /// </summary>
    public class IPSocket : IIPSocket
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private int buffPos;

        /// <summary>
        /// 
        /// </summary>
        private readonly byte[] byteBuff;

        /// <summary>
        /// 
        /// </summary>
        private  IPAddress ipAddr;

        /// <summary>
        /// 
        /// </summary>
        private IPEndPoint ipe;

        /// <summary>
        /// 
        /// </summary>
        private int lastErrorCode;

        /// <summary>
        /// 
        /// </summary>
        private string lastErrorText;

        /// <summary>
        /// 
        /// </summary>
        private string lineDelim;

        /// <summary>
        /// 
        /// </summary>
        private const int MAXBACKLOG = 0x10;

        /// <summary>
        /// 
        /// </summary>
        private ProtocolType proto;

        /// <summary>
        /// 
        /// </summary>
        private string sockBuff;

        /// <summary>
        /// 
        /// </summary>
        private const int sockBuffSIZE = 0x4000;

        /// <summary>
        ///  declare socket Timeout 
        /// </summary>
        private uint sockTimeout;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Socket Socket { get; set; }
        
        /// <summary>
        /// read socket connection property
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return ((Socket != null) && Socket.Connected);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// IPSocket method
        /// </summary>
        public IPSocket()
        {
            sockTimeout = 0x3e8;
            proto = ProtocolType.Tcp;
            byteBuff = new byte[0x4000];
            sockBuff = "";
            lastErrorText = "";
            lineDelim = "\r\n";
        }

        /// <summary>
        /// IPSocket method
        /// </summary>
        /// <param name="newSock"></param>
        public IPSocket(Socket newSock) : this()
        {
            Socket = newSock;
            this.proto = Socket.ProtocolType;
            this.ipe = (IPEndPoint)Socket.RemoteEndPoint;
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// clean up buffer data
        /// </summary>
        public void ClearReceiveBuffer()
        {
            sockBuff = "";
        }


        /// <summary>
        /// Connect to addr and port
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool ConnectToServer(string addr, int port, uint timeout)
        {
            CreateNewSocketObject();

            var result = Socket.BeginConnect(IPAddress.Parse(addr), port, null, null);
            var success = result.AsyncWaitHandle.WaitOne((int)timeout, true);

            if (success)
            {
                return Socket.Connected;
            }

            result.AsyncWaitHandle.Close();
            Socket.EndConnect(result);

            throw new TimeoutException(string.Format("Unable to connect to {0}:{1}.", addr, port));
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateNewSocketObject()
        {
            if (this.proto == ProtocolType.Tcp)
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, this.proto);
                Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, 1);
            }
            else
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, this.proto);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            if (Socket != null && Socket.Connected)
            {
                Socket.Shutdown(SocketShutdown.Both);
                Socket.LingerState = new LingerOption(true, 0);
                Socket.Close();
            }

            return true;
        }
       
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetReceiveBuffer()
        {
            return sockBuff;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ReadSocket()
        {
            int count = 0;

            int available = Socket.Available;
            if (available > 0)
            {
                if (available > 0x4000)
                {
                    available = 0x4000;
                }
                count = Socket.Receive(this.byteBuff, available, SocketFlags.None);
                if (count > 0)
                {
                    sockBuff = sockBuff + Telegrams.GetString(this.byteBuff, 0, count);
                }
            }
            return count;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WaitForSocketData()
        {
            if ((Socket == null) || (sockBuff.Length > 0))
            {
                return true;
            }
            uint num = 0;
            bool flag = false;
            uint num2 = 100;

            while ((num < num2) && !flag)
            {
                flag = this.ReadSocket() > 0;
                if (!flag)
                {
                    Thread.Sleep(10);
                }
                num++;
            }
            return flag;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sockData"></param>
        /// <returns></returns>
        public bool WriteSocket(string sockData)
        {
            byte[] bytes = Telegrams.GetBytes(sockData);
            Socket.Send(bytes);
            return true;
        }

        #endregion

        #region Destructor

        /// <summary>
        /// IPSocket Destructor
        /// </summary>
        ~IPSocket()
        {
            if ((Socket != null) && Socket.Connected)
            {
                this.Disconnect();
            }
        }

        #endregion        
    }
}
