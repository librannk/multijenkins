using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Model
{
    /// <summary>
    /// Formulary deatils
    /// </summary>
    public class FormularyRequest:ModelBase
    {
        /// <summary>
        /// Medicine id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Item id")]
        public int ItemId { get; set; } 
        /// <summary>
        /// Formulary Description
        /// </summary>
        [Required(AllowEmptyStrings =false, ErrorMessage = "Please enter a valid description")]
        public string  Description { get; set; }
        /// <summary>
        /// Formular Status
        /// </summary>
        [Required(ErrorMessage = "Please enter a Active value")]
        public bool Active { get; set; }
        /// <summary>
        /// Item Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid ItemName")]
        public string ItemName { get; set; }
    }

}
