using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.TransactionPriority.RequestResponseModel
{

    [ExcludeFromCodeCoverage]
    public class TransactionPriorityGet
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        public string TranPriorityKey { get; set; }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        public string PriorityCode { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        public string PriorityName { get; set; }

        /// <summary>
        /// Gets or Sets ManualPick
        /// </summary>
        public bool ForManualPickFlag { get; set; }

        public bool ForManualRestockFlag { get; set; }

        /// <summary>
        /// Gets or Sets UseInterfaceMedName
        /// </summary>
        public bool UseInterfaceMedNameFlag { get; set; }

        /// <summary>
        /// Gets or Sets IsADU
        /// </summary>
        public bool ADUFlag { get; set; }

        /// <summary>
        /// Foreground Color
        /// </summary>
        /// <value>Foreground Color</value>
        public string LegendForeColor { get; set; }

        /// <summary>
        /// Background Color
        /// </summary>
        /// <value>Background Color</value>
        public string LegendBackColor { get; set; }

        /// <summary>
        /// Gets or Sets IsActive
        /// </summary>
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// Gets or Sets MaxHoldLength
        /// </summary>
        public int MaxOnHoldLength { get; set; }

        /// <summary>
        /// Gets or Sets System
        /// </summary>
        public bool SystemFlag { get; set; }

        /// <summary>
        /// Gets or Sets PriorityOrder
        /// </summary>
        public long PriorityOrder { get; set; }

        /// <summary>
        /// Gets or Sets SmartSorts
        /// </summary>
        public List<TransactionPrioritySmartSort> SmartSorts { get; set; }


    }
}
