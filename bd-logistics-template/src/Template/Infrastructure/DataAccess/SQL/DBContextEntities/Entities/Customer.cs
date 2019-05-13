using System;
using System.ComponentModel.DataAnnotations.Schema;
using BD.Template.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// It's an entity,which is going to manipulate in DB with its properties as fields.
    /// </summary>
    [Table("Designation", Schema = "dbo")]
    public class Customer : BaseEntity
    {
        /// <summary>
        /// It's a property whcih represent a field in DB.
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public String Name { get; set; }
    }
}
