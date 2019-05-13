using AutoMapper;

namespace Facility.API.Automapper
{
    /// <summary>
    /// Helper Class for Automapper.
    /// </summary>
    public static class AutomapperRegistrations
    {

        /// <summary>
        /// Configures the facility mapping.
        /// </summary>
        public static void ConfigureFacilityMapping()
        {
            Mapper.Initialize(mapperConfiguration =>
            {
                mapperConfiguration.AddProfile<FacilityMapProfile>();
            });
        }
    }
}
