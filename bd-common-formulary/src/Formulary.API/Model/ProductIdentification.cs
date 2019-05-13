using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Formulary.API.Model
{
    /// <summary>
    /// It consist of NDC details.
    /// </summary>
    public class ProductIdentification
    {
        /// <summary>
        /// Gets or Sets ProductIdentificationKey
        /// </summary>
        [DataMember(Name = "ProductIdentificationKey")]
        public Guid ProductIDKey { get; set; }

        /// <summary>
        /// Gets or Sets ProductID
        /// </summary>
        [DataMember(Name = "ProductID")]
        public string ProductID { get; set; }

        //TODO : To be used with NDC

        ///// <summary>
        ///// Gets or Sets preferredOrderingKey
        ///// </summary>
        //[DataMember(Name = "preferredOrderingKey")]
        //public Guid PreferredOrderingKey { get; set; }

        ///// <summary>
        ///// Gets or Sets PreferredNDC
        ///// </summary>
        //[DataMember(Name = "PreferredNDC")]
        //public bool PreferredNDC { get; set; }

        /// <summary>
        /// Gets or Sets AltCode
        /// </summary>
        [DataMember(Name = "AltCode")]
        public string AltCode { get; set; }

        /// <summary>
        /// Gets or Sets ManufacturerID
        /// </summary>
        [DataMember(Name = "ManufacturerID")]
        public Guid? ManufacturerID { get; set; }

        //TODO : To be used when manufacturer will be used

        ///// <summary>
        ///// Gets or Sets Manufacturer
        ///// </summary>
        //[DataMember(Name = "Manufacturer")]
        //public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or Sets GenericName
        /// </summary>
        [DataMember(Name = "GenericName")]
        public string GenericName { get; set; }

        /// <summary>
        /// Gets or Sets DrugFlag
        /// </summary>
        [DataMember(Name = "DrugFlag")]
        public Boolean DrugFlag { get; set; }

        /// <summary>
        /// Gets or Sets TradeName
        /// </summary>
        [DataMember(Name = "TradeName")]
        public string TradeName { get; set; }

        /// <summary>
        /// Gets or Sets PackageSize
        /// </summary>
        [DataMember(Name = "PackageSize")]
        public int PackageSize { get; set; }

        /// <summary>
        /// Gets or Sets Strength
        /// </summary>
        [DataMember(Name = "Strength")]
        public string Strength { get; set; }

        /// <summary>
        /// Gets or Sets Volume
        /// </summary>
        [DataMember(Name = "Volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or Sets TotalVolume
        /// </summary>
        [DataMember(Name = "TotalVolume")]
        public string TotalVolume { get; set; }

        ///// <summary>
        ///// Gets or Sets AHFSClassName
        ///// </summary>
        //[DataMember(Name = "AHFSClassName")]
        //public int AHFSClassName { get; set; }
    }
}
