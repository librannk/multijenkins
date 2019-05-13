using TransactionQueue.API.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace TransactionQueue.API.Infrastructure.Filters
{
    /// <summary> Custom  Exception filter </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        // FIELDS
        private readonly ExceptionConfig _exceptionConfig;

        // CONSTRUCTORS
        /// <summary>  </summary>
        /// <param name="exceptionConfig"></param>
        public CustomExceptionFilter(ExceptionConfig exceptionConfig)
        {
            _exceptionConfig = exceptionConfig;
        }

        // METHODS
        /// <summary> OnException </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {

            //  ExceptionConfig _exceptionConfig = new ExceptionConfig();
            var statusCode = (context.Exception as WebException != null &&
                         ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                          ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                          : _exceptionConfig.GetErrorCode(context.Exception.GetType());
            string errorMessage = context.Exception.Message;
            string customErrorMessage = "Hear Customer message will code depending on code";
            string customCode = "Hear Customer code will code depending on code";
            string stackTrace = context.Exception.StackTrace;


            // Loging Hear



            var response = context.HttpContext.Response;
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
                    stackTrace = stackTrace
                });
            #region Logging  

            #endregion Logging  
            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }

    }
}
