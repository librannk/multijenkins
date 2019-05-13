using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using SiteConfiguration.API.AutofacModule;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.Registration;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Business;
using SiteConfiguration.API.Schedule.Repository;
using SiteConfiguration.API.RoutingRules.Repository;
using SiteConfiguration.API.RoutingRules.Abstractions;
using SiteConfiguration.API.RoutingRules.Business;
using SiteConfiguration.API.Printers.Abstractions;
using SiteConfiguration.API.Printers.Business;
using SiteConfiguration.API.Printers.Repository;
using BD.Core.ElasticClient.Extensions;
using BD.Core.Context.Http.Middlewares;

namespace SiteConfiguration.API
{
    public class Startup
    {
        /// <summary> Configuration of type IConfiguration </summary>
        public IConfiguration Configuration { get; }

        /// <summary> IHostingEnvironment </summary>
        public IHostingEnvironment Environment { get; }
        /// <summary>
        /// 
        /// </summary>
        public ILogger<Startup> Logger { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Environment = env;
            Logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServices(Configuration, Environment, Logger);
            services.AddElasticDBClient(Configuration, logger: Logger);
            services.RegisterDependency();
            //Configure Autofac
            var container = new ContainerBuilder();
            container.RegisterModule(new ApplicationModule());
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/{documentName}/siteconfiguration/swagger/swagger.json";
            });
            app
                .UseSwaggerUI(c =>
                {
                    //Build a swagger endpoint for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/api/{description.GroupName}/siteconfiguration/swagger/swagger.json", "SiteConfiguration API " + description.GroupName.ToUpperInvariant());
                    }
                    c.RoutePrefix = "api/v1/siteconfiguration/swagger";
                });

            app.UseServices(Configuration, Environment, Logger);
            app.UseMiddleware<ExecutionContextMiddleware>().UseElasticClient(Configuration, Logger);
        }
    }
}
