using Location.API.Dto;
using StorageSpace.API.Common;

namespace StorageSpace.API.Model
{
    /// <summary> Storage space item details </summary>
    public class StorageSpace
    {
        #region Auto-Properties
        /// <summary> Storage space type like Rack, Shelf, Shelf, Bin, Slot </summary>
        public StorageSpaceItemType ItemType { get; set; }

        /// <summary> Storage space item number like Rack Number </summary>
        public int Number { get; set; }

        /// <summary> Storage space item attribute </summary>
        public StorageSpaceItemAttribute Attribute { get; set; }
        #endregion
    }
}
