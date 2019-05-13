using Facility.API.Constants;
using Facility.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Facility Entity
    /// Implements the <see cref="BaseEntity" />
    /// </summary>
    /// <seealso cref="BaseEntity" />
    [Table(DbConstants.TableNames.Facilities, Schema = DbConstants.DefaultDboSchema)]
    public class FacilityEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the facility key.
        /// </summary>
        /// <value>The facility key.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FacilityKey { get; set; }
        /// <summary>
        /// Gets or sets the tenant key.
        /// </summary>
        /// <value>The tenant key.</value>
        public Guid? TenantKey { get; set; }
        /// <summary>
        /// Gets or sets the pharmacy information system key.
        /// </summary>
        /// <value>The pharmacy information system key.</value>
        public Guid PharmacyInformationSystemKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the facility.
        /// </summary>
        /// <value>The name of the facility.</value>
        [MaxLength(50)]
        public string FacilityName { get; set; }

        /// <summary>
        /// Gets or sets the facility code.
        /// </summary>
        /// <value>The facility code.</value>
        [MaxLength(20)]
        public string FacilityCode { get; set; }

        /// <summary>
        /// Gets or sets the time zone identifier.
        /// </summary>
        /// <value>The time zone identifier.</value>
        [Column(TypeName = "VARCHAR(64)")]
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        [MaxLength(50)]
        public string SiteId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [MaxLength(100)]
        public string DescriptionText { get; set; }
        /// <summary>
        /// Gets or sets the name of the customer contact.
        /// </summary>
        /// <value>The name of the customer contact.</value>
        [MaxLength(50)]
        public string CustomerContactName { get; set; }
        /// <summary>
        /// Gets or sets the customer contact phone number text.
        /// </summary>
        /// <value>The customer contact phone number text.</value>
        [MaxLength(50)]
        public string CustomerContactPhoneNumberText { get; set; }
        /// <summary>
        /// Gets or sets the customer contact fax number text.
        /// </summary>
        /// <value>The customer contact fax number text.</value>
        [MaxLength(50)]
        public string CustomerContactFaxNumberText { get; set; }
        /// <summary>
        /// Gets or sets the customer contact email address value.
        /// </summary>
        /// <value>The customer contact email address value.</value>
        [MaxLength(50)]
        public string CustomerContactEmailAddressValue { get; set; }

        /// <summary>
        /// Gets or sets the street address text.
        /// </summary>
        /// <value>The street address text.</value>
        [MaxLength(120)]
        public string StreetAddressText { get; set; }
        /// <summary>
        /// Gets or sets the street address2 text.
        /// </summary>
        /// <value>The street address2 text.</value>
        [MaxLength(50)]
        public string StreetAddress2Text { get; set; }
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>The name of the city.</value>
        [MaxLength(50)]
        public string CityName { get; set; }
        /// <summary>
        /// Gets or sets the name of the sub division.
        /// </summary>
        /// <value>The name of the sub division.</value>
        [MaxLength(50)]
        public string SubDivisionName { get; set; }
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
        /// <summary>
        /// Gets or sets the RX license identifier.
        /// </summary>
        /// <value>The RX license identifier.</value>
        [MaxLength(20)]
        public string RxLicenseId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [active flag].
        /// </summary>
        /// <value><c>true</c> if [active flag]; otherwise, <c>false</c>.</value>
        public bool ActiveFlag { get; set; }

        //navigation properties        

        /// <summary>
        /// Gets or sets the controlled substance license facilities.
        /// </summary>
        /// <value>The controlled substance license facilities.</value>
        public virtual ICollection<ControlledSubstanceLicenseFacilityEntity> ControlledSubstanceLicenseFacilities { get; set; }
    }
}