using BD.Template.API.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace BD.Template.API.Infrastructure.Filters
{
    /// <summary>
    /// Custom  Exception filter
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ExceptionConfig _exceptionConfig;

        /// <summary>
        /// CustomExceptionFilter
        /// </summary>
        /// <param name="exceptionConfig"></param>
        public CustomExceptionFilter(ExceptionConfig exceptionConfig)
        {
            _exceptionConfig = exceptionConfig;
        }

        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {

          //  ExceptionConfig _exceptionConfig = new ExceptionConfig();
            HttpStatusCode statusCode = (context.Exception as WebException != null &&
                         ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                          ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                          : _exceptionConfig.GetErrorCode(context.Exception.GetType());
            string errorMessage = context.Exception.Message;
            string customErrorMessage = "Hear Customer message will code depending on code";
            string customCode = "Hear Customer code will code depending on code";
            string stackTrace = context.Exception.StackTrace;


            // Loging Hear

            

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(
                new
                {
                    message = customErrorMessage,
                    customCode,
                    isError = true,
                    errorMessage,
                    errorCode = statusCode,
                    stackTrace
                });
            #region Logging  
            
            #endregion Logging  
            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }
    
    }
}
