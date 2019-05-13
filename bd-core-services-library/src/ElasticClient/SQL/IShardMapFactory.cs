using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;

namespace BD.Core.ElasticClient.SQL
{
    public interface IShardMapFactory
    {
        ShardMap CreateShardMap();
    }
}
