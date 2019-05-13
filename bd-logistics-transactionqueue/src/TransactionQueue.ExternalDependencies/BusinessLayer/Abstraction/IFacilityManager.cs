using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction
{
    /// <summary>
    /// This interface is responsible for handling facility related operations.
    /// </summary>
    public interface IFacilityManager
    {
        /// <summary>
        /// This method is used to store a facility in DB.
        /// </summary>
        /// <param name="facility">Facility to be inserted/updated.</param>
        Task<bool> ProcessFacilityRequest(Facility facility);

        /// <summary>
        /// This method is used to validate facility.
        /// </summary>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        Task<Facility> ValidateFacility(int facilityId);
    }
}
