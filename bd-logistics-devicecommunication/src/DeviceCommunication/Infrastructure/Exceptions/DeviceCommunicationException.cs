using System;

namespace Logistics.Services.DeviceCommunication.API.Infrastructure.Exceptions
{
    /// <summary>
    /// class DeviceCommunicationException
    /// </summary>
    public class DeviceCommunicationException: Exception
    {
        /// <summary>
        /// constructor DeviceCommunicationException
        /// </summary>
        public DeviceCommunicationException()
        {
        }

        /// <summary>
        /// Constructor DeviceCommunicationException 
        /// </summary>
        /// <param name="message"></param>
        public DeviceCommunicationException(string message) : base(message)
        {
        }

        /// <summary>
        ///  Constructor DeviceCommunicationException 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="responseModel"></param>
        public DeviceCommunicationException(string message, string responseModel) : base(message)
        {
        }
        /// <summary>
        ///  Constructor DeviceCommunicationException 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DeviceCommunicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        ///  Constructor DeviceCommunicationException 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="customMessage"></param>
        /// <param name="innerException"></param>
        public DeviceCommunicationException(string message,string customMessage, Exception innerException) : base(message, innerException)
        {
        }
    }
}
