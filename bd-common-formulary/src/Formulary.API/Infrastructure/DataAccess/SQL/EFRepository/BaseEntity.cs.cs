using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.EFRepository
{
    /// <summary>
    /// Base Entity is a class has Id property for all Entities.All Entities class has are inherited from BaseEntity 
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Id property is used by all entities to create Id field in DB as primary key.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        /// <summary>
        /// Record created by
        /// </summary>
        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }
        /// <summary>
        /// Record created date
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Record modified by
        /// </summary>
        [Column(TypeName = "int")]
        public int? LastModifiedBy { get; set; }
        /// <summary>
        /// Record modified date
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedDate { get; set; }
    }
}
