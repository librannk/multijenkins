using System;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.FacilityConfiguration.RequestResponseModel
{
    /// <summary>
    /// BusinessResponse entity
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BusinessResponse
    {
        /// <summary>
        /// Key for GUID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message key
        /// </summary>
        public string Message { get; set; }
    }
}
