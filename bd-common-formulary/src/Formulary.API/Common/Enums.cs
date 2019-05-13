using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Common
{
    /// <summary>
    /// Enum for Data Creation
    /// </summary>
    public enum CreateUpdateResultEnum
    {
        /// <summary>
        /// None (Default)
        /// </summary>
        None = 0,

        /// <summary>
        /// Success Result.
        /// </summary>
        Success = 1,

        /// <summary>
        /// Object Already exists.
        /// </summary>
        ErrorAlreadyExists = 2,

        /// <summary>
        /// Not created due to validation failure.
        /// </summary>
        ValidationFailed = 3,

        /// <summary>
        /// Unknown Error
        /// </summary>
        UnknownError = 4,

        /// <summary>
        /// Object not found
        /// </summary>
        NotFound = 5
    }
}
