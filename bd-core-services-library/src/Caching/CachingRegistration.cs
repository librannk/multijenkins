using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caching
{
    /// <summary>
    /// Redis registration class
    /// </summary>
    public static class CachingRegistration
    {
            /// <summary>
            /// This method is used to register the redis
            /// </summary>
            /// <param name="services"></param>
            /// <param name="configuration"></param>
            /// <returns></returns>
            public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
            {
                var connectionString = configuration.GetSection("Redis:ConnectionString").Value;
                var instance = configuration.GetSection("Redis:Instance").Value;
                services.AddDistributedRedisCache(option =>
                {
                    option.Configuration = connectionString;
                    option.InstanceName = instance;
                });
                return services;
            }
        
    }
}
