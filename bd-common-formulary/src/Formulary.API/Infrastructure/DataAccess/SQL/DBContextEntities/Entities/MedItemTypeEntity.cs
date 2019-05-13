using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Model Class for Medication item type
    /// </summary>
    public class MedItemTypeEntity : BaseEntityFormulary
    {
        /// <summary>Gets or sets the medication item type internal code.</summary>
        /// <value>The medication item type internal code.</value>
        [Required]
        [Column(TypeName = "varchar(10)")]
        [Key]
        public Guid MedItemTypeInternalCode { get; set; }
        /// <summary>Gets or sets the tenant key.</summary>
        /// <value>The tenant key.</value>
        //[Required]//To do.
        public Guid? TenantKey { get; set; }
        /// <summary>Gets or sets the description text.</summary>
        /// <value>The description text.</value>
        [Required]
        [MaxLength(50)]
        public Guid? DescriptionText { get; set; }
        /// <summary>Gets or sets the sort value.</summary>
        /// <value>The sort value.</value>
        public int SortValue { get; set; }

    }
}
