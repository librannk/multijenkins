using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.Infrastructure.DBModel;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Repository
{    /// <summary>
    /// This interface handles the Destination mongo db operations
    /// </summary>
    public interface IDestinationRepository : IBaseRepository<Destination>
    {
        /// <summary>
        /// Get Destination record from DB based on DestinationCode.
        /// </summary>
        /// <param name="destinationCode">DestinationCode</param>
        /// <returns></returns>
        Task<Destination> GetDestinationByCode(string destinationCode);
    }
}
