using System;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.Common.DeviceInterface.Enum;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Factory
{
    /// <summary>
    /// Factory class for generating the carousal object
    /// </summary>
    public class CarouselFactory : ICarouselFactory
    {
        #region Public Methods

        /// <summary>
        /// Carousel factory method instantiate carousel as per  ControllerType
        /// </summary>
        /// <param name="carouselData"></param>
        /// <param name="deviceResponse"></param>
        /// <param name="socket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<ICarousel> GetCarouselType(Device carouselData, IDeviceResponse deviceResponse, IIPSocket socket, string timeOut)
        {
            //Required inputs
            ControllerType controllerType = ControllerType.Undefined;
            string deviceClass = carouselData?.Attribute?.DeviceClass?.ToUpper();
            string ipAddress = carouselData?.Attribute?.IPAddress;
            string deviceNumber = carouselData?.Attribute?.DeviceNumber;
            bool? returnStatus = carouselData?.Attribute?.ReturnStatus;
            int? ipPort = carouselData?.Attribute?.Port;
            int carouselTimeOut = default(int);
            Int32.TryParse(timeOut, out carouselTimeOut);

            //Setting the controller type
            if (deviceClass == ControllerType.WhiteTB1470_2300.ToString().ToUpper())
            {
                controllerType = ControllerType.WhiteTB1470_2300;
            }
            else if (deviceClass == ControllerType.WhiteTB1470.ToString().ToUpper())
            {
                controllerType = ControllerType.WhiteTB1470;
            }
            else if (deviceClass == ControllerType.WhiteTB1470H.ToString().ToUpper())
            {
                controllerType = ControllerType.WhiteTB1470H;
            }
            else if (deviceClass == ControllerType.WhiteIPCDualAccess.ToString().ToUpper())
            {
                controllerType = ControllerType.WhiteIPCDualAccess;
            }

            //Creating the instance
            switch (controllerType)
            {
                case ControllerType.WhiteTB1470_2300:
                    return new CarWhiteTb14702300(controllerType, deviceNumber, ipAddress, ipPort.Value, carouselTimeOut, returnStatus.Value, deviceResponse, socket);

                case ControllerType.WhiteTB1470:
                    return new CarWhiteTB1470(controllerType, deviceNumber, ipAddress, ipPort.Value, carouselTimeOut, returnStatus.Value, deviceResponse, socket);

                case ControllerType.WhiteTB1470H:
                    return new CarWhiteTB1470H(controllerType, deviceNumber, ipAddress, ipPort.Value, carouselTimeOut, returnStatus.Value, deviceResponse, socket);

                case ControllerType.WhiteIPCDualAccess:
                    return new CarWhiteIpcDualAccess(controllerType, deviceNumber, ipAddress, ipPort.Value, carouselTimeOut, returnStatus.Value, deviceResponse, socket);

                default:
                    return null;
            }
        }

        #endregion
    }
}
