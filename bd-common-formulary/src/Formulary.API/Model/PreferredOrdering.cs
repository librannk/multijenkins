using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model
{
    /// <summary>
    /// PreferredOrdering.
    /// </summary>
    public class PreferredOrdering
    {
        /// <summary>
        /// Gets or sets the preferred ordering key.
        /// </summary>
        /// <value>The preferred ordering key.</value>
        public Guid PreferredOrderingKey { get; set; }
        /// <summary>
        /// Gets or sets the vendor key.
        /// </summary>
        /// <value>The vendor key.</value>
        public Guid VendorKey { get; set; }
        /// <summary>
        /// Gets or sets the product identifier key.
        /// </summary>
        /// <value>The product identifier key.</value>
        public Guid ProductIDKey { get; set; }
        /// <summary>
        /// Gets or sets the item key.
        /// </summary>
        /// <value>The item key.</value>
        public Guid ItemKey { get; set; }
        /// <summary>
        /// Gets or sets the vendor item code.
        /// </summary>
        /// <value>The vendor item code.</value>
        public string VendorItemCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is preferred.
        /// </summary>
        /// <value><c>true</c> if this instance is preferred; otherwise, <c>false</c>.</value>
        public bool IsPreferred { get; set; }
    }
}
