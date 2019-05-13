using Formulary.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Dispensing Unit Entity
    /// </summary>
    [Table(DbConstants.TableNames.DispenseUnit, Schema = DbConstants.DefaultDboSchema)]
    public class DispensingUnitLookupEntity : BaseEntityFormulary
    {
        /// <summary>Gets or sets the dispense unit lookup key.</summary>
        /// <value>The dispense unit lookup key.</value>
        [Required]
        [Key]
        public Guid DispenseUnitLookupKey { get; set; }

        /// <summary>Gets or sets the Tenant key.</summary>
        /// <value>The Tenant key.</value>
        public Guid TenantKey { get; set; }

        /// <summary>Gets or sets the dispense unit.</summary>
        /// <value>The dispense unit.</value>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string DispenseUnit { get; set; }

        /// <summary>Gets or sets the dispense unit Active flag.</summary>
        /// <value>The dispense unit Active flag.</value>
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// MedicationItems navigation property
        /// </summary>
        public virtual ICollection<MedicationItemEntity> MedicationItems { get; set; }
    }
}