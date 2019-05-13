using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// ProductIDRecommendedItemEntity
    /// </summary>
    public class ProductIDRecommendedItemEntity : BaseEntityFormulary
    {
        /// <summary>
        /// Primary Key ProductIDRecommendedItemKey
        /// </summary>
        [Key]
        public Guid ProductIDRecommendedItemKey { get; set; }
        /// <summary>
        /// Resulting ProductIdentification Key
        /// </summary>
        [Required]
        public Guid ResultingProductIdentificationKey { get; set; }
        /// <summary>
        /// Recommended ItemKey
        /// </summary>
        [Required]
        public Guid RecommendedItemKey { get; set; }
        /// <summary>
        /// Recommended Medication ItemKey
        /// </summary>
        [Required]
        public Guid RecommendedMedicationItemKey { get; set; }
        /// <summary>
        /// Recommended ItemResult InternalCode
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string RecommendedItemResultInternalCode { get; set; }
        /// <summary>
        /// User AccountKey
        /// </summary>
        [Required]
        public Guid UserAccountKey { get; set; }
        /// <summary>
        /// Sequence Number
        /// </summary>
        [Required]
        public int SequenceNumber { get; set; }
        /// <summary>
        /// ProductID
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ProductID { get; set; }
        /// <summary>
        /// Recommandation Followed Flag
        /// </summary>
        [Required]
        public bool RecommandationFollowedFlag { get; set; }

        /// <summary>
        /// ProductIdentification
        /// </summary>
        [ForeignKey("ResultingProductIdentificationKey")]
        public virtual ProductIdentificationEntity ProductIdentification { get; set; }
        /// <summary>
        /// RecommendedItemResult
        /// </summary>
        [ForeignKey("RecommendedItemResultInternalCode")]
        public virtual RecommendedItemResultEntity RecommendedItemResult { get; set; }
        /// <summary>
        /// Item
        /// </summary>
        [ForeignKey("RecommendedItemKey")]
        public virtual ItemEntity Item { get; set; }
        /// <summary>
        /// MedicationItem
        /// </summary>
        [ForeignKey("RecommendedMedicationItemKey")]
        public virtual MedicationItemEntity MedicationItem { get; set; }


    }
}
