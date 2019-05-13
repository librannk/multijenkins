///-------------------------------------------------------------------------
/// Copyright ©  2019 Becton Dickinson Corporation. All rights reserved.
/// Warning: This computer program is protected by  Copyright   law and
/// international treaties. Unauthorized reproduction or distribution
/// of this program, or any portion of it, may result in severe civil
/// and criminal penalties,  and will be prosecuted to the maximum
/// extent possible under the law. 

using Moq;
using System.Collections.Generic;
using CCEProxy.API.BusinessLayer.Contracts;
using CCEProxy.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using CCEProxy.API.Entity;

namespace CCEProxyUnitTests.Controller
{
    /// <summary>
    /// Unit Tests ForProcessIncomingController
    /// </summary>
    public class IncomingRequestControllerUnitTest
    {
        #region Private readonly properties
        private readonly Mock<IRequestManager> requestManager;
        private readonly Mock<ILogger<IncomingRequestController>> logger;
        private readonly IncomingRequestController controller;
        #endregion

        #region Constructor
        public IncomingRequestControllerUnitTest()
        {
            requestManager = new Mock<IRequestManager>();
            logger = new Mock<ILogger<IncomingRequestController>>();
            controller = new IncomingRequestController(
                requestManager.Object, 
                logger.Object);

        }

        #endregion

        #region Test case for ProcessIncomingRequest Method 
        [Fact]
        public async Task ProcesIncomingRequest_ModelStateValid_ShouldReturnAccepted()
        {
            //Arrange
            IncomingRequest incomingRequest = new IncomingRequest();

            requestManager.Setup(x => x.InsertIncomingRequest(incomingRequest)).Returns(Task.FromResult("5c8a01a5efe48745ac3c98c6"));
            requestManager.Setup(x => x.ProcessIncomingRequest(It.IsAny<IncomingRequest>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(It.IsAny<string>()));

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["X-Requested-With"]).Returns("XMLHttpRequest");

            var context = new Mock<HttpContext>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            controller.ControllerContext = new ControllerContext(new ActionContext(context.Object, new RouteData(), new ControllerActionDescriptor()));

            //Act
            var mockResponse = await controller.ProcessIncomingRequest(incomingRequest) as AcceptedResult;

            //Assert
            Assert.Equal(202, mockResponse.StatusCode);
        }

        [Fact]
        public async Task ProcessIncomingRequest_InvalidModel_ShouldReturnBadRequest()
        {
            //Arrange
            controller.ModelState.AddModelError("invalid","model is not valid");
            IncomingRequest incomingRequest = new IncomingRequest();

            //Act
            var mockResponse = await controller.ProcessIncomingRequest(incomingRequest) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, mockResponse.StatusCode);

        }

        [Fact]
        public async Task ProcessIncomingRequest_Exception_ShouldReturnInternalServerError()
        {
            //Arrange
            requestManager.Setup(x => x.InsertIncomingRequest(It.IsAny<IncomingRequest>())).Throws(new System.Exception());
            IncomingRequest incomingRequest = new IncomingRequest();

            //Act
            var response = await controller.ProcessIncomingRequest(incomingRequest) as StatusCodeResult;

            //Assert
            Assert.Equal(500, response.StatusCode);
        }

        [Theory]
        [InlineData("xyz")]
        public async Task ProcessIncomingRequest_ValidModelNullResponse_ShouldReturnBadRequestResponse(string expectedResponse)
        {
            IncomingRequest incomingRequest = new IncomingRequest();

            requestManager.Setup(x => x.InsertIncomingRequest(incomingRequest)).Returns(Task.FromResult("5c8a01a5efe48745ac3c98c6"));
            var mockResponse = expectedResponse;
            requestManager.Setup(x => x.ProcessIncomingRequest(It.IsAny<IncomingRequest>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(mockResponse));

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["X-Requested-With"]).Returns("XMLHttpRequest");

            var context = new Mock<HttpContext>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            controller.ControllerContext = new ControllerContext(new ActionContext(context.Object, new RouteData(), new ControllerActionDescriptor()));

            //Act
            var response = await controller.ProcessIncomingRequest(incomingRequest) as BadRequestObjectResult;

            //Assert
            Assert.Equal(mockResponse, response.Value);
        }
        #endregion
    }
}
