using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using Formulary.API.Model.ViewModel;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// IMedicationItemRepository
    /// </summary>
    public interface IMedicationItemRepository : IRepository<MedicationItemEntity>
    {
        /// <summary>
        /// Add System Item
        /// </summary>
        /// <param name="medicationItemEntity"></param>
        /// <returns></returns>
        Task<bool> AddMedicationItem(MedicationItemEntity medicationItemEntity);
    }
}
