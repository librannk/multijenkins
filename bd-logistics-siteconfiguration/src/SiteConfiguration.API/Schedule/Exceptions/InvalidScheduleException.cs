using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Schedule.Exceptions
{
    /// <summary>
    /// Custom exception for schedules
    /// </summary>
    [Serializable]
    public class InvalidScheduleException : Exception
    {
        /// <summary>
        /// Only constructor, params are automatically injected
        /// </summary>
        public InvalidScheduleException(string errorMessage, int errorCode):base(errorMessage)
        {
            this.HResult = errorCode;
        }
    }
}
