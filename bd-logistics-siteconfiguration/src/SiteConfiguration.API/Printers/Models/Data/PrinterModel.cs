using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Models.Data
{
    /// <summary>
    /// PrinterModel
    /// </summary>
    public class PrinterModel : BaseEntity
    {


        /// <summary>
        /// PrinterModelKey
        /// </summary>
        [Key]
        public Guid PrinterModelKey { get; set; }

        /// <summary>
        /// DescriptionText
        /// </summary>
        public string DescriptionText { get; set; }
    }
}
