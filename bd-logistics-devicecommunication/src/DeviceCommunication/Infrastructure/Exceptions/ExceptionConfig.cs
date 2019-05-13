using System;
using System.Net;

namespace Logistics.Services.DeviceCommunication.API.Infrastructure.Exceptions
{
    /// <summary>
    /// class ExceptionConfig
    /// </summary>
    public class ExceptionConfig
    {
        #region Enum 

        /// <summary>  
        /// Different types of exceptions.  
        /// </summary>  
        public enum Exceptions
        {
            /// <summary>
            /// enum constant NullReferenceException 
            /// </summary>
            NullReferenceException = 1,
            /// <summary>
            /// enum constant FileNotFoundException 
            /// </summary>
            FileNotFoundException = 2,
            /// <summary>
            /// enum constant OverflowException 
            /// </summary>
            OverflowException = 3,
            /// <summary>
            /// enum constant OutOfMemoryException 
            /// </summary>
            OutOfMemoryException = 4,
            /// <summary>
            /// enum constant InvalidCastException 
            /// </summary>
            InvalidCastException = 5,
            /// <summary>
            /// enum constant ObjectDisposedException 
            /// </summary>
            ObjectDisposedException = 6,
            /// <summary>
            /// enum constant UnauthorizedAccessException 
            /// </summary>
            UnauthorizedAccessException = 7,
            /// <summary>
            /// enum constant NotImplementedException 
            /// </summary>
            NotImplementedException = 8,
            /// <summary>
            /// enum constant NotSupportedException 
            /// </summary>
            NotSupportedException = 9,
            /// <summary>
            /// enum constant InvalidOperationException 
            /// </summary>
            InvalidOperationException = 10,
            /// <summary>
            /// enum constant TimeoutException 
            /// </summary>
            TimeoutException = 11,
            /// <summary>
            /// enum constant ArgumentException 
            /// </summary>
            ArgumentException = 12,
            /// <summary>
            /// enum constant FormatException 
            /// </summary>
            FormatException = 13,
            /// <summary>
            /// enum constant StackOverflowException 
            /// </summary>
            StackOverflowException = 14,
            /// <summary>
            /// enum constant SqlException 
            /// </summary>
            SqlException = 15,
            /// <summary>
            /// enum constant IndexOutOfRangeException 
            /// </summary>
            IndexOutOfRangeException = 16,
            /// <summary>
            /// enum constant IOException 
            /// </summary>
            IOException = 17
        }

        #endregion

        #region Method

        /// <summary>  
        /// This method will return the status code based on the exception type.  
        /// </summary>  
        /// <param name="exceptionType"></param>  
        /// <returns>HttpStatusCode</returns>  
        public  HttpStatusCode GetErrorCode(Type exceptionType)
        {
            if (Enum.TryParse<Exceptions>(exceptionType.Name, out Exceptions tryParseResult))
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

        #endregion
    }
}
