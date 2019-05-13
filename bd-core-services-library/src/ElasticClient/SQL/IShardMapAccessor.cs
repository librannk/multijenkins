using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;

namespace BD.Core.ElasticClient.SQL
{
    public interface IShardMapAccessor
    {
        ShardMap ShardMap { get; set; }
    }
}