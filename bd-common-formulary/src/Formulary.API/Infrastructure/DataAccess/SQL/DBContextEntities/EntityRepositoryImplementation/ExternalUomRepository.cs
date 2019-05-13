using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{
    public class ExternalUomRepository : BaseRepository<ExternalUomEntity>, EntityRepositoryContracts.IExternalUomRepository
    {
        public ExternalUomRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
