using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BD.Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteConfiguration.API.FacilityConfiguration.Constants;
using SiteConfiguration.API.FacilityConfiguration.RequestResponseModel;

namespace SiteConfiguration.API.FacilityConfiguration.Controllers
{
    /// <summary>
    /// base controller for facility    /// </summary>
    [ExcludeFromCodeCoverage]
    [ApiController]
    public class FacilityLogisticsBaseController : ControllerBase
    {
        internal ObjectResult InternalErrorMessage()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { ErrorCode = StatusCodes.Status500InternalServerError.ToString(), ErrorDescription = "" });
        }

        internal ObjectResult BadRequestErrorMessage()
        {
            return BadRequest(new ErrorMessage() { ErrorCode = StatusCodes.Status400BadRequest.ToString(), ErrorDescription = BusinessError.InvalidInput });
        }

    }
}