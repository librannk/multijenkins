using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace BD.Core.ElasticClient.Mongo
{
    public class ListTenantDatabaseMap<TKey> : IListTenantDatabaseMap<TKey>
    {
        //Tenant Database Mapping
        private static ConcurrentDictionary<TKey, DatabaseMap<TKey>> _tenantDB;
        private readonly IConfigurationProvider<TKey> _configuration;
        private TenantDatabaseConfiguration<TKey> _tenantConfiguration;
        private readonly IListAccountMap _listAccountMap;
        private readonly ILogger<ListTenantDatabaseMap<TKey>> _logger;

        public ListTenantDatabaseMap(IConfigurationProvider<TKey> configuration,
            IListAccountMap listAccountMap, ILogger<ListTenantDatabaseMap<TKey>> logger)
        {
            _configuration = configuration;
            if (_tenantDB is null)
            {
                _tenantDB = new ConcurrentDictionary<TKey, DatabaseMap<TKey>>();
            }
            _listAccountMap = listAccountMap;
            _logger = logger;
        }
        public DatabaseMap<TKey> GetOrAddDatabase(TKey tenantKey)
        {
            DatabaseMap<TKey> tenantDatabase;

            //If Tenant does exists in cache get database of tenant
            if (!_tenantDB.TryGetValue(tenantKey, out tenantDatabase))
            {
                //Get Tenant Configurations like account and connection string
                _tenantConfiguration = _configuration.GetTenantDatabase(tenantKey);

                if (_tenantConfiguration != null)
                {
                    _logger.LogInformation($"NoSQL(Cosmos) sharding details for Tenant key {tenantKey} fetched from Configuration provider. AccountKey: {_tenantConfiguration.AccountKey}, DbName: {_tenantConfiguration.TenantDatabase} ");

                    //Create Database Map
                    tenantDatabase =
                        new DatabaseMap<TKey>(tenantKey,
                        _tenantConfiguration.AccountKey,
                        _tenantConfiguration.TenantDatabase);

                    //Get Account Map for database where database exists

                    var accountDatabaseMap = _listAccountMap.GetOrAddAccountMap(
                        tenantDatabase.AccountKey,
                        _tenantConfiguration.ConnectionString);
                    //Throw error in case database is mapped for any other tenant
                    //As per design there needs to be one to one mapping between
                    //tenant and cosmos database.
                    if (_tenantDB.Any(x=>x.Value.DatabaseName==
                                    tenantDatabase.DatabaseName))
                    {   
                        throw new DuplicateDatabaseMappedException($"NoSQL(Cosmos) Tenant  {tenantKey} database {tenantDatabase.DatabaseName} mapping is wrong, database is already mapped to different tenant. Contact administrator.");
                    }

                    tenantDatabase.DatabaseContext = accountDatabaseMap.MongoClient.
                        GetDatabase(tenantDatabase.DatabaseName);

                    _tenantDB.TryAdd(tenantKey, tenantDatabase);
                }
                else //Tenant Not Configured
                {
                    throw new Exception($"NoSQL(Cosmos) sharding details for Tenant key {tenantKey} does not exist. Contact administrator.");
                }
            }
            return tenantDatabase;
        }
    }
    public class DuplicateDatabaseMappedException : Exception
    {
        public DuplicateDatabaseMappedException(string message) : base(message) { }
    }

}
