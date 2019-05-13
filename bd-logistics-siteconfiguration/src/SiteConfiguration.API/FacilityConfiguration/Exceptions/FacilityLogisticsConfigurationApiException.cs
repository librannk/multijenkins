using System;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.FacilityConfiguration.Exceptions
{
    /// <summary>
    ///  class map for API exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FacilityLogisticsConfigurationApiException : Exception
    {
        /// <summary>
        /// Constructor for auto initialization
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        public FacilityLogisticsConfigurationApiException(string errorMessage, string errorCode): base(errorMessage)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Error Code Key
        /// </summary>
        public string ErrorCode { get; }
    }
}
