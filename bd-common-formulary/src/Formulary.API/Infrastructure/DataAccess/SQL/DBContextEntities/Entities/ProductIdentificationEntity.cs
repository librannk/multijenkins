using Formulary.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// ProductIdentification
    /// </summary>
    [Table(DbConstants.TableNames.ProductID, Schema = DbConstants.DefaultDboSchema)]
    public class ProductIdentificationEntity : BaseEntityFormulary
    {
        /// <summary>
        /// ProductIdentificationKey
        /// </summary>
        [Key]
        public Guid ProductIDKey { get; set; }

        /// <summary>
        /// TenantKey
        /// </summary>
        [Required]
        public Guid TenantKey { get; set; }

        /// <summary>
        /// ItemKey
        /// </summary>
        [ForeignKey("Item")]
        [Required]
        public Guid ItemKey { get; set; }

        /// <summary>
        /// Manufacturer Key
        /// </summary>
        public Guid? ManufacturerID { get; set; }

        /// <summary>
        /// MedClass Key
        /// </summary>
        public Guid? MedClassKey { get; set; }

        ///// <summary>
        ///// AHFS Class
        ///// </summary>
        //[Column(TypeName = "nvarchar(15)")]
        //public int AHFSClassName { get; set; }
        /// <summary>
        /// Product ID
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string ProductID { get; set; }

        /// <summary>
        /// Alt Code
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string AltCode { get; set; }

        /// <summary>
        /// Generic Name
        /// </summary>
        [Column(TypeName = "nvarchar(225)")]
        public string GenericName { get; set; }

        /// <summary>
        /// Trade Name
        /// </summary>
        [Column(TypeName = "nvarchar(225)")]
        public string TradeName { get; set; }

        /// <summary>
        /// Package Size
        /// </summary>
        [Required]
        public int PackageSize { get; set; }

        ///// <summary>
        ///// Picture TODO {Need discussion}
        ///// </summary>
        //public byte?[] Picture { get; set; }
        /// <summary>
        /// Drug Flag
        /// </summary>
        [Column(TypeName = "bit")]
        public bool DrugFlag { get; set; }

        /// <summary>
        /// Active Flag
        /// </summary>
        [Column(TypeName = "bit")]
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// From ExternalSystem Flag
        /// </summary>
        [Column(TypeName = "bit")]
        public bool FromExternalSystemFlag { get; set; }

        /// <summary>
        /// Linked By User AccountKey
        /// </summary>
        public Guid? LinkedByUserAccountKey { get; set; }

        /// <summary>
        /// Verified By User AccountKey
        /// </summary>
        public Guid? VerifiedByUserAccountKey { get; set; }

        /// <summary>
        /// Linked DateTime
        /// </summary>
        [Column(TypeName = "DateTimeOffset(7)")]
        public DateTimeOffset LinkedDateTime { get; set; }

        /// <summary>
        /// Verified DateTime
        /// </summary>
        [Column(TypeName = "DateTimeOffset(7)")]
        public DateTimeOffset VerifiedDateTime { get; set; }

        /// <summary>
        /// Created By External System Name
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string CreatedByExternalSystemName { get; set; }

        /// <summary>
        /// Deleted By External System Name
        /// </summary>
        [MaxLength(50)]
        public string DeletedByExternalSystemName { get; set; }

        /// <summary>
        /// Strength
        /// </summary>
        [MaxLength(10)]
        public string Strength { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        [MaxLength(10)]
        public string Volume { get; set; }

        /// <summary>
        /// Total Volume
        /// </summary>
        [MaxLength(10)]
        public string TotalVolume { get; set; }

        /// <summary>
        /// productIDRecommendedItems navigation property
        /// </summary>
        public virtual ICollection<ProductIDRecommendedItemEntity> ProductIDRecommendedItems { get; set; }

        /// <summary>
        /// medicationItem navigation property
        /// </summary>
        public virtual MedicationItemEntity MedicationItem { get; set; }

        /// <summary>
        /// itemEntity navigation property
        /// </summary>
        [ForeignKey("ItemKey")]
        public virtual ItemEntity Item { get; set; }
        
        public virtual ICollection<PreferredOrderingEntity> PreferredOrderings { get; set; }                                   

    }
}
