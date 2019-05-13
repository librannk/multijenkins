using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    public class DatabaseMap<TKey>
    { 
        public DatabaseMap(TKey tenantKey,
            string  accountKey, 
            string databaseName)
        {
            TenantKey = tenantKey;
            AccountKey = accountKey;
            DatabaseName = databaseName;
        }
        public string AccountKey;
        public TKey TenantKey;        
        public string DatabaseName;
        public IMongoDatabase DatabaseContext;
    }
}
