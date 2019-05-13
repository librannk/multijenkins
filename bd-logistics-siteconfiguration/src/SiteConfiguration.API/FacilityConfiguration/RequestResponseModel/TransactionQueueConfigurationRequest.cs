using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteConfiguration.API.FacilityConfiguration.RequestResponseModel
{
    /// <summary>
    /// TransactionQueueConfigurationRequest model
    /// </summary>
    public class TransactionQueueConfigurationRequest
    {
        [JsonIgnore]
        [Required(ErrorMessage = "Facility key is required.")]
        public Guid FacilityKey { get; set; }
        
        [Required]
        public bool VerifyQuantityFlag { get; set; }

        [Required]        
        public bool PrepositionISAFlag { get; set; }
        [Required]        
        public bool ReqRestockDestFlag { get; set; }
        [Required]        
        public bool RequestRestockLotInfoFlag { get; set; }
        [Required]        
        public bool NotifyOrderPickedFlag { get; set; }
        [Required]        
        public bool SubmitGeneralLedgerInfoFlag { get; set; }
        [Required]        
        public bool SubmitReturnCreditFlag { get; set; }
        [Required]        
        public bool PrintLabelOnQtyChangeFlag { get; set; }
        [Required]        
        public bool PrepackBulkSuffixFlag { get; set; }
        [Required]
        [Range(0, int.MaxValue,ErrorMessage = "Enter positive value for duplicate time delay.")]
        public int? ADMDupeTimeDelay { get; set; }


        #region PrinterInfo
        public Guid? ManualRestockPrinterKey { get; set; }
        public Guid? ReceivingPrinterKey { get; set; }
        public Guid? ExceptionPrinterKey { get; set; }
        public Guid? BinLabelPrinterKey { get; set; }
        #endregion

    }
}
