using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Model.ViewModel;

namespace Formulary.API.BusinessLayer.Contract
{
    /// <summary>
    /// ItemSetup Manager Interface
    /// </summary>
    public interface IItemSetupManager
    {
        /// <summary>
        /// Get SystemItemList
        /// </summary>
        /// <returns></returns>
        Task<List<MedicationItemList>> GetMedicationItems();
    }
}
