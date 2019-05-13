using System;
using Autofac;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Logistics.Services.DeviceCommunication.API.Registration;
using Logistics.Services.DeviceCommunication.API.AutofacModule;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Microsoft.Extensions.Logging;

namespace Logistics.Services.DeviceCommunication.API
{
    /// <summary>
    /// Main StartUp Class
    /// </summary>
    public class Startup
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// 
        /// </summary>
        public ILogger<Startup> Logger { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Environment = env;
            Logger = logger;
        }

        #endregion

        #region Public Methods for Startup Configuration

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="serviceProvider"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseAuthentication()
                .UseMvc()        
                .UseSwagger();

            //This line enables Swagger UI, which provides us with a nice, simple UI with which we can view our API calls.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Constants.Swagger.SwaggerEndPoint, $"{Constants.Swagger.Title } {Constants.Swagger.Version}");
                c.RoutePrefix = Constants.Swagger.RoutePrefix;
            });

            //Use service dependencies
            app.UseServices(configuration: Configuration, env: env, logger: Logger);

            //API message display middleware
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("DeviceCommunication.API is listening.......!");
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Dependency injection for the application
            services.AddApplicationServices();
            
            //Adding dependecy services
            services.AddGlobalServices(Configuration, Environment, Logger);

            //Add swagger
            services.AddSwagger();

            //configure autofac
            var container = new ContainerBuilder();
            container.RegisterModule(new ApplicationModule());
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        #endregion
    }
}