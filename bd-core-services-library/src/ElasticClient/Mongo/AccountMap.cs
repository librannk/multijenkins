using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    public class AccountMap
    {
        public AccountMap(string accountKey, string connectionString)
        {
            AccountKey = accountKey;
            ConnectionString = connectionString;
            MongoClient = CreateClient();
        }
        public string AccountKey;
        public string ConnectionString;
        public MongoClient MongoClient;
        private MongoClient CreateClient()
        {
            return new MongoClient(ConnectionString);
        }

    }
}
