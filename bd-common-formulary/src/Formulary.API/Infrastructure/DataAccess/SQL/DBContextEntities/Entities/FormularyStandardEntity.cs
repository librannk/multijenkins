using Formulary.API.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Model Class for Formulary Standard
    /// </summary>

    [Table(DbConstants.TableNames.FormularyStandard, Schema = DbConstants.DefaultDboSchema)]
    public class FormularyStandard : BaseEntityFormulary
    {
        /// <summary>Gets or sets the formulary standard key.</summary>
        /// <value>The formulary standard key.</value>
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormularyStandardKey { get; set; }

        /// <summary>Gets or sets the tenant key.</summary>
        /// <value>The tenant key.</value>
        //[Required]//To do.
        public Guid? TenantKey { get; set; }

        /// <summary>Gets or sets the formulary standard name.</summary>
        /// <value>The formulary standard name.</value>
        [Required]
        [MaxLength(50)]
        public string FormularyStandardName { get; set; }

        /// <summary>Gets or sets the formulary standard description.</summary>
        /// <value>The formulary standard description.</value>
        [MaxLength(250)]
        public string FormularyStandardDescription { get; set; }

        /// <summary>Gets or sets the formulary standard ADM quantity rounding flag.</summary>
        /// <value>The formulary standard ADM quantity rounding flag.</value>
        public bool ADMQuantityRoundingFlag { get; set; }

        /// <summary>Gets or sets the formulary standard ADM Ignore Stockout flag.</summary>
        /// <value>The formulary standard ADM Ignore Stockout flag.</value>
        public bool ADMIgnoreStockoutFlag { get; set; }



        /// <summary>Gets or sets the formulary standard ADM Ignore Critical low flag.</summary>
        /// <value>The formulary standard ADM Ignore Critical low flag.</value>
        public bool ADMIgnoreCriticalLowFlag { get; set; }

        /// <summary>Gets or sets the formulary standard Require Lot Expiration During Restock flag.</summary>
        /// <value>The formulary standard Require Lot Expiration During Restock flag.</value>
        public bool RequireLotExpirationDuringRestockFlag { get; set; }

        /// <summary>Gets or sets the formulary standard Exclude From Inventory Report flag.</summary>
        /// <value>The formulary standard Exclude From Inventory Report flag.</value>
        public bool ExcludeFromInventoryReportFlag { get; set; }

        /// <summary>Gets or sets the formulary standard Enable Earliest Expiration Date flag.</summary>
        /// <value>The formulary standard Enable Earliest Expiration Date flag.</value>
        public bool EnableEarliestExpirationDateFlag { get; set; }

        /// <summary>Gets or sets the formulary standard Send To Packager flag.</summary>
        /// <value>The formulary standard Send To Packager flag.</value>
        public bool SendToPackagerFlag { get; set; }

        /// <summary>Gets or sets the formulary standard Active flag.</summary>
        /// <value>The formulary standard Active flag.</value>
        public bool ActiveFlag { get; set; }

        /// <summary>Gets or sets the formulary standard Cycle Count Interval Day.</summary>
        /// <value>The formulary standard Cycle Count Interval Day.</value>
        public int? CycleCountIntervalDay { get; set; }

    }
}