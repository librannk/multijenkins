using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Logistics.Services.DeviceCommunication.API.Infrastructure.Exceptions;

namespace Logistics.Services.DeviceCommunication.API.Infrastructure.Filters
{
    /// <summary>
    /// Custom  Exception filter
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        #region Fields

        private readonly ExceptionConfig _exceptionConfig;
        /// <summary>
        /// CustomExceptionFilter constructor
        /// </summary>
        /// <param name="exceptionConfig"></param>

        #endregion

        #region Constructor

        /// <summary>
        /// Parametarized constructor
        /// </summary>
        /// <param name="exceptionConfig"></param>
        public CustomExceptionFilter(ExceptionConfig exceptionConfig)
        {
            _exceptionConfig = exceptionConfig;
        }

        #endregion

        #region Exception handler method

        /// <summary>
        /// Exception handler
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {

         
            HttpStatusCode statusCode = (context.Exception as WebException != null &&
                         ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                          ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                          : _exceptionConfig.GetErrorCode(context.Exception.GetType());

            //Variable initialization
            string errorMessage = context.Exception.Message;
            string customErrorMessage = Constants.CustomExceptionFilter.CustomErrorMessage;
            string customCode = Constants.CustomExceptionFilter.CustomCode;
            string stackTrace = context.Exception.StackTrace;

            // Loging Hear
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(
                new
                {
                    message = customErrorMessage,
                    customCode = customCode,
                    isError = true,
                    errorMessage = errorMessage,
                    errorCode = statusCode,
                    stackTrace=stackTrace
                });

            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }

        #endregion
    }
}
