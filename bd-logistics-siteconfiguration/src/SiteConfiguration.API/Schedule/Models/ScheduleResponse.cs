using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.Schedule.Models
{
    public class ScheduleResponse
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> ScheduleDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
