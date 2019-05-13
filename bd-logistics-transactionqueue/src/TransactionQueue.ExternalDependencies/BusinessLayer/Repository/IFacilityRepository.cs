using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.Infrastructure.DBModel;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Repository
{
    /// <summary>
    /// This interface handles the Facility mongo db operations
    /// </summary>
    public interface IFacilityRepository : IBaseRepository<Facility>
    {
        /// <summary>
        /// Get Facility record from DB based on FacilityId.
        /// </summary>
        /// <param name="facilityId">FacilityId</param>
        /// <returns></returns>
        Task<Facility> GetFacilityById(int facilityId);

        /// <summary>
        /// Inserts a facility in db.
        /// </summary>
        /// <param name="facility">Facility to be stored.</param>
        Task<bool> InsertFacility(Models.Facility facility);

        /// <summary>
        /// Update a facility in db.
        /// </summary>
        /// <param name="facility">Facility to be updated.</param>
        Task<bool> UpdateFacility(Models.Facility facility);
    }
}
