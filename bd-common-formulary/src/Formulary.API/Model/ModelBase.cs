using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Formulary.API.Model
{
    /// <summary>
    /// Data transfer base object with common properties
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        /// Object identifier
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Object creation Date
        /// </summary>
        [JsonIgnore]
        public int CreatedBy { get; set; }
        /// <summary>
        /// Object created date
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Object last modified by 
        /// </summary>
        [JsonIgnore]
        public int? LastModifiedBy { get; set; }
        /// <summary>
        /// Object last modified date
        /// </summary>
        [JsonIgnore]
        public DateTime LastModifiedDate { get; set; }
    }
}
