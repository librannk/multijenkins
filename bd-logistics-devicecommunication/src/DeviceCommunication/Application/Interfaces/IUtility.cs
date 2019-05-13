using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;

namespace Logistics.Services.DeviceCommunication.API.Application.Interfaces
{
    /// <summary>
    /// IUtility interface
    /// </summary>
    public interface IUtility
    {
        /// <summary>
        /// creating storage space items
        /// </summary>
        /// <param name="storageSpaceItems"></param>
        /// <returns></returns>
        Task<Slot> BuildStorageSpaceItem(List<StorageSpace> storageSpaceItems);
    }
}
