using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HateoasFilter.Model;

namespace SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository
{
    /// <summary>
    /// Base Entity is a class has Id property for all Entities.All Entities class has are inherited from BaseEntity 
    /// </summary>
    public class BaseEntity
    {
        public Guid CreatedByActorKey { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public Guid LastModifiedByActorKey { get; set; }
        public DateTime LastModifiedUTCDateTime { get; set; }
    }
}
