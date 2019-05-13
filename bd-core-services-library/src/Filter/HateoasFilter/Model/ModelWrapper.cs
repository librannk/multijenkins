using System;
using System.Collections.Generic;
using System.Text;

namespace HateoasFilter.Model
{
    /// <summary>
    /// Link model to be used for HATEOAS
    /// </summary>
    public class ModelWrapper
    {
        /// <summary>
        /// To hold collection of links
        /// </summary>
        public List<Model> Links { get; set; } = new List<Model>();
    }
}
