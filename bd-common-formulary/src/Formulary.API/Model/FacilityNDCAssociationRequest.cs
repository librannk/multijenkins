using Formulary.API.Model;
using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model
{
    /// <summary>
    /// It's an facility NDC association entity,which is going to manipulate in DB with its properties as fields.
    /// </summary>
   
    public class FacilityNDCAssociationRequest:ModelBase
    {
        /// <summary>
        /// Facility Id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid facility id")]
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
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter a valid cost")]
        public decimal Cost { get; set; }
    }
}
