using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Model Class for Medication Class 
    /// </summary>
    public class MedClassEntity : BaseEntityFormulary
    {
        /// <summary>Gets or sets the Facility medication class standard key.</summary>
        /// <value>The medication class key.</value>
        [Required]
        [Key]
        public Guid MedClassKey { get; set; }
        /// <summary>Gets or sets the tenant key.</summary>
        /// <value>The tenant key.</value>
        //[Required]//To do.
        public Guid? TenantKey { get; set; }
        /// <summary>Gets or sets the External system key.</summary>
        /// <value>The External system key.</value>
        [Required]
        public Guid? ExternalSystemKey { get; set; }
        /// <summary>Gets or sets the medication class code.</summary>
        /// <value>The medication class code.</value>
        [MaxLength(20)]
        [Required]
        public string MedClassCode { get; set; }
        /// <summary>Gets or sets the description text.</summary>
        /// <value>The medication class code.</value>
        [MaxLength(100)]
        public string DescriptionText { get; set; }
        /// <summary>Gets or sets the medication class active flag.</summary>
        /// <value>The medication class active flag.</value>
        public bool ActiveFlag { get; set; }
        /// <summary>Gets or sets the medication class system flag.</summary>
        /// <value>The medication class system flag.</value>
        public bool SystemFlag { get; set; }

        public virtual ICollection<MedicationItemEntity> MedicationItems { get; set; }
    }
}
