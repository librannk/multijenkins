using Microsoft.AspNetCore.Http;
using System;

namespace Facility.API.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for current HttpContext 
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// The count key to be used for maintaining the total result count.
        /// </summary>
        private const string CountKey = "TotalPageCount";

        /// <summary>
        /// Sets the total count in the items dictionary in current HTTP context.
        /// </summary>
        /// <param name="currentHttpContext">The current HTTP context.</param>
        /// <param name="count">The count.</param>
        public static void SetTotalCount(this HttpContext currentHttpContext, int count)
        {
            currentHttpContext?.Items?.Add(CountKey, count);
        }

        /// <summary>
        /// Tries to set the header value for current count. If not set, ignores request.
        /// </summary>
        /// <param name="currentHttpContext">The current HTTP context.</param>
        /// <param name="headerKey">The header key.</param>
        public static void TrySetHeaderValue(this HttpContext currentHttpContext, string headerKey)
        {
            var contextItems = currentHttpContext?.Items;
            if (contextItems != null && contextItems.ContainsKey(CountKey))
            {
                var count = Convert.ToInt32(contextItems[CountKey]);
                currentHttpContext.Response.Headers.Add(headerKey, count.ToString());
            }
        }
    }
}
