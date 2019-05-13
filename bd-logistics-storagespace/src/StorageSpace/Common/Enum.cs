namespace StorageSpace.API.Common
{
    /// <summary> Storage space item type enum </summary>
    public enum StorageSpaceItemType
    {
        /// <summary> None </summary>
        None = 0,

        /// <summary> Rack </summary>
        Rack = 1,

        /// <summary> Shelf </summary>
        Shelf = 2,

        /// <summary> Bin </summary>
        Bin = 3,

        /// <summary> Slot </summary>
        Slot = 4
    }

    /// <summary> StorageSpaceType </summary>
    public enum StorageSpaceType
    {
        /// <summary> None </summary>
        None = 0,

        /// <summary> Carousel </summary>
        Carousel = 1,

        /// <summary> Display </summary>
        Display = 2,
    }
}
