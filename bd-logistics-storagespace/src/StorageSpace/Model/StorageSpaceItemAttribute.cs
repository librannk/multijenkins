namespace Location.API.Dto
{
    /// <summary> StorageSpaceItemAttribute </summary>
    public class StorageSpaceItemAttribute
    {
        #region Auto-Properties
        /// <summary> DispenseForm </summary>
        public string DispenseForm { get; set; }

        /// <summary> LeftOffset </summary>
        public decimal? LeftOffset { get; set; }

        /// <summary> OverideBaseAddr </summary>
        public int? OverideBaseAddress { get; set; }
        #endregion
    }
}
