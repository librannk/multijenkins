
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;

namespace TransactionQueue.ManageQueues.Business.Concrete
{
    public class PriorityRules : IPriorityRules
    {
        /// <summary>
        /// Context variable
        /// </summary>
        protected IMongoDatabase _Context;

        public PriorityRules(MongoDbClient mongoClient)
        {
            _Context = mongoClient.GetContext();

        }

        async Task<List<TransactionPriority>> IPriorityRules.GetPriorityRules()
        {
            try
            {
                var collection = _Context.GetCollection<TransactionPriority>("TransactionPriority");

                var transactionPriority = await collection.Find(Builders<TransactionPriority>.Filter.Empty).ToListAsync();

                return transactionPriority;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}