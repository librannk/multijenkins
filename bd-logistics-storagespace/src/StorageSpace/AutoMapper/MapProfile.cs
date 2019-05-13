using AutoMapper;
using StorageSpace.API.Model;

namespace StorageSpace.API.AutoMapper
{   
    /// <summary>
    /// This class contains mappings
    /// </summary>
    public class MapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the MapProfile class.
        /// </summary>
        public MapProfile()
        {
            CreateMap<ItemStorageSpace,ISA>()
                .ForMember(dest => dest.IsaId, opt => opt.MapFrom(src => src.Id));

        }
    }
}
