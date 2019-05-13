using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteConfiguration.API.FacilityConfiguration.RequestResponseModel
{
    /// <summary>
    /// PostFacilityExtensions Entity
    /// </summary>
    public class FacilityLogisticsConfigurationExtension
    {
       
        [JsonIgnore]
        [Required(ErrorMessage = "Facility key is required.")]
        public Guid FacilityKey { get; set; }


        /// <summary>
        /// Gets or Sets AutoCalcSlowPrepackFlag
        /// </summary>
        [JsonProperty(PropertyName = "AutoCalcSlowPrepackFlag")]
        [Required]
        
        public bool AutoCalcSlowPrepackFlag { get; set; }

        /// <summary>
        /// Gets or Sets ADMQuantityRoundingFlag
        /// </summary>
        [JsonProperty(PropertyName = "ADMQuantityRoundingFlag")]
        [Required]
        
        public bool ADMQuantityRoundingFlag { get; set; }

        /// <summary>
        /// Gets or Sets ADMIgnoreStockOutFlag
        /// </summary>
        [JsonProperty(PropertyName = "ADMIgnoreStockOutFlag")]
        [Required]
        
        public bool ADMIgnoreStockOutFlag { get; set; }

        /// <summary>
        /// Gets or Sets ADMIgnoreCritLowFlag
        /// </summary>
        [JsonProperty(PropertyName = "ADMIgnoreCritLowFlag")]
        [Required]
        
        public bool ADMIgnoreCritLowFlag { get; set; }

        /// <summary>
        /// Gets or Sets CalcPrepackWithPOFlag
        /// </summary>
        [JsonProperty(PropertyName = "CalcPrepackWithPOFlag")]
        [Required]
        
        public bool CalcPrepackWithPOFlag { get; set; }

        /// <summary>
        /// Gets or Sets ReceiveAndSendRemoteOrdersFlag
        /// </summary>
        [JsonProperty(PropertyName = "ReceiveAndSendRemoteOrdersFlag")]
        [Required]
        
        public bool ReceiveAndSendRemoteOrdersFlag { get; set; }

        /// <summary>
        /// Gets or Sets SmartOrderRoutingFlag
        /// </summary>
        [JsonProperty(PropertyName = "SmartOrderRoutingFlag")]
        [Required]
        
        public bool SmartOrderRoutingFlag { get; set; }

        /// <summary>
        /// Gets or Sets SubmitMedListInfoFlag
        /// </summary>
        [JsonProperty(PropertyName = "SubmitMedListInfoFlag")]
        [Required]
        
        public bool SubmitMedListInfoFlag { get; set; }

        /// <summary>
        /// Gets or Sets ProcessInactiveAsExceptionFlag
        /// </summary>
        [JsonProperty(PropertyName = "ProcessInactiveAsExceptionFlag")]
        [Required]
        
        public bool ProcessInactiveAsExceptionFlag { get; set; }

        /// <summary>
        /// Gets or Sets SendCheckStationPickInfoFlag
        /// </summary>
        [JsonProperty(PropertyName = "SendCheckStationPickInfoFlag")]
        [Required]
        
        public bool SendCheckStationPickInfoFlag { get; set; }

        /// <summary>
        /// Gets or Sets PrintCompositLabelFlag
        /// </summary>
        [JsonProperty(PropertyName = "PrintCompositLabelFlag")]
        [Required]
        
        public bool PrintCompositLabelFlag { get; set; }

        /// <summary>
        /// Gets or Sets ScanVerify
        /// </summary>
        [JsonProperty(PropertyName = "ScanVerifyFlag")]
        [Required]
        
        public bool ScanVerifyFlag { get; set; }

        /// <summary>
        /// get or set RequireHoldReasonFlag key
        /// </summary>
        [JsonProperty(PropertyName = "RequireHoldReasonFlag")]
        [Required]
        
        public bool RequireHoldReasonFlag { get; set; }

        /// <summary>
        /// get or set ReturnsOnHoldDefFlag
        /// </summary>
        [JsonProperty(PropertyName = "ReturnsOnHoldDefFlag")]
        [Required]
        
        public bool ReturnsOnHoldDefFlag { get; set; }
    }
}
