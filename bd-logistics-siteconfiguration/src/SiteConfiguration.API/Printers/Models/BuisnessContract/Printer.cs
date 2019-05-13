using System;

namespace SiteConfiguration.API.Printers.Models.BuisnessContract
{
    /// <summary>
    /// Printer
    /// </summary>
    public class Printer
    {
        /// <summary>
        /// The Key property represents the printer's id/key.
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// The Name property represents the printer's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Description property represents the printer's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The IsActive property represents if printer is active or not.
        /// </summary>
        /// <value>True if active.</value>
        public bool IsActive { get; set; }
    }
}
