using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// RecommendedItemResultEntity
    /// </summary>
    public class RecommendedItemResultEntity : BaseEntityFormulary
    {
        /// <summary>
        /// Primary Key RecommendedItemResultInternalCode
        /// </summary>
        [Key]
        [Column(TypeName = "varchar(20)")]
        public string RecommendedItemResultInternalCode { get; set; }
        /// <summary>
        /// TenantKey
        /// </summary>
        public Guid TenantKey { get; set; }
        /// <summary>
        /// DescriptionText
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string DescriptionText { get; set; }
        /// <summary>
        /// SortValue
        /// </summary>
        public int SortValue { get; set; }

        /// <summary>
        /// ProductIDRecommendedItems
        /// </summary>
        public virtual ICollection<ProductIDRecommendedItemEntity> ProductIDRecommendedItems { get; set; }

    }
}
