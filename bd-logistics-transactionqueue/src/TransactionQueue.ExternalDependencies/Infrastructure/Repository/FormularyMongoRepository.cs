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
    /// This class handles the Formulary mongo db operations
    /// </summary>
    public class FormularyMongoRepository : BaseRepository<Formulary>, IFormularyRepository
    {
        #region Private Fields
        private readonly IMongoCollection<Formulary> _collection;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public FormularyMongoRepository(MongoDbClient dataContext, IMapper mapper)
            : base(dataContext)
        {
            _collection = base.CollectionName<Formulary>();
            _mapper = mapper;
        }

        #endregion Constructors

        #region Data Operations

        /// <summary>
        /// Get Formulary record from DB based on ItemId.
        /// </summary>
        /// <param name="itemId">ItemId</param>
        /// <returns></returns>
        public async Task<Formulary> GetFormularyByItemId(int itemId)
        {
            var result = await _collection.FindAsync(m => m.ItemId == itemId);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Get Formulary record from DB based on formularyId.
        /// </summary>
        /// <param name="formularyId">FormularyId</param>
        /// <returns></returns>
        public async Task<Formulary> GetFormularyById(int formularyId)
        {
            var result = await _collection.FindAsync(m => m.FormularyId == formularyId);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Inserts a formulary in DB.
        /// </summary>
        /// <param name="formulary">Formulary to be stored in db.</param>
        public async Task<bool> InsertFormulary(BusinessLayer.Models.Formulary formulary)
        {
            if (formulary != null)
            {
                var entity = _mapper.Map<Formulary>(formulary);
                entity.CreatedBy = UserName.Admin.ToString();
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.InsertOneAsync(entity);
                return true;
            };
            return false;
        }

        /// <summary>
        /// Update a formulary in DB.
        /// </summary>
        /// <param name="formulary">Formulary to be updated in db.</param>
        public async Task<bool> UpdateFormulary(BusinessLayer.Models.Formulary formulary)
        {
            var entity = await GetFormularyById(formulary.FormularyId);
            if (entity != null)
            {
                entity.IsActive = formulary.IsActive ?? false;
                entity.Description = formulary.Description;
                entity.ItemId = formulary.ItemId;
                entity.ModifiedBy = UserName.Admin.ToString();
                entity.ModifiedDate = DateTime.Now;
                await _collection.ReplaceOneAsync(e => e.FormularyId == entity.FormularyId, entity);
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method is used to store a formulary facility in DB.
        /// </summary>
        /// <param name="formulary">formulary facility to be updated.</param>
        public async Task<bool> UpdateFormularyFacility(BusinessLayer.Models.Formulary formulary)
        {
            if (formulary != null && formulary.FacilityFormulary != null)
            {
                var entity = await GetFormularyById(formulary.FormularyId);
                if (entity != null)
                {
                    if (entity.FacilityFormulary == null)
                        entity.FacilityFormulary = new FacilityFormulary();

                    entity.FacilityFormulary.FacilityId = formulary.FacilityFormulary.FacilityId;
                    entity.FacilityFormulary.AduIgnoreStockout = formulary.FacilityFormulary.AduIgnoreStockout;
                    entity.FacilityFormulary.AduIgnoreCritLow = formulary.FacilityFormulary.AduIgnoreCritLow;
                    entity.FacilityFormulary.Approved = formulary.FacilityFormulary.Approved;

                    entity.ModifiedBy = UserName.Admin.ToString();
                    entity.ModifiedDate = DateTime.Now;
                    await _collection.ReplaceOneAsync(e => e.FormularyId == entity.FormularyId, entity);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This method is used to delete a formulary.
        /// </summary>
        /// <param name="formularyId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFormulary(int formularyId)
        {
            var filter = Builders<Formulary>.Filter.Eq(nameof(formularyId), formularyId);
            await _collection.DeleteOneAsync(filter);
            return true;
        }

        #endregion
    }
}
