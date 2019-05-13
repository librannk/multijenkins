using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{  /// <summary>
    /// Class ProductIdentificationRepository.
    /// Implements the <see cref="ProductIdentificationEntity" />
    /// Implements the <see cref="Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts.IProductIdentificationRepository" />
    /// </summary>
    /// <seealso cref="ProductIdentificationEntity" />
    /// <seealso cref="Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts.IProductIdentificationRepository" />
    public class ProductIdentificationRepository : BaseRepository<ProductIdentificationEntity>, IProductIdentificationRepository
    {
        /// <summary>
        /// Constructor is used to initialize this class
        /// </summary>
        /// <param name="context"></param>
        public ProductIdentificationRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
