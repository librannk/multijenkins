using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;


namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// It's an facility NDC association entity,which is going to manipulate in DB with its properties as fields.
    /// </summary>
    [Table(DbConstants.TableNames.Ndc, Schema = DbConstants.DefaultDboSchema)]
    public class FacilityNDCAssocEntity : BaseEntity
    {
        /// <summary>
        /// Facility Id
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// NDC Identifier
        /// </summary>
        public int NDCId { get; set; }
        /// <summary>
        /// Whether it is prefered or not 
        /// </summary>
        public bool IsPreferred { get; set; }
        /// <summary>
        /// Cost
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Cost { get; set; }
    }
}
