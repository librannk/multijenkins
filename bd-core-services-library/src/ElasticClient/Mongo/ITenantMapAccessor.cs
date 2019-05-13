using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    public interface ITenantMapAccessor<TKey>
    { 
        DatabaseMap<TKey> GetOrAddDatabase(TKey tenantKey);
    }
}
