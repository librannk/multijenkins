using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CCEProxy.API.AutofacModule;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using BD.Core.Context.Http.Middlewares;

namespace CCEProxy.API
{
    /// <summary>
    /// Startup Class for the application
    /// </summary>
    public class Startup
    {
        /// <summary> Configuration of type IConfiguration </summary>
        public IConfiguration Configuration { get; }

        /// <summary> IHostingEnvironment </summary>
        public IHostingEnvironment Environment { get; }

        /// <summary> ILogger </summary>
        public ILogger<Startup> Logger { get; }

        /// <summary> Startup constructor </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logger">ILogger</param>
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Environment = env;
            Logger = logger;
        }

        /// <summary> This method gets called by the runtime. Use this method to add services to the container. </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServices(Configuration, Environment, Logger);

            //configure autofac
            var container = new ContainerBuilder();
            container.RegisterModule(new ApplicationModule());
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        /// <summary> Gets called by the runtime. Use this method to configure the HTTP request pipeline. </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExecutionContextMiddleware>();

            app.UseServices(Configuration, Environment, Logger);

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/{documentName}/cceproxy/swagger/swagger.json";
            });
            app
                .UseSwaggerUI(c =>
                {
                    //Build a swagger endpoint for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/api/{description.GroupName}/cceproxy/swagger/swagger.json", "CCEProxy API " + description.GroupName.ToUpperInvariant());
                    }
                    c.RoutePrefix = "api/v1/cceproxy/swagger";
                });
        }
    }
}
