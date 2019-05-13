using Jaeger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using System;

namespace BD.Core.Logging.Extensions
{
    public static class TracingExtension
    {
        /// <summary>
        /// Add distributed tracing to services, configuring JAEGER client for tracing.
        /// </summary>
        /// <param name="services" cref="IServiceCollection">Service collection.</param>
        /// <param name="serviceName">Service name to be passed to JAEGER client.If not passed by default dll name would be considered as service name for development environment.</param>
        public static void AddTracing(this IServiceCollection services, string serviceName = null)
        {
            // Use "OpenTracing.Contrib.NetCore" to automatically generate spans for ASP.NET Core, Entity Framework Core, ...
            // See https://github.com/opentracing-contrib/csharp-netcore for details.
            services.AddOpenTracing();

            // Adds the JAEGER Tracer.
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                Tracer tracer = null;
                Jaeger.Configuration jaegerConfig = null;
                var hostingEnv = serviceProvider.GetRequiredService<IHostingEnvironment>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                // Add project name as service name as a default to JAEGER client if not passed from outside as a parameter.
                serviceName = serviceName ?? hostingEnv.ApplicationName;
                try
                {
                    jaegerConfig = hostingEnv.IsDevelopment()
                        ? new Jaeger.Configuration(serviceName, loggerFactory)
                       : Jaeger.Configuration.FromEnv(loggerFactory);
                }
                catch (Exception)
                {
                    jaegerConfig = new Jaeger.Configuration(serviceName, loggerFactory);
                }

                // Get tracer instance with configurations.
                tracer = (Tracer)jaegerConfig
                    .WithSampler(Jaeger.Configuration.SamplerConfiguration.FromEnv(loggerFactory)) // This will fetch all the configurations for Sampler from environment variables.
                    .WithReporter(Jaeger.Configuration.ReporterConfiguration.FromEnv(loggerFactory)) // This will fetch all the configurations for Reporter from environment variables.
                    .WithTraceId128Bit(true)
                    .GetTracer();

                // Allows code that can't use DI to also access the tracer.
                GlobalTracer.Register(tracer);

                return tracer;
            });
        }
    }
}
