using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// It's an entity,which is going to manipulate in DB with its properties as fields.
    /// </summary>
    [Table(DbConstants.TableNames.Facility, Schema = DbConstants.DefaultDboSchema)]
    public class FacilityEntity : BaseEntity
    {
        /// <summary>
        /// Formulary identifier
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// Whether formulary is active or not for this perticular facility
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Approved 
        /// </summary>
        public bool Approved { get; set; }
        /// <summary>
        /// ADU quantity rounding
        /// </summary>
        public bool ADUQtyRounding { get; set; }
        /// <summary>
        /// ADU Ignore stok out
        /// </summary>
        public bool ADUIgnoreStockOut { get; set; }
        /// <summary>
        /// ADU ignore critical low
        /// </summary>
        public bool ADUIgnoreCritLow { get; set; }
        /// <summary>
        /// Facility
        /// </summary>
        public int FacilityId { get; set; }

    }
}
