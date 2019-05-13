using BD.Template.API.Infrastructure.DataAccess.Mongo.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace BD.Template.API.Infrastructure.DBModel
{
    /// <summary>
    /// User model class
    /// </summary>
    [BsonIgnoreExtraElements]
    public class User : Entity
    {
        /// <summary>
        /// To hold the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// To hold the last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// To hold the age
        /// </summary>
        public int Age { get; set; }
    }

}
