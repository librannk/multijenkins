using BD.Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SiteConfiguration.API.FacilityConfiguration.Abstractions;
using SiteConfiguration.API.FacilityConfiguration.Constants;
using SiteConfiguration.API.FacilityConfiguration.Controllers;
using SiteConfiguration.API.FacilityConfiguration.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.FacilityConfigurations
{
    public class FacilityLogisticsConfigurationControllerUnitTest
    {
        #region Private Fields
        private readonly Mock<IFacilityLogisticsConfiguration> _business;
        private readonly Mock<ILogger<FacilityLogisticsConfigurationController>> _logger;
        private readonly FacilityLogisticsConfigurationController _controller;
        private readonly IExecutionContextAccessor accessor;
        #endregion

        public FacilityLogisticsConfigurationControllerUnitTest()
        {
            _business = new Mock<IFacilityLogisticsConfiguration>();
            _logger = new Mock<ILogger<FacilityLogisticsConfigurationController>>();

            string facilitykey = "BF521211-CEAF-4DCA-82C7-40446D4C46ED";
            string facilityCode = "BF521211-CEAF-4DCA-82C7-40446D4C46ED";

            accessor = new ExecutionContextAccessor() { Current = new Context() { Facility = new FacilityContext(facilitykey, facilityCode) } };
            _controller = new FacilityLogisticsConfigurationController(_business.Object, accessor, _logger.Object);
            //
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["X-Requested-With"]).Returns("XMLHttpRequest");

            var context = new Mock<HttpContext>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            //
            RouteValueDictionary routeDictionary = new RouteValueDictionary();
            routeDictionary.Add("facilitykey", Guid.Parse(facilitykey));
            _controller.ControllerContext = new ControllerContext(new ActionContext(context.Object, new RouteData(routeDictionary), new ControllerActionDescriptor()));
        }

        #region Post FacilityConfiguration
        [Fact]
        public async Task FacilitySpecificConfigurationController_CreateAsync_ShouldReturnCreated()
        {
            //Arrange           
            _business.Setup(m => m.CreateFacilitySpecificConfigurationAsync(It.IsAny<TransactionQueueConfigurationRequest>())).Returns(Task.FromResult(default(object)));

            //Act
            var result = (await _controller.CreateAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(201, ((CreatedResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_CreateAsync_ShouldReturnBadRequestOnInvalidModelState()
        {
            //Arrange  
            _controller.ModelState.AddModelError("VerifyQuantityFlag", "Required");
            _business.Setup(m => m.CreateFacilitySpecificConfigurationAsync(It.IsAny<TransactionQueueConfigurationRequest>())).Returns(Task.FromResult(default(object)));

            //Act
            var result = (await _controller.CreateAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_CreateAsync_ShouldReturnBadRequestOnArgumentNullException()
        {
            //Arrange  
            _business.Setup(m => m.CreateFacilitySpecificConfigurationAsync(It.IsAny<TransactionQueueConfigurationRequest>())).ThrowsAsync(new ArgumentNullException());
            //Act
            var result = (await _controller.CreateAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }
        #endregion 

        #region PostFacilityExtensionAsync
        [Fact]
        public async Task FacilitySpecificConfigurationController_PostFacilityExtensionAsync_ShouldReturnCreated()
        {
            BusinessResponse businessResponse = new BusinessResponse() { IsSuccess=true,Message= "Facility Extension Created Successfully" };
            //Arrange           
            _business.Setup(m => m.CreateFacilityExtensionAsync(It.IsAny<FacilityLogisticsConfigurationExtension>())).ReturnsAsync(businessResponse);

            //Act
            var result = (await _controller.PostFacilityExtensionAsync(new FacilityLogisticsConfigurationExtension()));

            //Assert
            Assert.Equal(201, ((CreatedResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_PostFacilityExtensionAsync_ShouldReturnBadRequestOnInvalidModelState()
        {
            //Arrange  
            _controller.ModelState.AddModelError("VerifyQuantityFlag", "Required");
            _business.Setup(m => m.CreateFacilityExtensionAsync(It.IsAny<FacilityLogisticsConfigurationExtension>())).Returns(Task.FromResult((BusinessResponse)null));

            //Act
            var result = (await _controller.PostFacilityExtensionAsync(new FacilityLogisticsConfigurationExtension()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_PostFacilityExtensionAsync_ShouldReturnBadRequestOnArgumentNullException()
        {
            //Arrange  
            _business.Setup(m => m.CreateFacilityExtensionAsync(It.IsAny<FacilityLogisticsConfigurationExtension>())).ThrowsAsync(new ArgumentNullException());
            //Act
            var result = (await _controller.PostFacilityExtensionAsync(new FacilityLogisticsConfigurationExtension()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }
        #endregion

        #region PutfacilityConfigurationAsync
        [Fact]
        public async Task FacilitySpecificConfigurationController_PutfacilityConfigurationAsync_ShouldReturnOkOnSuccessfulUpdate()
        {
            BusinessResponse businessResponse = new BusinessResponse() { IsSuccess = true, Message = BusinessResponseMessages.LogisticsConfigurationUpdated };
            //Arrange           
            _business.Setup(m => m.UpdateFacilityConfigAsync(It.IsAny<TransactionQueueConfigurationRequest>())).ReturnsAsync(businessResponse);

            //Act
            var result = (await _controller.PutfacilityConfigurationAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(200, ((OkObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_PutfacilityConfigurationAsync_ShouldReturnBadRequrestOnUpdateFailure()
        {
            BusinessResponse businessResponse = new BusinessResponse() { IsSuccess = false, Message = BusinessError.InvalidInput};
            //Arrange           
            _business.Setup(m => m.UpdateFacilityConfigAsync(It.IsAny<TransactionQueueConfigurationRequest>())).ReturnsAsync(businessResponse);

            //Act
            var result = (await _controller.PutfacilityConfigurationAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }
        [Fact]
        public async Task FacilitySpecificConfigurationController_PutfacilityConfigurationAsync_ShouldReturnBadRequrestOnInvalidModel()
        {
            //Arrange           
            _controller.ModelState.AddModelError("VerifyQuantityFlag", "Required");
            //Act
            var result = (await _controller.PutfacilityConfigurationAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_PutfacilityConfigurationAsync_ShouldReturnBadRequestOnDbUpdateException()
        {
            //Arrange  
            _business.Setup(m => m.UpdateFacilityConfigAsync(It.IsAny<TransactionQueueConfigurationRequest>())).ThrowsAsync(new DbUpdateException(BusinessError.RecordNotFound,new Exception()));

            //Act
            var result = (await _controller.PutfacilityConfigurationAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_PutfacilityConfigurationAsync_ShouldReturnBadRequestOnArgumentNullException()
        {
            //Arrange  
            _business.Setup(m => m.UpdateFacilityConfigAsync(It.IsAny<TransactionQueueConfigurationRequest>())).ThrowsAsync(new ArgumentNullException());
            //Act
            var result = (await _controller.PutfacilityConfigurationAsync(new TransactionQueueConfigurationRequest()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }
        #endregion


        #region PutFacilityExtensionAsync
        [Fact]
        public async Task FacilitySpecificConfigurationController_PutFacilityExtensionAsync_ShouldReturnUpdated()
        {
            BusinessResponse businessResponse = new BusinessResponse() { IsSuccess = true, Message = "Logistics Configuration Updated Successfully." };
            //Arrange           
            _business.Setup(m => m.UpdateFacilityExtensionAsync(It.IsAny<FacilityLogisticsConfigurationExtension>())).ReturnsAsync(businessResponse);

            //Act
            var result = (await _controller.PutFacilityExtensionAsync(new FacilityLogisticsConfigurationExtension()));

            //Assert
            Assert.Equal(200, ((OkObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_PutFacilityExtensionAsync_ShouldReturnBadRequestOnInvalidModelState()
        {
            //Arrange  
            _controller.ModelState.AddModelError("VerifyQuantityFlag", "Required");
            _business.Setup(m => m.UpdateFacilityExtensionAsync(It.IsAny<FacilityLogisticsConfigurationExtension>())).Returns(Task.FromResult((BusinessResponse)null));

            //Act
            var result = (await _controller.PutFacilityExtensionAsync(new FacilityLogisticsConfigurationExtension()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task FacilitySpecificConfigurationController_PutFacilityExtensionAsync_ShouldReturnBadRequestOnArgumentNullException()
        {
            //Arrange  
            _business.Setup(m => m.UpdateFacilityExtensionAsync(It.IsAny<FacilityLogisticsConfigurationExtension>())).ThrowsAsync(new ArgumentNullException());
            //Act
            var result = (await _controller.PutFacilityExtensionAsync(new FacilityLogisticsConfigurationExtension()));

            //Assert
            Assert.Equal(400, ((BadRequestObjectResult)result.Result).StatusCode);
        }
        #endregion

    }
}
