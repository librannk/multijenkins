using Facility.API.Constants;
using Facility.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Controlled Substance License Entity
    /// Implements the <see cref="BaseEntity" />
    /// </summary>
    /// <seealso cref="BaseEntity" />
    [Table(DbConstants.TableNames.ControlledSubstanceLicenses, Schema = DbConstants.DefaultDboSchema)]
    public class ControlledSubstanceLicenseEntity : BaseEntity
    {

        /// <summary>
        /// Gets or sets the controlled substance license primary key.
        /// </summary>
        /// <value>The controlled substance license key.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ControlledSubstanceLicenseKey { get; set; }
        /// <summary>
        /// Gets or sets the tenant key.
        /// </summary>
        /// <value>The tenant key.</value>
        public Guid TenantKey { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [MaxLength(100)]
        public string LicenseId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [external flag].
        /// </summary>
        /// <value><c>true</c> if [external flag]; otherwise, <c>false</c>.</value>
        public bool ExternalFlag { get; set; }

        /// <summary>
        /// Gets or sets the street address text.
        /// </summary>
        /// <value>The street address text.</value>
        [MaxLength(120)]
        public string StreetAddressText { get; set; }
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>The name of the city.</value>
        [MaxLength(50)]
        public string CityName { get; set; }
        /// <summary>
        /// Gets or sets the name of the subdivision.
        /// </summary>
        /// <value>The name of the subdivision.</value>
        [MaxLength(50)]
        public string SubdivisionName { get; set; }
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        [MaxLength(20)]
        public string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>The name of the country.</value>
        [MaxLength(50)]
        public string CountryName { get; set; }

        //navigation properties        

        /// <summary>
        /// Gets or sets the controlled substance license facilities.
        /// </summary>
        /// <value>The controlled substance license facilities.</value>
        public virtual ICollection<ControlledSubstanceLicenseFacilityEntity> ControlledSubstanceLicenseFacilities { get; set; }
    }
}
