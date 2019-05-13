using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Item Setup
    /// </summary>
    public class ItemEntity : BaseEntityFormulary
    {
        /// <summary>
        /// Item Key
        /// </summary>
        [Key]
        [Required]
        public Guid ItemKey { get; set; }
        /// <summary>
        /// Tenant Key
        /// </summary>
        [Required]
        public Guid TenantKey { get; set; }
        /// <summary>
        /// External System Key
        /// </summary>
        public Guid ExternalSystemKey { get; set; }
        /// <summary>
        /// Facility Key
        /// </summary>
        public Guid FacilityKey { get; set; }
        /// <summary>
        /// Item ID
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string ItemID { get; set; }
        /// <summary>
        /// Alternate Item ID
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string AlternateItemID { get; set; }
        /// <summary>
        /// External System Delete UTC Datetime
        /// </summary>
        [Column(TypeName = "DateTimeOffset(7)")]
        public DateTimeOffset? ExternalSystemDeleteUTCDatetime { get; set; }
        /// <summary>
        /// Custom Field1 Text
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string CustomField1Text { get; set; }
        /// <summary>
        /// Custom Field2 Text
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string CustomField2Text { get; set; }
        /// <summary>
        /// Custom Field3 Text
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string CustomField3Text { get; set; }
        /// <summary>
        /// Custom Enterprise Item ID
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string EnterpriseItemID { get; set; }
        /// <summary>
        /// Custom Medication Item Flag 
        /// </summary>
        [Required]
        public bool MedicationItemFlag { get; set; }

        /// <summary>
        /// productIDRecommendedItems Navigation Property
        /// </summary>
        public virtual ICollection<ProductIDRecommendedItemEntity> ProductIDRecommendedItems { get; set; }
        /// <summary>
        /// productIdentifications navigation property
        /// </summary>
        public virtual ICollection<ProductIdentificationEntity> ProductIdentifications { get; set; }
        /// <summary>
        /// MedicationItems navigation property
        /// </summary>
        public virtual MedicationItemEntity MedicationItem { get; set; }
        /// <summary>
        /// FacilityItems navigation property
        /// </summary>
        public virtual ICollection<FacilityItemEntity> FacilityItems { get; set; }
        /// <summary>
        /// PreferredOrderingS navigation property
        /// </summary>
        public virtual ICollection<PreferredOrderingEntity> PreferredOrderings { get; set; }
    }
}