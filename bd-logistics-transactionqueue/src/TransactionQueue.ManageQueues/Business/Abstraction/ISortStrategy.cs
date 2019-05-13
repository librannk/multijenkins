
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Infrastructure.DBEntity;
using TransactionQueue.ManageQueues.QueueHelper;

namespace TransactionQueue.ManageQueues.Business.Abstraction
{
    public interface ISortStrategy
    {
        SortedQueue GetSortedQueue(List<Models.TransactionQueue> pendingTransactions, string sortedColumn, int sortedDirection, int page, int pageSize);
    }
}