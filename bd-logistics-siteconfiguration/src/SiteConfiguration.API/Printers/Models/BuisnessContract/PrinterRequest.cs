using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Common.Constants;

namespace SiteConfiguration.API.Printers.Models.BuisnessContract
{
    /// <summary>
    /// PrinterRequest
    /// </summary>
    public class PrinterRequest
    {
        /// <summary>
        /// ModelKey
        /// </summary>
        [Required]
        public Guid PrinterModelKey { get; set; }

        /// <summary>
        /// The Name property represents the printer's name.
        /// </summary>
        [MaxLength(50)]
        public string PrinterName { get; set; }

        /// <summary>
        /// The Description property represents the printer's description.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// IPAddress
        /// </summary>
        [Required]
        [RegularExpression(@"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b", ErrorMessage = Constants.InvalidIpAddress)]
        public string IpAddress { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public ushort IpPort { get; set; }

        /// <summary>
        /// MacAddress
        /// </summary>
        [MaxLength(12)]
        [MinLength(12)]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = Constants.InvalidMacAddress)]
        public string MacAddress { get; set; }

        /// <summary>
        /// PrintableAreaHeight
        /// </summary>
        [RegularExpression(@"^(\d{1,15}|\d{1,15}\.\d{1,3})$", ErrorMessage = Constants.InvalidDimensions)]
        public double? PrintableAreaHeight { get; set; }

        /// <summary>
        /// PrintableAreaWidth
        /// </summary>
        [RegularExpression(@"^(\d{1,15}|\d{1,15}\.\d{1,3})$", ErrorMessage = Constants.InvalidDimensions)]
        public double? PrintableAreaWidth { get; set; }

        /// <summary>
        /// LabelBarcode
        /// </summary>
        [MaxLength(1000)]
        public string LabelBarcode { get; set; }

        /// <summary>
        /// isLabelPrinter
        /// </summary>
        public bool LabelPrinterFlag { get; set; }

        /// <summary>
        /// isAppendFormFeed
        /// </summary>
        public bool AppendFormFeedFlag { get; set; }

        /// <summary>
        /// The IsActive property represents if printer is active or not.
        /// </summary>
        /// <value>True if active.</value>
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// IsRotateLabelOrder
        /// </summary>
        public bool RotateLabelOrderFlag { get; set; }
    }
}
