using System;
using System.ComponentModel.DataAnnotations;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Model Class for Facility Medication Class Standard
    /// </summary>
    public class FacilityMedClassStandardEntity : BaseEntityFormulary
    {
        /// <summary>Gets or sets the Facility medication class standard key.</summary>
        /// <value>The Facility medication class standard account key.</value>
        [Required]
        [Key]
        public Guid FacilityMedClassStandardKey { get; set; }

        /// <summary>Gets or sets the tenant key.</summary>
        /// <value>The tenant key.</value>
        ///TODO.
        public Guid? TenantKey { get; set; }

        /// <summary>Gets or sets the facility key.</summary>
        /// <value>The facility key.</value>
        [Required]
        public Guid FacilityKey { get; set; }

        /// <summary>Gets or sets the formulary standard key.</summary>
        /// <value>The formulary standard key.</value>
        [Required]
        public Guid FormularyStandardKey { get; set; }

        /// <summary>Gets or sets the medication class key.</summary>
        /// <value>The medication class key.</value>
        [Required]
        public Guid MedClassKey { get; set; }

    }
}