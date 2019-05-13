namespace BD.Core.Security
{
    /// <summary>
    /// The security configuration class implementation.
    /// </summary>
    internal class SecurityConfiguration
    {
        /// <summary>
        /// Identity Server for checking jwt token
        /// </summary>
        /// <value>
        /// Identity Server.
        /// </value>
        public virtual string Authority { get; set; }

        /// <summary>
        /// Identity Server for checking jwt token
        /// </summary>
        /// <value>
        /// Identity Server.
        /// </value>
        public virtual string Audience { get; set; }
        /// <summary>
        /// Identity Server for checking jwt token
        /// </summary>
        /// <value>
        /// Identity Server.
        /// </value>
        public virtual string Endpoint { get; set; }
        /// <summary>
        /// Identity Server for checking jwt token
        /// </summary>
        /// <value>
        /// Identity Server.
        /// </value>
        public virtual string IdentityServer { get; set; }

        /// <summary>
        /// The anti forgery token header
        /// </summary>
        /// <value>
        /// The anti forgery token header.
        /// </value>
        public virtual string AntiForgeryTokenHeader { get; set; }

        /// <summary>
        /// The security application group
        /// </summary>
        /// <value>
        /// The security application group.
        /// </value>
        public virtual string SecurityApplicationGroup { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [cors enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cors enabled]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool CorsEnabled { get; set; }
        /// <summary>
        /// The cors policy
        /// </summary>
        /// <value>
        /// The cors policy.
        /// </value>
        public virtual CorsPolicy CorsPolicy { get; set; }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL for login
        /// </value>
        public virtual string LoginPath { get; set; }

        /// <summary>
        /// Gets or sets the cookie domain.
        /// </summary>
        /// <value>
        /// The cookie domain.
        /// </value>
        public virtual string CookieDomain { get; set; }

        /// <summary>
        /// Gets or sets the cookie authentication ticket expiry in minutes.
        /// </summary>
        /// <value>
        /// The cookie authentication ticket expiry in minutes.
        /// </value>
        public virtual string CookieAuthenticationTicketExpiryInMinutes { get; set; }

        /// <summary>
        /// Gets or set Disable Cookie Secure Flag. By Default Cookie is secured. If needed set it to false in appsettings.
        /// </summary>
        /// <value>
        /// Secure Flag
        /// </value>
        public virtual bool DisableCookieSecureFlag { get; set; }

    }

    /// <summary>
    /// The Cors policy class configuration.
    /// </summary>
    public class CorsPolicy
    {
        /// <summary>
        /// This is to http domains allowed for Cors Policy
        /// </summary>
        /// <value>
        /// The allow white list domains.
        /// </value>
        public virtual string[] AllowWhiteListDomains { get; set; }
        /// <summary>
        /// This is to http headers allowed for Cors Policy
        /// </summary>
        /// <value>
        /// The allow headers.
        /// </value>
        public virtual string[] AllowHeaders { get; set; }
        /// <summary>
        /// This is to http methods allowed for Cors Policy
        /// </summary>
        /// <value>
        /// The allow methods.
        /// </value>
        public virtual string[] AllowMethods { get; set; }

    }
}