using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DemoUsageApp.Infrastructure.EFRepository;

namespace DemoUsageApp
{
    [Table("Facility", Schema = "dbo")]
    public class Facility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int FacilityId { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string FacilityCode { get; set; }
        /// <summary>
        /// Facility Is Process Inactive As Exception
        /// </summary>
     

    }
}
