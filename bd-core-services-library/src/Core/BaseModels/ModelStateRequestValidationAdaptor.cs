using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace BD.Core.BaseModels
{
    /// <summary> The model request validation adaptor. </summary>
    [DataContract]
    public class ModelStateRequestValidationAdaptor
    {
        /// <summary> Initializes a new instance of the <see cref="ModelStateRequestValidationAdaptor" /> class. </summary>
        /// <param name="modelState">State of the model.</param>
        public ModelStateRequestValidationAdaptor(ModelStateDictionary modelState)
        {
            //Model State Error are by defaulted to business Exception
            ResponseStatus = new ResponseStatus(modelState)
            {
                Type = ResponsePayloadType.BusinessException
            };
        }

        /// <summary> Initializes a new instance of the <see cref="ModelStateRequestValidationAdaptor" /> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="underlyingService">The underlying service.</param>
        /// <param name="responsePayloadType">Type of the return.</param>
        /// <param name="code">The returning status code.</param>
        public ModelStateRequestValidationAdaptor(ModelStateDictionary modelState, string underlyingService, ResponsePayloadType responsePayloadType, int code)
        {
            ResponseStatus = new ResponseStatus(modelState, underlyingService, responsePayloadType, code);
        }

        /// <summary> Initializes a new instance of the <see cref="ModelStateRequestValidationAdaptor" /> class. </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="underlyingService">The underlying service.</param>
        public ModelStateRequestValidationAdaptor(ModelStateDictionary modelState, string underlyingService)
        {
            ResponseStatus = new ResponseStatus(modelState, underlyingService);
        }

        /// <summary> Gets or sets the status. </summary>
        /// <value> The status. </value>
        [DataMember(Name = "status")]
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary> Gets or sets the response payload. In case of error this will be null. </summary>
        /// <value> The response payload. </value>
        [DataMember(Name = "responsePayload")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ResponsePayload { get; set; }

        /// <summary> Returns a <see cref="string" /> that represents this instance. </summary>
        /// <returns> A <see cref="string" /> that represents this instance. </returns>
        public override string ToString() => $"{nameof(ResponseStatus)}: {ResponseStatus}, {nameof(ResponsePayload)}: {ResponsePayload}";
    }
}