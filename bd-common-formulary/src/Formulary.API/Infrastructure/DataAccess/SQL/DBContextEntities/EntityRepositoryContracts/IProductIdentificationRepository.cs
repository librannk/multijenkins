using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// Interface IProductIdentificationRepository
    /// Implements the <see cref="ProductIdentificationEntity" />
    /// </summary>
    /// <seealso cref="ProductIdentificationEntity" />
    public interface IProductIdentificationRepository : IRepository<ProductIdentificationEntity>
    {
    }
}
