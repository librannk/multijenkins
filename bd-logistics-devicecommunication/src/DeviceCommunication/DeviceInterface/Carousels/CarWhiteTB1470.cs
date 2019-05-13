using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels
{
    /// <summary>
    /// Class CarWhiteTB1470
    /// </summary>
    public class CarWhiteTB1470 : Carousel
    {
        #region Private Properties
        /// <summary>
        /// read-write TargetShelf property
        /// </summary>
        private int TargetShelf { get; set; }
        /// <summary>
        /// read-write CarFieldLength property
        /// </summary>
        private int CarFieldLength { get; set; }
        /// <summary>
        /// read-write PosFieldLength property
        /// </summary>
        private int PosFieldLength { get; set; }
        /// <summary>
        /// read-write Pos2FieldLength property
        /// </summary>
        private int Pos2FieldLength { get; set; }
        /// <summary>
        /// /read-write DepthFieldLength property
        /// </summary>
        private int DepthFieldLength { get; set; }
        /// <summary>
        /// read-write HeightFieldLength property
        /// </summary>
        private int HeightFieldLength { get; set; }
        /// <summary>
        /// read-write QtyFieldLength property
        /// </summary>
        private int QtyFieldLength { get; set; }
        /// <summary>
        /// read-write AlphaCharCount property
        /// </summary>
        private int AlphaCharCount { get; set; }
        /// <summary>
        /// read-write CanConfirm property
        /// </summary>
        private bool CanConfirm { get; set; }
        #endregion

        #region Contructor
        public CarWhiteTB1470(ControllerType controlType, string carAddr, string ipAddr, int port, int TimeOut, bool returnsStatus, IDeviceResponse deviceResponse, IIPSocket socket)
           : base(controlType, carAddr, ipAddr, port, TimeOut, returnsStatus, deviceResponse, socket)
        {
            // SET DEFAULT VALUES
            SetCharacterAttributes();
            TargetShelf = 0;
        }
        #endregion

        #region Override method

        /// <summary>
        /// SetCharacterAttributes</summary>
        public override void SetCharacterAttributes()
        {
            CarFieldLength = 3;
            PosFieldLength = 2;
            DepthFieldLength = 1;
            QtyFieldLength = 6;
            AlphaCharCount = 20;
            HeightFieldLength = 0;
            CanConfirm = true;
            TermChars = Constants.TelegramAppend.CarWhiteTermChars;
            UseCheckSum = false;
        }

        /// <summary>
        /// GetStatus
        /// </summary>
        /// <returns></returns>
        public override IDeviceResponse GetStatus()
        {
            ResetLog();

            int telegramLen = 0;
            Telegram.Length = 0;
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);
            Telegram.Append(CarAddress);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend9);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppendSS);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend11);
            telegramLen = 18;
            return TelegramResponse(telegramLen, ItemNeeded.STATUS);
        }
        /// <summary>
        /// GetCurrentLocation
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
        /// move using shelf num
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
            ResetLog();
            Telegram.Length = 0;
            Connect();
            shelfNum = shelfNum > 0 ? shelfNum : (int)slot.Bin.Shelf.ShelfNum;
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend2);
            Telegram.Append(CarAddress);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend9);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend0);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend10);
            Telegram.Append(shelfNum.ToString(Constants.TelegramAppend.CarWhiteTelegramAppend12));
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend3);
            Telegram.Append(Constants.TelegramAppend.CarWhiteTelegramAppend11);
            TargetShelf = shelfNum;

            _deviceResponse = TelegramResponse(1, ItemNeeded.STATUS, string.Empty);
            Disconnect();
            return _deviceResponse;
        }

        #endregion

    }
}
