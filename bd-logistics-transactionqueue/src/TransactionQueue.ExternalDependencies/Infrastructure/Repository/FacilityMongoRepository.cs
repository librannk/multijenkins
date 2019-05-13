using AutoMapper;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;
using TransactionQueue.ExternalDependencies.Infrastructure.DBModel;
using TransactionQueue.Shared.DataAccess.Mongo;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.ExternalDependencies.Infrastructure.Repository
{
    /// <summary>
    /// This class handles the Facility mongo db operations
    /// </summary>
    public class FacilityMongoRepository : BaseRepository<Facility>, IFacilityRepository
    {
        #region Private Fields
        private readonly IMongoCollection<Facility> _collection;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public FacilityMongoRepository(MongoDbClient dataContext, IMapper mapper)
            : base(dataContext)
        {
            _collection = CollectionName<Facility>();
            _mapper = mapper;
        }

        #endregion Constructors

        #region Data Operations

        /// <summary>
        /// Get Facility record from DB based on FacilityId.
        /// </summary>
        /// <param name="facilityId">FacilityId</param>
        /// <returns></returns>
        public async Task<Facility> GetFacilityById(int facilityId)
        {
            var result = await _collection.FindAsync(m => m.FacilityId == facilityId);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Stores a facility in db.
        /// </summary>
        /// <param name="facility">Facility to be added</param>
        public async Task<bool> InsertFacility(BusinessLayer.Models.Facility facility)
        {
            if (facility != null)
            {
                var entity = _mapper.Map<Facility>(facility);
                entity.CreatedBy = UserName.Admin.ToString();
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.InsertOneAsync(entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Stores a facility in db.
        /// </summary>
        /// <param name="facility">Facility to be updated</param>
        public async Task<bool> UpdateFacility(BusinessLayer.Models.Facility facility)
        {
            var entity = await GetFacilityById(facility.Id);
            if (entity != null)
            {
                entity.ProcessInactiveAsException = facility.ProcessInactiveAsException;
                entity.AduIgnoreCritLow = facility.AduIgnoreCritLow;
                entity.AduIgnoreStockout = facility.AduIgnoreStockout;
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.ReplaceOneAsync(e => e.FacilityId == entity.FacilityId, entity);
                return true;
            }
            return false;
        }

        #endregion
    }
}
