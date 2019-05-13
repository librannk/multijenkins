using System.Threading;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels
{
    /// <summary>
    /// 
    /// </summary>
    public class CarWhiteIpcDualAccess : Carousel
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private int TargetShelf { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="carAddr"></param>
        /// <param name="ipAddr"></param>
        /// <param name="port"></param>
        /// <param name="timeOut"></param>
        /// <param name="returnsStatus"></param>
        /// <param name="deviceResponse"></param>
        /// <param name="socket"></param>
        public CarWhiteIpcDualAccess(ControllerType controlType, string carAddr,
            string ipAddr, int port, int timeOut, bool returnsStatus, IDeviceResponse deviceResponse, IIPSocket socket)
            : base(controlType, carAddr, ipAddr, port, timeOut, returnsStatus, deviceResponse, socket)
        {
            SetCharacterAttributes();
            TargetShelf = 0;
        }

        #endregion

        #region Public Overridden Methods

        /// <summary>
        /// 
        /// </summary>
        public override void SetCharacterAttributes()
        {
            TermChars = "\x0003";
            UseCheckSum = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDeviceResponse GetCurrentLocation()
        {
            ResetLog();

            IDeviceResponse status = GetStatus();
            status.CurrentCarrier = TargetShelf;
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shelfNum"></param>
        /// <returns></returns>
        public override async Task<IDeviceResponse> Move(int shelfNum = 0, Slot slot = null, TransactionData transactionData = null,
            string message = "", int leftOffset = 0, int positionNum = 0, bool isOutsideComputer = false)
        {
            Connect();
            ClearBuffer();

            if (shelfNum > 0 && slot == null)
            {
                _deviceResponse = MoveCarousel(shelfNum);
            }
            else
            {
                _deviceResponse = MoveCarousel(slot, isOutsideComputer);
            }

            Disconnect();
            return _deviceResponse;
        }

        /// <summary>
        /// Parses the telegram response to fill the deviceResponse object
        /// </summary>
        /// <param name="which"></param>
        /// <returns></returns>
        protected override IDeviceResponse ParseResponseTelegram(ItemNeeded which)
        {
            if (response.Length >= 12)
            {
                string subStr = response.Substring(response.IndexOf('\x0002') + 1);
                if (subStr[8] == 'M')
                {
                    _deviceResponse.HasError = false;
                    _deviceResponse.Message = DevResponse.ManualMode.ToString();
                    return _deviceResponse;
                }
                else if (subStr[9] == 'I' || subStr[9] == 'B')
                {
                    _deviceResponse.HasError = false;
                    _deviceResponse.Message = DevResponse.InMotion.ToString();
                    return _deviceResponse;
                }
                else if (subStr[9] == 'F')
                {
                    _deviceResponse.HasError = true;
                    _deviceResponse.Message = DevResponse.SafetyError.ToString();
                    return _deviceResponse;
                }
            }
            else
            {
                _deviceResponse.HasError = false;
                _deviceResponse.Message = DevResponse.NoResponse.ToString();
                return _deviceResponse;
            }

            return base.ParseResponseTelegram(which);
        }

        /// <summary>
        /// Calls the telegramResponse method to send the data to carousel
        /// </summary>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        public override IDeviceResponse GetStatus(bool isOutsideComputer = false)
        {
            ResetLog();

            int telegramLen = 0;
            Telegram.Length = 0;
            Telegram.Append('\x0002');
            Telegram.Append(CarAddress);
            Telegram.Append(isOutsideComputer ? "4" : "3");
            Telegram.Append("0000");
            Telegram.Append("SS");
            Telegram.Append('\x0003');
            Telegram.Append("s");
            telegramLen = 18;

            return TelegramResponse(telegramLen, ItemNeeded.STATUS);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        public override IDeviceResponse WaitForCarouselMoveCompletion(bool isOutsideComputer = false)
        {
            if (ReturnsStatus)
            {
                Thread.Sleep(500);
                const int responseTimout = 60;
                var i = 0;

                while (i < responseTimout)
                {
                    _deviceResponse = GetStatus(isOutsideComputer);
                    if (_deviceResponse.Message.Equals(DevResponse.NoResponse.ToString()))
                    {
                        _deviceResponse.HasError = false;
                        _deviceResponse.Message = DevResponse.NoResponse.ToString();
                        break;
                    }
                    if (_deviceResponse.Message.Equals(DevResponse.OK.ToString())
                            || _deviceResponse.Message.Equals(DevResponse.SafetyError.ToString())
                            || _deviceResponse.Message.Equals(DevResponse.ManualMode.ToString()))
                        break;

                    Thread.Sleep(1000);
                    i++;

                    if (i == responseTimout)
                    {
                        _deviceResponse.HasError = true;
                        _deviceResponse.Message = DevResponse.SafetyError.ToString();
                        break;
                    }
                }
            }

            return _deviceResponse;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shelfNum"></param>
        /// <returns></returns>
        private IDeviceResponse MoveCarousel(int shelfNum = 0)
        {
            ResetLog();

            Telegram.Length = 0;
            Telegram.Append('\x0002');
            Telegram.Append(CarAddress);
            Telegram.Append("3");                      //sub chanel value
            Telegram.Append("0000");
            Telegram.Append("G");
            Telegram.Append(shelfNum.ToString("000"));
            Telegram.Append('\x0003');
            Telegram.Append("s");
            TargetShelf = shelfNum;

            return TelegramResponse(1, ItemNeeded.STATUS);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        private IDeviceResponse MoveCarousel(Slot slot, bool isOutsideComputer)
        {
            ResetLog();

            int shelfNum = 0;
            if (slot.Bin.Shelf.ShelfNum != null) shelfNum = (int)slot.Bin.Shelf.ShelfNum;

            Telegram.Length = 0;
            Telegram.Append('\x0002');
            Telegram.Append(CarAddress);
            Telegram.Append(isOutsideComputer ? "4" : "3");
            Telegram.Append("0000");
            Telegram.Append("G");
            Telegram.Append(shelfNum.ToString("000"));
            Telegram.Append('\x0003');
            Telegram.Append("s");
            TargetShelf = shelfNum;

            return TelegramResponse(1, ItemNeeded.STATUS);
        }

        #endregion
    }
}

