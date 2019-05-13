using System;
using System.Collections.Generic;
using System.Text;
using BD.Core.ElasticClient.Mongo.Providers;
namespace BD.Core.ElasticClient.Mongo
{
    public class TenantMapAccessor<TKey> : ITenantMapAccessor<TKey>
    {
        private readonly IConfigurationProvider<TKey> _configuration;
        private readonly IListTenantDatabaseMap<TKey> _listTenantDatabaseMap;
        public TenantMapAccessor(IConfigurationProvider<TKey>  configuration,
            IListTenantDatabaseMap<TKey> listTenantDatabaseMap)
        {
            _configuration = configuration;
            _listTenantDatabaseMap = listTenantDatabaseMap;
        }
        public DatabaseMap<TKey> GetOrAddDatabase(TKey tenantKey)
        {
            return _listTenantDatabaseMap.GetOrAddDatabase(tenantKey);
        }
    }
}
