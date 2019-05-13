using System;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Enum;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface
{
    /// <summary>
    ///It display device status 
    /// </summary>
    public class DeviceResponse: IDeviceResponse
    {
        #region Properties
        /// <summary>
        /// read-write property HasError
        /// </summary>
        public bool HasError { get; set; }
        /// <summary>
        /// read-write property Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// read-write property CurrentCarrier
        /// </summary>
        public int CurrentCarrier { get; set; }

        /// <summary>
        /// read-write property ErrorCode
        /// </summary>
        public int ErrorCode { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// deviceResponse constructor
        /// </summary>
        public DeviceResponse()
        {
            HasError = false;
            Message = string.Empty;
            CurrentCarrier = 0;
        }
      

        #endregion
    }
}
