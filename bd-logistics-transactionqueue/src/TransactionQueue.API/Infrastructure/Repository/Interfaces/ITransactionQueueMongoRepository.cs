using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;

namespace TransactionQueue.API.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// This interface handles the transactionqueue mongo db operations
    /// </summary>
    public interface ITransactionQueueMongoRepository : IBaseRepository<Ingestion.Infrastructure.DBModel.TransactionQueue>
    {
        /// <summary> Activates a transaction and status against the TransactionId. </summary>
        /// <param name="transactionQueueId">TransactionQueueId </param>
        /// <param name="status"> status </param>
        Task<Ingestion.Infrastructure.DBModel.TransactionQueue> UpdateTransactionStatus(string transactionQueueId, TransactionStatus status);
    }
}
