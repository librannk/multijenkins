using AutoMapper;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models;

namespace TransactionQueue.API.AutoMapper
{
    /// <summary>
    /// This class contains mappings.
    /// </summary>
    public class MapProfile : Profile
    {
        /// <summary>
        /// Public contructor.
        /// </summary>
        public MapProfile()
        {
            CreateMap<ExternalDependencies.Infrastructure.DBModel.Facility, Facility>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FacilityId));
            CreateMap<ExternalDependencies.Infrastructure.DBModel.FacilityStorageSpace, FacilityStorageSpace>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IsaId));
            CreateMap<ExternalDependencies.Infrastructure.DBModel.Formulary, Formulary>();
            CreateMap<Formulary, ExternalDependencies.Infrastructure.DBModel.Formulary>();
            CreateMap<ExternalDependencies.Infrastructure.DBModel.FacilityFormulary, FacilityFormulary>();
            CreateMap<ExternalDependencies.Infrastructure.DBModel.TransactionPriority, TransactionPriority>()
                .ForMember(dest => dest.TransactionPriorityId, opt => opt.MapFrom(src => src.TransactionPriorityId));
            CreateMap<TransactionPriority, ExternalDependencies.Infrastructure.DBModel.TransactionPriority>()
                .ForMember(dest => dest.TransactionPriorityId, opt => opt.MapFrom(src => src.TransactionPriorityId));
            CreateMap<TransactionQueueModel, Ingestion.Infrastructure.DBModel.TransactionQueue>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransactionQueueId));
            CreateMap<Ingestion.Infrastructure.DBModel.TransactionQueue, TransactionQueueModel>()
                .ForMember(dest => dest.TransactionQueueId, opt => opt.MapFrom(src => src.Id));
            CreateMap<TransactionQueueModel, Ingestion.Infrastructure.DBModel.TransactionQueueHistory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransactionQueueId));
            CreateMap<Ingestion.Infrastructure.DBModel.TransactionQueueHistory, TransactionQueueModel>()
                .ForMember(dest => dest.TransactionQueueId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ExternalDependencies.Infrastructure.DBModel.Destination, Destination>()
           .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => src.DestinationId));
        }
    }
}
