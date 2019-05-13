using BD.Core.ElasticClient.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUsageApp.Infrastructure.MongoRepository
{
    public class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
        #region Constructors
        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public FacilityRepository(ElasticDbContext dataContext) : base(dataContext)
        {
        }

        #endregion Constructors

        #region Data Operations

        /// <summary>
        /// Get Facility record from DB based on FacilityCode.
        /// </summary>
        /// <param name="facilityCode">FacilityCode</param>
        /// <returns></returns>
        public async Task<Facility> GetFacilityByCode(string facilityCode)
        {
            var facilityObj = this as IFacilityRepository;
            var facilityResults = (await facilityObj.GetAllAsync()).FirstOrDefault(x => x.FacilityCode == facilityCode);
            return facilityResults;
        }
        #endregion
    }
}
