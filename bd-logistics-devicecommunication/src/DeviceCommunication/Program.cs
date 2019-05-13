﻿using System;
using Serilog;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using BD.Core.Logging.Enrichers;
using BD.Core.Logging.Formatters;

namespace Logistics.Services.DeviceCommunication.API
{
    /// <summary>
    /// Program class
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// Main method entry point
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "DEVELOPMENT"}.json", optional: true, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.With(new CustomEnricher(configuration: configuration))
            .WriteTo.Async(a => a.Console(formatter:
                                new CustomFormatter(configuration: configuration)))
            .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }


        /// <summary>
        /// CreateWebHostBuilder method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()            
                .UseSerilog();
    }
}
