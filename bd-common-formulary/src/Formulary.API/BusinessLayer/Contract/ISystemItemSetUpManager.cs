using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Model;
using Formulary.API.Model.InternalModel;

namespace Formulary.API.BusinessLayer.Contract
{
    /// <summary>
    /// IItemSetUpManager
    /// </summary>
    public interface ISystemItemSetUpManager
    {
        /// <summary>
        /// Updates the system item set up.
        /// </summary>
        /// <param name="itemkey">The itemkey.</param>
        /// <param name="medicationItem">The medication item.</param>
        /// <returns>Task&lt;BusinessResult&lt;Model.MedicationItem&gt;&gt;.</returns>
        Task<BusinessResult<Model.SystemItemSetupRequest>> UpdateSystemItemSetUp(Guid itemkey, SystemItemSetupRequest medicationItem);
    }
}
