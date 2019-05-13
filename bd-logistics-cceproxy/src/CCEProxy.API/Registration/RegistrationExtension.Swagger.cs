using CCEProxy.API.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CCEProxy.API.Registration
{
    public static partial class RegistrationExtension
    {
        // METHODS
        #region IServiceCollection Extension Methods
        /// <summary> Register all Swagger dependencies </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Resolve the temprary IApiVersionDescriptionProvider service  
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                //Add a swagger document for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new Info()
                    {
                        Title = "CCEProxy.API",
                        Version = description.ApiVersion.ToString(),
                    });
                }

                // Add a custom filter for settint the default values  
                c.OperationFilter<SwaggerDefaultValues>();

                // Tells swagger to pick up the output XML document file  
                c.IncludeXmlComments(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{Assembly.GetExecutingAssembly().GetName().Name}.XML"
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
