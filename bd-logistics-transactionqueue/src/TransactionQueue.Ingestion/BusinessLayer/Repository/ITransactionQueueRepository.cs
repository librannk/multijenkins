using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;

namespace TransactionQueue.Ingestion.BusinessLayer.Repository
{
    /// <summary>
    /// This interface handles the transactionqueue mongo db operations
    /// </summary>
    public interface ITransactionQueueRepository : IBaseRepository<Infrastructure.DBModel.TransactionQueue>
    {
        /// <summary> Insert transaction queue data into DB. </summary>
        /// <param name="tranQueue"> TransactionQueueModel model </param>
        /// <returns>transactionQueueId </returns>
        Task<string> CreateTransaction(TransactionQueueModel tranQueue);

        /// <summary> 
        /// Update transaction queue data into DB. 
        /// </summary>
        /// <param name="transaction"> TransactionQueueModel model </param>
        Task<Infrastructure.DBModel.TransactionQueue> UpdateTransaction(TransactionQueueModel transaction);

        /// <summary> Activates a transaction and status against the TransactionId. </summary>
        /// <param name="transactionQueueId">TransactionQueueId </param>
        /// <param name="status"> status </param>
        Task<Infrastructure.DBModel.TransactionQueue> UpdateTransactionStatus(string transactionQueueId, TransactionStatus status);

        /// <summary> Get a record from DB based on TransactionId. </summary>
        /// <param name="transactionQueueId"> transactionQueueId </param>
        /// <returns> Model of type TransactionQueueModel </returns>
        Task<Infrastructure.DBModel.TransactionQueue> GetTransactionDetails(string transactionQueueId);

        /// <summary>
        /// Update transaction data with storage location into DB
        /// </summary>
        /// <param name="transactionQueueId">TransactionQueueId</param>
        /// <param name="storageSpaces">StorageSpace Model</param>
        /// <param name="location">location</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        Task<bool> UpdateTransactionWithStorageDetails(string transactionQueueId, TransactionStatus status, string location, List<Device> devices);

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <returns></returns>
        Task<List<Infrastructure.DBModel.TransactionQueue>> GetAllTransactions();

        /// <summary>
        /// Get all transactions based on predicate filter
        /// </summary>
        /// <returns></returns>
        Task<List<Infrastructure.DBModel.TransactionQueue>> GetAllTransactions(Predicate<Infrastructure.DBModel.TransactionQueue> isValidTransactionBasedOnFilter);
    }
}