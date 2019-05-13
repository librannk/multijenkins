using System.Threading.Tasks;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels
{
    using System;
    using System.Text;
    using Logistics.Services.DeviceCommunicationAPI.Application.Common;
    using Logistics.Services.DeviceCommunication.API.Application.Models;
    using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
    using global::Logistics.Services.DeviceCommunication.API.DeviceInterface.Enum;
    using global::Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;


    /// <summary>
    /// Carousel Class
    /// </summary>
    public abstract class Carousel : ICarousel
    {
        #region Fields

        /// <summary>
        /// declare DeviceResponse
        /// </summary>
        protected IDeviceResponse _deviceResponse = null;

        /// <summary>
        /// declare CarAddress
        /// </summary>
        protected string CarAddress;

        /// <summary>
        /// declare RetryLimit
        /// </summary>
        protected int RetryLimit = 1;

        /// <summary>
        /// declare isWriteSocket
        /// </summary>
        private bool isWriteSocket = false;

        #endregion

        #region Properties

        /// <summary>
        /// read-write Type property
        /// </summary>
        protected ControllerType Type { get; set; }
        /// <summary>
        /// read-write IpAddress property
        /// </summary>
        protected string IpAddress { get; set; }
        /// <summary>
        /// read-write IpPort property
        /// </summary>
        protected int IpPort { get; set; }
        /// <summary>
        /// read-write Timeout property
        /// </summary>
        protected int Timeout { get; set; }
        /// <summary>
        /// read-write TermChars property
        /// </summary>
        protected string TermChars { get; set; }
        /// <summary>
        /// read-write UseCheckSum property
        /// </summary>
        protected bool UseCheckSum { get; set; }
        /// <summary>
        /// read-write LastErrorText property
        /// </summary>
        public string LastErrorText { get; set; }
        /// <summary>
        /// read-write ReturnsStatus property
        /// </summary>
        public bool ReturnsStatus { get; set; }
        /// <summary>
        /// read-write LastCommandDT property
        /// </summary>
        public DateTime LastCommandDT { get; set; }
        /// <summary>
        /// read-write IgnoreDisplayCommands property
        /// </summary>
        public bool IgnoreDisplayCommands { get; set; }
        /// <summary>
        /// read-write IgnoreMoveCommand property
        /// </summary>
        public bool IgnoreMoveCommand { get; set; }
        /// <summary>
        /// read-write telegramLog property
        /// </summary>
        public string telegramLog { get; set; }

        /// <summary>
        /// read-write response property
        /// </summary>
        public string response { get; set; }
        /// <summary>
        /// read-write responseLog property
        /// </summary>
        public string responseLog { get; set; }

        /// <summary>
        /// read-write Sock property
        /// </summary>
        public IIPSocket IPSocket { get; set; }

        /// <summary>
        /// read-write TelegramLog property
        /// </summary>
        public string TelegramLog { get; set; }
        /// <summary>
        /// read-write Telegram property
        /// </summary>
        public StringBuilder Telegram { get; set; }
        /// <summary>
        ///  read-write Response property
        /// </summary>
        public string Response { get; set; }
        /// <summary>
        /// read-write ResponseLog property
        /// </summary>
        public string ResponseLog { get; set; }

        #endregion

        #region Constructor   

        /// <summary>
        /// Carousel Constructor
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="carAddr"></param>
        /// <param name="ipAddr"></param>
        /// <param name="port"></param>
        /// <param name="TimeOut"></param>
        /// <param name="returnsStatus"></param>
        /// <param name="deviceResponse"></param>
        /// <param name="socket"></param>
        protected Carousel(ControllerType controlType, string carAddr, string ipAddr, int port, int TimeOut,
                        bool returnsStatus, IDeviceResponse deviceResponse, IIPSocket socket)
        {
            // SET DEFAULT VALUES
            CarAddress = carAddr;
            Type = controlType;
            IpAddress = ipAddr;
            IpPort = port;
            Timeout = TimeOut;
            ReturnsStatus = returnsStatus;
            Telegram = new StringBuilder();
            LastErrorText = "";

            ResetLog();
            _deviceResponse = deviceResponse;
            IPSocket = socket;

            IgnoreDisplayCommands = false;
            IgnoreMoveCommand = false;

            SetCharacterAttributes();
        }

        #endregion

        #region CommunicationMethods

        /// <summary>
        /// 
        /// </summary>
        public void Flush()
        {
            if (IPSocket.IsConnected)
            {
                IPSocket.ReadSocket();
            }
            IPSocket.ClearReceiveBuffer();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsConnected
        {
            get
            {
                return (IPSocket != null && IPSocket.IsConnected);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Connect()
        {
            if (!IPSocket.IsConnected)
            {
                IPSocket.ConnectToServer(IpAddress, IpPort, (uint)Timeout);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Disconnect()
        {
            if (IPSocket != null)
            {
                IPSocket.Disconnect();
                IPSocket = null;
            }
        }


        #endregion

        #region Abstract

        /// <summary>
        /// 
        /// </summary>
        public abstract void SetCharacterAttributes();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IDeviceResponse GetCurrentLocation();

        #endregion        

        #region Virtual methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IDeviceResponse ClearDisplay()
        {
            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IDeviceResponse ClearBuffer()
        {
            ResetLog();
            _deviceResponse.HasError = false;
            _deviceResponse.Message = DevResponse.OK.ToString();
            return _deviceResponse;
        }
        /// <summary>
        /// Device Status
        /// </summary>
        /// <returns></returns>
        public virtual IDeviceResponse GetStatus()
        {
            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shelfNum"></param>
        /// <param name="slot"></param>
        /// <param name="transactionData"></param>
        /// <param name="message"></param>
        /// <param name="leftOffset"></param>
        /// <param name="positionNum"></param>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        public async virtual Task<IDeviceResponse> Move(int shelfNum = 0, Slot slot = null,
                                    TransactionData transactionData = null, string message = "",
                                    int leftOffset = 0, int positionNum = 0, bool isOutsideComputer = false)
        {
            _deviceResponse.HasError = false;
            _deviceResponse.Message = DevResponse.OK.ToString();
            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        public virtual IDeviceResponse GetStatus(bool isOutsideComputer = false)
        {
            _deviceResponse.HasError = false;
            _deviceResponse.Message = DevResponse.OK.ToString();
            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        public virtual IDeviceResponse WaitForCarouselMoveCompletion(bool isOutsideComputer = false)
        {
            _deviceResponse.HasError = false;
            _deviceResponse.Message = DevResponse.OK.ToString();
            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void ResetLog()
        {
            TelegramLog = string.Empty;
            ResponseLog = string.Empty;
            LastCommandDT = DateTime.Now;

            if (_deviceResponse != null)
            {
                _deviceResponse.HasError = false;
                _deviceResponse.Message = string.Empty;
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Instantiate carousel and check connection
        /// </summary>
        protected void ResetConnection()
        {
            if (IsConnected)
            {
                Disconnect();
                Task.Delay(200);
            }

            Connect();
            Task.Delay(200);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldWidth"></param>
        /// <returns></returns>
        protected int MaxValue(int fieldWidth)
        {
            if (fieldWidth < 1)
            {
                return 0;
            }
            int num = 10;
            for (int i = 1; i < fieldWidth; i++)
            {
                num *= 10;
            }
            return (num - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="which"></param>
        /// <returns></returns>
        protected virtual IDeviceResponse ParseResponseTelegram(ItemNeeded which)
        {
            _deviceResponse.HasError = false;
            _deviceResponse.Message = Constants.Carousel.DeviceOKResponse;

            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedLength"></param>
        /// <param name="termChars"></param>
        /// <param name="whichItem"></param>
        /// <returns></returns>
        protected IDeviceResponse TelegramResponse(int expectedLength, ItemNeeded whichItem, string termChars = "")
        {

            if (!IPSocket.IsConnected)
            {
                _deviceResponse.Message = DevResponse.DeviceNotConnected.ToString();
                _deviceResponse.HasError = true;
                return _deviceResponse;
            }

            if (string.IsNullOrEmpty(TelegramLog))
                TelegramLog = Telegram.ToString();
            else
                TelegramLog += Environment.NewLine + Telegram.ToString();

            Response = "";
            Response = SendAndReceive(Telegram.ToString(), expectedLength, termChars, true);

            if (isWriteSocket)
            {
                if (Response.Length == 0)
                {
                    _deviceResponse.HasError = false;
                    _deviceResponse.Message = Constants.Carousel.DeviceWriteSuccess;
                    return _deviceResponse;
                }

                return ParseResponseTelegram(whichItem);
            }

            _deviceResponse.HasError = true;
            _deviceResponse.Message = Constants.Carousel.DeviceWriteFail;
            return _deviceResponse;
        }

        /// <summary>
        /// Send data to device and read data from device
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="MaxReceive"></param>
        /// <param name="TermChars"></param>
        /// <param name="ReturnTerm"></param>
        /// <returns></returns>
        public string SendAndReceive(string Message, int MaxReceive, string TermChars, bool ReturnTerm)
        {
            if (((Message.Length == 0) || (MaxReceive <= 0)) || !IsConnected)
            {
                return "";
            }
            Flush();

            StringBuilder builder = new StringBuilder();
            int termIndex = 0;

            if (IPSocket.WriteSocket(Message) && ReturnsStatus)
            {
                isWriteSocket = true;
                System.Threading.Thread.Sleep(50);

                bool flag2 = false;
                while (!flag2)
                {
                    if (IPSocket.WaitForSocketData())
                    {
                        builder.Append(IPSocket.GetReceiveBuffer().Replace("\0", ""));
                        IPSocket.ClearReceiveBuffer();

                        termIndex = builder.ToString().IndexOf(TermChars);
                        if (termIndex > 0)
                            flag2 = true;
                    }
                    else
                        break;

                    if (TermChars.Length == 0 && builder.Length >= MaxReceive)
                        break;
                }
            }

            if (TermChars.Length == 0
                && MaxReceive > 1
                && builder.Length > MaxReceive)
            {
                return builder.ToString().Substring(0, MaxReceive - 1);
            }
            else
            {
                return termIndex > 0 ? builder.ToString().Substring(0, termIndex - TermChars.Length) : builder.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected string GetAsciiEncodedPosition(int position)
        {
            int asciiPosition = 48 + position;

            char c = Convert.ToChar(asciiPosition);

            return c.ToString();
        }

        #endregion
    }
}
