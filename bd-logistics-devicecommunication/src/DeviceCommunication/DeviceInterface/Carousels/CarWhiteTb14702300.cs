using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels
{
    /// <summary>
    /// class CarWhiteTb14702300
    /// </summary>
    public class CarWhiteTb14702300 : Carousel
    {

        #region Properties

        /// <summary>
        /// read-write TargetShelf property
        /// </summary>
        public int TargetShelf { get; set; }

        /// <summary>
        /// read-write BinBaseAddr property
        /// </summary>        
        public string BinBaseAddr { get; set; }

        /// <summary>
        /// read-write AlphaBaseAddr property
        /// </summary>
        public string AlphaBaseAddr { get; set; }

        /// <summary>
        /// read-write IgnoreSendMessage property
        /// </summary>
        public bool IgnoreSendMessage { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// CarWhiteTb14702300 constructor to set default value 
        /// </summary>
        /// <param name="controlType"></param>
        /// <param name="carAddr"></param>
        /// <param name="ipAddr"></param>
        /// <param name="port"></param>
        /// <param name="TimeOut"></param>
        /// <param name="returnsStatus"></param>
        /// <param name="deviceResponse"></param>
        /// <param name="socket"></param>
        public CarWhiteTb14702300(ControllerType controlType, string carAddr,
            string ipAddr, int port, int TimeOut, bool returnsStatus, IDeviceResponse deviceResponse, IIPSocket socket)
            : base(controlType, carAddr, ipAddr, port, TimeOut, returnsStatus, deviceResponse, socket)
        {
            SetCharacterAttributes();
            TargetShelf = 0;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// SetCharacterAttributes
        /// </summary>
        public override void SetCharacterAttributes()
        {
            TermChars = Constants.TelegramAppend.CarWhiteTermChars;
            UseCheckSum = false;
            BinBaseAddr = Constants.TelegramAppend.CarWhiteBinBaseAddr;
            AlphaBaseAddr = Constants.TelegramAppend.CarWhiteAlphaBaseAddr;
            IgnoreSendMessage = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDeviceResponse ClearBuffer()
        {
            ResetLog();

            if (IgnoreSendMessage)
                return _deviceResponse;
            Telegram.Length = 0;
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend1);

            return TelegramResponse(1, ItemNeeded.STATUS);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDeviceResponse ClearDisplay()
        {
            if (IgnoreSendMessage)
                return _deviceResponse;

            Telegram.Length = 0;

            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);
            Telegram.Append(BinBaseAddr.ToString());
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend7);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);
            Telegram.Append(AlphaBaseAddr.ToString());
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend8);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);
            return TelegramResponse(1, ItemNeeded.STATUS);
        }

        /// <summary>
        /// This method is used move carousel using slot
        /// </summary>
        /// <param name="shelfNum"></param>
        /// <param name="slot"></param>
        /// <param name="transactionData"></param>
        /// <param name="message"></param>
        /// <param name="leftOffset"></param>
        /// <param name="positionNum"></param>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        public override async Task<IDeviceResponse> Move(int shelfNum = 0, Slot slot = null, TransactionData transactionData = null,
                                        string message = "", int leftOffset = 0, int positionNum = 0, bool isOutsideComputer = false)
        {
            Connect();
            ClearBuffer();
            if (shelfNum > 0)
            {
                _deviceResponse = MoveCarousel(shelfNum);
            }
            else
            {
                _deviceResponse = MoveCarousel(slot, transactionData?.Quantity, message);
            }

            Disconnect();
            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDeviceResponse GetStatus(bool isOutsideComputer = false)
        {
            _deviceResponse.CurrentCarrier = TargetShelf;
            return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDeviceResponse GetCurrentLocation()
        {
            return GetStatus();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="quantity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private IDeviceResponse MoveCarousel(Slot slot, int? quantity, string message)
        {
            ResetLog();
            ClearDisplay();

            if (slot != null)
            {
                int shelfNum = (int)slot.Bin.Shelf.ShelfNum;

                Telegram.Length = 0;

                if (!IgnoreSendMessage)
                {
                    Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);

                    Telegram.Append(BinBaseAddr.ToString());

                    Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
                    Telegram.Append("{");

                    if (slot.Bin.LeftOffset != null)
                        Telegram.Append(GetAsciiEncodedPosition((int)slot.Bin.LeftOffset));
                    else
                        Telegram.Append(GetAsciiEncodedPosition((int)slot.Bin.BinNum));

                    string slotText = slot.SlotNum.ToString();

                    if (slotText.Length > 1)
                        Telegram.Append(" ");
                    else
                        Telegram.Append(slotText);

                    Telegram.Append("}");
                    Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);

                    Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);

                    Telegram.Append(AlphaBaseAddr.ToString());
                    Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
                    Telegram.Append("{0");

                    message = quantity.ToString();

                    if (!string.IsNullOrEmpty(slot.DispenseForm))
                        message += " " + slot.DispenseForm.Trim();

                    if (message.Length > 10)
                        message = message.Substring(0, 10);

                    message = message.PadRight(10, ' ');

                    Telegram.Append(message);
                    Telegram.Append("}");
                    Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);
                }

                Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);
                Telegram.Append(CarAddress);
                Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend9);
                Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
                Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend10);
                Telegram.Append(shelfNum.ToString(Constants.TelegramAppend.CarWhiteTelegramAppend12));
                Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);
                Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend6);
                TargetShelf = shelfNum;

                return TelegramResponse(1, ItemNeeded.STATUS);
            }
            else
                return _deviceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shelfNum"></param>
        /// <returns></returns>
        private IDeviceResponse MoveCarousel(int shelfNum)
        {
            ResetLog();

            ClearDisplay();

            Telegram.Length = 0;
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);
            Telegram.Append(CarAddress);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend9);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend10);
            Telegram.Append(shelfNum.ToString(Constants.TelegramAppend.CarWhiteTelegramAppend12));
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend11);
            TargetShelf = shelfNum;

            return TelegramResponse(1, ItemNeeded.STATUS);
        }

        #endregion
    }
}
