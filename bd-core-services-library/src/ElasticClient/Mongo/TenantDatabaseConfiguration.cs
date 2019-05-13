using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{

    public class TenantDatabaseConfiguration<TKey>
    {
        public TenantDatabaseConfiguration(
            TKey tenantKey,
            string accountKey,
            string tenantDatabase,
            string connectionString)
        {
            TenantKey = tenantKey;
            AccountKey = accountKey;
            TenantDatabase = tenantDatabase;
            ConnectionString = connectionString;
        }
        public TKey TenantKey;
        public string AccountKey;
        public string TenantDatabase;
        public string ConnectionString;
    } 
}
