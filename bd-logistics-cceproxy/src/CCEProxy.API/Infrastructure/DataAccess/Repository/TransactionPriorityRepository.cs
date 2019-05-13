using BD.Core.ElasticClient.Mongo;
using CCEProxy.API.Infrastructure.DataAccess.DBModel;
using CCEProxy.API.Infrastructure.DataAccess.Mongo.Clients;
using CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CCEProxy.API.Infrastructure.DataAccess.Repository
{
    /// <summary>
    /// This class handles the TransactionPriority db operations
    /// </summary>
    public class TransactionPriorityRepository : BaseRepository<TransactionPriority>, ITransactionPriorityRepository
    {
        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public TransactionPriorityRepository(ElasticDbContext dataContext) : base(dataContext)
        {
        }

        #endregion Constructors

        #region Data Operations

        /// <summary>
        /// Get TransactionPriority record from DB based on FacilityId and PriorityCode.
        /// </summary>
        /// <param name="facilityId">FacilityId</param>
        /// <param name="priorityCode">PriorityCode</param>
        /// <returns></returns>
        public async Task<TransactionPriority> GetTransactionPriority(int facilityId, string priorityCode)
        {
            var transactionPriorityObj = this as ITransactionPriorityRepository;
            var transactionPriorityResults = (await transactionPriorityObj.GetAllAsync()).FirstOrDefault(x => x.FacilityId == facilityId && x.PriorityCode.Equals(priorityCode, StringComparison.OrdinalIgnoreCase) && x.IsActive);
            return transactionPriorityResults;
        }
        #endregion
    }
}
