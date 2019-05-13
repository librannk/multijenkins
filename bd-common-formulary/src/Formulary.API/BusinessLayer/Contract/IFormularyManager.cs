using System.Threading.Tasks;
using Formulary.API.Model;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Model.InternalModel;

namespace Formulary.API.BusinessLayer.Contract
{
    /// <summary>
    /// Formulary Manager class
    /// </summary>
    public interface IFormularyManager
    {
        /// <summary>
        /// Add system formulary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BusinessResult<SystemItemSetupRequest>> AddSystemItem(SystemItemSetupRequest request);
        /// <summary>
        /// Save formulary to the database
        /// </summary>
        /// <param name="request"></param>
        Task<FormularyEntity> SaveFormulary(FormularyRequest request);
        /// <summary>
        /// Update Formulary in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<FormularyEntity> UpdateFormulary(int Id,FormularyRequest request);
        /// <summary>
        /// Save Facility to the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<FacilityEntity> SaveFacility(FacilityRequest request);
        /// <summary>
        /// Save NDC details to the database
        /// </summary>
        /// <param name="nDCDetails"></param>
        Task<NDCEntity> SaveNDC(NDCRequest nDCDetails);
        /// <summary>
        /// Save FacilityNDCAssociation Details to the database
        /// </summary>
        /// <param name="facilityNDCAssoc"></param>
        /// <returns></returns>
        Task<FacilityNDCAssocEntity> SaveFacilityNDCAssoc(FacilityNDCAssociationRequest facilityNDCAssoc);
    }
}

