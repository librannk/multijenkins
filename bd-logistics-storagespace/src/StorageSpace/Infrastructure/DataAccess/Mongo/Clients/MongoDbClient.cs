using MongoDB.Driver;
using System;
using static StorageSpace.API.Common.Constant;

namespace StorageSpace.API.Infrastructure.DataAccess.Mongo.Clients
{
    /// <summary> Wrapper class for the MongoClient </summary>
    public class MongoDbClient
    {
        /// <summary> Name of the Database we want data from </summary>
        private static string DatabaseName;

        /// <summary> The connection string (IP-Adress + Port) </summary>
        private static string ConnectionString;

        /// <summary> Threadsafe (According to the documentation) </summary>
        private static MongoClient Client;

        /// <summary>  </summary>
        /// <param name="databaseName"></param>
        /// <param name="connectionString"></param>
        public MongoDbClient(string databaseName, string connectionString)
        {
            DatabaseName = databaseName;
            ConnectionString = connectionString;

            if (Client == null)
                Client = CreateClient();
        }

        /// <summary> Wrapper for creating client, so in the future when we want to make a more complex client, we just edit here. </summary>
        /// <returns></returns>
        private MongoClient CreateClient()
        {
            return new MongoClient(ConnectionString);

        }

        /// <summary>
        /// Returns the Database with the given name in the configuration.
        /// </summary>
        /// <returns>IMongoDatabase instance</returns>
        public IMongoDatabase GetContext()
        {
            if (Client == null) throw new NullReferenceException(ClientNotInitiated);

            return Client.GetDatabase(DatabaseName);
        }

        /// <summary>
        /// To drop the database
        /// </summary>
        public void DropContext()
        {
            if (Client == null) throw new NullReferenceException(ClientNotInitiated);

            Client.DropDatabase(DatabaseName);
        }
    }
}
