using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Medication Item
    /// </summary>
    public class MedicationItemEntity : BaseEntityFormulary
    {
        /// <summary>
        /// Medication Item Key
        /// </summary>
        [Required]
        [Key]
        public Guid MedicationItemKey { get; set; }
        /// <summary>
        /// Tenant Key
        /// </summary>
        [Required]
        public Guid TenantKey { get; set; }
        /// <summary>
        /// Medication Item Type Internal Code
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string MedicationItemTypeInternalCode { get; set; }
        /// <summary>
        /// Medication Class Key
        /// </summary>
        [ForeignKey("MedClassEntity")]
        public Guid? MedicationClassKey { get; set; }
        /// <summary>
        /// Total Volume Unit Of Measure Key
        /// </summary>
        public Guid? TotalVolumeUnitOfMeasureKey { get; set; }
        /// <summary>
        /// Concentration Volume Unit Of Measure Key
        /// </summary>
        public Guid? ConcentrationVolumeUnitOfMeasureKey { get; set; }
        /// <summary>
        /// Strength Unit Of Measure Key
        /// </summary>
        public Guid? StrengthUnitOfMeasureKey { get; set; }
        /// <summary>
        /// Dispense Form Lookup Key
        /// </summary>
        public Guid? DispenseFormLookupKey { get; set; }
        /// <summary>
        /// Dispense Unit Lookup Key
        /// </summary>
        public Guid? DispenseUnitLookupKey { get; set; }
        /// <summary>
        /// Total Volume External Unit Of Measure Key
        /// </summary>
        public Guid? TotalVolumeExternalUnitOfMeasureKey { get; set; }
        /// <summary>
        /// Strength External Unit Of Measure Key
        /// </summary>
        public Guid? StrengthExternalUnitOfMeasureKey { get; set; }
        /// <summary>
        /// Concentration Volume External Unit Of Measure Key
        /// </summary>
        public Guid? ConcentrationVolumeExternalUnitOfMeasureKey { get; set; }
        /// <summary>
        /// GL Account Key
        /// </summary>
        public Guid? GLAccountKey { get; set; }
        /// <summary>
        /// Medication Item Description
        /// </summary>
        [Column(TypeName = "varchar(225)")]
        public string Description { get; set; }
        /// <summary>
        /// Medication Item Generic Name
        /// </summary>
        [Column(TypeName = "varchar(225)")]
        public string GenericName { get; set; }
        /// <summary>
        /// Charge Code
        /// </summary>
        [Column(TypeName = "varchar(25)")]
        public string ChargeCode { get; set; }
        /// <summary>
        /// High Risk Flag
        /// </summary>
        public bool HighRiskFlag { get; set; }
        /// <summary>
        /// Chemotherapy Flag
        /// </summary>
        public bool ChemotherapyFlag { get; set; }
        /// <summary>
        /// Non Formulary Flag
        /// </summary>
        public bool NonFormularyFlag { get; set; }
        /// <summary>
        /// LASA Flag
        /// </summary>
        public bool LASAFlag { get; set; }
        /// <summary>
        /// Active Flag
        /// </summary>
        public bool ActiveFlag { get; set; }
        /// <summary>
        /// Refrigerated Flag
        /// </summary>
        public bool RefrigeratedFlag { get; set; }
        /// <summary>
        /// Drug Flag
        /// </summary>
        public bool DrugFlag { get; set; }

        /// <summary>
        /// Chemo Agent Flag
        /// </summary>
        public bool ChemoAgentFlag { get; set; }
        /// <summary>
        /// BioHaz Flag
        /// </summary>
        public bool BioHazFlag { get; set; }
        /// <summary>
        /// HazAerosol Flag
        /// </summary>
        public bool HazAerosolFlag { get; set; }
        /// <summary>
        /// HazBase Flag
        /// </summary>
        public bool HazBaseFlag { get; set; }
        /// <summary>
        /// HazAcid Flag
        /// </summary>
        public bool HazAcidFlag { get; set; }
        /// <summary>
        /// Haz Chemical Flag
        /// </summary>
        public bool HazChemicalFlag { get; set; }
        /// <summary>
        /// Haz Oxidizer
        /// </summary>
        public bool HazOxidizerFlag { get; set; }
        /// <summary>
        /// Haz Toxic Flag
        /// </summary>
        public bool HazToxicFlag { get; set; }
        /// <summary>
        /// Freezer Flag
        /// </summary>
        public bool FreezerFlag { get; set; }
        /// <summary>
        /// Requires Setup Flag
        /// </summary>
        public bool RequiresSetupFlag { get; set; }
        /// <summary>
        /// Deleted Flag
        /// </summary>
        public bool DeletedFlag { get; set; }
        /// <summary>
        /// Deleted Flag
        /// </summary>
        public bool PrepHazFlag { get; set; }

        /// <summary>
        /// Strength Amount
        /// </summary>
        public double? StrengthAmount { get; set; }
        /// <summary>
        /// Strength Text
        /// </summary>
        public string StrengthText { get; set; }
        /// <summary>
        /// Concentration Volume Amount
        /// </summary>
        public double? ConcentrationVolumeAmount { get; set; }
        /// <summary>
        /// Total Volume Amount
        /// </summary>
        public double? TotalVolumeAmount { get; set; }
        /// <summary>
        /// Created productIdentifications navigations
        /// </summary>
        public virtual ICollection<ProductIdentificationEntity> ProductIdentifications { get; set; }
        /// <summary>
        /// productIDRecommendedItems Navigation Property
        /// </summary>
        public virtual ICollection<ProductIDRecommendedItemEntity> ProductIDRecommendedItems { get; set; }
        /// <summary>
        /// Item navigation property
        /// </summary>
        public virtual ItemEntity Item { get; set; }
        /// <summary>
        /// dispensingFormLookup navigation property
        /// </summary>
        public virtual DispensingFormLookupEntity dispensingFormLookup { get; set; }
        /// <summary>
        /// dispensingUnitLookup navigation property
        /// </summary>
        public virtual DispensingUnitLookupEntity dispensingUnitLookup { get; set; }
        /// <summary>
        /// GLAccount navigation property
        /// </summary>
        public virtual GLAccountEntity GLAccount { get; set; }

        /// <summary>
        /// Gets or sets the med class.
        /// </summary>
        /// <value>The med class.</value>
        [ForeignKey("MedicationClassKey")]
        public virtual MedClassEntity MedClassEntity { get; set; }
    }
}

