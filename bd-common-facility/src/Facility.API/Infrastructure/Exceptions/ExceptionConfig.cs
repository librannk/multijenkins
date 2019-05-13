using System;
using System.Net;

namespace Facility.API.Infrastructure.Exceptions
{
    /// <summary>
    /// ExceptionConfig
    /// </summary>
    public class ExceptionConfig
    {
        /// <summary>  
        /// Different types of exceptions.  
        /// </summary>  
        public  enum Exceptions
        {
            /// <summary>
            /// NullReferenceException
            /// </summary>
            NullReferenceException = 1,
            /// <summary>
            /// FileNotFoundException
            /// </summary>
            FileNotFoundException = 2,
            /// <summary>
            /// OverflowException
            /// </summary>
            OverflowException = 3,
            /// <summary>
            /// OutOfMemoryException
            /// </summary>
            OutOfMemoryException = 4,
            /// <summary>
            /// InvalidCastException
            /// </summary>
            InvalidCastException = 5,
            /// <summary>
            /// ObjectDisposedException
            /// </summary>
            ObjectDisposedException = 6,
            /// <summary>
            /// UnauthorizedAccessException
            /// </summary>
            UnauthorizedAccessException = 7,
            /// <summary>
            /// NotImplementedException
            /// </summary>
            NotImplementedException = 8,
            /// <summary>
            /// NotSupportedException
            /// </summary>
            NotSupportedException = 9,
            /// <summary>
            /// InvalidOperationException
            /// </summary>
            InvalidOperationException = 10,
            /// <summary>
            /// TimeoutException
            /// </summary>
            TimeoutException = 11,
            /// <summary>
            /// ArgumentException
            /// </summary>
            ArgumentException = 12,
            /// <summary>
            /// FormatException
            /// </summary>
            FormatException = 13,
            /// <summary>
            /// StackOverflowException
            /// </summary>
            StackOverflowException = 14,
            /// <summary>
            /// SqlException
            /// </summary>
            SqlException = 15,
            /// <summary>
            /// IndexOutOfRangeException
            /// </summary>
            IndexOutOfRangeException = 16,
            /// <summary>
            /// IoException
            /// </summary>
            IoException = 17
        }

        /// <summary>  
        /// This method will return the status code based on the exception type.  
        /// </summary>  
        /// <param name="exceptionType"></param>  
        /// <returns>HttpStatusCode</returns>  
        public  HttpStatusCode GetErrorCode(Type exceptionType)
        {
            Exceptions tryParseResult;
            if (Enum.TryParse(exceptionType.Name, out tryParseResult))
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

                    case Exceptions.IoException:
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
