using DemoUsageApp.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoUsageApp.Infrastructure;

namespace DemoUsageApp
{
    /// <summary>
    /// FacilityRepository class implements the all member of ICustomerRepository of type Customer.
    /// </summary> 
    public class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
        /// <summary>
        /// Constructor is used to initialize this class
        /// </summary>
        /// <param name="context"></param>
        public FacilityRepository(FacilityDBContext context) : base(context)
        {
            
        }
    }
}
