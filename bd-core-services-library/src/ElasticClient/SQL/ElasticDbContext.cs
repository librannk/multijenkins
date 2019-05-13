using BD.Core.Context;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BD.Core.ElasticClient.SQL
{
    /// <summary> The Elastic DB Context. </summary>
    /// <seealso cref="DbContext" />
    public class ElasticDbContext : DbContext  
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IShardMapAccessor _shardMapAccessor;
        private readonly ILogger<ElasticDbContext> _logger;
        private SqlConnection _connection;
        private readonly IConfiguration _configuration;
        private const string ShardingDatabaseKey = "ShardingDatabaseCredential";
        
        public ElasticDbContext(IServiceProvider servideProvider,
            DbContextOptions options)
             : base(options)
        {
            _executionContextAccessor= servideProvider.GetService<IExecutionContextAccessor>();
            _shardMapAccessor = servideProvider.GetService<IShardMapAccessor>();
            _configuration = servideProvider.GetService<IConfiguration>();
            _logger = servideProvider.GetService<ILogger<ElasticDbContext>>();
        }

        /// <summary>
        /// This method overriding configuration to route per tenant.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Guid.TryParse(_executionContextAccessor.Current.Tenant.TenantKey, out var tenantKey))
            {
                optionsBuilder.UseSqlServer(OpenDDRConnection(
                    tenantKey,
                    _configuration.GetConnectionString(ShardingDatabaseKey)));
                base.OnConfiguring(optionsBuilder);
            }
            else
            {
                _logger.LogCritical($"Tenant Key for opening connection is invalid : {tenantKey}");
            }
        }

        /// <summary>
        /// this method is used for setting tenant id in session context and return sqlconnection
        /// </summary>
        /// <param name="shardingKey"></param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public SqlConnection OpenDDRConnection(Guid shardingKey, string connectionStr)
        {
            try
            {
                if (_shardMapAccessor.ShardMap != null)
                {
                    _connection = _shardMapAccessor.ShardMap.OpenConnectionForKey(shardingKey,
                        connectionStr, ConnectionOptions.None);
                    // Set TenantId in SESSION_CONTEXT to shardingKey to enable Row-Level Security filtering
                    SqlCommand cmd = _connection.CreateCommand();
                    cmd.CommandText = @"exec sp_set_session_context @key=N'TenantKey', @value=@shardingKey";
                    cmd.Parameters.AddWithValue("@shardingKey", shardingKey);
                    cmd.ExecuteNonQuery();
                    return _connection;
                }
                else
                {
                    throw new SystemException($"Shard Map Not Initialized For Tenant {shardingKey}");
                }
            }
            catch 
            {
                _logger.LogCritical($"Not able to open connection for Tenant: {shardingKey}");
                if (_connection != null) 
                    _connection.Dispose();
                throw;
            }
        }
        /// <summary>
        /// for closed connection at dispose dbcontext is per request
        /// </summary>
        public override void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
    }
}