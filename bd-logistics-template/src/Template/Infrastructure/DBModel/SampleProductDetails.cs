
using Newtonsoft.Json;

namespace Template.API.Infrastructure.DBModel
{
    /// <summary>
    /// model class SampleProductDetails
    /// </summary>
    public class SampleProductDetails
    {
        /// <summary>
        /// Make is json property
        /// </summary>
        [JsonProperty]
        public string Make { get; set; }
        /// <summary>
        /// Model is json property
        /// </summary>
        [JsonProperty]
        public string Model { get; set; }
    }
}
