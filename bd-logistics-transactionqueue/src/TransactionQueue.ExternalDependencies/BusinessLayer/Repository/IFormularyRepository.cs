using TransactionQueue.ExternalDependencies.Infrastructure.DBModel;
using System.Threading.Tasks;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Repository
{
    /// <summary>
    /// This interface handles the Formulary mongo db operations
    /// </summary>
    public interface IFormularyRepository : IBaseRepository<Formulary>
    {
        /// <summary>
        /// Get Formulary record from DB based on ItemId.
        /// </summary>
        /// <param name="itemId">ItemId</param>
        /// <returns></returns>
        Task<Formulary> GetFormularyByItemId(int itemId);

        /// <summary>
        /// Get Formulary record from DB based on formularyId.
        /// </summary>
        /// <param name="formularyId">FormularyId</param>
        /// <returns></returns>
        Task<Formulary> GetFormularyById(int formularyId);

        /// <summary>
        /// Inserts a formulary in DB.
        /// </summary>
        /// <param name="formulary">Formulary to be stored in db.</param>
        Task<bool> InsertFormulary(Models.Formulary formulary);

        /// <summary>
        /// Update a formulary in DB.
        /// </summary>
        /// <param name="formulary">Formulary to be stored in db.</param>
        Task<bool> UpdateFormulary(Models.Formulary formulary);

        /// <summary>
        /// This method is used to update a formulary facility in DB.
        /// </summary>
        /// <param name="formulary">formulary facility to be updated.</param>
        Task<bool> UpdateFormularyFacility(Models.Formulary formulary);

        /// <summary>
        /// This method is used to delete a formulary.
        /// </summary>
        /// <param name="formularyId"></param>
        /// <returns></returns>
        Task<bool> DeleteFormulary(int formularyId);
    }
}