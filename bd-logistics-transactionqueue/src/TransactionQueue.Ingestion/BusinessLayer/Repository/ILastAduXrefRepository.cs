using System.Threading.Tasks;
using System.Collections.Generic;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;

namespace TransactionQueue.Ingestion.BusinessLayer.Repository
{
    /// <summary>
    /// This interface handles the LastAduXref mongo db operations
    /// </summary>
    public interface ILastAduXrefRepository : IBaseRepository<Infrastructure.DBModel.LastAduXref>
    {
        /// <summary>
        /// Get all LastAduXref record from DB based.
        /// </summary>
        /// <returns></returns>
        Task<List<Infrastructure.DBModel.LastAduXref>> GetAllLastAduXrefTransactions();
    }
}
