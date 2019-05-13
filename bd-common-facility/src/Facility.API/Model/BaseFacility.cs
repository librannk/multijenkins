using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Facility.API.Model
{
    /// <summary>
    /// Base Facility with default attributes
    /// </summary>
    public class BaseFacility
    {
        /// <summary>
        /// Pharmacy information system key.
        /// </summary>
        /// <value>Pharmacy information system key.</value>
        public Guid PharmacyInformationSystemKey { get; set; }

        /// <summary>
        /// Facility Name.
        /// </summary>
        /// <value>Facility Name.</value>
        [Required]
        [MaxLength(50)]
        [DataMember(Name = "FacilityName")]
        public string FacilityName { get; set; }

        /// <summary>
        /// Time zone id.
        /// </summary>
        /// <value>Time zone id.</value>
        [Required]
        [MaxLength(64)]
        [DataMember(Name = "TimeZoneId")]
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Site id of facility.
        /// </summary>
        /// <value>Site id of facility.</value>
        [MaxLength(50)]
        [DataMember(Name = "SiteId")]
        public string SiteId { get; set; }

        /// <summary>
        /// Description of facility.
        /// </summary>
        /// <value>Description of facility.</value>
        [Required(ErrorMessage = "Please enter Description")]
        [DataMember(Name = "DescriptionText")]
        [MaxLength(100)]
        public string DescriptionText { get; set; }

        /// <summary>
        /// Customer Contact Name
        /// </summary>
        /// <value>Customer Contact Name.</value>
        [MaxLength(50)]
        [DataMember(Name = "CustomerContactName")]
        public string CustomerContactName { get; set; }

        /// <summary>
        /// Customer Phone Number.
        /// </summary>
        /// <value>Customer Phone Number.</value>
        [MaxLength(50)]
        [DataMember(Name = "CustomerContactPhoneNumberText")]
        public string CustomerContactPhoneNumberText { get; set; }

        /// <summary>
        /// Customer Number Fax.
        /// </summary>
        /// <value>Customer Number Fax.</value>
        [MaxLength(50)]
        [DataMember(Name = "CustomerContactFaxNumberText")]
        public string CustomerContactFaxNumberText { get; set; }

        /// <summary>
        /// Customer Email.
        /// </summary>
        /// <value>Customer Email.</value>
        [MaxLength(50)]
        [DataMember(Name = "CustomerContactEmailAddressValue")]
        public string CustomerContactEmailAddressValue { get; set; }

        /// <summary>
        /// Street Address.
        /// </summary>
        /// <value>Street Address.</value>
        [MaxLength(120)]
        [DataMember(Name = "StreetAddressText")]
        public string StreetAddressText { get; set; }

        /// <summary>
        /// Street Address Line 2.
        /// </summary>
        /// <value>Street Address Line 2.</value>
        [MaxLength(50)]
        [DataMember(Name = "StreetAddress2Text")]
        public string StreetAddress2Text { get; set; }

        /// <summary>
        /// Name of city for customer address.
        /// </summary>
        /// <value>Name of city for customer address.</value>
        [MaxLength(50)]
        [DataMember(Name = "CityName")]
        public string CityName { get; set; }

        /// <summary>
        /// Address Sub divison name.
        /// </summary>
        /// <value>Address Sub divison name.</value>
        [MaxLength(50)]
        [DataMember(Name = "SubDivisionName")]
        public string SubDivisionName { get; set; }

        /// <summary>
        /// Postal Code.
        /// </summary>
        /// <value>Postal Code.</value>
        [MaxLength(20)]
        [DataMember(Name = "PostalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country Name.
        /// </summary>
        /// <value>Country Name.</value>
        [MaxLength(50)]
        [DataMember(Name = "CountryName")]
        public string CountryName { get; set; }

        /// <summary>
        /// Rx  License Id.
        /// </summary>
        /// <value>Rx  License Id.</value>
        [MaxLength(20)]
        [DataMember(Name = "RxLicenseId")]
        public string RxLicenseId { get; set; }
    }
}
