using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HateoasFilter.Model;

namespace BD.Template.API.Infrastructure.DataAccess.SQL.EFRepository
{

    /// <summary>
    /// Base Entity is a class has Id property for all Entities.All Entities class has are inherited from BaseEntity 
    /// </summary>
    public class BaseEntity : ModelWrapper
    {
        /// <summary>
        /// Id property is used by all entities to create Id field in DB as primary key.
        /// </summary>
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
