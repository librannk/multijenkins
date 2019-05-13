using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.ManageQueues.Business.Models
{
    /// <summary> Device</summary>
    public class Device
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// To hold value for DeviceId
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// To hold value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To hold value for FormularyId
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// To hold value for IsDefault
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// To hold value for IsStatic
        /// </summary>
        public bool IsStatic { get; set; }
        /// <summary>
        /// To hold value for ShortDescription
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// To hold value for ConnectionResetMinutes
        /// </summary>
        public int? ConnectionResetMinutes { get; set; }
        /// <summary>
        /// To hold value for DeviceAttribute
        /// </summary>
        public DeviceAttribute Attribute { get; set; }
        /// <summary>
        /// To hold value for StorageSpaceItems
        /// </summary>
        public List<StorageSpace> StorageSpaces { get; set; }

        /// <summary> Computers </summary>
        public List<Computer> Computers { get; set; }
        #endregion
    }
}
