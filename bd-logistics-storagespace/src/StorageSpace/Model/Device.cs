using System.Collections.Generic;

namespace StorageSpace.API.Model
{
    /// <summary> Storage space details </summary>
    public class Device
    {
        #region Auto-Properties
        /// <summary> Storage space type </summary>
        public string Type { get; set; }

        /// <summary> Storage space identifier </summary>
        public int DeviceId { get; set; }

        /// <summary> Flag to identify whether it is default or not </summary>
        public bool IsDefault { get; set; }

        /// <summary> IsStatic </summary>
        public bool IsStatic { get; set; }

        /// <summary> ShortDescription </summary>
        public string ShortDescription { get; set; }

        /// <summary> ConnectionResetMinutes </summary>
        public int? ConnectionResetMinutes { get; set; }

        /// <summary> Storage space attribute details </summary>
        public DeviceAttribute Attribute { get; set; }

        /// <summary> Storage space items </summary>
        public List<StorageSpace> StorageSpaces { get; set; }

        /// <summary> Computers </summary>
        public List<Computer> Computers { get; set; }
        #endregion
    }
}
