using Facility.API.Constants;
using Facility.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// It's an entity,which is going to manipulate in DB with its properties as fields.
    /// </summary>
    [Table(DbConstants.TableNames.ControlledSubstanceLicenseFacilities, Schema = DbConstants.DefaultDboSchema)]
    public class ControlledSubstanceLicenseFacilityEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the controlled substance license facility key.
        /// </summary>
        /// <value>The controlled substance license facility key.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ControlledSubstanceLicenseFacilityKey { get; set; }
        /// <summary>
        /// Gets or sets the tenant key.
        /// </summary>
        /// <value>The tenant key.</value>
        public Guid TenantKey { get; set; }

        /// <summary>
        /// Gets or sets the facility key.
        /// </summary>
        /// <value>The facility key.</value>
        public Guid FacilityKey { get; set; }

        /// <summary>
        /// Gets or sets the controlled substance license key.
        /// </summary>
        /// <value>The controlled substance license key.</value>
        public Guid ControlledSubstanceLicenseKey { get; set; }

        /// <summary>
        /// Gets or sets the facility.
        /// </summary>
        /// <value>The facility.</value>
        [ForeignKey(nameof(FacilityKey))]
        public virtual FacilityEntity Facility { get; set; }

        /// <summary>
        /// Gets or sets the controlled substance license.
        /// </summary>
        /// <value>The controlled substance license.</value>
        [ForeignKey(nameof(ControlledSubstanceLicenseKey))]
        public virtual ControlledSubstanceLicenseEntity ControlledSubstanceLicenseEntity { get; set; }
    }
}