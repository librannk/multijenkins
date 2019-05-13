using CCEProxy.API.Infrastructure.DataAccess.Mongo.Contracts;
using System.Threading.Tasks;

namespace CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces
{
    /// <summary>
    /// This interface handles the Facility db operations
    /// </summary>
    public interface IFacilityRepository : IBaseRepository<DBModel.Facility>
    {
        /// <summary>
        /// Get Facility record from DB based on FacilityId.
        /// </summary>
        /// <param name="facilityCode">facilityCode</param>
        /// <returns></returns>
        Task<DBModel.Facility> GetFacilityByCode(string facilityCode);
    }
}
