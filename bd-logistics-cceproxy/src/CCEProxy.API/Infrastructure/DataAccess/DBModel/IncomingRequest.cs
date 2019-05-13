using CCEProxy.API.Entity;
using System.Collections.Generic;

namespace CCEProxy.API.Infrastructure.DataAccess.DBModel
{
    /// <summary> DB model containing detail of IncomingRequest</summary>
    public class IncomingRequest : Mongo.Entities.Entity
    {
        ///Status
        public string Status { get; set; }
        /// <summary>
        /// Status Message
        /// </summary>
        public string StatusMessage { get; set; }
        ///Priority
        public string Priority { get; set; }
        /// <summary>
        /// FacilityDetails
        /// </summary>
        public IncomingFacility Facility { get; set; }
        /// <summary>
        /// PatientDetails
        /// </summary>
        public Patient Patient { get; set; }
        /// <summary>
        /// OrderDetails
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// AduDetails
        /// </summary>
        public ADM ADM { get; set; }
        /// <summary>
        /// UserDef
        /// </summary>
        public UserDef UserDef { get; set; }
        ///Items
        public IEnumerable<Item> Items { get; set; }
    }
}
