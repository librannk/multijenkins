using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.RoutingRules.Models;

namespace SiteConfiguration.API.Schedule.Models
{
    public class ScheduleTiming : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ScheduleTimingKey { get; set; }
        public Guid FacilityKey { get; set; }
        public string ScheduleTimingName { get; set; }
        public bool MondayFlag { get; set; }
        public bool TuesdayFlag { get; set; }
        public bool WednesdayFlag { get; set; }
        public bool ThursdayFlag { get; set; }
        public bool FridayFlag { get; set; }
        public bool SaturdayFlag { get; set; }
        public bool SundayFlag { get; set; }
        public int StartMinutes { get; set; }
        public int EndMinutes { get; set; }

        public virtual RoutingRuleScheduleTiming RoutingRuleScheduleTiming { get; set; }
    }
}
