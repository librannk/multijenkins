using AutoMapper;
using static CCEProxy.API.Common.Constants.Constants;
using CCEProxy.API.Infrastructure.DataAccess.DBModel;
using CCEProxy.API.IntegrationEvents.Events;
using System;

namespace CCEProxy.API.AutoMapper
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
            CreateMap<Facility, Entity.Facility>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FacilityId));

            CreateMap<TransactionPriority, Entity.TransactionPriority>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransactionPriorityId));

            CreateMap<Entity.IncomingRequest, IncomingRequest>().BeforeMap((s, d) => {
                d.CreatedBy = User.CreatedBy;
                d.CreatedDate = DateTime.Now;
                d.ModifiedBy = User.ModifiedBy;
                d.ModifiedDate = DateTime.Now;
                });

            CreateMap<Entity.Facility, Facility>().BeforeMap((s, d) => {
                d.CreatedBy = User.CreatedBy;
                d.CreatedDate = DateTime.Now;
                d.ModifiedBy = User.ModifiedBy;
                d.ModifiedDate = DateTime.Now;
            }).ForMember(dest => dest.FacilityId, opt => opt.MapFrom(src => src.Id)).ForMember(dest => dest.Id, dest => dest.Ignore());

            CreateMap<Entity.TransactionPriority, TransactionPriority>().BeforeMap((s, d) => {
                d.CreatedBy = User.CreatedBy;
                d.CreatedDate = DateTime.Now;
                d.ModifiedBy = User.ModifiedBy;
                d.ModifiedDate = DateTime.Now;
            }).ForMember(dest => dest.TransactionPriorityId, opt => opt.MapFrom(src => src.Id)).ForMember(dest => dest.Id, dest => dest.Ignore());

            CreateMap<FacilityAddedIntegrationEvent, Entity.Facility>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FacilityId));
            CreateMap<TransactionPriorityAddedIntegrationEvent, Entity.TransactionPriority>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransactionPriorityId));
        }
    }
}
