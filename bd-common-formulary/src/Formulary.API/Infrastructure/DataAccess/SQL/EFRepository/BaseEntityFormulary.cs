using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities
{
    /// <summary>
    /// Model Class Base Entity Formulary contains common attributes for tables
    /// </summary>
    public class BaseEntityFormulary
    {
        /// <summary>Gets or sets the Created by actor key.</summary>
        /// <value>The Created by actor key.</value>
        public Guid CreatedByActorKey { get; set; }
        /// <summary>Gets or sets the Created date.</summary>
        /// <value>The Created by actor key.</value>
        [Column(TypeName = "DateTimeOffset(7)")]
        public DateTimeOffset CreatedDateTime { get; set; }
        /// <summary>Gets or sets the last modified by actor key.</summary>
        /// <value>The last modified by actor key.</value>
        public Guid LastModifiedByActorKey { get; set; }
        /// <summary>Gets or sets the last modified datetime</summary>
        ///<value>The last modified datetime.</value>
        [Column(TypeName = "DateTimeOffset(7)")]
        public DateTimeOffset LastModifiedDateTime { get; set; }
    }
}
