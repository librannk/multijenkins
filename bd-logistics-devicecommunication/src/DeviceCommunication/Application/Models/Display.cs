using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Logistics.Services.DeviceCommunicationAPI.Application.Common.Constants;

namespace Logistics.Services.DeviceCommunication.API.Application.Models
{
    /// <summary>
    /// Display
    /// </summary>
    public class Display : IValidatableObject
    {
        /// <summary> DisplayAttachedFlag </summary>
        [Required]
        public bool DisplayAttachedFlag { get; set; } = false;

        /// <summary> DisplayIPAddress </summary>
        public string DisplayIPAddress { get; set; }

        /// <summary> DisplayPort </summary>
        [Range(0, 65535)]
        public ushort DisplayPort { get; set; }

        /// <summary> isOnlineFlag </summary>
        public bool isOnlineFlag { get; set; } = false;

        /// <summary>
        /// Custom Validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DisplayAttachedFlag && !System.Net.IPAddress.TryParse(DisplayIPAddress, out System.Net.IPAddress address))
            {
                yield return new ValidationResult(
                ErrorMessage.InvalidDisplayIP);
            }

            if(DisplayAttachedFlag && DisplayPort < 0 || DisplayPort > 65535 )
            {
                yield return new ValidationResult(
                ErrorMessage.InvalidDisplayPort);
            }
        }
    }
}
