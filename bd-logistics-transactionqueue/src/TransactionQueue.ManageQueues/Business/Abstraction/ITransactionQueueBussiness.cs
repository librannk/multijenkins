using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.Shared.Models;

namespace TransactionQueue.ManageQueues.Business.Abstraction
{
    public interface ITransactionQueueBussiness
    {
        Task<Tuple<List<Models.TransactionQueue>, bool>> GetTransactions(string activeTQId, List<int?> activeISA, string transactionType);

        Task<List<int?>> GetActiveISA(int actorId);

        Task<List<Business.Models.TransactionQueue>> GetHoldTransactions(List<int?> activeISA, string transactionType);

        Task<BusinessResponse> PickNow(string activeTransactionQueueKey, int actorKey, PickNow pickNow, Dictionary<string, string> headers);

        BusinessResponse MarkCompleteTransaction(string activeTransactionQueueKey, int actorKey, TQRequestObjectForComplete tQRequestObjectForComplete);
    }
}
