using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    ///  It's an NDC entity,which is going to manipulate in DB with its properties as fields.
    /// </summary>
    public class NDCEntity : BaseEntity
    {
        /// <summary>
        /// Formulary Identifier
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// Whether it is active or not
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// trade name
        /// </summary>
        public string TradeName { get; set; }
        /// <summary>
        /// Generic Name
        /// </summary>
        public string GenericName { get; set; }
        /// <summary>
        /// National Drug Code
        /// </summary>
        public string NDC { get; set; }
    }
}
