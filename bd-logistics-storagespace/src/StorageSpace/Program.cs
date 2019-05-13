using BD.Core.Logging.Enrichers;
using BD.Core.Logging.Formatters;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using StorageSpace.API.Common;
using System;
using static StorageSpace.API.Common.Constant;

namespace StorageSpace.API
{
    /// <summary> Program class </summary>
    public class Program
    {
        /// <summary> Main method, also the entry point of the application </summary>
        /// <param name="args"></param>
        public static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"{Appsettings}{Dot}{JsonLower}", optional: true, reloadOnChange: true)
                .AddJsonFile($"{Appsettings}{Dot}{Environment.GetEnvironmentVariable(AspNetCoreEnvironment) ?? Production}{Dot}{JsonLower}",
                    optional: true,
                    reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.With(new CustomEnricher(configuration: configuration))
                .WriteTo.Async(a => a.Console(formatter: new CustomFormatter(configuration: configuration)))
                .CreateLogger();

            try
            {
                Log.Information(LoggingConstants.StartingWebHost);
                CreateWebHostBuilder(args).Build().Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, LoggingConstants.HostTerminatedUnexpectedly);

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary> CreateWebHostBuilder </summary>
        /// <param name="args">array of string</param>
        /// <returns>of type IWebHostBuilder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
