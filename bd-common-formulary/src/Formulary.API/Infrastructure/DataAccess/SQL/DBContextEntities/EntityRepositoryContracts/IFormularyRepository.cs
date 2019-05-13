using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// IFormularyRepository interface represent the all member of IRepository of type Customer.
    /// IFormularyRepository provide the extensibility for new operation other than IRepository.
    /// </summary> 
    public interface IFormularyRepository : IRepository<FormularyEntity>
    {

    }
}
