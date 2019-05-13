using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.ManageQueues.Business.Models
{
    /// <summary> Device Attribute </summary>
    public class DeviceAttribute
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for IPAddress
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// To hold value for Port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// To hold value for LightDisplayType
        /// </summary>
        public string LightDisplayType { get; set; }
        /// <summary>
        /// To hold value for DeviceNumber
        /// </summary>
        public string DeviceNumber { get; set; }
        /// <summary>
        /// To hold value for RestrictControl
        /// </summary>
        public bool RestrictControl { get; set; }
        /// <summary>
        /// To hold value for MaxRack
        /// </summary>
        public int MaxRack { get; set; }
        /// <summary>
        /// To hold value for IsDisplayAttached
        /// </summary>
        public bool IsDisplayAttached { get; set; }
        /// <summary>
        /// To hold value for DeviceClass
        /// </summary>
        public string DeviceClass { get; set; }
        /// <summary>
        /// To hold value for IsDualAccess
        /// </summary>
        public bool IsDualAccess { get; set; }
        /// <summary>
        /// To hold value for ReturnStatus
        /// </summary>
        public bool ReturnStatus { get; set; }
        /// <summary>
        /// To hold value for BaseLeftOffset
        /// </summary>
        public int? BaseLeftOffset { get; set; }
        #endregion
    }
}
