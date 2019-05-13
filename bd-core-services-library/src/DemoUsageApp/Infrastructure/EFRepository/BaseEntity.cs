using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DemoUsageApp.Infrastructure.EFRepository
{
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
