using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BD.Core.ElasticClient.Mongo.Providers
{
    public class ShardConfigurationDataProvider<TKey> : IConfigurationProvider<TKey>
    {

        /// <summary> Connection string for cosmos database shard configuration</summary>
        private const string NoSqlDBConfigConStr = "ShardingDatabaseKey";

        /// <summary> connection string for sharding management database </summary>
        private readonly string _connectionString;

        /// <summary> Stored procedure to fetch shard configuration by TenantKey </summary>
        private readonly string _tenantDbShardConfigSP = "GetTenantShardDetails";

        private readonly ILogger<ShardConfigurationDataProvider<TKey>> _logger;

        public ShardConfigurationDataProvider(IConfiguration configuration, ILogger<ShardConfigurationDataProvider<TKey>> logger)
        {
            _connectionString = configuration.GetConnectionString(NoSqlDBConfigConStr);
            _logger = logger;
        }

        public TenantDatabaseConfiguration<TKey> GetTenantDatabase(TKey tenantKey)
        {

            string tenantKeyString = tenantKey.ToString();
            string accountKey = string.Empty;
            string database = string.Empty;
            string connectionString = string.Empty;
            TenantDatabaseConfiguration<TKey> tenantDatabaseConfiguration = null;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    sqlCon.Open();

                    using (SqlCommand sqlcmd = new SqlCommand(_tenantDbShardConfigSP, sqlCon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.Add(new SqlParameter("@tenantKey", tenantKey));

                        SqlDataReader sqlReader = sqlcmd.ExecuteReader();
                        while (sqlReader.Read())
                        {
                            tenantDatabaseConfiguration = new TenantDatabaseConfiguration<TKey>(
                                tenantKey,
                                sqlReader["AccountKey"].ToString(),
                                sqlReader["DatabaseName"].ToString(),
                                sqlReader["ConnectionStr"].ToString()
                                );
                        }
                        sqlReader.Close();
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Issue while fetching NoSql(Cosmos) sharding configuration from database for TenantKey: {tenantKey}.");
                throw;
            }

            return tenantDatabaseConfiguration;
        }
    }
}
