using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCEProxy.API.Entity
{
    /// <summary>
    /// Public class Order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// To hold the OrderNo
        /// </summary>
        public string OrderNo { get; set; }
      
        /// <summary>
        /// To hold the CopeOrderNo
        /// </summary>
        public string CopeOrderNo { get; set; }
       
        /// <summary>
        /// To hold the OrderControlId
        /// </summary>
        public string OrderControlId { get; set; }
      
        /// <summary>
        /// To hold the IsStatOrder
        /// </summary>
        public bool IsStatOrder { get; set; }
   
        /// <summary>
        /// To hold the OrderingPriority
        /// </summary>
        public string OrderingPriority { get; set; }
   
        /// <summary>
        /// To hold the OrderingDueTime
        /// </summary>
        public string OrderingDueTime { get; set; }
      
        /// <summary>
        /// To hold the OrderingDrInstructions
        /// </summary>
        public string OrderingDrInstructions { get; set; }
    }
}
