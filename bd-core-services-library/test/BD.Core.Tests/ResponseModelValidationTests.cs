
using Xunit;
using BD.Core.BaseModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BD.Core.Tests
{
    public class ResponseModelValidationTests
    {
        ModelStateRequestValidationAdaptor _modelStateRequestValidationAdaptor;
        public ResponseModelValidationTests()
        {
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError("Error1", "TestError");
            _modelStateRequestValidationAdaptor = new ModelStateRequestValidationAdaptor(modelStateDictionary);
        }
        [Fact]
        public void TestForErrorCount()
        {
            Assert.True(_modelStateRequestValidationAdaptor.ResponseStatus.Errors.Count == 1);
        }
        [Fact]
        public void TestForErrorMessage()
        {
            Assert.True(_modelStateRequestValidationAdaptor.ResponseStatus.Errors[0].Message == "TestError");
        }

        [Fact]
        public void ModelRequestValidationWithService()
        {
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError("Error1", "TestError");
            _modelStateRequestValidationAdaptor = new ModelStateRequestValidationAdaptor(modelStateDictionary, "Sample Service");
            Assert.IsType<ResponseStatus>(_modelStateRequestValidationAdaptor.ResponseStatus);
        }

        [Fact]
        public void ModelRequestValidationWithServiceAndError()
        {
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError("Error1", "TestError");
            _modelStateRequestValidationAdaptor = new ModelStateRequestValidationAdaptor(modelStateDictionary, "Sample Service", ResponsePayloadType.BusinessException, 400);
            Assert.IsType<ResponseStatus>(_modelStateRequestValidationAdaptor.ResponseStatus);
            Assert.Equal(400, _modelStateRequestValidationAdaptor.ResponseStatus.Code);
            Assert.Null(_modelStateRequestValidationAdaptor.ResponsePayload);
        }
    }
}
