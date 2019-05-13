using System;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models;

namespace TransactionQueue.Ingestion.BusinessLayer.Abstraction
{
    /// <summary>
    /// This interface is responsible for handling AduTransaction related operations.
    /// </summary>
    public interface IAduTransactionManager
    {
        /// <summary>
        /// This method is used for processing Adu transaction
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transactionQueueModel"></param>
        /// <param name="item"></param>
        /// <param name="priority"></param>
        /// <param name="facility"></param>
        /// <returns></returns>
        Task<Tuple<bool,bool>> ProcessAduTransaction(TransactionRequest request, TransactionQueueModel transactionQueueModel, Item item,
            TransactionPriority priority, Facility facility);
    }
}
