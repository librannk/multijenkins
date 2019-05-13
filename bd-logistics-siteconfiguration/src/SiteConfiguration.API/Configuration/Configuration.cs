using BD.Core.EventBusKafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Configuration
{
    /// <summary> Configuration </summary>
    public class Configuration : EventBusConfiguration
    {
        /// <summary>
        /// RoutingRule Topic
        /// </summary>
        public string RoutingRule { get; set; }

        /// <summary>
        /// TransactionPriority Topic
        /// </summary>
        public string TransactionPriority { get; set; }

        public string TransactionPrioritySmartSort { get; set; }

    }

}
