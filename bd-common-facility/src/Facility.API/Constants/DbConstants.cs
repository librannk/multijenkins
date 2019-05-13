namespace Facility.API.Constants
{
    /// <summary>
    /// Constants pertaining to Database
    /// </summary>
    public static class DbConstants
    {
        /// <summary>
        /// Constant for default Schema Name
        /// </summary>
        public const string DefaultDboSchema = "dbo";
        /// <summary>
        /// Constants for Table Names
        /// </summary>
        public class TableNames
        {
            /// <summary>
            /// Facilities Table
            /// </summary>
            public const string Facilities = "Facilities";
            /// <summary>
            /// ControlledSubstanceLicenses table
            /// </summary>
            public const string ControlledSubstanceLicenses = "ControlledSubstanceLicenses";
            /// <summary>
            /// ControlledSubstanceLicenseFacilities mapping table
            /// </summary>
            public const string ControlledSubstanceLicenseFacilities = "ControlledSubstanceLicenseFacilities";
        }
    }
}
