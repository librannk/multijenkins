using BD.Core.EventBus.Abstractions;
using BD.Core.EventBusKafka.Extensions;
using BD.Core.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using StorageSpace.API.Configuration;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Clients;
using StorageSpace.API.IntegrationEvents.EventHandling;
using StorageSpace.API.IntegrationEvents.Events;
using static StorageSpace.API.Common.Constant;
using BD.Core.ElasticClient.Extensions.Mongo;
using BD.Core.Context.Http.Extensions;

namespace StorageSpace.API
{
    /// <summary> Adds and Configure all Services for the Startup.cs file </summary>
    public static class ServiceDependencies
    {
        /// <summary> Adds services required in the Startup.cs file by the API </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env, ILogger logger)
        {
            services
                .AddSwaggerServices()
                .AddEventBusServices(configuration, logger)
                .AddTransient<StorageSpaceRequestEventHandler>()
                .Configure<Configuration.Configuration>(configuration.GetSection(MessageBusTopics))
                .AddOptions();

            services.AddExecutionContext(logger); // .AddElasticMongoClient(configuration, logger);

            services
                .AddSingleton(sp => new MongoDbClient(configuration.GetSection(MongoDbDatabase).Value, configuration.GetSection(MongoDbConnectionString).Value));

            services
                .AddMvcCoreServices(configuration, logger, env)
                .AddAutoMapper();

            return services;
        }

        /// <summary> Configure Services added in the AddServices method </summary>
        /// <param name="app">IApplicationBuilderparam>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        /// <returns>Instance of type IApplicationBuilder</returns>
        public static IApplicationBuilder UseServices(this IApplicationBuilder app, IConfiguration configuration, IHostingEnvironment env,
            ILogger logger, IApiVersionDescriptionProvider provider)
        {
          
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "storagespace/swagger/{documentName}/swagger.json";
            });
            app
                .UseSwaggerUI(c =>
                {
                    //Build a swagger endpoint for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                       c.SwaggerEndpoint($"/api/{description.GroupName}/storagespaces/swagger/swagger.json", "StorageSpace API " + description.GroupName.ToUpperInvariant());
                    }
                    c.RoutePrefix = "api/v1/storagespaces/swagger";
                });

            app
                .UseSwagger(c =>
                {
                    c.RouteTemplate = "api/{documentName}/storagespaces/swagger/swagger.json";
                });
            
            app
                .UseMvcCoreServices(configuration, logger, env)
                .UseEventBusServices(configuration);

            return app;
        }

        /// <summary> Adds swagger to the service collection </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(a =>
            {
                // Resolve the temprary IApiVersionDescriptionProvider service  
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // Add a swagger document for each discovered API version  
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    a.SwaggerDoc(description.GroupName, new Info()
                    {
                        Title = "StorageSpaces.API",
                        Version = description.ApiVersion.ToString(),
                    });
                }

                // Add a custom filter for settint the default values  
                a.OperationFilter<SwaggerDefaultValues>();

                // Tells swagger to pick up the output XML document file  
                a.IncludeXmlComments(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{Assembly.GetExecutingAssembly().GetName().Name}.XML"
                    ));

                //Enable authorization in swagger                
                a.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                a.AddSecurityRequirement(
                    new Dictionary<string, IEnumerable<string>> {
                        { "Bearer", Enumerable.Empty<string>() },
                });
            });

            return services;
        }

        /// <summary> Adds event bus subscriptions </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>Instance of type IApplicationBuilder</returns>
        private static IApplicationBuilder UseEventBusServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<StorageSpaceRequestEvent, StorageSpaceRequestEventHandler>(
                configuration[MessageBusTopicsKafkaRequestTopic],
                configuration[MessageBusTopicsKafkaRequestTopic]);

            return app;
        }
    }
}
