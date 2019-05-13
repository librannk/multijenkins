
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Formulary.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        // METHODS
        #region IServiceCollection Extension Methods
        /// <summary> Register all Event-Buses details </summary>
        /// <param name="services"> Of type IServiceCollection </param>
        /// <param name="configuration"> Of type IConfiguration </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterJWTAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
        {
            //Adding Authentication and JWT parameters to authenticate the token
            var setting = configuration.GetSection("IdentityServer");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.Authority = setting["Endpoint"];
              options.Audience = setting["Audience"];
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
                  ValidAudience = setting["Audience"],
                  ValidateIssuer = true,
                  ValidIssuer = setting["Endpoint"]
              };
          });

            return services;
        }
        #endregion
    }
}
