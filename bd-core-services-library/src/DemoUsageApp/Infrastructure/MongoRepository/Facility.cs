using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUsageApp.Infrastructure.MongoRepository
{
    public class Facility : TestEntity
    {
        public string FacilityKey { get; set; }
        /// <summary>
        /// FacilityCode
        /// </summary>
        public string FacilityCode { get; set; }
        /// <summary>
        /// FacilityId
        /// </summary>
        public int FacilityId { get; set; }
    }
}
