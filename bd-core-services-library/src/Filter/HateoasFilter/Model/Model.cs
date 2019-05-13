using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HateoasFilter.Model
{
    /// <summary>
    /// Link model to be used for HATEOAS
    /// </summary>
    public class Model
    {
        /// <summary>
        /// To hold URL
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// To hold the relation of URL
        /// </summary>
        [Key]
        public string Rel { get; set; }
        /// <summary>
        /// To hold the verb that is effective for that particular URL
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// constructor to initialize the values
        /// </summary>
        public Model(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
