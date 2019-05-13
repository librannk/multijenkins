using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// IFacilityNDCAssocRepository interface represent the all member of IRepository of type Customer.
    /// IFacilityNDCAssocRepository provide the extensibility for new operation other than IRepository.
    /// </summary> 
    public interface IFacilityNDCAssocRepository:IRepository<FacilityNDCAssocEntity>
    {

    }
}
