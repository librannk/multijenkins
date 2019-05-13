using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{
    public class FacilityNDCAssocRepository: BaseRepository<FacilityNDCAssocEntity>, IFacilityNDCAssocRepository
    {
        /// <summary>
        /// Constructor is used to initialize this class
        /// </summary>
        /// <param name="context"></param>
        public FacilityNDCAssocRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
