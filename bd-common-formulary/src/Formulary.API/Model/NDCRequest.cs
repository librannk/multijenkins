using System.ComponentModel.DataAnnotations;

namespace Formulary.API.Model
{
    /// <summary>
    /// NDC Details
    /// </summary>
    public class NDCRequest : ModelBase
    {
        /// <summary>
        /// Formulary Identifier
        /// </summary>  
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid formulary id")]
        public int FormularyId { get; set; }
        /// <summary>
        /// Whether it is active or not
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// trade name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter valid trade name")]
        public string TradeName { get; set; }
        /// <summary>
        /// Generic Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter valid generic name")]
        public string GenericName { get; set; }
        /// <summary>
        /// National Drug Code
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter valid NDC code")]
        public string NDC { get; set; }
        /// <summary>
        /// Facility NDC Association Details
        /// </summary>
        
        public FacilityNDCAssociationRequest FacilityNDCDetails { get; set; }
    }
}
