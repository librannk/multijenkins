using System;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Constants;
using MongoDB.Driver;


namespace BD.Template.API.Infrastructure.DataAccess.Mongo.Clients
{
    /// <summary>
    /// Wrapper class for the MongoClient
    /// </summary>
    public class MongoDbClient
    {
        /// <summary>
        /// Name of the Database we want data from
        /// </summary>
        private static string _databaseName;

        /// <summary>
        /// The connection string (IP-Adress + Port)
        /// </summary>
        private static string _connectionString;

        /// <summary>
        /// Threadsafe (According to the documentation)
        /// </summary>
        private static MongoClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="connectionString"></param>
        public MongoDbClient(string databaseName, string connectionString)
        {
            _databaseName = databaseName;
            _connectionString = connectionString;

            if (_client == null)
                _client = CreateClient();
        }

        /// <summary>
        /// Wrapper for creating client, so in the future when we want to make a more complex client, we just edit here.
        /// </summary>
        /// <returns></returns>
        private MongoClient CreateClient()
        {
            return new MongoClient(_connectionString);

        }

        /// <summary>
        /// Returns the Database with the given name in the configuration.
        /// </summary>
        /// <returns>IMongoDatabase instance</returns>
        public IMongoDatabase GetContext()
        {
            if (_client == null) throw new NullReferenceException(ErrorConstants.ClientNotInstantiated);

            return _client.GetDatabase(_databaseName);
        }

        /// <summary>
        /// To drop the database
        /// </summary>
        public void DropContext()
        {
            if (_client == null) throw new NullReferenceException(ErrorConstants.ClientNotInstantiated);

            _client.DropDatabase(_databaseName);
        }
    }
}
