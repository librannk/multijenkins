using BD.Core.ElasticClient.SQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BD.Core.ElasticClient.Extensions
{
    public static class SQLElasticExtension
    {
        /// <summary>
        /// This method is used as an extension to use the common core services method
        /// and applies across all micro-services.
        /// Sequence first Logging, MVC, Authorization, Resilience etc.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IServiceCollection AddElasticDBClient(this IServiceCollection services,
            IConfiguration configuration, ILogger logger)
        {
            services.AddSingleton<IShardMapAccessor, ShardMapAccessor>();
            services.AddSingleton<IShardMapFactory, ShardMapFactory>();
            return services;
        }

        /// <summary> Configure Core services </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseElasticClient(this IApplicationBuilder app, 
            IConfiguration configuration,
            ILogger logger)
        {
            app.ApplicationServices.GetService<IShardMapFactory>().CreateShardMap();
            return app;
        }
    }
}
