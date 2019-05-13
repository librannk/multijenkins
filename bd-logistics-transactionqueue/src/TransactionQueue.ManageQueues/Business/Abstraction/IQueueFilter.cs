
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.ManageQueues.Infrastructure.DBEntity;
using TransactionQueue.ManageQueues.QueueHelper;
using TransactionQueue.ManageQueues.ViewModel;

namespace TransactionQueue.ManageQueues.Business.Abstraction
{
    public interface IQueueFilter
    {
        SortedQueue GetAllSortedTransaction(List<Models.TransactionQueue> transaction, string sortedColumnName, int sortedDirection, int page, int pageSize);
        TransactionQueueStatusWiseResponse GetSortedTransaction(List<Models.TransactionQueue> activeAndPendingTransaction, bool isStaled, List<Models.TransactionQueue> holdTransaction, string sortedColumnName, int sortedDirection, int page, int pageSize);
    }
}