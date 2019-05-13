using System;
using Xunit;
using BD.Core.ElasticClient.Mongo;
using BD.Core.ElasticClient.Mongo.Providers;
using Microsoft.Extensions.Logging;
using Moq;

namespace BD.Core.ElasticClient.Mongo.Tests
{
    public class TestElasticClientMongo
    {
        private IConfigurationProvider<string> _config;

        public TestElasticClientMongo()
        {
            _config =
                new InMemoryConfigurationProvider<string>();
        }
        [Theory]
        [InlineData("e767b738-3944-4896-93a0-6f074ba16616", "MainAccount", "Tenant1Db")]
        public void Test_ListTenantDatabaseMap(string tenantKey,
            string accountKey,
            string tenantDatabase)
        {
            Mock<ILogger<ListTenantDatabaseMap<string>>> logger = new Mock<ILogger<ListTenantDatabaseMap<string>>>();
            var listAccountMap = new ListAccountMap();
            var listdatabaseMap = new ListTenantDatabaseMap<string>(_config,
                listAccountMap, logger.Object);
            var databaseMap = listdatabaseMap.GetOrAddDatabase(tenantKey);
            Assert.Equal(tenantDatabase, databaseMap.DatabaseName);
            Assert.Equal(accountKey, databaseMap.AccountKey);
        }
        //Summary testing scenario of wrong configuration for tenant
        [Theory]
        [InlineData("6ad085b7-7644-4a56-8a8a-e2ece463efcf")]
        public void Test_ListTenantDatabaseMap_ForDuplicateDatbaseMapping(string tenantKey)
        {
            Mock<ILogger<ListTenantDatabaseMap<string>>> logger = new Mock<ILogger<ListTenantDatabaseMap<string>>>();
            var listAccountMap = new ListAccountMap();
            var listdatabaseMap = new ListTenantDatabaseMap<string>(_config,
                listAccountMap, logger.Object);

            //First one is added
            listdatabaseMap.GetOrAddDatabase("3383716d-7c07-4619-afcc-3bb0c933aa47");

            Assert.Throws<DuplicateDatabaseMappedException>(() =>
            listdatabaseMap.GetOrAddDatabase(tenantKey));
        }
        [Theory]
        [InlineData("3383716d-7c07-4619-afcc-3bb0c933aa47", "MainAccount")]
        public void Test_ListTenantDatabaseMap_AccountKey(string tenantKey,
                                            string accountKey)
        {
            Mock<ILogger<ListTenantDatabaseMap<string>>> logger = new Mock<ILogger<ListTenantDatabaseMap<string>>>();
            var listAccountMap = new ListAccountMap();
            var listdatabaseMap = new ListTenantDatabaseMap<string>(_config,
                listAccountMap, logger.Object);
            var databaseMap = listdatabaseMap.GetOrAddDatabase(tenantKey);
            Assert.Equal(accountKey, databaseMap.AccountKey);
        }

    }
}