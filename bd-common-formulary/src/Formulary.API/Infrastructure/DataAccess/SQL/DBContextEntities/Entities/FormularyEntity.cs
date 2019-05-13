using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// It's an Formualry Entity,which is going to manipulate in DB with its properties as fields.
    /// </summary>
    [Table(DbConstants.TableNames.Formulary, Schema = DbConstants.DefaultDboSchema)]
    public class FormularyEntity : BaseEntity
    {
        /// <summary>
        /// Item Id would be the Medicine identifier
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Formulary description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Whether formulary is active or in-active 
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }
    }
}
