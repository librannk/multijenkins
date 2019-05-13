using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model.ViewModel
{
    /// <summary>
    /// System formulary view model
    /// </summary>
    public class SystemFormulary:SystemItemSetupRequest
    {
        /// <summary>
        /// Item Key
        /// </summary>
        public Guid ItemKey { get; set; }
        /// <summary>
        /// MedicationItemKey
        /// </summary>
        public Guid MedicationItemKey { get; set; }
    }
}
