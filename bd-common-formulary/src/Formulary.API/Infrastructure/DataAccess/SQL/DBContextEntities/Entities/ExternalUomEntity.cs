using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// ExternalUom Table
    /// </summary>
    public class ExternalUomEntity : BaseEntityFormulary
    {
        /// <summary>
        /// ExternalUOMKey primary key
        /// </summary>
        [Required]
        [Key]
        public Guid ExternalUOMKey { get; set; }

        /// <summary>
        /// ExternalSystemKey
        /// </summary>
        [Required]
        public Guid ExternalSystemKey { get; set; }

        /// <summary>
        /// TenantKey
        /// </summary>
        [Required]
        public Guid TenantKey { get; set; }

        /// <summary>
        /// UOMKey FK
        /// </summary>
        public Guid? UOMKey { get; set; }

        /// <summary>
        /// UOMCode
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string UOMCode { get; set; }

        /// <summary>
        /// SortValue
        /// </summary>
        public int? SortValue { get; set; }

        /// <summary>
        /// UseOnOutboundFlag
        /// </summary>
        [Required]
        public Boolean UseOnOutboundFlag { get; set; }

        /// <summary>
        /// ActiveFlag
        /// </summary>
        [Required]
        public Boolean ActiveFlag { get; set; }

        /// <summary>
        /// UomEntity relationship
        /// </summary>
        public virtual UomEntity UomEntity { get; set; }
    }
}