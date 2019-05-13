using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    public interface IListTenantDatabaseMap<TKey>
    {
        DatabaseMap<TKey> GetOrAddDatabase(TKey tenantKey);
    }
}
