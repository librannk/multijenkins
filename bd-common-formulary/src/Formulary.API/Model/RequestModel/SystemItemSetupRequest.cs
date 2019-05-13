using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.Model.RequestModel;

namespace Formulary.API.Model
{
    /// <summary>
    /// Incoming Request Model for System Item Set up
    /// </summary>
    public class SystemItemSetupRequest
    {
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter valid Item Id")]
        public string ItemId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [active flag].
        /// </summary>
        /// <value><c>true</c> if [active flag]; otherwise, <c>false</c>.</value>
        public bool ActiveFlag { get; set; }
        /// <summary>
        /// Gets or sets the alternate item identifier.
        /// </summary>
        /// <value>The alternate item identifier.</value>
        public string AlternateItemID { get; set; }
        /// <summary>
        /// Gets or sets the medication class key.
        /// </summary>
        /// <value>The medication class key.</value>
        /// [Required(ErrorMessage = "Pls enter MedicationClassKey")]
        public Guid? MedicationClassKey { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the genral ledger account key.
        /// </summary>
        /// <value>The genral ledger account key.</value>
        /// [Required(ErrorMessage = "Pls enter GenralLedgerAccountKey")]
        public Guid? GenralLedgerAccountKey { get; set; }
        /// <summary>
        /// Gets or sets the dispensing form key.
        /// </summary>
        /// <value>The dispensing form key.</value>
        /// [Required(ErrorMessage = "Pls enter DispensingFormKey")]
        public Guid? DispensingFormKey { get; set; }
        /// <summary>
        /// Gets or sets the dispensing unit key.
        /// </summary>
        /// <value>The dispensing unit key.</value>
        /// [Required(ErrorMessage = "Pls enter DispensingUnitKey")]
        public Guid? DispensingUnitKey { get; set; }
        /// <summary>
        /// Gets or sets the strength amount.
        /// </summary>
        /// <value>The strength amount.</value>
        public double? StrengthAmount { get; set; }
        /// <summary>
        /// Gets or sets the concentration volume amount.
        /// </summary>
        /// <value>The concentration volume amount.</value>
        public double? ConcentrationVolumeAmount { get; set; }
        /// <summary>
        /// Gets or sets the total volume amount.
        /// </summary>
        /// <value>The total volume amount.</value>
        public double? TotalVolumeAmount { get; set; }
        /// <summary>
        /// Gets or sets the total volume unit of messure key.
        /// </summary>
        /// <value>The total volume unit of messure key.</value>
        /// [Required(ErrorMessage = "Pls enter TotalVolumeUnitOfMessureKey")]
        public Guid? TotalVolumeUnitOfMessureKey { get; set; }
        /// <summary>
        /// Gets or sets the strength unit of messure key.
        /// </summary>
        /// <value>The strength unit of messure key.</value>
        /// [Required(ErrorMessage = "Pls enter StrengthUnitOfMessureKey")]
        public Guid? StrengthUnitOfMessureKey { get; set; }
        /// <summary>
        /// Gets or sets the concentration volume unit of messure key.
        /// </summary>
        /// <value>The concentration volume unit of messure key.</value>
        /// [Required(ErrorMessage = "Pls enter ConcentrationVolumeUnitOfMessureKey")]
        public Guid? ConcentrationVolumeUnitOfMessureKey { get; set; }
        /// <summary>
        /// Gets or sets the charge code.
        /// </summary>
        /// <value>The charge code.</value>
        public string ChargeCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [high risk flag].
        /// </summary>
        /// <value><c>true</c> if [high risk flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter HighRiskFlag")]
        public bool HighRiskFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [chemotherapy flag].
        /// </summary>
        /// <value><c>true</c> if [chemotherapy flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter ChemotherapyFlag")]
        public bool ChemotherapyFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [non formulary flag].
        /// </summary>
        /// <value><c>true</c> if [non formulary flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter NonFormularyFlag")]
        public bool NonFormularyFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [lasa flag].
        /// </summary>
        /// <value><c>true</c> if [lasa flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter LASAFlag")]
        public bool LASAFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [refrigerated flag].
        /// </summary>
        /// <value><c>true</c> if [refrigerated flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter RefrigeratedFlag")]
        public bool RefrigeratedFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [drug flag].
        /// </summary>
        /// <value><c>true</c> if [drug flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter DrugFlag")]
        public bool DrugFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [chemo agent flag].
        /// </summary>
        /// <value><c>true</c> if [chemo agent flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter ChemoAgentFlag")]
        public bool ChemoAgentFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [bio haz flag].
        /// </summary>
        /// <value><c>true</c> if [bio haz flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter BioHazFlag")]
        public bool BioHazFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [haz aerosol flag].
        /// </summary>
        /// <value><c>true</c> if [haz aerosol flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter HazAerosolFlag")]
        public bool HazAerosolFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [haz base flag].
        /// </summary>
        /// <value><c>true</c> if [haz base flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter HazBaseFlag")]
        public bool HazBaseFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [haz acid flag].
        /// </summary>
        /// <value><c>true</c> if [haz acid flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter HazAcidFlag")]
        public bool HazAcidFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [haz chemical flag].
        /// </summary>
        /// <value><c>true</c> if [haz chemical flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter HazChemicalFlag")]
        public bool HazChemicalFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [haz oxidizer flag].
        /// </summary>
        /// <value><c>true</c> if [haz oxidizer flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter HazOxidizerFlag")]
        public bool HazOxidizerFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [haz toxic flag].
        /// </summary>
        /// <value><c>true</c> if [haz toxic flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter HazToxicFlag")]
        public bool HazToxicFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [freezer flag].
        /// </summary>
        /// <value><c>true</c> if [freezer flag]; otherwise, <c>false</c>.</value>
        [Required(ErrorMessage = "Pls enter FreezerFlag")]
        public bool FreezerFlag { get; set; }
        

        //TODO: For NDC

        ///// <summary>
        ///// Gets or sets the product identifications.
        ///// </summary>
        ///// <value>The product identifications.</value>
        //public IList<ProductIdentification> ProductIdentifications { get; set; }
    }
}
