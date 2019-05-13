using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction
{
    /// <summary>
    /// This interface is responsible for handling formulary related operations.
    /// </summary>
    public interface IFormularyManager
    {
        /// <summary>
        /// This method is used to store a formulary in DB.
        /// </summary>
        /// <param name="formulary">formulary to be inserted/updated.</param>
        Task<bool> ProcessFormularyRequest(Formulary formulary);

        /// <summary>
        /// Get Formulary record by ItemId.
        /// </summary>
        /// <param name="itemId">ItemId</param>
        /// <returns></returns>
        Task<Formulary> GetFormularyByItemId(int itemId);

        /// <summary>
        /// This method is used to delete a formulary in DB.
        /// </summary>
        /// <param name="formularyId">formularyId</param>
        Task<bool> DeleteFormulary(int formularyId);

        /// <summary>
        /// This method is used to store a formulary facility in DB.
        /// </summary>
        /// <param name="formulary">formulary facility to be updated.</param>
        Task<bool> UpdateFormularyFacility(Formulary formulary);
    }
}