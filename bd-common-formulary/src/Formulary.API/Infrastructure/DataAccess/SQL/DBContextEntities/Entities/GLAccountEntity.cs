using Formulary.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Entity Class for GL Account
    /// </summary>
    [Table(DbConstants.TableNames.Gla, Schema = DbConstants.DefaultDboSchema)]
    public class GLAccountEntity : BaseEntityFormulary
    {
        /// <summary>Gets or sets the gl account key.</summary>
        /// <value>The gl account key.</value>
        [Required]
        [Key]
        public Guid GLAccountKey { get; set; }
        /// <summary>Gets or sets the tenant key.</summary>
        /// <value>The tenant key.</value>
        //[Required]//Commented for time being.
        public Guid TenantKey { get; set; }
        /// <summary>Gets or sets the GL account code.</summary>
        /// <value>The GL account code.</value>
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string AccountCode { get; set; }
        /// <summary>Gets or sets the GL account description.</summary>
        /// <value>The GL account description.</value>
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
        /// <summary>Gets or sets the GL account active flag.</summary>
        /// <value>The GL account active flag.</value>
        public bool ActiveFlag { get; set; }
       
        ///// <summary>
        ///// MedicationItemEntitys navigation property
        ///// </summary>
        public virtual ICollection<MedicationItemEntity> MedicationItems { get; set; }
    }
}
