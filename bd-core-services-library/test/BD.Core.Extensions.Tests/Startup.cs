
using System;
using BD.Core.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BD.Core.Extensions.Tests
{
    /// <summary>
    /// Mock Start up for testing
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
            _logger = loggerFactory.CreateLogger<Startup>();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCoreServices(Configuration, _logger, _hostingEnvironment);
        }

        /// <summary>
        /// Handles the global exception filter.
        /// </summary>
        /// <param name="app">The application.</param>
        private static void HandleGlobalExceptionFilter(IApplicationBuilder app)
        {
            app.Run(context => throw new Exception("test"));
        }
    }
}
