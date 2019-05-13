using System;
using System.Net;

namespace CCEProxy.API.Infrastructure.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionConfig
    {
        /// <summary>
        /// Exception enum class
        /// </summary>
        public enum Exceptions
        {
            /// <summary>
            /// Enum for NullReferenceException
            /// </summary>
            NullReferenceException = 1,

            /// <summary>
            /// Enum for FileNotFoundException
            /// </summary>
            FileNotFoundException = 2,

            /// <summary>
            /// Enum for OverflowException
            /// </summary>
            OverflowException = 3,

            /// <summary>
            /// Enum for OutOfMemoryException
            /// </summary>
            OutOfMemoryException = 4,

            /// <summary>
            /// Enum for InvalidCastException
            /// </summary>
            InvalidCastException = 5,

            /// <summary>
            /// Enum for ObjectDisposedException
            /// </summary>
            ObjectDisposedException = 6,

            /// <summary>
            /// Enum for UnauthorizedAccessException
            /// </summary>
            UnauthorizedAccessException = 7,

            /// <summary>
            /// Enum for NotImplementedException
            /// </summary>
            NotImplementedException = 8,

            /// <summary>
            /// Enum for NotSupportedException
            /// </summary>
            NotSupportedException = 9,

            /// <summary>
            /// Enum for InvalidOperationException
            /// </summary>
            InvalidOperationException = 10,

            /// <summary>
            /// Enum for TimeoutException
            /// </summary>
            TimeoutException = 11,

            /// <summary>
            /// Enum for ArgumentException
            /// </summary>
            ArgumentException = 12,

            /// <summary>
            /// Enum for FormatException
            /// </summary>
            FormatException = 13,

            /// <summary>
            /// Enum for StackOverflowException
            /// </summary>
            StackOverflowException = 14,

            /// <summary>
            /// Enum for SqlException
            /// </summary>
            SqlException = 15,

            /// <summary>
            /// Enum for IndexOutOfRangeException
            /// </summary>
            IndexOutOfRangeException = 16,

            /// <summary>
            /// Enum for IOException
            /// </summary>
            IOException = 17
        }

        /// <summary>  
        /// This method will return the status code based on the exception type.  
        /// </summary>  
        /// <param name="exceptionType"></param>  
        /// <returns>HttpStatusCode</returns>  
        public HttpStatusCode getErrorCode(Type exceptionType)
        {
            Exceptions tryParseResult;
            if (Enum.TryParse<Exceptions>(exceptionType.Name, out tryParseResult))
            {
                switch (tryParseResult)
                {
                    case Exceptions.NullReferenceException:
                        return HttpStatusCode.LengthRequired;

                    case Exceptions.FileNotFoundException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.OverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.OutOfMemoryException:
                        return HttpStatusCode.ExpectationFailed;

                    case Exceptions.InvalidCastException:
                        return HttpStatusCode.PreconditionFailed;

                    case Exceptions.ObjectDisposedException:
                        return HttpStatusCode.Gone;

                    case Exceptions.UnauthorizedAccessException:
                        return HttpStatusCode.Unauthorized;

                    case Exceptions.NotImplementedException:
                        return HttpStatusCode.NotImplemented;

                    case Exceptions.NotSupportedException:
                        return HttpStatusCode.NotAcceptable;

                    case Exceptions.InvalidOperationException:
                        return HttpStatusCode.MethodNotAllowed;

                    case Exceptions.TimeoutException:
                        return HttpStatusCode.RequestTimeout;

                    case Exceptions.ArgumentException:
                        return HttpStatusCode.BadRequest;

                    case Exceptions.StackOverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.FormatException:
                        return HttpStatusCode.UnsupportedMediaType;

                    case Exceptions.IOException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.IndexOutOfRangeException:
                        return HttpStatusCode.ExpectationFailed;

                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
