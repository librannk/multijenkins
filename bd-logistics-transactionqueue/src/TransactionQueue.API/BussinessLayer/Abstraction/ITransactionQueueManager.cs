using System.Collections.Generic;
using TransactionQueue.API.Application.Models;
using TransactionQueue.API.Application.Entities;
using TransactionQueue.API.Application.Models.Enums;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;

namespace TransactionQueue.API.Application.BussinessLayer.Abstraction
{
    /// <summary> This interface is reponsible for handling the Transaction Queue operations </summary>
    public interface ITransactionQueueManager
    {
        /// <summary> This method is used to update the transaction </summary>
        /// <param name="transactionQueueId">update transaction status against transactionQueueId </param>
        /// <param name="status">status updated against transaction </param>
        /// <param name="headers"> headers</param>
        /// <returns></returns>
        Task<TransactionQueueModel> UpdateTransactionStatus(string transactionQueueId, TransactionStatus status, Dictionary<string, string> headers);
    }
}
