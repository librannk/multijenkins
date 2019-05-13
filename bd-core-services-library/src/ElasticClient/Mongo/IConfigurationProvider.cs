using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    public interface IConfigurationProvider<TKey>
    {
        TenantDatabaseConfiguration<TKey> GetTenantDatabase(TKey tenantKey);

    }
}
