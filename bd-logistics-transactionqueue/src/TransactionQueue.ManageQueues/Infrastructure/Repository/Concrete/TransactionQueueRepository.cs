
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.Shared.Models.Enums;


namespace TransactionQueue.ManageQueues.Infrastructure.Repository.Concrete
{
    public class TransactionQueueRepository : ITransactionQueueRepository
    {
        /// <summary>
        /// Context variable
        /// </summary>
        protected IMongoDatabase _Context;


        public TransactionQueueRepository(MongoDbClient mongoClient)
        {
            _Context = mongoClient.GetContext();
        }

        public async Task<Tuple<List<Business.Models.TransactionQueue>, bool>> GetTransactions(string activeTQId, List<int?> activeISA, string transactionType)
        {
            try
            {
                var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");

                if (activeTQId == "0")
                {
                    //   await collection.UpdateManyAsync(s => activeISA.Contains(s.IsaId) && s.Status == TransactionQueueStatus.Active.ToString() && s.Type == transactionType, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Pending.ToString()));

                    //code due to shard key issue- start

                    var existingActiveTransaction = await collection.Find(s => activeISA.Contains(s.IsaId) && (s.Status == TransactionQueueStatus.Active.ToString()) && s.Type == transactionType).ToListAsync();

                    foreach (var transaction in existingActiveTransaction)
                    {
                        await collection.UpdateOneAsync(s => s.Id == transaction.Id, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Pending.ToString()));
                    }

                    //code due to shard key issue - end

                    var transactions = await collection.Find(s => activeISA.Contains(s.IsaId) && (s.Status == TransactionQueueStatus.Pending.ToString()) && s.Type == transactionType).ToListAsync();

                    return new Tuple<List<Business.Models.TransactionQueue>, bool>(transactions, false);
                }
                else
                {
                    var transactionsOutPut = await collection.Find(s => activeISA.Contains(s.IsaId) && s.Status == TransactionQueueStatus.Active.ToString() && s.Id == activeTQId && s.Type == transactionType).FirstOrDefaultAsync();

                    if (transactionsOutPut != null)
                    {
                        var timeDifference = (DateTime.UtcNow - transactionsOutPut.StatusChangeUtcDateTime).Value.TotalSeconds;

                        if (timeDifference < transactionsOutPut.TimeToLive)
                        {
                            var transactions = await collection.Find(s => activeISA.Contains(s.IsaId) && (s.Status == TransactionQueueStatus.Pending.ToString() || s.Status == TransactionQueueStatus.Active.ToString()) && s.Type == transactionType).ToListAsync();

                            return new Tuple<List<Business.Models.TransactionQueue>, bool>(transactions, false);
                        }
                        else
                        {
                            await collection.UpdateOneAsync(s => activeISA.Contains(s.IsaId) && s.Status == TransactionQueueStatus.Active.ToString() && s.Type == transactionType && s.Id == activeTQId, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Pending.ToString()));

                            var allTransactions = await collection.Find(s => activeISA.Contains(s.IsaId) && (s.Status == TransactionQueueStatus.Pending.ToString()) && s.Type == transactionType).ToListAsync();

                            return new Tuple<List<Business.Models.TransactionQueue>, bool>(allTransactions, true);
                        }
                    }
                    else
                    {
                        //    await collection.UpdateManyAsync(s => activeISA.Contains(s.IsaId) && s.Status == TransactionQueueStatus.Active.ToString() && s.Type == transactionType, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Pending.ToString()));

                        //code due to shard key issue- start

                        var existingActiveTran = await collection.Find(s => activeISA.Contains(s.IsaId) && (s.Status == TransactionQueueStatus.Active.ToString()) && s.Type == transactionType).ToListAsync();

                        foreach (var tran in existingActiveTran)
                        {
                            await collection.UpdateOneAsync(s => s.Id == tran.Id, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Pending.ToString()));
                        }

                        //code due to shard key issue- end

                        var trans = await collection.Find(s => activeISA.Contains(s.IsaId) && s.Status == TransactionQueueStatus.Pending.ToString() && s.Type == transactionType).ToListAsync();


                        return new Tuple<List<Business.Models.TransactionQueue>, bool>(trans, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Business.Models.TransactionQueue>> GetHoldTransactions(List<int?> activeISA, string transactionType)
        {
            try
            {
                var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueueHold");

                var holdTransactions = await collection.Find(s => activeISA.Contains(s.IsaId) && s.Type == transactionType).ToListAsync();

                return holdTransactions;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<int?>> GetActiveISA(int actorId)
        {
            try
            {
                List<int?> LstISAId = new List<int?>();

                var collection = _Context.GetCollection<Business.Models.ActorISA>("ActorISA");

                var actorISAs = await collection.Find(s => s.ActorId == actorId).FirstOrDefaultAsync();

                if (actorISAs == null)
                {
                    return null;
                }

                foreach (var isa in actorISAs.ActiveISA)
                {
                    LstISAId.Add(isa.ISAId);
                }
                return LstISAId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetTransactionStatus(string transactionQueueKey)
        {
            try
            {
              

                var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");

                var tq = await collection.Find(s => s.Id == transactionQueueKey).FirstOrDefaultAsync();

                return tq.Status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateTransactionsAsync(string id)
        {
            var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");
            var res = await collection.UpdateOneAsync(s => s.Id == id, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Active.ToString()).Set(x => x.StatusChangeUtcDateTime, DateTime.UtcNow));
            return res.IsAcknowledged;
        }


        public async Task<List<Business.Models.TransactionQueue>> GetPendingTransactions(List<int?> isaIds)
        {

            var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");
            var transactions = await collection.Find(s => isaIds.Contains(s.IsaId) && s.Status == TransactionQueueStatus.Pending.ToString()).ToListAsync();
            return transactions;
        }

        public async Task<long> UpdateTransactionQueueStatus(string activeTransactionQueueKey, string transactionQueueKeyToActivate, List<int?> isaIds)
        {
            var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");

            await collection.UpdateManyAsync(s => isaIds.Contains(s.IsaId) && s.Id == activeTransactionQueueKey, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Pending.ToString()).Set(d => d.StatusChangeUtcDateTime, DateTime.UtcNow));
            var updateResult = await collection.UpdateManyAsync(s => isaIds.Contains(s.IsaId) && s.Id == transactionQueueKeyToActivate, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, TransactionQueueStatus.Active.ToString()).Set(d => d.StatusChangeUtcDateTime, DateTime.UtcNow));
            return updateResult.ModifiedCount;

        }

        public Business.Models.TransactionQueue CheckTransactionIsActive(string activeTransactionQueueKey)
        {
            try
            {
                var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");
                var activeTqStatus = collection.Find(s => s.Id == activeTransactionQueueKey && s.Status == TransactionQueueStatus.Active.ToString()).FirstOrDefault();
                if (activeTqStatus != null)
                {
                    return activeTqStatus;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateTQStatus(string activeTransactionQueueKey, string whereStatus, string toStatus)
        {
            try
            {
                var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");

                var result = collection.UpdateMany(s => s.Id != activeTransactionQueueKey && s.Status== whereStatus, Builders<Business.Models.TransactionQueue>.Update.Set(p => p.Status, toStatus));
                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MarkComplete(string activeTransactionQueueKey)
        {
            try
            {
                var collection = _Context.GetCollection<Business.Models.TransactionQueue>("TransactionQueue");
                // TO DO Update other fields check with SP
                var result = collection.UpdateOne(s => s.Id == activeTransactionQueueKey && s.Status == TransactionQueueStatus.Active.ToString(), Builders<Business.Models.TransactionQueue>
                    .Update.Set(p => p.Status, TransactionQueueStatus.Complete.ToString())
                    .Set(p => p.StatusChangeDt, DateTime.Now)
                    .Set(p => p.StatusChangeUtcDateTime, DateTime.UtcNow));
                    
                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
