using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    public class ListAccountMap : IListAccountMap
    { 
        //DB account mapping
        private static ConcurrentDictionary<string, AccountMap> _dbaccounts;
        public ListAccountMap()
        {
            if (_dbaccounts is null)
                _dbaccounts = new ConcurrentDictionary<string, AccountMap>();
        }

        public AccountMap GetOrAddAccountMap(string accountKey,
            string connectionString)
        {
            AccountMap accountDatabaseMap; 
            if (!_dbaccounts.TryGetValue(accountKey, out accountDatabaseMap))
            {   
                accountDatabaseMap = new AccountMap(accountKey, connectionString);
                _dbaccounts.TryAdd(accountKey, accountDatabaseMap);
            }
            return accountDatabaseMap;
        }
    }
}
