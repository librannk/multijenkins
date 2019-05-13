using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{
    /// <summary>
    /// FormularyRepository class implements the all member of IFormularyRepository of type Formulary.
    /// </summary> 
    public class FormularyRepository : BaseRepository<FormularyEntity>, IFormularyRepository
    {
        /// <summary>
        /// Constructor is used to initialize this class
        /// </summary>
        /// <param name="context"></param>
        public FormularyRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
