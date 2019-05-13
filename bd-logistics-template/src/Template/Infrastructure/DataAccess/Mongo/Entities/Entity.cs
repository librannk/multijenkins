using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BD.Template.API.Infrastructure.DataAccess.Mongo.Entities
{
    /// <summary>
    /// 
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
        public DateTime ModifiedDate { get; set; }
    }
}
