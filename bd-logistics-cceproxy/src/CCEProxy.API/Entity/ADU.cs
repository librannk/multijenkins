using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCEProxy.API.Entity
{
    /// <summary>
    /// Class ADU
    /// </summary>
    public class ADU
    {
        /// <summary>
        /// To hold the value for AduTransId
        /// </summary>
        public string AduTransId { get; set; }

        /// <summary>
        /// To hold the value for StationName
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// To hold the value for Drawer
        /// </summary>
        public string Drawer { get; set; }

        /// <summary>
        /// To hold the value for Subdrawer
        /// </summary>
        public string Subdrawer { get; set; }

        /// <summary>
        /// To hold the value for Pocket
        /// </summary>
        public string Pocket { get; set; }
    }
}
