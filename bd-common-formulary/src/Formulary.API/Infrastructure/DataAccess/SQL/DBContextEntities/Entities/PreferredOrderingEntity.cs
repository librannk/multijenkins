using Formulary.API.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Preferred Ordering table
    /// </summary>
    [Table(DbConstants.TableNames.PreferredOrdering, Schema = DbConstants.DefaultDboSchema)]
    public class PreferredOrderingEntity : BaseEntityFormulary
    {
        /// <summary>Gets or sets the Preferred Ordering key.</summary>
        /// <value>The Preferred Ordering key.</value>
        [Required]
        [Key]
        public Guid PreferredOrderingKey { get; set; }

        /// <summary>Gets or sets the Vendor key.</summary>
        /// <value>The Vendor key.</value>
        [Required]
        public Guid VendorKey { get; set; }
        /// <summary>Gets or sets the Product Identification key.</summary>
        /// <value>The Product Identification key.</value>
        [Required]
        [ForeignKey("ProductIdentificationObject")]
        public Guid ProductIDKey { get; set; }
        /// <summary>Gets or sets the Item key.</summary>
        /// <value>The Item key.</value>
        [ForeignKey("Item")]
        [Required]
        public Guid ItemKey { get; set; }
        /// <summary>Gets or sets the Vendor item code.</summary>
        /// <value>The Vendor item code.</value>
        [MaxLength(25)]
        [Required]
        public string VendorItemCode { get; set; }
        /// <summary>Gets or sets the IsPreferred flag.</summary>
        /// <value>The IsPreferred flag.</value>
        [Required]
        public bool IsPreferred { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        [ForeignKey("ItemKey")]
        public virtual ItemEntity Item { get; set; }

        [ForeignKey("ProductIDKey")]
        public virtual ProductIdentificationEntity ProductIdentificationObject { get; set; }

    }
}
