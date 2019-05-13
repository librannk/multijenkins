using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BD.Template.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("3.0")]
    [Route("/api/v{version:apiVersion}/template/[controller]")]
    [ApiController]
    public class MultiVersionController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>]
        [HttpGet("GetData")]
        [MapToApiVersion("3.0")]
        public string Get()
        {
            return "Version 3";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetData")]
        [MapToApiVersion("1.0")]
        public string GetData()
        {
            return "Version 2";
        }
    }
}