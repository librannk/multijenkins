//using System.Collections.Generic;
//using TransactionQueue.API.Application.Entities;
//using TransactionQueue.API.Application.Models.Enums;

//namespace TransactionQueue.UnitTest.Common
//{
//    /// <summary>
//    /// This class is responsible for providing mock data to test cases
//    /// </summary>
//    public class StorageLocationChildObject
//    {
//        /// <summary>
//        /// return storage location for transaction
//        /// </summary>
//        public static List<Device> StorageLocations
//        {
//            get
//            {
//                return new List<Device>
//                {
//                    new Device
//                    {
//                        DeviceId=1,
//                        IsDefault=true,
//                        Type=StorageSpaceType.Carousel.ToString(),
//                        Attribute= new DeviceAttribute()
//                        {
//                            DeviceClass="DeviceClass",
//                            DeviceNumber="1",
//                            IPAddress="172.16.22.134",
//                            IsDualAccess=true,
//                            MaxRack=10,
//                            Port=8080,
//                            RestrictControl=false,
//                        },
//                        StorageSpaces= new List<StorageSpace>
//                        {
//                            new StorageSpace { ItemType = StorageSpaceItemType.Rack, Number=2 }
//                        }
//                    },
//                    new Device
//                    {
//                        DeviceId=1,
//                        IsDefault=true,
//                        Type=StorageSpaceType.Display.ToString(),
//                        Attribute= new DeviceAttribute
//                        {
//                            DeviceClass="DisplayClass",
//                            DeviceNumber="1",
//                            IPAddress="172.16.22.134",
//                            IsDualAccess=true,
//                            MaxRack=10,
//                            Port=8080,
//                            RestrictControl=false,
//                        },
//                        StorageSpaces= new List<StorageSpace>
//                        {
//                            new StorageSpace { ItemType = StorageSpaceItemType.Rack, Number=2 }
//                        }
//                    }
//                };
//            }
//        }
//    }
//}
