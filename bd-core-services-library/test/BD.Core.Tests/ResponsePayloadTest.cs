
using Xunit;
using BD.Core.BaseModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BD.Core.Tests
{
    public class ResponsePayloadTest
    {
        private ResponsePayload<string> _responsePayload;

        [Fact]
        public void ResponsePayloadDefaultConstructor()
        {
            _responsePayload = new ResponsePayload<string>();
            Assert.Equal(20000, _responsePayload.ResponseStatus.Code);
        }

        [Fact]
        public void ResponsePayloadModelState()
        {
            var modelState = new ModelStateDictionary();
            _responsePayload = new ResponsePayload<string>(modelState);
            Assert.IsType<ResponseStatus>(_responsePayload.ResponseStatus);
        }

        [Fact]
        public void ResponsePayloadModelStateService()
        {
            var modelState = new ModelStateDictionary();
            _responsePayload = new ResponsePayload<string>(modelState, string.Empty);
            Assert.IsType<ResponseStatus>(_responsePayload.ResponseStatus);
        }

        [Fact]
        public void ResponsePayloadModelStateServiceReturnType()
        {
            var error = new ErrorResponse("Sample ErrorResponse", 400, ResponsePayloadType.BusinessException, "Sample Service");
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("SampleError", "Sample ErrorResponse Message");
            _responsePayload = new ResponsePayload<string>(modelState, "Sample Service", ResponsePayloadType.BusinessException, 500);
            _responsePayload.ResponseStatus.Errors.Add(error);
            Assert.IsType<ResponseStatus>(_responsePayload.ResponseStatus);
            Assert.Equal(ResponsePayloadType.BusinessException, _responsePayload.ResponseStatus.Type);
            Assert.Equal(500, _responsePayload.ResponseStatus.Code);
            Assert.Equal(2, _responsePayload.ResponseStatus.Errors.Count);
        }
    }
}
