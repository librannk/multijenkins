    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Formulary.API.AutoMapper
{
    public static class AutomapperRegistrations
    {
        /// <summary>
        /// Configures the facility mapping.
        /// </summary>
        public static void ConfigureFacilityMapping()
        {
            Mapper.Initialize(mapperConfiguration =>
            {
                mapperConfiguration.AddProfile<SystemItemSetUpMapProfile>();
            });
        }
    }
}
