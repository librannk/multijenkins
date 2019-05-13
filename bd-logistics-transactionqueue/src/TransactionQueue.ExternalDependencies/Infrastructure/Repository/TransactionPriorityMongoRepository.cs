using AutoMapper;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models.Enums;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;
using TransactionQueue.ExternalDependencies.Infrastructure.DBModel;
using TransactionQueue.Shared.DataAccess.Mongo;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.ExternalDependencies.Infrastructure.Repository
{
    /// <summary>
    /// This class handles the TransactionPriority mongo db operations
    /// </summary>
    public class TransactionPriorityMongoRepository : BaseRepository<TransactionPriority>, ITransactionPriorityRepository
    {
        #region Private Fields
        private readonly IMongoCollection<TransactionPriority> _collection;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public TransactionPriorityMongoRepository(MongoDbClient dataContext, IMapper mapper)
            : base(dataContext)
        {
            _collection = base.CollectionName<TransactionPriority>();
            _mapper = mapper;
        }

        #endregion Constructors

        #region Data Operations

        /// <summary>
        /// Get TransactionPriority record from DB based on FacilityId and PriorityCode.
        /// </summary>
        /// <param name="facilityId">FacilityId</param>
        /// <param name="priorityCode">PriorityCode</param>
        /// <returns></returns>
        public async Task<TransactionPriority> GetPriorityByFacilityIdAndPriorityCode(int facilityId, string priorityCode)
        {
            var result = await _collection.FindAsync(m => m.FacilityId == facilityId && m.TransactionPriorityCode == priorityCode);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Inserts a TransactionPriority in DB.
        /// </summary>
        /// <param name="priority">TransactionPriority to be stored in db.</param>
        public async Task<bool> InsertTransactionPriority(BusinessLayer.Models.TransactionPriority priority)
        {
            if (priority != null)
            {
                var entity = _mapper.Map<TransactionPriority>(priority);
                entity.CreatedBy = UserName.Admin.ToString();
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.InsertOneAsync(entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update a TransactionPriority in DB.
        /// </summary>
        /// <param name="priority">TransactionPriority to be updated in db.</param>
        public async Task<bool> UpdateTransactionPriority(BusinessLayer.Models.TransactionPriority priority)
        {
            var entity = await GetPriorityByFacilityIdAndPriorityCode(priority.FacilityId, priority.TransactionPriorityCode);
            if (entity != null)
            {
                entity.IsActive = priority.IsActive;
                entity.IsAdu = priority.IsAdu;
                entity.UseInterfaceItemName = priority.UseInterfaceItemName;
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.ReplaceOneAsync(e => e.TransactionPriorityId == entity.TransactionPriorityId, entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get TransactionPriority record from DB based on transactionPriorityId.
        /// </summary>
        /// <param name="transactionPriorityId">TransactionPriorityId</param>
        /// <returns></returns>
        public async Task<TransactionPriority> GetTransactionPriority(int transactionPriorityId)
        {
            var result = await _collection.FindAsync(m => m.TransactionPriorityId == transactionPriorityId);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Get TransactionPriority record from DB based on transactionPriorityCode.
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public async Task<TransactionPriority> GetTransactionPriority(Priority priority, int facilityId)
        {
            var result = await _collection.FindAsync(m => m.TransactionPriorityCode == priority.ToString() && m.FacilityId == facilityId);
            return result.FirstOrDefault();
        }
    }

    #endregion
}
