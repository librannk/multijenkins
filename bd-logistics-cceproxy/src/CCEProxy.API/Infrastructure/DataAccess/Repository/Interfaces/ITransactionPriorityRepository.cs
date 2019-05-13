using CCEProxy.API.Infrastructure.DataAccess.Mongo.Contracts;
using System.Threading.Tasks;

namespace CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces
{
    /// <summary>
    /// This interface handles the TransactionPriority db operations
    /// </summary>
    public interface ITransactionPriorityRepository : IBaseRepository<DBModel.TransactionPriority>
    {
        /// <summary>
        /// Get Facility record from DB based on FacilityId.
        /// </summary>
        /// <param name="facilityId">facilityId</param>
        /// <param name="priorityCode">priorityCode</param>
        /// <returns></returns>
        Task<DBModel.TransactionPriority> GetTransactionPriority(int facilityId, string priorityCode);
    }
}
