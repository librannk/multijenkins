using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Unit of measure entity
    /// </summary>
    public class UomEntity : BaseEntityFormulary
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Key]
        public Guid UomKey { get; set; }
        /// <summary>
        /// Base Uom
        /// </summary>
        public Guid? BaseUomKey { get; set; }
        /// <summary>
        /// Tenant Key
        /// </summary>
        [Required]
        public Guid TenantKey { get; set; }
        /// <summary>
        /// Display code
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string DisplayCode { get; set; }
        /// <summary>
        /// Internal code
        /// </summary>
        [Column(TypeName = "varchar(10)")]
        public string InternalCode { get; set; }
        /// <summary>
        /// Description code
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string DescriptionText { get; set; }
        /// <summary>
        /// Conversion Amount
        /// </summary>
        [Column(TypeName = "decimal(28,14)")]
        public decimal? ConversionAmount { get; set; }
        /// <summary>
        /// Streangth flag
        /// </summary>
        [Required]
        public bool StrengthFlag { get; set; }
        /// <summary>
        /// Volume flag
        /// </summary>
        [Required]
        public bool VolumeFlag { get; set; }
        /// <summary>
        /// Predefined flag
        /// </summary>
        [Required]
        public bool PredefinedFlag { get; set; }
        /// <summary>
        /// Active flag
        /// </summary>
        [Required]
        public bool ActiveFlag { get; set; }
        /// <summary>
        /// FacilityItemEntities relationship
        /// </summary>
        public virtual ICollection<FacilityItemEntity> FacilityItems { get; set; }
        /// <summary>
        /// ExternalUomEntity relationship
        /// </summary>
        public virtual ICollection<ExternalUomEntity> ExternalUoms { get; set; }
        /// <summary>
        /// Uom self relationship
        /// </summary>
        public virtual ICollection<UomEntity> Uoms { get; set; }
        /// <summary>
        /// Uom self relationship
        /// </summary>
        public virtual UomEntity Uom { get; set; }



    }
}
