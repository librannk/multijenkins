using System;

using Logistics.Services.DeviceCommunication.API.DeviceInterface.Enum;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces
{
    /// <summary>
    /// Interface to implement device status class
    /// </summary>
    public interface IDeviceResponse
    {
        #region Properties
        /// <summary>
        /// read-write property HasError
        /// </summary>
        bool HasError { get; set; }
        /// <summary>
        /// read-write property Message
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// read-write property CurrentCarrier
        /// </summary>
        int CurrentCarrier { get; set; }
        /// <summary>
        /// read-write property ErrorCode
        /// </summary>
        int ErrorCode { get; set; }

        #endregion

    }
}
