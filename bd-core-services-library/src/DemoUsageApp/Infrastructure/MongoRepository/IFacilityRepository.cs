using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUsageApp.Infrastructure.MongoRepository
{
    public interface IFacilityRepository : IBaseRepository<Facility>
    {
        /// <summary>
        /// Get Facility record from DB based on FacilityId.
        /// </summary>
        /// <param name="facilityCode">facilityCode</param>
        /// <returns></returns>
        Task<Facility> GetFacilityByCode(string facilityCode);
    }
}
