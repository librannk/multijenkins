using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models.Enums;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction
{
    /// <summary>
    /// This interface is responsible for handling Transaction Priority related operations.
    /// </summary>
    public interface ITransactionPriorityManager
    {
        /// <summary>
        /// This method is used to store a Priority in DB.
        /// </summary>
        /// <param name="priority">Priority to be inserted/updated.</param>
        Task<bool> ProcessTransactionPriorityRequest(TransactionPriority priority);

        /// <summary>
        /// This method is used to validate priority.
        /// </summary>
        /// <param name="facilityId">FacilityId</param>
        /// <param name="priorityCode">PriorityCode</param>
        /// <returns></returns>
        Task<TransactionPriority> ValidatePriority(int facilityId, string priorityCode);

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