using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace BD.Core.BaseModels
{
    /// <summary> The repsonse Payload implementation. </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class ResponsePayload<T>
    {
        private const int DefaultSuccessCode = 20000;

        /// <summary> Gets or sets the response. </summary>
        /// <value> The response. </value>
        [DataMember(Name = "responsePayload")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Response { get; set; }

        /// <summary> The status </summary>
        [DataMember(Name = "status")]
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary> Initializes a new instance of the <see cref="ResponsePayload{T}"/> class. </summary>
        public ResponsePayload()
        {
            ResponseStatus = new ResponseStatus
            {
                Code = DefaultSuccessCode,
                Type = ResponsePayloadType.Success,
                Message = "Success",
                Errors = null
            };
        }

        /// <summary> Initializes a new instance of the <see cref="ResponsePayload{T}"/> class. </summary>
        /// <param name="modelState">State of the model.</param>
        public ResponsePayload(ModelStateDictionary modelState)
        {
            ResponseStatus = new ResponseStatus(modelState);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponsePayload{T}"/> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="source">The source.</param>
        public ResponsePayload(ModelStateDictionary modelState, string source)
        {
            ResponseStatus = new ResponseStatus(modelState, source);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponsePayload{T}"/> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="source">The source.</param>
        /// <param name="type">Type of the return.</param>
        /// <param name="code">The error code.</param>
        public ResponsePayload(ModelStateDictionary modelState, string source, ResponsePayloadType type, int code)
        {
            ResponseStatus = new ResponseStatus(modelState, source, type, code);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponsePayload{T}"/> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="source">The source.</param>
        /// <param name="type">Type of the return.</param>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        public ResponsePayload(ModelStateDictionary modelState, string source, ResponsePayloadType type, int code, string message)
        {
            ResponseStatus = new ResponseStatus(modelState, source, type, code, message);
        }

        /// <summary> Initializes a new instance of the <see cref="ResponsePayload{T}"/> class. </summary>'
        /// <param name="message">The error message.</param>
        /// <param name="code">The error code.</param>
        /// <param name="type">Type of the return.</param>
        public ResponsePayload(string message, int code, ResponsePayloadType type)
        {
            ResponseStatus = new ResponseStatus(message, code, type);
        }

        /// <summary> Returns a <see cref="string" /> that represents this instance. </summary>
        /// <returns> A <see cref="string" /> that represents this instance. </returns>
        public override string ToString() => $"{nameof(Response)}: {Response}, {nameof(ResponseStatus)}: {ResponseStatus}";
    }
}
