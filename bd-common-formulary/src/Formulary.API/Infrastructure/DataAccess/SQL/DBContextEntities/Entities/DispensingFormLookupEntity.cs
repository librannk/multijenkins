using Formulary.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// DispensingFormEntity class
    /// </summary>
    [Table(DbConstants.TableNames.DispenseForm, Schema = DbConstants.DefaultDboSchema)]
    public class DispensingFormLookupEntity : BaseEntityFormulary
    {
        /// <summary>Gets or sets the dispense form lookup key.</summary>
        /// <value>The dispense form lookup key.</value>
        [Required]
        [Key]
        public Guid DispenseFormLookupKey { get; set; }

        /// <summary>Gets or sets the Tenant key.</summary>
        /// <value>The Tenant key.</value>
        public Guid TenantKey { get; set; }

        /// <summary>Gets or sets the dispense form.</summary>
        /// <value>The dispense form.</value>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string DispenseForm { get; set; }

        /// <summary>Gets or sets the dispense form Active flag.</summary>
        /// <value>The dispense form Active flag.</value>
        public bool ActiveFlag { get; set; }
        /// <summary>
        /// MedicationItems navigation property
        /// </summary>
        public virtual ICollection<MedicationItemEntity> MedicationItems { get; set; }

    }
}