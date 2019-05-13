using DemoUsageApp.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUsageApp
{
    /// <summary>
    /// IFacilityRepository interface represent the all member of IRepository of type Customer.
    /// IFacilityRepository provide the extensibility for new operation other than IRepository.
    /// </summary> 
    public interface IFacilityRepository : IRepository<Facility>
    {

    }
}
