using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StorageSpace.API.Infrastructure.DataAccess.Mongo.Entities
{
    /// <summary>  </summary>
    public class Entity
    {
        /// <summary> To hold the Bson Id </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// To hold the created by
        /// </summary>
        public int CreatedByActorKey { get; set; }

        /// <summary>
        /// To hold the created date
        /// </summary>
        public DateTime CreatedDateUTCDateTime { get; set; }

        /// <summary> To hold the modified by </summary>
        public int LastModifiedByActorKey { get; set; }

        /// <summary> To hold modified date </summary>
        public DateTime LastModifiedUTCDateTime { get; set; }
    }
}
