using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Repository;
using TransactionQueue.Ingestion.Infrastructure.DBModel;
using TransactionQueue.Shared.DataAccess.Mongo;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.Ingestion.Infrastructure.Repository
{
    /// <summary> Class TransactionQueueHistoryMongoRepository </summary>
    public class TransactionQueueHistoryMongoRepository : BaseRepository<TransactionQueueHistory>, ITransactionQueueHistoryRepository
    {
        #region Private Fields
        private readonly IMongoCollection<TransactionQueueHistory> _collection;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public TransactionQueueHistoryMongoRepository(MongoDbClient dataContext, IMapper mapper)
            : base(dataContext)
        {
            _collection = CollectionName<TransactionQueueHistory>();
            _mapper = mapper;
        }
        #endregion

        #region Data Operations
        public async Task<string> CreateTransaction(TransactionQueueModel model)
        {
            var entity = _mapper.Map<TransactionQueueHistory>(model);
            entity.CreatedBy = UserName.Admin.ToString();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedBy = UserName.Admin.ToString();
            entity.ModifiedDate = DateTime.Now;

            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }
        #endregion
    }
}
