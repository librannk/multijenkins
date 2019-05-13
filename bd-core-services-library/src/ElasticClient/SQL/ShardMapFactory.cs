using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BD.Core.ElasticClient.SQL
{
    public class ShardMapFactory : IShardMapFactory
    {
        /// <summary> Key for sharding database </summary>
        private const string ShardingDatabaseKey = "ShardingDatabaseKey";
        /// <summary> connection string for sharding management database </summary>
        private readonly string _connectionString;

        private readonly string _configuredShardMap  = "Logistics";

        private readonly IShardMapAccessor _shardMapAccessor;
        public ShardMapFactory(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _connectionString = configuration.GetConnectionString(ShardingDatabaseKey);
            _shardMapAccessor = serviceProvider.GetService<IShardMapAccessor>();
        }
        public ShardMapFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ShardMap CreateShardMap()
        {
            // Deploy shard map manager.
            ShardMapManager shardMapManager;
            ListShardMap<Guid> listShardMap;
            if (!ShardMapManagerFactory.TryGetSqlShardMapManager(_connectionString, 
                ShardMapManagerLoadPolicy.Lazy, out shardMapManager))
            {
                shardMapManager = ShardMapManagerFactory.CreateSqlShardMapManager(_connectionString);
            }
            if (!shardMapManager.TryGetListShardMap(_configuredShardMap, out listShardMap))
            {
                listShardMap = shardMapManager.CreateListShardMap<Guid>(_configuredShardMap);
            }
            if (_shardMapAccessor!=null)
                _shardMapAccessor.ShardMap = listShardMap;
           /*
            TO DO : This code needs to be externalized
            string appserver = "bddbdev.database.windows.net";
            string appdatabase = "Test";
            Guid TenantId = Guid.Parse("3e376368-bf33-49ce-a0de-e9ad6438aa1c");
            var shardLocation = new ShardLocation(appserver, appdatabase);
            if (!listShardMap.TryGetShard(shardLocation, out var shard))
            {
                shard=listShardMap.CreateShard(new ShardLocation(appserver, appdatabase));

            }
            PointMapping<Guid> mapping;
            if (!listShardMap.TryGetMappingForKey(TenantId, out mapping))
            {
                listShardMap.CreatePointMapping(TenantId, shard);
            }*/
            return listShardMap;
        }
    }
}