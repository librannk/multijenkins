using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction
{
    /// <summary> This interface is responsible for handling the Destination data operations </summary>
    public interface IDestinationManager
    {
        /// <summary>
        /// Get Destination record from DB based on DestinationCode.
        /// </summary>
        /// <param name="destinationCode">DestinationCode</param>
        /// <returns></returns>
        Task<Destination> GetDestinationByCode(string destinationCode);
    }
}
