using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model.ViewModel
{
    /// <summary>
    /// Item Setup 
    /// </summary>
    public class ItemRequest
    {
        /// <summary>
        /// Item identification
        /// </summary>
        public Guid ItemKey { get; set; }
        /// <summary>
        /// Item Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Item TradeName
        /// </summary>
        public string TradeName { get; set; }
        /// <summary>
        /// Item PreferredNDC
        /// </summary>
        public string PreferredProductID { get; set; }
        /// <summary>
        /// Item AltCode
        /// </summary>
        public string AltCode { get; set; }
        /// <summary>
        /// Item AliasItemID
        /// </summary>
        public string AliasItemID { get; set; }

    }
}
