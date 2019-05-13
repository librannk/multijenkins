using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;

namespace BD.Core.ElasticClient.SQL
{
    public class ShardMapAccessor : IShardMapAccessor
    {
        private static ShardMap _shardMap;

        public ShardMap ShardMap
        {
            get => _shardMap;
            set
            {
                if (_shardMap != null)
                {
                    _shardMap = null;
                }
                if (value != null)
                {
                    _shardMap = value;
                }
            }
        }

    }
}