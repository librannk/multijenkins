using BD.Core.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;

namespace BD.Core.ElasticClient.Mongo
{
    public class ElasticDbContext : IElasticDbContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILogger<ElasticDbContext> _logger;
        private readonly ITenantMapAccessor<string> _tenantMapAccessor;
        private readonly IConfiguration _configuration;

        public ElasticDbContext(IServiceProvider servideProvider)     
        {
            _executionContextAccessor = servideProvider.GetService<IExecutionContextAccessor>();
            _configuration = servideProvider.GetService<IConfiguration>();
            _logger = servideProvider.GetService<ILogger<ElasticDbContext>>();
            _tenantMapAccessor= servideProvider.GetService<ITenantMapAccessor<string>>();
        }
        
        public IMongoDatabase GetContext()
        {
            return _tenantMapAccessor.GetOrAddDatabase
                (_executionContextAccessor.Current.Tenant.TenantKey)
                .DatabaseContext;
        }
    }
}
