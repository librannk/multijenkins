using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransactionQueue.Shared.DataAccess.Mongo.Entities
{
    /// <summary>
    /// Base Entity for Mongo Object
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// To hold the Bson Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// To hold the modified by
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// To hold modified date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// To hold created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// To hold created date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
