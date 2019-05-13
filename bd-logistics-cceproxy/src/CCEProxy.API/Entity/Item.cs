
namespace CCEProxy.API.Entity
{
     /// <summary>
     /// This model contains Item details.
     /// </summary>
    public class Item
    {
        /// <summary>
        /// Type of component
        /// </summary>
        public string ComponentType { get; set; }

        /// <summary>
        /// ID of Item
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Name of Item
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// ComponentAmount
        /// </summary>
        public string ComponentAmount { get; set; }

        /// <summary>
        /// Order Amount of that Item
        /// </summary>
        public string OrderAmount { get; set; }

        /// <summary>
        /// Dispense units of Item
        /// </summary>
        public string DispenseUnits { get; set; }

        /// <summary>
        /// Component Strength of Item
        /// </summary>
        public string ComponentStrength { get; set; }

        /// <summary>
        ///Component Strength Units of Item
        /// </summary>
        public string ComponentStrengthUnits { get; set; }

        /// <summary>
        /// Supplementary Code of Item
        /// </summary>
        public string SupplementaryCode { get; set; }

        /// <summary>
        /// Total Dose of item
        /// </summary>
        public string TotalDose { get; set; }
        ///Strength
        public string Strength { get; set; }
        ///Volume
        public string Volume { get; set; }
        ///Concentration
        public string Concentration { get; set; }
        ///PharmacySpecialDispInstructions
        public string PharmacySpecialDispInstructions { get; set; }
        ///DispenseAmount
        public string DispenseAmount { get; set; }
    }
}
