using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SiteConfiguration.API.Schedule.Models
{
    public class ScheduleRequest : IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$",ErrorMessage = "Name Should Be AlphaNumeric")]
        public string Name { get; set; }
        //[EnumDataType(typeof(ScheduleDays))]
        public IEnumerable<ScheduleDays> ScheduleDays { get; set; }
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "Incorrect format of Start Time")]
        public TimeSpan StartTime { get; set; }
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "Incorrect format of End Time")]
        public TimeSpan EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TimeSpan.Compare(EndTime, StartTime) < 1)
            {
                yield return new ValidationResult("End time should be greater then start time", new[] { "EndTime" });
            }

            if (!ScheduleDays.Any())
            {
                yield return new ValidationResult("Atleast one weekday should be selected", new[] { "ScheduleDays" });
            }
        }
    }
}
