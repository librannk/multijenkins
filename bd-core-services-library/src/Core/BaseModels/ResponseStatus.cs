using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BD.Core.BaseModels
{
    /// <summary> The overall status of the response </summary>
    [DataContract]
    public class ResponseStatus
    {
        /// <summary> Initializes a new instance of the <see cref="ResponseStatus" /> class. </summary>
        public ResponseStatus() { }

        /// <summary> The error success code </summary>
        private const int ErrorSuccessCode = 40000;
        /// <summary> The default service </summary>
        private const string DefaultService = "Service";

        /// <summary> Initializes a new instance of the <see cref="ResponseStatus" /> class. </summary>
        /// <param name="modelState">State of the model.</param>
        public ResponseStatus(ModelStateDictionary modelState)
        {
            Code = ErrorSuccessCode;
            Type = ResponsePayloadType.BusinessException;
            LoadErrors(modelState, ResponsePayloadType.BusinessException, null);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponseStatus" /> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="source">The error service.</param>
        public ResponseStatus(ModelStateDictionary modelState, string source)
        {
            Code = ErrorSuccessCode;
            Type = ResponsePayloadType.BusinessException;
            LoadErrors(modelState, ResponsePayloadType.BusinessException, source);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponseStatus" /> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="source">The error service.</param>
        /// <param name="type">Type of the return.</param>
        /// <param name="errorCode">The error code.</param>
        public ResponseStatus(ModelStateDictionary modelState,
            string source, ResponsePayloadType type,
            int errorCode)
        {
            Code = errorCode;
            Type = type;
            LoadErrors(modelState, ResponsePayloadType.BusinessException, source);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseStatus" /> class.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="source">The error service.</param>
        /// <param name="type">The type.</param>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public ResponseStatus(ModelStateDictionary modelState,
            string source, ResponsePayloadType type,
            int code, string message)
        {
            Code = code;
            Type = type;
            Message = message;
            LoadErrors(modelState, ResponsePayloadType.BusinessException, source);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponseStatus" /> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="source">The error service.</param>
        /// <param name="type">The type.</param>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public ResponseStatus(ModelStateDictionary modelState,
            string source, ResponsePayloadType type,
            int code, string message, IList<ErrorResponse> errors)
        {
            Code = code;
            Type = type;
            Message = message;
            Errors = errors;
            LoadErrors(modelState, ResponsePayloadType.BusinessException, source);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponseStatus" /> class. </summary>
        /// <param name="type">The type.</param>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public ResponseStatus(
            ResponsePayloadType type,
            int code, string message, IList<ErrorResponse> errors)
        {
            Code = code;
            Type = type;
            Message = message;
            Errors = errors;
        }

        /// <summary> Initializes a new instance of the <see cref="ResponseStatus" /> class. </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="type">The type.</param>

        public ResponseStatus(string message,
            int code, ResponsePayloadType type)
        {
            Code = code;
            Type = type;
            Message = message;
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

        /// <summary> The errors </summary>
        [DataMember(Name = "errors", IsRequired = false)]
        public IList<ErrorResponse> Errors = new List<ErrorResponse>();

        /// <summary> Loads the errors. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="type">The type.</param>
        /// <param name="underlyingService">The underlying service.</param>
        private void LoadErrors(ModelStateDictionary modelState, ResponsePayloadType type, string underlyingService = DefaultService)
        {
            foreach (var stateKeyValuePair in modelState)
            {

                Errors.Add(
                    new ErrorResponse(stateKeyValuePair.Value?.Errors != null ? stateKeyValuePair.Value.Errors.First().ErrorMessage : string.Empty, type)
                    {
                        Service = underlyingService,
                        Property = stateKeyValuePair.Key
                    });
            }
        }

        /// <summary> Returns a <see cref="string" /> that represents this instance. </summary>
        /// <returns> A <see cref="string" /> that represents this instance. </returns>
        public override string ToString()
        {
            return $"{nameof(Errors)}: {Errors}, {nameof(Code)}: " +
                   $"{Code}, {nameof(Type)}: {Type}, {nameof(Message)}: " +
                   $"{Message}";
        }
    }
}
