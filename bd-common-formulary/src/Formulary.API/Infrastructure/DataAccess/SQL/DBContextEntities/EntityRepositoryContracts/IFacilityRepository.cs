using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// IFacilityRepository interface represent the all member of IRepository of type Customer.
    /// IFacilityRepository provide the extensibility for new operation other than IRepository.
    /// </summary> 
    public interface IFacilityRepository : IRepository<FacilityEntity>
    {

    }
}
