using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.ManageQueues.Infrastructure.DBEntity;

namespace TransactionQueue.ManageQueues.Business.Abstraction
{
    public interface ITransactionQueueRepository
    {
        Task<Tuple<List<Models.TransactionQueue>, bool>> GetTransactions(string activeTQId, List<int?> activeISA, string transactionType);

        Task<List<int?>> GetActiveISA(int actorId);

        Task<List<Models.TransactionQueue>> GetHoldTransactions(List<int?> activeISA, string transactionType);
        Task<bool> UpdateTransactionsAsync(string Id);

        Task<List<Models.TransactionQueue>> GetPendingTransactions(List<int?> isaIds);
        Task<long> UpdateTransactionQueueStatus(string activeTransactionQueueKey, string transactionQueueKeyToActivate, List<int?> isaIds);
        Task<string> GetTransactionStatus(string transactionQueueKey);
        Business.Models.TransactionQueue CheckTransactionIsActive(string activeTransactionQueueKey);
        bool UpdateTQStatus(string activeTransactionQueueKey, string whereStatus, string toStatus);
        bool MarkComplete(string activeTransactionQueueKey);
    }
}
