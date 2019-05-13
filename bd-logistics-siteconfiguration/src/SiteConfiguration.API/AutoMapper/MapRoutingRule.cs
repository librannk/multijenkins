using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SiteConfiguration.API.Common.Constants;
using SiteConfiguration.API.RoutingRules.Models;
using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using SiteConfiguration.API.Schedule.Models;

namespace SiteConfiguration.API.AutoMapper
{
    /// <summary>
    /// This class contains mappings
    /// </summary>
    public class MapRoutingRule : Profile
    {
        /// <summary>
        /// Initializes a new instance of the MapProfile class.
        /// </summary>
        public MapRoutingRule()
        {
            CreateMap<RoutingRule, RoutingRulesById>()
                .ForMember(dest => dest.RoutingRuleKey, opt => opt.MapFrom(src => src.RoutingRuleKey))
                .ForMember(dest => dest.RoutingRuleName, opt => opt.MapFrom(src => src.RoutingRuleName))
                .ForMember(dest => dest.SearchCriteriaGranularityLevel, opt => opt.MapFrom(src => src.SearchCriteriaGranularityLevel))
                .ForMember(dest => dest.RoutingRuleDestinations, opt => opt.MapFrom(src => src.RoutingRuleDestination.Select(x => x.RoutingRuleDestinationKey).ToList()))
                .ForMember(dest => dest.RoutingRuleScheduleTimings, opt => opt.MapFrom(src => src.RoutingRuleScheduleTiming.Select(x => x.ScheduleTimingKey).ToList()))
                .ForMember(dest => dest.RoutingRuleTranPriorities, opt => opt.MapFrom(src => src.RoutingRuleTranPriority.Select(x => x.RoutingRuleTranPriorityKey).ToList()));

          

        }
    }
}
