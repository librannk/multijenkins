using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SiteConfiguration.API.Common.Constants;
using SiteConfiguration.API.Printers.Models.BuisnessContract;
using Data = SiteConfiguration.API.Printers.Models.Data;
using SiteConfiguration.API.Schedule.Models;

namespace SiteConfiguration.API.AutoMapper
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
            CreateMap<ScheduleRequest, ScheduleTiming>()
                .ForMember(dest => dest.ScheduleTimingName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartMinutes, opt => opt.MapFrom(src => (int)Math.Floor(src.StartTime.TotalMinutes)))
                .ForMember(dest => dest.EndMinutes, opt => opt.MapFrom(src => (int)Math.Floor(src.EndTime.TotalMinutes)))
                .ForMember(dest => dest.MondayFlag, opt => opt.MapFrom(src => (src.ScheduleDays.Contains(ScheduleDays.Monday))))
                .ForMember(dest => dest.TuesdayFlag, opt => opt.MapFrom(src => (src.ScheduleDays.Contains(ScheduleDays.Tuesday))))
                .ForMember(dest => dest.WednesdayFlag, opt => opt.MapFrom(src => (src.ScheduleDays.Contains(ScheduleDays.Wednesday))))
                .ForMember(dest => dest.ThursdayFlag, opt => opt.MapFrom(src => (src.ScheduleDays.Contains(ScheduleDays.Thursday))))
                .ForMember(dest => dest.FridayFlag, opt => opt.MapFrom(src => (src.ScheduleDays.Contains(ScheduleDays.Friday))))
                .ForMember(dest => dest.SaturdayFlag, opt => opt.MapFrom(src => (src.ScheduleDays.Contains(ScheduleDays.Saturday))))
                .ForMember(dest => dest.SundayFlag, opt => opt.MapFrom(src => (src.ScheduleDays.Contains(ScheduleDays.Sunday))))


            .BeforeMap((s, d) =>
                {
                    d.CreatedByActorKey = Guid.NewGuid();
                    d.CreatedDateTime = DateTimeOffset.Now;
                    d.LastModifiedByActorKey = Guid.NewGuid();
                    d.LastModifiedUTCDateTime = DateTime.UtcNow;
                });



            CreateMap<ScheduleTiming, Schedule.Models.ScheduleResponse>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ScheduleTimingKey))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ScheduleTimingName))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.StartMinutes)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.EndMinutes)))
            .ForMember(dest => dest.ScheduleDays, opt => opt.MapFrom(src => MapScheduleDays(src)));

            CreateMap<ScheduleTiming, ScheduleResponseByKey>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ScheduleTimingKey))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ScheduleTimingName))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.StartMinutes)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.EndMinutes)))
                .ForMember(dest => dest.ScheduleDays, opt => opt.MapFrom(src => MapScheduleDays(src)));

            CreateMap<Data.PrinterModel, PrinterModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PrinterModelKey))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DescriptionText));

            CreateMap<Data.Printer, Printer>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.PrinterKey))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ActiveFlag))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PrinterName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionText));

            CreateMap<Data.Printer, PrinterResponse>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.PrinterKey))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ActiveFlag))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PrinterName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionText))
                .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.IPAddressText))
                .ForMember(dest => dest.LabelBarcode, opt => opt.MapFrom(src => src.LabelBarcodeText))
                .ForMember(dest => dest.MacAddress, opt => opt.MapFrom(src => src.MacAddressText))
                .ForMember(dest => dest.IpPort, opt => opt.MapFrom(src => src.IPPortNumber))
                .ForMember(dest => dest.PrintableAreaHeight, opt => opt.MapFrom(src => src.PrintableAreaHeightValue))
                .ForMember(dest => dest.PrintableAreaWidth, opt => opt.MapFrom(src => src.PrintableAreaWidthValue));

            CreateMap<PrinterRequest, Data.Printer>()
            .ForMember(dest => dest.DescriptionText, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.IPAddressText, opt => opt.MapFrom(src => src.IpAddress))
            .ForMember(dest => dest.IPPortNumber, opt => opt.MapFrom(src => src.IpPort))
            .ForMember(dest => dest.MacAddressText, opt => opt.MapFrom(src => src.MacAddress))
            .ForMember(dest => dest.PrintableAreaHeightValue, opt => opt.MapFrom(src => src.PrintableAreaHeight))
            .ForMember(dest => dest.PrintableAreaWidthValue, opt => opt.MapFrom(src => src.PrintableAreaWidth))
            .ForMember(dest => dest.LabelBarcodeText, opt => opt.MapFrom(src => src.LabelBarcode))
            .BeforeMap((s, d) =>
            {
                d.CreatedByActorKey = Guid.NewGuid();
                d.CreatedDateTime = DateTimeOffset.Now;
                d.LastModifiedByActorKey = Guid.NewGuid();
                d.LastModifiedUTCDateTime = DateTime.UtcNow;
            });

        }

        private List<string> MapScheduleDays(ScheduleTiming scheduleTiming)
        {
            var scheduleDays = new List<string>();
            if (scheduleTiming.MondayFlag)
                scheduleDays.Add(Enum.GetName(typeof(ScheduleDays), ScheduleDays.Monday));
            if (scheduleTiming.TuesdayFlag)
                scheduleDays.Add(Enum.GetName(typeof(ScheduleDays), ScheduleDays.Tuesday));
            if (scheduleTiming.WednesdayFlag)
                scheduleDays.Add(Enum.GetName(typeof(ScheduleDays), ScheduleDays.Wednesday));
            if (scheduleTiming.ThursdayFlag)
                scheduleDays.Add(Enum.GetName(typeof(ScheduleDays), ScheduleDays.Thursday));
            if (scheduleTiming.FridayFlag)
                scheduleDays.Add(Enum.GetName(typeof(ScheduleDays), ScheduleDays.Friday));
            if (scheduleTiming.SaturdayFlag)
                scheduleDays.Add(Enum.GetName(typeof(ScheduleDays), ScheduleDays.Saturday));
            if (scheduleTiming.SundayFlag)
                scheduleDays.Add(Enum.GetName(typeof(ScheduleDays), ScheduleDays.Sunday));

            return scheduleDays;
        }
    }
}
