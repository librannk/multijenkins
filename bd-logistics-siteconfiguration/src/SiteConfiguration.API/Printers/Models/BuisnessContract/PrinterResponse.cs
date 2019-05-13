using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Models.BuisnessContract
{
    /// <summary>
    /// PrinterResponse
    /// </summary>
    public class PrinterResponse
    {

        /// <summary>
        /// PrinterModelKey
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// ModelKey
        /// </summary>
        public Guid PrinterModelKey { get; set; }

        /// <summary>
        /// The Name property represents the printer's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Description property represents the printer's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// IPAddress
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public ushort IpPort { get; set; }

        /// <summary>
        /// MacAddress
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// PrintableAreaHeight
        /// </summary>
        public double? PrintableAreaHeight { get; set; }

        /// <summary>
        /// PrintableAreaWidth
        /// </summary>
        public double? PrintableAreaWidth { get; set; }

        /// <summary>
        /// LabelBarcode
        /// </summary>
        public string LabelBarcode { get; set; }


        /// <summary>
        /// LabelPrinterFlag
        /// </summary>
        public bool LabelPrinterFlag { get; set; }

        /// <summary>
        /// AppendFormFeedFlag
        /// </summary>
        public bool AppendFormFeedFlag { get; set; }

        /// <summary>
        /// The IsActive property represents if printer is active or not.
        /// </summary>
        /// <value>True if active.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// RotateLabelOrderFlag
        /// </summary>
        public bool RotateLabelOrderFlag { get; set; }
    }
}
