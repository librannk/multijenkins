using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Models;

namespace TransactionQueue.Ingestion.BusinessLayer.Abstraction
{
    /// <summary> This interface is reponsible for handling the Transaction Queue operations </summary>
    public interface ITransactionManager
    {
        /// <summary> This method is used for filtering the incoming request and save the data into database </summary>
        /// <param name="request"> Incoming Request</param>
        /// <param name="headers"> headers</param>
        Task ProcessTransactionRequest(TransactionRequest request, Dictionary<string, string> headers);

        /// <summary>
        /// This method is used to update formulary location against transactionQueueId in DB.
        /// </summary>
        /// <param name="transactionQueueId">Update Storage location against this TransactionQueueId</param>
        /// <param name="devices">devices data to be updated</param>
        /// <returns></returns>
        Task<bool> UpdateTransactionWithStorageDetails(string transactionQueueId, List<Device> devices);

        /// <summary> Get a record from DB based on TransactionId. </summary>
        /// <param name="transactionQueueId"> transactionQueueId </param>
        /// <returns> Model of type TransactionQueueModel </returns>
        Task<TransactionQueueModel> GetTransactionDetails(string transactionQueueId);
    }
}
