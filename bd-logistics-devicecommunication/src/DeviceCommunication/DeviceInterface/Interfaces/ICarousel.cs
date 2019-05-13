using System;
using System.Text;
using System.Threading.Tasks;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;


namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces
{
    /// <summary>
    /// Interface for All Carousel Classes
    /// </summary>
    public interface ICarousel
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string LastErrorText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IIPSocket IPSocket { get; set; }

        /// <summary>
        /// 
        /// </summary>

        bool IsConnected { get; }

        /// <summary>
        /// Connect to Carousel server
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnect to Carousel server
        /// </summary>
        void Disconnect();


        /// <summary>
        /// 
        /// </summary>
        bool ReturnsStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime LastCommandDT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IgnoreDisplayCommands { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IgnoreMoveCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string TelegramLog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        StringBuilder Telegram { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Response { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ResponseLog { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDeviceResponse ClearDisplay();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDeviceResponse ClearBuffer();

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
        Task<IDeviceResponse> Move(int shelfNum = 0, Slot slot = null, TransactionData transactionData = null,
                                    string message = "", int leftOffset = 0, int positionNum = 0, bool isOutsideComputer = false);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDeviceResponse GetCurrentLocation();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOutsideComputer"></param>
        /// <returns></returns>
        IDeviceResponse WaitForCarouselMoveCompletion(bool isOutsideComputer = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="MaxReceive"></param>
        /// <param name="TermChars"></param>
        /// <param name="ReturnTerm"></param>
        /// <returns></returns>
        string SendAndReceive(string Message, int MaxReceive, string TermChars, bool ReturnTerm);

        #endregion
    }
}
