using BD.Core.ElasticClient.SQL;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using System;

namespace BD.Core.TenantManagement
{
    class Program
    {
        /// <summary> This app will be for automating on-boarding of new tenant </summary>
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting Shard Registration For Tenant:{args[0]}" );
            var tenantKey = args[0];
            var sqlShardDBServer = args[1];
            var sqlShardDB = args[2];
            var shardedDBConnection = args[3];

            ShardMapFactory shardMapFactory = new ShardMapFactory(shardedDBConnection);
            ListShardMap<Guid> shardMap = (ListShardMap<Guid>)shardMapFactory.CreateShardMap();
            

             string appserver = sqlShardDBServer;
             string appdatabase = sqlShardDB;
             Guid TenantId = Guid.Parse(tenantKey);

             var shardLocation = new ShardLocation(appserver, appdatabase);
             if (!shardMap.TryGetShard(shardLocation, out var shard))
             {
                 shard= shardMap.CreateShard(new ShardLocation(appserver, appdatabase));

             }
            PointMapping<Guid> mapping;
             if (!shardMap.TryGetMappingForKey(TenantId, out mapping))
             {
                 shardMap.CreatePointMapping(TenantId, shard);
             }
        }
    }
}
