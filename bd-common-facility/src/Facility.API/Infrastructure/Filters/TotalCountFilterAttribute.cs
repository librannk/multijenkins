using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facility.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Facility.API.Infrastructure.Filters
{

    /// <summary>
    /// Class TotalCountFilterAttribute.
    /// Implements the <see cref="ActionFilterAttribute" />
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    public class TotalCountFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets the name of the header key that should be used for providing the result count in headers.
        /// </summary>
        /// <value>The name of the header key.</value>
        public string HeaderKeyName { get; } = "X-Total-Count";

        /// <summary>
        /// Called when action is executed and sets the count header.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.TrySetHeaderValue(HeaderKeyName);
        }
    }
}
