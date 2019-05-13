using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BD.Core.ElasticClient.Mongo;

namespace BD.Core.ElasticClient.Extensions.Mongo
{
    public static class MongoElasticExtension
    {
        public static IServiceCollection AddElasticMongoClient(this IServiceCollection services,
                      IConfiguration configuration, ILogger logger)
        {
            services.AddScoped<IConfigurationProvider<string>, ElasticClient.Mongo.Providers.ShardConfigurationDataProvider<string>>();
            services.AddSingleton<IListAccountMap, ListAccountMap>();
            services.AddSingleton<IListTenantDatabaseMap<string>, ListTenantDatabaseMap<string>>();
            services.AddSingleton<ITenantMapAccessor<string>, TenantMapAccessor<string>>();
            var sp = services.BuildServiceProvider();
            services.AddSingleton(new ElasticDbContext(sp));
            return services;
        }
    }
}
