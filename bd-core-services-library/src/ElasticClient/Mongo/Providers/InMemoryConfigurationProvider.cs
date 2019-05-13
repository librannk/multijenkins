using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo.Providers
{
    public class InMemoryConfigurationProvider<TKey> :
        IConfigurationProvider<TKey>
    {
        public InMemoryConfigurationProvider()
        {
            InMemoryDataProvider.Load();
        }
        public TenantDatabaseConfiguration<TKey> GetTenantDatabase(TKey tenantKey)
        {
            var tenantKeyString = tenantKey.ToString();
            var data= InMemoryDataProvider.keyValuePairs[tenantKeyString];
            return new TenantDatabaseConfiguration<TKey>(
                tenantKey,
                data.AccountKey,
                data.Database,
                data.ConnectionString
                );
        }        
    }
    public class DummConfig
    {
        public DummConfig(string tenantKey,
            string accountKey,
            string database,
            string con)
        {
            TenantKey = tenantKey;
            AccountKey = accountKey;
            Database = database;
            ConnectionString = con;
        }

        public string TenantKey;
        public string AccountKey;
        public string Database;
        public string ConnectionString;
    }
    public static class InMemoryDataProvider
    {
        public static Dictionary<string, DummConfig> keyValuePairs =
                new Dictionary<string, DummConfig>();
        public static void Load()
        {
            var tenantDbConfig =
            new DummConfig
            (
            "e767b738-3944-4896-93a0-6f074ba16616",
            "MainAccount",
            "Tenant1Db",
            "mongodb://cosmos-db-48806:JH7vr68uTQbAjqrbGoIu5KeSVUPCerplaguuQz8TsVVmKg115ewlVtWWOPJrSiqngaytOXm32MjLDybkSubhsA==@cosmos-db-48806.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
            if (!keyValuePairs.ContainsKey("e767b738-3944-4896-93a0-6f074ba16616"))
                keyValuePairs.Add("e767b738-3944-4896-93a0-6f074ba16616", tenantDbConfig);

            tenantDbConfig =
                new DummConfig
                (
                "3383716d-7c07-4619-afcc-3bb0c933aa47",
                "MainAccount",
                "Tenant2Db",
                "mongodb://cosmos-db-48806:JH7vr68uTQbAjqrbGoIu5KeSVUPCerplaguuQz8TsVVmKg115ewlVtWWOPJrSiqngaytOXm32MjLDybkSubhsA==@cosmos-db-48806.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
            if (!keyValuePairs.ContainsKey("3383716d-7c07-4619-afcc-3bb0c933aa47"))
                keyValuePairs.Add("3383716d-7c07-4619-afcc-3bb0c933aa47", tenantDbConfig);

            tenantDbConfig =
    new DummConfig
    (
    "6ad085b7-7644-4a56-8a8a-e2ece463efcf",
    "MainAccount",
    "Tenant2Db",
    "mongodb://cosmos-db-48806:JH7vr68uTQbAjqrbGoIu5KeSVUPCerplaguuQz8TsVVmKg115ewlVtWWOPJrSiqngaytOXm32MjLDybkSubhsA==@cosmos-db-48806.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
            if (!keyValuePairs.ContainsKey("6ad085b7-7644-4a56-8a8a-e2ece463efcf"))
                keyValuePairs.Add("6ad085b7-7644-4a56-8a8a-e2ece463efcf", tenantDbConfig);
        }
    }
}