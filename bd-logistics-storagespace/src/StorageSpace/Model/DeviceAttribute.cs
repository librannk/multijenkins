namespace StorageSpace.API.Model
{
    /// <summary> StorageSpaceAttribute </summary>
    public class DeviceAttribute
    {
        #region Auto-Properties
        /// <summary> IP Address </summary>
        public string IPAddress { get; set; }

        /// <summary> Port </summary>
        public int Port { get; set; }

        /// <summary> Device class </summary>
        public string DeviceClass { get; set; }

        /// <summary> Device number </summary>
        public string DeviceNumber { get; set; }

        /// <summary> Is dual access </summary>
        public bool IsDualAccess { get; set; }

        /// <summary> Restrict Control </summary>
        public bool RestrictControl { get; set; }

        /// <summary> Max Rack </summary>
        public int MaxRack { get; set; }

        /// <summary> IsDisplayAttached </summary>
        public bool IsDisplayAttached { get; set; }// new added

        /// <summary> ReturnStatus </summary>
        public bool ReturnStatus { get; set; }// new added

        /// <summary> BaseLeftOffset </summary>
        public int? BaseLeftOffset { get; set; }// new added
        #endregion
    }
}
