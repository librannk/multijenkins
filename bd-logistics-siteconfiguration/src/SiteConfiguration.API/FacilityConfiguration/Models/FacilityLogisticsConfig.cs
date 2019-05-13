using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.FacilityConfiguration.Models
{
    /// <summary>
    /// FacilityLogisticsConfig model entity
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FacilityLogisticsConfig : BaseEntity
    {
        [Key]
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", ErrorMessage = "Please enter a valid GUID")]
        public Guid FacilityKey { get; set; }
        
       
        public Guid? ManualRestockPrinterKey { get; set; }
        public Guid? ReceivingPrinterKey { get; set; }
        public Guid? ExceptionPrinterKey { get; set; }
        public Guid? BinLabelPrinterKey { get; set; }
        public string CostCenterCode { get; set; }

        /// <summary>
        /// Request Destination selection for returns
        /// </summary>
        public bool ReqRestockDestFlag { get; set; }
        public bool SubmitMedListInfoFlag { get; set; }
        public bool SubmitReturnCreditFlag { get; set; }
        public bool PrintLabelOnQtyChangeFlag { get; set; }
        public bool RequestRestockLotInfoFlag { get; set; }
        public bool ProcessInactiveAsExceptionFlag { get; set; }
        public bool ReceiveAndSendRemoteOrdersFlag { get; set; }
        public bool RequireHoldReasonFlag { get; set; }
        public bool NotifyOrderPickedFlag { get; set; }

        /// <summary>
        /// Send Pyxis Check Pick Info
        /// </summary>
        public bool SendCheckStationPickInfoFlag { get; set; }

        public bool SubmitGeneralLedgerInfoFlag { get; set; }
        public bool PrintCompositLabelFlag { get; set; }
        public bool SmartOrderRoutingFlag { get; set; }
        public bool PrepositionISAFlag { get; set; }
        public bool VerifyMultiDispenseFlag { get; set; }
        public bool ScanTranLabelFlag { get; set; }
        public bool VerifyQuantityFlag { get; set; }
        /// <summary>
        /// Require final scan validation of all components (Only enable final scan validation if all  components are in same ISA)
        /// </summary>
        public bool ScanVerifyFlag { get; set; }
        public bool ADMQuantityRoundingFlag { get; set; }
        public int? ADMDupeTimeDelay { get; set; }
        public bool ADMIgnoreStockOutFlag { get; set; }
        public bool ADMIgnoreCritLowFlag { get; set; }
        public bool PrepackBulkSuffixFlag { get; set; }

        /// <summary>
        /// Create Fast mover prepack orders when Distributor POs are created
        /// </summary>
        public bool CalcPrepackWithPOFlag { get; set; }
        /// <summary>
        /// Old Screen Mapped To : Auto create slow mover prepack orders
        /// </summary>
        public bool AutoCalcSlowPrepackFlag { get; set; }

        public bool ReturnsOnHoldDefFlag { get; set; }

        #region Not in use As of now, need to decide.
        public bool PackageShareQuantityRoundingFlag { get; set; }
        public bool UsePreferredOrderingFlag { get; set; }
        public bool SeparateOrderByISAFlag { get; set; }
        public bool AutoGenCycleCountScheduleFlag { get; set; }
        public bool EnableOldestExpirationDateFlag { get; set; }
        public bool AutoGenCycleCountFlag { get; set; }
        public bool CaptureLotExpirationFlag { get; set; }

        #endregion

    }
}
