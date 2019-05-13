using AutoMapper;
using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Facility.API.Model;
using System;

namespace Facility.API.Automapper
{
    /// <summary>
    /// Map profile for facility models.
    /// Implements the <see cref="Profile" />
    /// </summary>
    /// <seealso cref="Profile" />
    public class FacilityMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityMapProfile"/> class.
        /// </summary>
        public FacilityMapProfile()
        {
            CreateMap<FacilityEntity, FacilityList>()
                .ConstructUsing(b => new FacilityList()
                {
                    FacilityCode = b.FacilityCode,
                    FacilityKey = b.FacilityKey,
                    Active = b.ActiveFlag,
                    Description = b.DescriptionText,
                    FacilityName = b.FacilityName
                });
            CreateMap<NewFacilityRequest, FacilityEntity>()
                .ConstructUsing(b => new FacilityEntity() { FacilityKey = Guid.NewGuid(), ActiveFlag = true });
            CreateMap<FacilityEntity, Model.Facility>()
                .ReverseMap();

            CreateMap<UpdateFacilityRequest, FacilityEntity>();
        }
    }
}
