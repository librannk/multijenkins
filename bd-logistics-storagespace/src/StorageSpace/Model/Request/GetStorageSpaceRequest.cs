namespace StorageSpace.API.Model.Request
{
    /// <summary> GetStorageSpaceRequest </summary>
    public class GetStorageSpaceRequest
    {
        #region Auto-Properties
        /// <summary> FormularyId </summary>
        public int FormularyId { get; set; }

        /// <summary> FacilityId </summary>
        public int FacilityId { get; set; }

        /// <summary> ISAId </summary>
        public int ISAId { get; set; }
        #endregion
    }
}
