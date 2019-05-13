using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// FacilityItemEntity table
    /// </summary>
    public class FacilityItemEntity : BaseEntityFormulary
    {
        /// <summary>
        /// FacilityItemKey Key
        /// </summary>
        [Required]
        [Key]
        public Guid FacilityItemKey { get; set; }

        /// <summary>
        /// TenantKey 
        /// </summary>
        [Required]
        public Guid TenantKey { get; set; }

        /// <summary>
        /// FacilityKey
        /// </summary>
        [Required]
        public Guid FacilityKey { get; set; }

        /// <summary>
        /// ItemKey FK
        /// </summary>
        [Required]
        public Guid ItemKey { get; set; }

        /// <summary>
        /// UOMKey FK
        /// </summary>
        [Required]
        public Guid UOMKey { get; set; }

        /// <summary>
        /// RestockRoundingFactor
        /// </summary>
        [Column(TypeName = "decimal(3,1)")]
        public decimal? RestockRoundingFactor { get; set; }

        /// <summary>
        /// PrepackFlag
        /// </summary>
        [Required]
        public bool PrepackFlag { get; set; }

        /// <summary>
        /// FastmoverFlag
        /// </summary>
        [Required]
        public bool FastmoverFlag { get; set; }

        /// <summary>
        /// ForPackagerFlag
        /// </summary>
        [Required]
        public bool ForPackagerFlag { get; set; }

        /// <summary>
        /// ADMQuantityRoundingFlag
        /// </summary>
        [Required]
        public bool ADMQuantityRoundingFlag { get; set; }

        /// <summary>
        /// ADMIgnoreStockOutFlag
        /// </summary>
        [Required]
        public bool ADMIgnoreStockOutFlag { get; set; }

        /// <summary>
        /// ADMIgnoreCritLowFlag
        /// </summary>
        [Required]
        public bool ADMIgnoreCritLowFlag { get; set; }

        /// <summary>
        /// ActiveFlag
        /// </summary>
        [Required]
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// EnableOldestExpirationDateFlag
        /// </summary>
        [Required]
        public bool EnableOldestExpirationDateFlag { get; set; }

        /// <summary>
        /// PackageShareQuantityRoundingFlag
        /// </summary>
        [Required]
        public bool PackageShareQuantityRoundingFlag { get; set; }

        /// <summary>
        /// RequestRestockLotInfoFlag
        /// </summary>
        [Required]
        public bool RequestRestockLotInfoFlag { get; set; }

        /// <summary>
        /// RequiresSetupFlag
        /// </summary>
        [Required]
        public bool RequiresSetupFlag { get; set; }

        /// <summary>
        /// NeedReviewFlag
        /// </summary>
        [Required]
        public bool NeedReviewFlag { get; set; }

        /// <summary>
        /// ConsignmentFlag
        /// </summary>
        [Required]
        public bool ConsignmentFlag { get; set; }

        /// <summary>
        /// ExcludeFromInventoryReportFlag
        /// </summary>
        [Required]
        public bool ExcludeFromInventoryReportFlag { get; set; }

        /// <summary>
        /// CostAmount
        /// </summary>
        public int? CostAmount { get; set; }

        /// <summary>
        /// AverageWholesalePriceAmount
        /// </summary>
        [Column(TypeName = "decimal(14,4)")]
        public decimal? AverageWholesalePriceAmount { get; set; }

        /// <summary>
        /// AverageSalesPriceAmount
        /// </summary>
        [Column(TypeName = "decimal(14,4)")]
        public decimal? AverageSalesPriceAmount { get; set; }

        /// <summary>
        /// CycleCountIntervalDay
        /// </summary>
        [Column(TypeName = "decimal(14,4)")]
        public decimal? CycleCountIntervalDay { get; set; }

        /// <summary>
        /// BulkItemKey
        /// </summary>
        public Guid? BulkItemKey { get; set; }

        /// <summary>
        /// OldestExpirationDateTime
        /// </summary>
        [Column(TypeName = "DateTimeOffset(7)")]
        public DateTimeOffset? OldestExpirationDateTime { get; set; }

        /// <summary>
        /// ConversionBarCodeValue
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string ConversionBarCodeValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(14,4)")]
        public Decimal? PrepackConversionFactor { get; set; }

        /// <summary>
        /// SpecialInstruction
        /// </summary>
        [Column(TypeName = "nvarchar(300)")]
        public string SpecialInstruction { get; set; }

        /// <summary>
        /// UomEntity relationship
        /// </summary>
        public virtual UomEntity UomEntity { get; set; }

        /// <summary>
        /// ItemEntity relationship
        /// </summary>
        public virtual ItemEntity ItemEntity { get; set; }
    }
}