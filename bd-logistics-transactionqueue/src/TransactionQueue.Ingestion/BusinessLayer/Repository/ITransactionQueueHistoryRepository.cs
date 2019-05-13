using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.Infrastructure.DBModel;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;

namespace TransactionQueue.Ingestion.BusinessLayer.Repository
{
    /// <summary> Interface ITransactionQueueHistoryRepository </summary>
    public interface ITransactionQueueHistoryRepository : IBaseRepository<TransactionQueueHistory>
    {
        /// <summary> Inserts transaction into TransactionQueueHistory collection </summary>
        /// <param name="model">instance of TransactionQueueModel</param>
        /// <returns>TransactionQueueId</returns>
        Task<string> CreateTransaction(TransactionQueueModel model);
    }
}
