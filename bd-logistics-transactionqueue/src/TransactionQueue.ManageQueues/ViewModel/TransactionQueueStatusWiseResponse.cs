
using System.Collections.Generic;
using TransactionQueue.ManageQueues.QueueHelper;

namespace TransactionQueue.ManageQueues.ViewModel
{
    /// <summary>
    /// To hold queue response
    /// </summary>
    public class TransactionQueueStatusWiseResponse
    {
        /// <summary>
        /// return active transaction
        /// </summary>
        public TransactionQueueItems ActiveTransaction;
        /// <summary>
        /// Get all pending Transactions
        /// </summary>
        public List<TransactionQueueItems> PendingTransactions;
        /// <summary>
        /// get all hold Transactions
        /// </summary>
        public List<TransactionQueueItems> HoldTransactions;
        /// <summary>
        /// Get all Locked Transactions
        /// </summary>
        public List<TransactionQueueItems> LockedTransactions;
        /// <summary>
        /// get all Restock / Return Transactions
        /// </summary>
        public List<TransactionQueueItems> RestockReturnTransactions;


        public bool IsStaled { get; set; }
    }
}
