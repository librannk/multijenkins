using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model
{
    /// <summary>
    /// Facility details
    /// </summary>
    public class FacilityRequest : ModelBase
    {
        /// <summary>
        /// Formulary identifier
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid formulary id")]
        public int FormularyId { get; set; }
        /// <summary>
        /// Whether formulary is active or not for this perticular facility
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Is accepted 
        /// </summary>
        public bool Approved{ get; set; }
        /// <summary>
        /// ADU quantity rounding
        /// </summary>
        public bool ADUQtyRounding { get; set; }
        /// <summary>
        /// ADU Ignore stok out
        /// </summary>
        public bool ADUIgnoreStockOut { get; set; }
        /// <summary>
        /// ADU ignore critical low
        /// </summary>
        public bool ADUIgnoreCritLow { get; set; }
        /// <summary>
        /// Facility Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid item id")]
        public int FacilityId { get; set; }
    }
}
