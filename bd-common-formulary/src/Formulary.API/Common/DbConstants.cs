using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Common
{
    /// <summary>
    /// Constants pertaining to Database
    /// </summary>
    public class DbConstants
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
            /// Item Table
            /// </summary>
            public const string Item = "Item";
            /// <summary>
            /// FormularyStandard table
            /// </summary>
            public const string FormularyStandard = "FormularyStandard";
            /// <summary>
            /// ProductID mapping table
            /// </summary>
            public const string ProductID = "ProductID";

            /// <summary>
            ///  Table used to save DispenseForm details
            /// </summary>
            public const string DispenseForm = "DispenseForm";

            /// <summary>
            /// Table used to save DispenseUnit details
            /// </summary>
            public const string DispenseUnit = "DispenseUnit";

            /// <summary>
            /// Table is used to save formulary's facility details
            /// </summary>
            public const string Facility = "CS_Formulary_FacilityFormulary";

            /// <summary>
            /// Table used to save NDC details 
            /// </summary>
            public const string Ndc = "CS_Formulary_NDC";

            /// <summary>
            /// Table is used to save formulary details
            /// </summary>
            public const string Formulary = "CS_Formulary_Formulary";

            /// <summary>
            ///  Table used to save GLA details 
            /// </summary>
            public const string Gla = "GeneralLedgerAccount";

            /// <summary>
            ///  Table used to save GLA details 
            /// </summary>
            public const string PreferredOrdering = "PreferredOrdering";
        }
    }
}
