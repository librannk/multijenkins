using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Ingestion.BusinessLayer.Repository;
using TransactionQueue.Shared.DataAccess.Mongo;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.Ingestion.Infrastructure.Repository
{
    /// <summary>
    /// This class handles the transactionqueue mongo db operations
    /// </summary>
    public class TransactionQueueMongoRepository : BaseRepository<DBModel.TransactionQueue>, ITransactionQueueRepository
    {
        #region Private Fields
        private readonly IMongoCollection<DBModel.TransactionQueue> _collection;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public TransactionQueueMongoRepository(MongoDbClient dataContext, IMapper mapper)
            : base(dataContext)
        {
            _collection = CollectionName<DBModel.TransactionQueue>();
            _mapper = mapper;
        }

        #endregion Constructors

        #region Data Operations

        /// <summary> Insert transaction queue data into DB. </summary>
        /// <param name="tranQueue"> TransactionQueueModel model </param>
        /// <returns>transactionQueueId </returns>
        public async Task<string> CreateTransaction(TransactionQueueModel tranQueue)
        {
            var entity = _mapper.Map<DBModel.TransactionQueue>(tranQueue);
            entity.CreatedBy = UserName.Admin.ToString();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedBy = UserName.Admin.ToString();
            entity.ModifiedDate = DateTime.Now;
            await _collection.InsertOneAsync(entity);
            return entity.Id;
        }

        /// <summary> 
        /// Update transaction queue data into DB. 
        /// </summary>
        /// <param name="transaction"> TransactionQueueModel model </param>
        public async Task<DBModel.TransactionQueue> UpdateTransaction(TransactionQueueModel transaction)
        {
            if (!string.IsNullOrEmpty(transaction.TransactionQueueId))
            {
                var entity = _mapper.Map<DBModel.TransactionQueue>(transaction);
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
                return entity;
            }
            return null;
        }

        /// <summary> Activates a transaction and status against the TransactionId. </summary>
        /// <param name="transactionQueueId">TransactionQueueId </param>
        /// <param name="status"> status </param>
        public async Task<DBModel.TransactionQueue> UpdateTransactionStatus(string transactionQueueId, TransactionStatus status)
        {
            var entity = await GetTransactionDetails(transactionQueueId);
            if (entity != null)
            {
                entity.Status = status.ToString();
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
                return entity;
            }

            return null;
        }

        /// <summary> Get a record from DB based on TransactionId. </summary>
        /// <param name="transactionQueueId"> transactionQueueId </param>
        /// <returns> Model of type TransactionQueueModel </returns>
        public async Task<DBModel.TransactionQueue> GetTransactionDetails(string transactionQueueId)
        {
            var result = await _collection.FindAsync(m => m.Id == transactionQueueId);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Update transaction data with storage location into DB
        /// </summary>
        /// <param name="transactionQueueId">TransactionQueueId</param>
        /// <param name="devices">StorageSpace Model</param>
        /// <param name="location">location</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        public async Task<bool> UpdateTransactionWithStorageDetails(string transactionQueueId, TransactionStatus status, string location, List<Device> devices)
        {
            var entity = await GetTransactionDetails(transactionQueueId);
            if (entity != null)
            {
                entity.Devices = devices;
                entity.Location = location;
                entity.Status = status.ToString();
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <returns></returns>
        public async Task<List<DBModel.TransactionQueue>> GetAllTransactions()
        {
            var obj = this as ITransactionQueueRepository;
            var result = await obj.GetAllAsync();
            if (result != null && result.Any())
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Get all transactions based on predicate filter
        /// </summary>
        /// <returns></returns>
        public async Task<List<DBModel.TransactionQueue>> GetAllTransactions(Predicate<DBModel.TransactionQueue> isValidTransactionBasedOnFilter)
        {
            var obj = this as ITransactionQueueRepository;
            var result = await obj.GetAllAsync();
            if (result != null && result.Any())
            {
                List<DBModel.TransactionQueue> transactions = new List<DBModel.TransactionQueue>();
                result.ForEach(transaction =>
                {
                    if (isValidTransactionBasedOnFilter(transaction))
                    {
                        transactions.Add(transaction);
                    }
                });

                return transactions;
            }

            return null;
        }
        #endregion
    }
}
