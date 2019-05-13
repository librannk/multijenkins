using Autofac;
using Autofac.Extensions.DependencyInjection;
using Facility.API.AutofacModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Facility.API
{
    /// <summary>
    /// Startup file for template
    /// </summary>
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

        /// <summary>
        /// this method is used 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServices(Configuration, Environment, Logger);

            //Configure Autofac
            var container = new ContainerBuilder();
            container.RegisterModule(new ApplicationModule());
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="provider">IApiVersionDescriptionProvider</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger(c => {
                c.RouteTemplate = "api/{documentName}/facilities/swagger/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                //Build a swagger endpoint for each discovered API version  
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/api/{description.GroupName}/facilities/swagger/swagger.json", "Facility API " + description.GroupName.ToUpperInvariant());
                }
                c.RoutePrefix = "api/v1/facilities/swagger";

            });

            app.UseServices(Configuration, Environment, Logger);
        }
    }
}
