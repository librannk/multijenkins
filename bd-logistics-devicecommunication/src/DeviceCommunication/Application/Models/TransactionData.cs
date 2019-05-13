using System.Collections.Generic;

namespace Logistics.Services.DeviceCommunication.API.Application.Models
{
    /// <summary>
    /// Computer 
    /// </summary>
    public class Computer
    {
        /// <summary> ComputerId </summary>
        public int ComputerId { get; set; }

        /// <summary> PrinterId </summary>
        public int PrinterId { get; set; }

        /// <summary> IsOutsideComputer </summary>
        public bool IsOutsideComputer { get; set; }
    }

    /// <summary>
    /// Storage space  model created to consume data from transaction Queue serivce. 
    /// </summary>
    public class DeviceAttribute
    {
        #region Properties
        /// <summary>
        /// read-write IPAddress property
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// read-write Port property
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// read-write LightDisplayType property
        /// </summary>
        public string LightDisplayType { get; set; }
        /// <summary>
        /// read-write DeviceNumber property
        /// </summary>
        public string DeviceNumber { get; set; }

        /// <summary>
        /// read-write RestrictControl property
        /// </summary>
        public bool RestrictControl { get; set; }
        /// <summary>
        /// read-write MaxRack property
        /// </summary>
        public int MaxRack { get; set; }

        /// <summary>
        /// read-write IsDisplayAttached property
        /// </summary>
        public bool IsDisplayAttached { get; set; }

        /// <summary>
        /// read-write DeviceClass property
        /// </summary>
        public string DeviceClass { get; set; }
        /// <summary>
        /// read-write IsDualAccess property
        /// </summary>
        public bool IsDualAccess { get; set; }
        /// <summary>
        /// read-write ReturnStatus property
        /// </summary>
        public bool ReturnStatus { get; set; }
        /// <summary>
        /// read-write BaseLeftOffset property
        /// </summary>
        public int? BaseLeftOffset { get; set; }
        /// <summary>
        /// read-write ConnectionResetMinutes property
        /// </summary>
        public int? ConnectionResetMinutes { get; set; }
        #endregion
    }
    /// <summary>
    ///class  type StorageSpaceItemAttr
    /// </summary>
    public class StorageSpaceAttribute
    {
        #region Properties
        /// <summary>
        /// read-write LeftOffset property
        /// </summary>
        public decimal? LeftOffset { get; set; }
        /// <summary>
        /// read-write DispenseForm property
        /// </summary>
        public string DispenseForm { get; set; }
        /// <summary>
        /// read-write OverideBaseAddr property
        /// </summary>
        public int? OverideBaseAddress { get; set; }

        #endregion
    }
    /// <summary>
    /// class type StorageSpaceItem
    /// </summary>
    public class StorageSpace
    {
        #region Properties
        /// <summary>
        /// read-write ItemType property
        /// </summary>
        public string ItemType { get; set; }
        /// <summary>
        /// read-write Number property
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// read-write StorageSpaceItemAttr
        /// </summary>
        public StorageSpaceAttribute Attribute { get; set; }

        #endregion
    }

    public class Device
    {
        #region Properties
        /// <summary>
        /// read-write Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// read-write Id
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// read-write StorageSpaceAttr
        /// </summary>
        public DeviceAttribute Attribute { get; set; }

        /// <summary>
        /// Computers
        /// </summary>
        public List<Computer> Computers { get; set; }

        /// <summary>
        /// read-write StorageSpaceItems
        /// </summary>
        public List<StorageSpace> StorageSpaces { get; set; }

        #endregion
    }
    /// <summary>
    /// TransactionData
    /// </summary>
    public class TransactionData
    {
        /// <summary>
        /// read-write storagespaces
        /// </summary>
        public List<Device> Devices { get; set; }
        /// <summary>
        /// read-write quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// read-write Transaction  queue type
        /// </summary>
        public string Type { get; set; }
    }
   
}
