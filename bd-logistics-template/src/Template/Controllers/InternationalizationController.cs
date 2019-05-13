
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Template.API.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("/api/v1/template/[controller]")]
    [ApiController]
    [Authorize]
    public class InternationalizationController : ControllerBase
    {

        private readonly IStringLocalizer<InternationalizationController> _localizer;
        private readonly BD.Core.Context.IExecutionContextAccessor _accessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localizer"></param>
        public InternationalizationController(IStringLocalizer<InternationalizationController> localizer, BD.Core.Context.IExecutionContextAccessor accessor)
        {
            _localizer = localizer;
            _accessor = accessor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("LocalizedFormatedContents")]
        public string GetLocalizedFormatedContents()
        {
            var value = _localizer["Greeting"];
            //var x=  _accessor.Current.Locale;
            return value;
        }
    }
}