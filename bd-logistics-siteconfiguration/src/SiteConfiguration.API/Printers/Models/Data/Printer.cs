using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteConfiguration.API.Printers.Models.Data
{

    /// <summary>
    /// Printer entity.
    /// </summary>
    public class Printer : BaseEntity
    {

        /// <summary>
        /// Printer Key
        /// </summary>
        [Key]
        public Guid PrinterKey { get; set; }

        /// <summary>
        /// ModelKey
        /// </summary>
        public Guid PrinterModelKey { get; set; }


        /// <summary>
        /// Facility Key
        /// </summary>
        public Guid FacilityKey { get; set; }

        /// <summary>
        /// Printer Name
        /// </summary>
        public string PrinterName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string DescriptionText { get; set; }

        /// <summary>
        /// IPAddress
        /// </summary>
        public string IPAddressText { get; set; }


        /// <summary>
        /// Port
        /// </summary>
        public ushort IPPortNumber { get; set; }

        /// <summary>
        /// MacAddress
        /// </summary>
        public string MacAddressText { get; set; }

        /// <summary>
        /// PrintableAreaHeight
        /// </summary>
        public decimal? PrintableAreaHeightValue { get; set; }


        /// <summary>
        /// PrintableAreaWidth
        /// </summary>
        public decimal? PrintableAreaWidthValue { get; set; }

        /// <summary>
        /// LabelBarcode
        /// </summary>
        public string LabelBarcodeText { get; set; }

        /// <summary>
        /// isLabelPrinter
        /// </summary>
        public bool LabelPrinterFlag { get; set; }

        /// <summary>
        /// isAppendFormFeed
        /// </summary>
        public bool AppendFormFeedFlag { get; set; }

        /// <summary>
        /// Active Flag
        /// </summary>
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// IsRotateLabelOrder
        /// </summary>
        public bool RotateLabelOrderFlag { get; set; }
    }
}
