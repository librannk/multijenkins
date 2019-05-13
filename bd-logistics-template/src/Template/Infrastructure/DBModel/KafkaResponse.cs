
using System;
using MongoDB.Bson.Serialization.Attributes;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Entities;
using Newtonsoft.Json;

namespace BD.Template.API.Infrastructure.DBModel
{
    /// <summary>
    /// Class for KafkaResponse properties to save into mongodb
    /// </summary>
    [BsonIgnoreExtraElements]
    public class KafkaResponse : Entity
    {
        /// <summary>
        /// kafkak EventMessage
        /// </summary>
        [JsonProperty]
        public string EventMessage { get; set; }
        /// <summary>
        /// kafka User Names
        /// </summary>
        [JsonProperty]
        public string[] Names { get; set; }
        /// <summary>
        /// kafka event Quantity
        /// </summary>
        [JsonProperty]
        public int Quantity { get; set; }
        /// <summary>
        /// kafka TranQType
        /// </summary>
        [JsonProperty]
        public string TranQType { get; set; }
        /// <summary>
        /// kafka ConnectionResetMinutes
        /// </summary>
        [JsonProperty]
        public int? ConnectionResetMinutes { get; set; }
        /// <summary>
        /// kafka event CreationDate
        /// </summary>
        [JsonProperty]
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// kafka Topic
        /// </summary>
        [JsonProperty]
        public string Topic { get; set; }
        /// <summary>
        /// Type of response like Publisher or Subscriber
        /// </summary>
        [JsonProperty]
        public string ResponseType { get; set; }
    }
}
