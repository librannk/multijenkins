using BD.Core.ElasticClient.Mongo;
using CCEProxy.API.Infrastructure.DataAccess.Mongo.Clients;
using CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace CCEProxy.API.Infrastructure.DataAccess.Repository
{
    /// <summary>
    /// This class handles the Facility db operations
    /// </summary>
    public class FacilityRepository : BaseRepository<DBModel.Facility>, IFacilityRepository
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
        public async Task<DBModel.Facility> GetFacilityByCode(string facilityCode)
        {
            var facilityObj = this as IFacilityRepository;
            var facilityResults = (await facilityObj.GetAllAsync()).FirstOrDefault(x => x.FacilityCode == facilityCode);
            return facilityResults;
        }
        #endregion
    }
}
