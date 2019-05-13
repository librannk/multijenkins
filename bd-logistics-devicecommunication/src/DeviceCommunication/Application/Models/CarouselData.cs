using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Logistics.Services.DeviceCommunicationAPI.Application.Common.Constants;

namespace Logistics.Services.DeviceCommunication.API.Application.Models
{
    /// <summary>
    /// CaraouselData
    /// </summary>
    public class CarouselData : IValidatableObject
    {
        /// <summary>
        /// ISAId
        /// </summary>
        [Required]
        public string ISAId { get; set; }

        /// <summary>
        /// IPAddress
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.IPNull)]
        public string IPAddress { get; set; }
        
        /// <summary>
        /// Port
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.PortNull)]
        [Range(0,65535)]
        public ushort Port { get; set; }
        
        /// <summary>
        /// isOnlineFlag
        /// </summary>
        public bool isOnlineFlag { get; set; } =  false;

        /// <summary>
        /// Display
        /// </summary>
        public Display Display { get; set; }

        /// <summary>
        ///  Custom Validations
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!System.Net.IPAddress.TryParse(IPAddress, out  System.Net.IPAddress address))
            {
                yield return new ValidationResult(
                ErrorMessage.InvalidIP);
            }
        }
    }
}
