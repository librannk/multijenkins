using System;
using System.Linq;
using System.Collections.Generic;
using Logistics.Services.DeviceCommunication.API.Application.Enum;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.Utilities
{
    /// <summary>
    /// This  class is created as an utility class for DeviceCommunication microservice.
    /// </summary>
    public class Utility : IUtility
    {
        #region Public Methods

        /// <summary>
        /// creating storage space items
        /// </summary>
        /// <param name="storageSpaceItems"></param>
        public async Task<Slot> BuildStorageSpaceItem(List<StorageSpace> storageSpaceItems)
        {
            return BuildStorageSpaceSlot(storageSpaceItems);
        }

        /// <summary>
        /// Creating Slot storage spcae item
        /// </summary>
        /// <param name="storageSpaceItems"></param>
        /// <returns></returns>
        private Slot BuildStorageSpaceSlot(List<StorageSpace> storageSpaceItems)
        {
            StorageSpace slot = storageSpaceItems.FirstOrDefault(item => item.ItemType.Equals(((int)StorageSpaceItem.Slot).ToString()));
            var spSlot = default(Slot);
            spSlot = new Slot()
            {
                SlotNum = slot?.Number,
                DispenseForm = slot?.Attribute?.DispenseForm,
                Bin = BuildStorageSpaceBin(storageSpaceItems)
            };

            return spSlot;
        }

        /// <summary>
        /// Creating bin storage space item
        /// </summary>
        /// <param name="storageSpaceItems"></param>
        /// <returns></returns>
        private Bin BuildStorageSpaceBin(List<StorageSpace> storageSpaceItems)
        {
            StorageSpace bin = storageSpaceItems.FirstOrDefault(item => item.ItemType.Equals(((int)StorageSpaceItem.Bin).ToString()));
            var spBin = default(Bin);
            spBin = new Bin()
            {
                BinNum = bin?.Number,
                LeftOffset = bin?.Attribute?.LeftOffset,
                Shelf = BuildStorageSpaceShelf(storageSpaceItems)
            };

            return spBin;
        }

        /// <summary>
        /// Creating shelf storage space item
        /// </summary>
        /// <param name="storageSpaceItems"></param>
        /// <returns></returns>
        private Shelf BuildStorageSpaceShelf(List<StorageSpace> storageSpaceItems)
        {
            var spShelf = default(Shelf);

            spShelf = BuildStorageSpaceShelfShallow(storageSpaceItems);
            spShelf.Rack = BuildStorageSpaceRack(storageSpaceItems);

            return spShelf;
        }

        /// <summary>
        /// Creating shelf shallow storage space item
        /// </summary>
        /// <param name="storageSpaceItems"></param>
        /// <returns></returns>
        private Shelf BuildStorageSpaceShelfShallow(List<StorageSpace> storageSpaceItems)
        {
            StorageSpace shelf = storageSpaceItems.FirstOrDefault(item => item.ItemType.Equals(((int)StorageSpaceItem.Shelf).ToString()));
            var spShelf = default(Shelf);
            if (shelf == null)
                return null;

            spShelf = new Shelf()
            {
                ShelfNum = shelf?.Number,
                OverideBaseAddr = shelf?.Attribute?.OverideBaseAddress
            };

            return spShelf;
        }

        /// <summary>
        /// Creating rack storage space item
        /// </summary>
        /// <param name="storageSpaceItems"></param>
        /// <returns></returns>
        private Rack BuildStorageSpaceRack(List<StorageSpace> storageSpaceItems)
        {
            StorageSpace rack = storageSpaceItems.FirstOrDefault(item => item.ItemType.Equals(((int)StorageSpaceItem.Rack).ToString()));
            var spRack = default(Rack);
            spRack = new Rack()
            {
                RackNum = rack?.Number
            };

            return spRack;
        }

        #endregion
    }
}
