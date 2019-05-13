using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Logistics.Services.DeviceCommunication.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        #region IServiceCollection Extension Methods

        /// <summary> Register all Swagger dependencies </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "DeviceCommunication.API",
                    Version = "v1",
                });
                
                // Tells swagger to pick up the output XML document file  
                c.IncludeXmlComments(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
                    ));

                //Enable authorization in swagger                
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(
                    new Dictionary<string, IEnumerable<string>> {
                        { "Bearer", Enumerable.Empty<string>() },
                });
            });

            return services;
        }

        #endregion
    }
}

