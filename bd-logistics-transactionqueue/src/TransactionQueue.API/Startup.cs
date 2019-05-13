using Autofac;
using Autofac.Extensions.DependencyInjection;
using TransactionQueue.API.AutofacModule;
using TransactionQueue.API.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TransactionQueue.API;
using TransactionQueue.SocketManager;
using TransactionQueue.SocketManager.Handlers;

namespace TransactionQueue.API
{
    /// <summary> Startup class </summary>
    public partial class Startup
    {
        /// <summary> Configuration of type IConfiguration </summary>
        public IConfiguration Configuration { get; }

        /// <summary> IHostingEnvironment </summary>
        public IHostingEnvironment Environment { get; }
        /// <summary>
        /// 
        /// </summary>
        public ILogger<Startup> Logger { get; }

        #region Constructors
        /// <summary> Startup Constructor </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Environment = env;
            Logger = logger;
        }
        #endregion

        #region Public Methods
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>  </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServices(Configuration, Environment, Logger);

            //This line adds Swagger generation services to our container.
            services.RegisterSwagger();

            //configure autofac
            var container = new ContainerBuilder();
            container.RegisterModule(new ApplicationModule());
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>  </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="serviceProvider"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseServices(Configuration, Environment, Logger);

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/{documentName}/transactionqueue/swagger/swagger.json";
            });
            app
                .UseSwaggerUI(c =>
                {
                    //Build a swagger endpoint for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/api/{description.GroupName}/transactionqueue/swagger/swagger.json", "TransactionQueue API " + description.GroupName.ToUpperInvariant());
                    }
                    c.RoutePrefix = "api/v1/transactionqueue/swagger";
                });

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024,
            };
            app.UseWebSockets(webSocketOptions);
            app.MapWebSocketManager("/notifications", serviceProvider.GetService<TQMessageHandler>());
        }
        #endregion
    }
}