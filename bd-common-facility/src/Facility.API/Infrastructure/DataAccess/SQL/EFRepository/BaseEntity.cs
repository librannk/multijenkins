using System;

namespace Facility.API.Infrastructure.DataAccess.SQL.EFRepository
{

    /// <summary>
    /// Base Entity is a class has common properties for all Entities.
    /// All Entities class has are inherited from BaseEntity 
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the created by actor.
        /// </summary>
        /// <value>The created by actor.</value>
        public string CreatedByActorKey { get; set; }
        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>The created date time.</value>
        public DateTimeOffset CreatedDateTime { get; set; }
        /// <summary>
        /// Gets or sets the last modified by actor.
        /// </summary>
        /// <value>The last modified by actor.</value>
        public string LastModifiedByActorKey { get; set; }
        /// <summary>
        /// Gets or sets the last modified date time.
        /// </summary>
        /// <value>The last modified date time.</value>
        public DateTimeOffset LastModifiedDateTime { get; set; }
    }
}
