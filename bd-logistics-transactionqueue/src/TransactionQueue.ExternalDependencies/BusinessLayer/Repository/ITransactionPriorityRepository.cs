using TransactionQueue.ExternalDependencies.Infrastructure.DBModel;
using System.Threading.Tasks;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models.Enums;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Repository
{
    /// <summary>
    /// This interface handles the TransactionPriority mongo db operations
    /// </summary>
    public interface ITransactionPriorityRepository : IBaseRepository<TransactionPriority>
    {
        /// <summary>
        /// Get TransactionPriority record from DB based on FacilityId and PriorityCode.
        /// </summary>
        /// <param name="facilityId">FacilityId</param>
        /// <param name="priorityCode">PriorityCode</param>
        /// <returns></returns>
        Task<TransactionPriority> GetPriorityByFacilityIdAndPriorityCode(int facilityId, string priorityCode);

        /// <summary>
        /// Inserts a TransactionPriority in DB.
        /// </summary>
        /// <param name="priority">TransactionPriority to be stored in db.</param>
        Task<bool> InsertTransactionPriority(Models.TransactionPriority priority);

        /// <summary>
        /// Update a TransactionPriority in DB.
        /// </summary>
        /// <param name="priority">TransactionPriority to be updated in db.</param>
        Task<bool> UpdateTransactionPriority(Models.TransactionPriority priority);

        /// <summary>
        /// Get TransactionPriority record from DB based on transactionPriorityId.
        /// </summary>
        /// <param name="transactionPriorityId">TransactionPriorityId</param>
        /// <returns></returns>
        Task<TransactionPriority> GetTransactionPriority(int transactionPriorityId);

        /// <summary>
        /// Get TransactionPriority record from DB based on transactionPriorityCode.
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        Task<TransactionPriority> GetTransactionPriority(Priority priority, int facilityId);
    }
}