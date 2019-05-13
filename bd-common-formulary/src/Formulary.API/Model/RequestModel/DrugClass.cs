using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model.RequestModel
{
    /// <summary>
    /// Drug Class
    /// </summary>
    public class DrugClass
    {
        /// <summary>
        /// MedicationClassKey
        /// </summary>
        public Guid MedicationClassKey { get; set; }
        /// <summary>
        /// MedicationClassCode
        /// </summary>
        public string MedicationClassCode { get; set; }
    }
}
