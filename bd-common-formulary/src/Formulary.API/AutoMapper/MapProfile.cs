using AutoMapper;
using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Model;
using System;

namespace Formulary.API.AutoMapper
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
            CreateMap<FormularyRequest, FormularyEntity>().ForMember(dest => dest.Id, opt => opt.Ignore()).BeforeMap((s, d) => s.CreatedBy = Admin.CreatedBy)
                .BeforeMap((s, d) => s.CreatedDate = DateTime.Now).BeforeMap((s, d) => s.LastModifiedBy = Admin.LastUpdatedBy)
                .BeforeMap((s, d) => s.LastModifiedDate = DateTime.Now);
        }
    }
}