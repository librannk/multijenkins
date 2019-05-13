using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BD.Core.Security.Middleware
{
    /// <summary> AntiForgeryMiddleware </summary>
    public class AntiForgeryMiddleware
    {
        /// <summary>
        /// This Middleware will add anti forgery token in httponly cookie to outgoing request,
        /// when request is web app submit request
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The anti antiforgery token check middles for CSRF check
        /// </summary>
        private readonly IAntiforgery _antiAntiforgery;

        /// <summary>
        /// The anti forgery request token
        /// </summary>
        private const string AntiForgeryRequestToken = "XSRF-REQUEST-TOKEN";

        /// <summary>
        /// The security configuration section
        /// </summary>
        private const string SecurityConfigurationSection = "SecurityConfiguration";

        /// <summary>
        /// The security configuration
        /// </summary>
        private readonly SecurityConfiguration _securityConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AntiForgeryMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="antiforgery">The antiforgery.</param>
        /// <param name="config">The configuration.</param>
        public AntiForgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery, IConfiguration config)
        {
            _antiAntiforgery = antiforgery;
            _next = next;
            _securityConfiguration = config.GetSection(SecurityConfigurationSection).Get<SecurityConfiguration>();
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var tokens = _antiAntiforgery.GetAndStoreTokens(context);
            var antiForgeryCookieOption = new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.None
            };
            if (_securityConfiguration.CookieDomain != null)
                antiForgeryCookieOption.Domain = _securityConfiguration.CookieDomain;
            context.Response.Cookies.Append(AntiForgeryRequestToken, tokens.RequestToken,
                antiForgeryCookieOption);
            await _next(context);
        }
    }
}
