using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model.ViewModel
{
    /// <summary>
    /// Medication Item Display class
    /// </summary>
    public class MedicationItemDisplay
    {
        /// <summary>
        /// Item identification
        /// </summary>
        public Guid ItemKey { get; set; }
        /// <summary>
        /// Item Id
        /// </summary>
        public string ItemID { get; set; }
        /// <summary>
        /// Active Flag
        /// </summary>
        public bool ActiveFlag { get; set; }
        /// <summary>
        /// Ignore Flag
        /// </summary>
        public bool IgnoreFlag { get; set; }
        /// <summary>
        /// Alias Item Key
        /// </summary>
        public string AliasItemKey { get; set; }
        /// <summary>
        /// Medication Class Key
        /// </summary>
        public Guid MedicationClassKey { get; set; }
        /// <summary>
        /// Item Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// GL Account Key
        /// </summary>
        public Guid GLAccountKey { get; set; }
        /// <summary>
        /// DispenseForm Lookup Key
        /// </summary>
        public Guid DispenseFormLookupKey { get; set; }
        /// <summary>
        /// Item Dispensing Unit Key
        /// </summary>
        public Guid DispensingUnitLookupKey { get; set; }
        /// <summary>
        /// Item Strength
        /// </summary>
        public string Strength { get; set; }
        /// <summary>
        /// Item Concentration
        /// </summary>
        public string Concentration { get; set; }
        /// <summary>
        /// Item Volume
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// Item Total Volume
        /// </summary>
        public string TotalVolume { get; set; }
        /// <summary>
        /// Item Strentgth UOM
        /// </summary>
        public Guid StrentgthUOM { get; set; }
        /// <summary>
        /// Item Concentration Volume UOM
        /// </summary>
        public Guid ConcentrationVolumeUOM { get; set; }
        /// <summary>
        /// Item Total Volume UOM
        /// </summary>
        public string TotalVolumeUOM { get; set; }
        /// <summary>
        /// Item Charge Code
        /// </summary>
        public string ChargeCode { get; set; }
        /// <summary>
        /// Item HighRisk
        /// </summary>
        public bool HighRisk { get; set; }
        /// <summary>
        /// Item LASA
        /// </summary>
        public bool LASA { get; set; }
        /// <summary>
        /// Item Is Drug Item
        /// </summary>
        public bool IsDrugItem { get; set; }
        /// <summary>
        /// Item Store In Freezer
        /// </summary>
        public bool StoreInFreezer { get; set; }
        /// <summary>
        /// Used For Chemotherapy
        /// </summary>
        public bool UsedForChemotherapy { get; set; }
        /// <summary>
        /// Refrigerate
        /// </summary>
        public bool Refrigerate { get; set; }
        /// <summary>
        /// NonFormulary
        /// </summary>
        public bool NonFormulary { get; set; }

        /// <summary>
        /// Item Hazardous Toxic
        /// </summary>
        public bool HazardousToxic { get; set; }
        /// <summary>
        /// Item Hazardous Aerosol
        /// </summary>
        public bool HazardousAerosol { get; set; }

        /// <summary>
        /// Hazardous Oxidizer
        /// </summary>
        public bool HazardousOxidizer { get; set; }
        /// <summary>
        /// Item Hazardous Bulk Chemical
        /// </summary>
        public bool HazardousBulkChemical { get; set; }
        /// <summary>
        /// Item Hazardous Acid
        /// </summary>
        public bool HazardousAcid { get; set; }
        /// <summary>
        /// Item Hazardous Base
        /// </summary>
        public bool HazardousBase { get; set; }
        /// <summary>
        /// Item Chemotherapeutic Agent
        /// </summary>
        public bool ChemotherapeuticAgent { get; set; }
        /// <summary>
        /// Item BioHazards/Sharps
        /// </summary>
        public bool BioHazardsOrSharps { get; set; }

        /// <summary>
        /// Gets or Sets ProductIdentificationKey
        /// </summary>
        //[DataMember(Name = "ProductIdentificationKey")]
        public Guid ProductIdentificationKey { get; set; }

        /// <summary>
        /// Gets or Sets RecommendationFollowedFlag
        /// </summary>
        //[DataMember(Name = "RecommendationFollowedFlag")]
        public bool? RecommendationFollowedFlag { get; set; }

        /// <summary>
        /// Gets or Sets ProductID
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// Gets or Sets AlternateCode
        /// </summary>
        //[DataMember(Name = "AlternateCode")]
        public string AlternateCode { get; set; }

        /// <summary>
        /// Gets or Sets Manufacturer
        /// </summary>
        //[DataMember(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or Sets GenericName
        /// </summary>
        //[DataMember(Name = "GenericName")]
        public string GenericName { get; set; }

        /// <summary>
        /// Gets or Sets TradeName
        /// </summary>
        //[DataMember(Name = "TradeName")]
        public string TradeName { get; set; }

        /// <summary>
        /// Gets or Sets PackageSize
        /// </summary>
        //[DataMember(Name = "PackageSize")]
        public int? PackageSize { get; set; }

        /// <summary>
        /// Gets or Sets Distributer
        /// </summary>
        //[DataMember(Name = "Distributer")]
        public string Distributer { get; set; }
    }
}
