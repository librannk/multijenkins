using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.BusinessLayer
{
    /// <summary>
    /// This class checks whether a carousel is connected or not.
    /// </summary>
    public class CarouselConnection : ICarouselConnection
    {
        #region Private Fields
        private readonly IConfiguration _configuration;
        private readonly IIPSocket _socket;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="socket"></param>
        public CarouselConnection(IConfiguration config, IIPSocket socket)
        {
            _configuration = config;
            _socket = socket;

        }
        #endregion

        #region Public Methods 
        /// <summary>
        /// Checks whether a carousel is connected or not.
        /// </summary>
        /// <param name="carouselData"></param>
        /// <returns></returns>
        public IEnumerable<CarouselData> Check(IEnumerable<CarouselData> carouselData)
        {
            foreach (var carousel in carouselData)
            {
                carousel.isOnlineFlag = CheckConnection(carousel.IPAddress, carousel.Port);

                if (carousel.isOnlineFlag && carousel.Display.DisplayAttachedFlag)
                {
                    Display display = carousel.Display;
                    if (display.DisplayIPAddress == carousel.IPAddress && display.DisplayPort == carousel.Port)
                    {
                        display.isOnlineFlag = true;
                    }
                    else
                    {
                        display.isOnlineFlag = CheckConnection(display.DisplayIPAddress, display.DisplayPort);
                    }
                }
                else
                {
                    carousel.Display.isOnlineFlag = false;
                }
            }
            return carouselData;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private bool CheckConnection(string ipAddress, int port)
        {
            var connection = false;

            var timeOut = _configuration.GetSection(Constants.Configuration.CarouselTimeout).Get<UInt32>();

            try
            {
                connection = _socket.ConnectToServer(ipAddress, port, timeOut);
            }
            catch
            {
                //timeout exception because carousel did not connect
            }
            finally
            {
                _socket.Disconnect();
            }
            return connection;
        }
        #endregion
    }
}
