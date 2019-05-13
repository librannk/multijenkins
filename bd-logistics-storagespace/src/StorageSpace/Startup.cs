using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using StorageSpace.API.AutofacModule;
using BD.Core.ElasticClient.Extensions.Mongo;
using BD.Core.Context.Http.Middlewares;

namespace StorageSpace.API
{
    /// <summary> Startup class </summary>
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

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServices(Configuration, Environment, Logger);

            // Configure Autofac
            var container = new ContainerBuilder();
            container.RegisterModule(new ApplicationModule());
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        /// <summary> This method gets called by the runtime. Use this method to configure the HTTP request pipeline. </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="provider">IApiVersionDescriptionProvider</param>
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseMiddleware<ExecutionContextMiddleware>()
                .UseServices(Configuration, Environment, Logger, provider);
        }
    }
}
