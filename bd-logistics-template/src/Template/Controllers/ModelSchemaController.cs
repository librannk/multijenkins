
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using Template.API.Infrastructure.DBModel;
using Template.API.Infrastructure.Filters;

namespace Template.API.Controllers
{
    /// <summary>
    /// controller ModelSchemaController
    /// </summary>
    [Route("/api/v1/template/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ModelSchemaController : ControllerBase
    {
        private ValidateJson obj = new ValidateJson();

        /// <summary>
        /// validation errors
        /// </summary>
        private IList<ValidationError> errors;
        /// <summary>
        /// action method ValidateModel
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("jsonValidation")]
        public IActionResult ValidateModel(SampleProducts product)
        {
            bool Isvalid = obj.IsValid(product, out errors);
            if (Isvalid)
            {
                productList.Add(product);
            }
            else
            {
                //if file does not exist 
                if (errors.Count == 0)
                {
                    return BadRequest("Json file does not exist in blob for Validation");
                }
                else
                {
                    return BadRequest(errors);
                }
            }
            return Ok(product);
        }
        private readonly List<SampleProducts> productList = new List<SampleProducts>();
    }
}