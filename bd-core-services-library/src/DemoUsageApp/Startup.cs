using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BD.Core.Context.Http.Extensions;
using BD.Core.Context.Http.Middlewares;
using BD.Core.ElasticClient.Extensions;
using BD.Core.Extension;
using DemoUsageApp.Infrastructure;
using BD.Core.ElasticClient.Mongo;
using BD.Core.ElasticClient.Extensions.Mongo;

namespace DemoUsageApp
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private ILogger<Startup> Logger { get; }
        public Startup(IConfiguration configuration,ILogger<Startup> logger, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvcCoreServices(Configuration,Logger, _env);
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<FacilityDBContext>(ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<Infrastructure.MongoRepository.IFacilityRepository,
                Infrastructure.MongoRepository.FacilityRepository>();
            services.AddElasticDBClient(Configuration, logger: Logger);

            services.AddElasticMongoClient(Configuration,Logger);
            //services.AddScoped<ITenantMapAccessor<string>, TenantMapAccessor<string>>();

          
            //var sp = services.BuildServiceProvider();
            //var con = sp.GetRequiredService<BD.Core.ElasticClient.Mongo.IConfigurationProvider<string>>();
            
            //services.AddSingleton(new ElasticDbContext(sp));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseMiddleware<ExecutionContextMiddleware>()
                .UseMvcCoreServices(Configuration,Logger, env)
                .UseElasticClient(Configuration, Logger);
        }
    }
}
