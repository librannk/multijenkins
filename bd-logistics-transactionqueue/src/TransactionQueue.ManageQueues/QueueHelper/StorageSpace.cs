using System;
using System.Linq;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;

namespace TransactionQueue.ManageQueues.QueueHelper
{
    public class StorageSpace
    {
        public static int GetStorageSpace(IEnumerable<ManageQueues.Business.Models.Device> devices, ManageQueues.Business.Models.StorageSpaceItemType storageType)
        {
            int result = 0;
            var carousel = devices.FirstOrDefault(x => x.Type == StorageSpaceType.Carousel.ToString());
            if (carousel != null && carousel.StorageSpaces.Any())
            {
                result = carousel.StorageSpaces.FirstOrDefault(x => x.ItemType == storageType).Number;

            }
            return result;
        }
    }
}
