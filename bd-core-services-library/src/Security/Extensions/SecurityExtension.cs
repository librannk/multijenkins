using BD.Core.Security.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;

namespace BD.Core.Security.Extensions
{
    /// <summary>
    /// Security Extension Will Add Cookie Authentication and Cors Policy, 
    /// </summary>
    /// TO DO: To be replaced by actual policy extension
    public static class SecurityExtension
    {
        /// <summary>
        /// The cors policy name
        /// </summary>
        private const string CorsPolicyName = "CorsPolicy";

        /// <summary>
        /// The anti frogery cookie
        /// </summary>
        private const string AntiForgeryCookie = "XSRF-TOKEN";

        /// <summary>
        /// The security configuration section
        /// </summary>
        private const string SecurityConfigurationSection = "SecurityConfiguration";

        /// <summary>
        /// The security configuration
        /// </summary>
        private static SecurityConfiguration _securityConfiguration;

        /// <summary>
        /// Adds Security For Authenticating and Authorizing User.
        /// </summary>
        /// <param name="services">Core Services Collection that needs to be added</param>
        /// <param name="env">Hosting Environment</param>
        /// <param name="config">Configuration</param>
        /// <param name="logger">The logger.</param>
        /// <returns></returns>
        public static IServiceCollection AddSecurityPolicy(this IServiceCollection services, IHostingEnvironment env, IConfiguration config, ILogger logger)
        {
            //Load Security Configurations
            logger.LogInformation("Loading Security Configurations");
            _securityConfiguration = config.GetSection(SecurityConfigurationSection).Get<SecurityConfiguration>();

            if (_securityConfiguration is null)
            {
                logger.LogCritical("Security Section Not Defined In Configuration");
            }
            else
            {
                //Adding Authentication and JWT parameters to authenticate the token
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = _securityConfiguration.Endpoint;
                    options.Audience = _securityConfiguration.Audience;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Clock skew compensates for server time drift.
                        // We recommend 5 minutes or less:
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = _securityConfiguration.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = _securityConfiguration.Endpoint
                    };
                });

                //After Authentication Add Cors Policy if it is enabled.
                if (_securityConfiguration.CorsPolicy != null)
                {
                    logger.LogInformation("Adding Cors Policy");
                    services.AddCors(options =>
                    {
                        options.AddPolicy(CorsPolicyName,
                            builder => builder.WithOrigins(_securityConfiguration.CorsPolicy.AllowWhiteListDomains)
                                .WithHeaders(_securityConfiguration.CorsPolicy.AllowHeaders)
                                .WithMethods(_securityConfiguration.CorsPolicy.AllowMethods)
                                .AllowCredentials());
                    });
                }
                if (!string.IsNullOrWhiteSpace(_securityConfiguration.AntiForgeryTokenHeader))
                {
                    logger.LogInformation("Adding Antiforgery Token");
                    var antiForgeryCookieBuilder = new CookieBuilder
                    {
                        Name = AntiForgeryCookie,
                        HttpOnly = true
                    };
                    if (_securityConfiguration.CookieDomain != null)
                        antiForgeryCookieBuilder.Domain = _securityConfiguration.CookieDomain;
                    services.AddAntiforgery(options =>
                    {
                        options.HeaderName = _securityConfiguration.AntiForgeryTokenHeader;
                        options.Cookie = antiForgeryCookieBuilder;
                    });
                }
            }
            logger.LogInformation("Security Configurations Loaded");
            return services;
        }

        /// <summary>
        /// Uses the Security policy.
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">Environment Context like development,QA,Production</param>
        /// <param name="config">Configuration reference.</param>
        /// <param name="logger">logger</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSecurityPolicy(this IApplicationBuilder app, IHostingEnvironment env, IConfiguration config, ILogger logger)
        {
            //First Authenticate, Use Cors,Add Anti Forgery Middleware
            if (_securityConfiguration != null)
            {
                if (!string.IsNullOrWhiteSpace(_securityConfiguration.Endpoint))
                    app.UseAuthentication();
                if (_securityConfiguration.CorsPolicy != null)
                    app.UseCors(CorsPolicyName);
                if (!string.IsNullOrWhiteSpace(_securityConfiguration.AntiForgeryTokenHeader))
                    app.UseMiddleware<AntiForgeryMiddleware>();
            }

            return app;
        }
    }
}