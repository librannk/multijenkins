using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace BD.Core.BaseModels
{
    /// <summary> Base Class for Error Response Message </summary>
    [DataContract]
    public class ErrorResponse
    {
        /// <summary> The default error code </summary>
        private const int DefaultErrorCode = 40000;

        /// <summary> The default service name </summary>
        private const string DefaultServiceName = "Service";

        /// <summary> Initializes a new instance of the <see cref="ErrorResponse" /> class. </summary>
        /// <param name="errorMessage">The error message.</param>
        public ErrorResponse(string errorMessage)
        {
            Code = DefaultErrorCode;
            Type = ResponsePayloadType.SystemException;
            Service = DefaultServiceName;
            Message = errorMessage;
        }

        /// <summary> Initializes a new instance of the <see cref="ErrorResponse" /> class. </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorcode">The errorcode.</param>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="errorService">The error service.</param>
        public ErrorResponse(string errorMessage, int errorcode, ResponsePayloadType errorType, string errorService)
        {
            Code = errorcode;
            Type = errorType;
            Service = errorService;
            Message = errorMessage;
        }

        /// <summary> Initializes a new instance of the <see cref="ErrorResponse" /> class. </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorcode">The errorcode.</param>
        public ErrorResponse(string errorMessage, int errorcode)
        {
            Code = errorcode;
            Type = ResponsePayloadType.SystemException;
            Service = DefaultServiceName;
            Message = errorMessage;
        }

        /// <summary> Initializes a new instance of the <see cref="ErrorResponse" /> class. </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorcode">The errorcode.</param>
        /// <param name="errorType">Type of the error.</param>
        public ErrorResponse(string errorMessage, int errorcode, ResponsePayloadType errorType)
        {
            Code = errorcode;
            Type = errorType;
            Service = DefaultServiceName;
            Message = errorMessage;
        }

        /// <summary> Initializes a new instance of the <see cref="ErrorResponse" /> class. </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorService">The error service.</param>
        public ErrorResponse(string errorMessage, string errorService)
        {
            Code = DefaultErrorCode;
            Type = ResponsePayloadType.SystemException;
            Service = errorService;
            Message = errorMessage;
        }

        /// <summary> Initializes a new instance of the <see cref="ErrorResponse" /> class. </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorType">Type of the error.</param>
        public ErrorResponse(string errorMessage, ResponsePayloadType errorType)
        {
            Code = DefaultErrorCode;
            Type = errorType;
            Message = errorMessage;
        }

        /// <summary> Initializes a new instance of the <see cref="ErrorResponse" /> class. </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="underlyingService">The underlying service.</param>
        public ErrorResponse(string errorMessage, ResponsePayloadType errorType, string underlyingService)
        {
            Code = DefaultErrorCode;
            Type = errorType;
            Service = underlyingService;
            Message = errorMessage;
        }

        /// <summary> Gets or sets the code. </summary>
        /// <value> The code. </value>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary> Gets or sets the type. </summary>
        /// <value> The type. </value>
        [DataMember(Name = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResponsePayloadType Type { get; set; }

        /// <summary> Gets or sets the message. </summary>
        /// <value> The message. </value>
        [DataMember(Name = "message")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary> Gets or sets the property. </summary>
        /// <value> The property. </value>
        [DataMember(Name = "property", IsRequired = false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Property { get; set; }

        /// <summary> Gets or sets the path. </summary>
        /// <value> The path </value>
        [DataMember(Name = "path", IsRequired = false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        /// <summary> Gets or sets the service. </summary>
        /// <value> The service. </value>
        [DataMember(Name = "service", IsRequired = false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Service { get; set; }

        /// <summary> Returns a <see cref="string" /> that represents this instance. </summary>
        /// <returns> A <see cref="string" /> that represents this instance. </returns>
        public override string ToString()
        {
            return $"{nameof(Code)}: {Code}, {nameof(Type)}: {Type}," +
                   $" {nameof(Message)}: {Message}, {nameof(Property)}: " +
                   $"{Property}, {nameof(Path)}: {Path}, {nameof(Service)}: " +
                   $"{Service}";
        }
    }
}
