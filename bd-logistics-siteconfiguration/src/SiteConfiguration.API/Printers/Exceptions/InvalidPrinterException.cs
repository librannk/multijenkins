using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Exceptions
{ 
    /// <summary>
    /// Custom exception for printers.
    /// </summary>
    [Serializable]
    public class InvalidPrinterException : Exception
    {
        /// <summary>
        /// Only constructor, params are automatically injected
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        public InvalidPrinterException(string errorMessage, int errorCode) : base(errorMessage)
        {
            this.HResult = errorCode;
        }
    }
}
