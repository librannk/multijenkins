using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageSpace.API.Common
{
    /// <summary>
    /// Logging Constants Detail
    /// </summary>
    public class LoggingConstants
    {
        /// <summary> STARTING_WEB_HOST </summary>
        public const string StartingWebHost = "Starting web host";

        /// <summary> HOST_TERMINATED_UNEXPECTEDLY </summary>
        public const string HostTerminatedUnexpectedly = "Host terminated unexpectedly";

        /// <summary>
        /// Constant for empty list.
        /// </summary>
        public const string Empty_List = "List Contains No Result";

        /// <summary>
        /// Logging constant for GetIsa
        /// </summary>
        public const string GetISA = "Get ISA with Facility Id : {0}";

        /// <summary>
        /// Logging constant for getting ISA by Id.
        /// </summary>
        public const string GetISAById = "Get ISA with IsaId and Facility Id : {0}";
    }
}
