
using BD.Core.BaseModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BD.Core.ExceptionHandling
{
    /// <summary> The global exception filter class. </summary>
    /// <seealso cref="IExceptionFilter" />
    public class GlobalExceptionFilter : IExceptionFilter
    {
        #region Constants
        /// <summary> ProductionErrorMessage </summary>
        private const string ProductionErrorMessage = "System Error Occured.";

        /// <summary> SystemErrorCode </summary>
        private const int SystemErrorCode = 50000;
        #endregion

        #region Private readonly
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IHostingEnvironment _env;
        #endregion

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        /// <summary> Called after an action has thrown an <see cref="T:System.Exception" /> </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext"/></param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var responseStatus = new ResponseStatus
                {
                    Code = SystemErrorCode,
                    Type = ResponsePayloadType.SystemException
                };

                responseStatus.Errors.Add(new ErrorResponse(_env.IsProduction() ? ProductionErrorMessage : context.Exception.ToString(),
                    SystemErrorCode, ResponsePayloadType.SystemException));

                var responsePayload = new ResponsePayload<string>
                {
                    ResponseStatus = responseStatus
                };

                context.Result = new JsonResult(responsePayload)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                _logger.LogError(context.Exception, nameof(GlobalExceptionFilter));
            }
        }
    }
}
