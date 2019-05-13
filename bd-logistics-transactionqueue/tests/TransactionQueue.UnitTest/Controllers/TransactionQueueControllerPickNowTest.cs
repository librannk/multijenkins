using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using TransactionQueue.API.Application.BussinessLayer.Abstraction;
using TransactionQueue.API.Controllers;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.Business.Models;
using Xunit;

namespace TransactionQueue.UnitTest.Controllers
{
   public  class TransactionQueueControllerPickNowTest
    {
        #region Fields
        private readonly Mock<ILogger<TransactionQueueController>> _logger;
        private readonly Mock<ITransactionQueueManager> _transactionQueueManager;
        private readonly Mock<ITransactionQueueBussiness> _transactionQueueBussiness;
        private readonly Mock<IQueueFilter> _queueFilter;
        private readonly TransactionQueueController _controller;
        #endregion

        #region Constructor
        public TransactionQueueControllerPickNowTest()
        {
             _logger=new Mock<ILogger<TransactionQueueController>>();
            _transactionQueueManager=new  Mock<ITransactionQueueManager>() ;
            _transactionQueueBussiness=new  Mock<ITransactionQueueBussiness>() ;
            _queueFilter = new Mock<IQueueFilter>();
            _controller = new TransactionQueueController(_logger.Object, _transactionQueueManager.Object, _transactionQueueBussiness.Object, _queueFilter.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["X-Requested-With"]).Returns("XMLHttpRequest");

            var context = new Mock<HttpContext>();
            context.SetupGet(x => x.Request).Returns(request.Object);
             RouteValueDictionary routeDictionary = new RouteValueDictionary();
            _controller.ControllerContext = new ControllerContext(new ActionContext(context.Object, new RouteData(routeDictionary), new ControllerActionDescriptor()));

        }
        #endregion

        #region Test Cases


        [Fact]
        public async Task PickNow_TQKey_NotValid_Test()
        {
            //Arrange
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccess = false, Message = "Not found to Activate" };

            _transactionQueueBussiness.Setup(x => x.PickNow(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<PickNow>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);
            //Act
            var notFoundObjectResult = (BadRequestObjectResult)(await _controller.PickNow("activeTransactioQueueKey", 1, new PickNow() { }));
            //Assert
            Assert.Equal(400, notFoundObjectResult.StatusCode);

        }
        [Fact]
        public async Task PickNow_TQKey_Valid_Test()
        {
            //Arrange
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccess = false, Message = "Not found to Activate" };

            _transactionQueueBussiness.Setup(x => x.PickNow(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<PickNow>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);
            //Act
            var OkObjectResult = (OkObjectResult)(await _controller.PickNow("5cd52509328dc42aa4e8cda2", 1, new PickNow() { }));
            //Assert
            Assert.Equal(200, OkObjectResult.StatusCode);

        }

        [Fact]
        public async Task PickNow_NotFound_Test()
        {
            //Arrange
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccess=false,Message="Not found to Activate",StatusCode=404};

            _transactionQueueBussiness.Setup(x => x.PickNow(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<PickNow>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);
            //Act
            var notFoundObjectResult=(NotFoundObjectResult)(await _controller.PickNow("5cd52509328dc42aa4e8cda2", 1, new PickNow() { }));
            //Assert
            Assert.Equal(404, notFoundObjectResult.StatusCode);

        }
        [Fact]
        public async Task PickNow_BadRequest_Test()
        {
            //Arrange
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccess = false, Message = "Not found to Activate", StatusCode = 400 };

            _transactionQueueBussiness.Setup(x => x.PickNow(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<PickNow>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);
            //Act
            var BadRequestObjectResult = (BadRequestObjectResult)(await _controller.PickNow("5cd52509328dc42aa4e8cda2", 1, new PickNow() { }));
            //Assert
            Assert.Equal(400, BadRequestObjectResult.StatusCode);

        }

        [Fact]
        public async Task PickNow_Activated_Test()
        {
            //Arrange
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccess = true, Message = "Activated", StatusCode = 200 };

            _transactionQueueBussiness.Setup(x => x.PickNow(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<PickNow>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);
            //Act
            var okObjectResult = (OkObjectResult)(await _controller.PickNow("5cd52509328dc42aa4e8cda2", 1, new PickNow() { }));
            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);

        }


        [Fact]
        public async Task PickNow_ThrowException_Test()
        {
            //Arrange
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccess = true, Message = "Activated" };

            _transactionQueueBussiness.Setup(x => x.PickNow(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<PickNow>(), It.IsAny<Dictionary<string, string>>())).ThrowsAsync(new Exception());
            //Act
            var statusCodeResult = (StatusCodeResult)(await _controller.PickNow("5cd52509328dc42aa4e8cda2", 1, new PickNow() { }));
            //Assert

            Assert.Equal(500, statusCodeResult.StatusCode);

        }


        #endregion


    }
}
