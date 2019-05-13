using AutoMapper;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Model;
using Formulary.API.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.AutoMapper
{
    /// <summary>
    /// This class contains system item set up mappings.
    /// Implements the <see cref="Profile" />
    /// </summary>
    /// <seealso cref="Profile" />
    public class SystemItemSetUpMapProfile : Profile
    {
        /// <summary>
        /// FormularyMapProfile contructor.
        /// </summary>
        public SystemItemSetUpMapProfile()
        {
            //CreateMap<ProductIdentification, ProductIdentificationEntity>();
            //CreateMap<PreferredOrdering, PreferredOrderingEntity>();

            CreateMap<ItemEntity, MedicationItemList>()
                .ConstructUsing(entity => new MedicationItemList
                {
                    ItemKey = entity.ItemKey,
                    AliasItemID = entity.AlternateItemID,
                    ItemId = entity.ItemID
                })
                .ForMember(b => b.Description, opt => opt.MapFrom((src, dst) => src.MedicationItem?.Description))
                .ForMember(b => b.TradeName, opt => opt.MapFrom((src, dst) => src.ProductIdentifications.FirstOrDefault()?.TradeName))
                .ForMember(b => b.AltCode, opt => opt.MapFrom((src, dst) => src.ProductIdentifications.FirstOrDefault()?.AltCode))
                .ForMember(b => b.PreferredProductID, opt => opt.MapFrom((src, dst) => src.PreferredOrderings?.FirstOrDefault()?.ProductIDKey));
            CreateMap<ItemEntity, SystemItemSetupRequest>();
            CreateMap<SystemItemSetupRequest, SystemFormulary>();
        }

    }
}
