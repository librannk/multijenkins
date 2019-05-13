
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Template.API.Infrastructure.DBModel
{
    /// <summary>
    /// Model class SampleProducts
    /// </summary>
    public class SampleProducts
    {
        /// <summary>
        /// ID is json property
        /// </summary>
        [JsonProperty]
        public int ID { get; set; }
        /// <summary>
        /// Name is json property
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }
        /// <summary>
        /// Feature is json property
        /// </summary>
        [JsonProperty]
        public string Feature { get; set; }
        /// <summary>
        /// Price is json property
        /// </summary>
        [JsonProperty]
        public double Price { get; set; }
        /// <summary>
        /// Location1 is json property
        /// </summary>
        [JsonProperty]
        public List<List<string>> Location1 { get; set; }
        /// <summary>
        /// Location2 is json property
        /// </summary>
        [JsonProperty]
        public List<string> Location2 { get; set; }
        /// <summary>
        /// Details is json property
        /// </summary>
        [JsonProperty]
        public SampleProductDetails Details { get; set; }
    }
}
