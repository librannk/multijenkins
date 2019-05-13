using System;
using System.Threading.Tasks;
using TransactionQueue.API.Infrastructure.Repository.Interfaces;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Shared.DataAccess.Mongo;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.API.Infrastructure.Repository
{
    /// <summary>
    /// This class handles the transactionqueue mongo db operations
    /// </summary>
    public class TransactionQueueMongoRepository : BaseRepository<Ingestion.Infrastructure.DBModel.TransactionQueue>, ITransactionQueueMongoRepository
    {
        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public TransactionQueueMongoRepository(MongoDbClient dataContext) : base(dataContext)
        {
        }

        /// <summary> Activates a transaction and status against the TransactionId. </summary>
        /// <param name="transactionQueueId">TransactionQueueId </param>
        /// <param name="status"> status </param>
        public async Task<Ingestion.Infrastructure.DBModel.TransactionQueue> UpdateTransactionStatus(string transactionQueueId, TransactionStatus status)
        {
            var obj = this as ITransactionQueueMongoRepository;
            var result = await obj.GetByIdAsync(transactionQueueId);
            if (result != null && result.Devices != null)
            {
                result.Status = status.ToString();
                result.ModifiedBy = UserName.Admin.ToString();
                result.ModifiedDate = DateTime.Now;
                await obj.UpdateAsync(result);
                return result;
            }

            return null;
        }

        #endregion Constructors
    }
}
