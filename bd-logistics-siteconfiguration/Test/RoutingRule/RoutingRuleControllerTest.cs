using Microsoft.AspNetCore.Mvc;
using Moq;
using SiteConfiguration.API.Common;
using SiteConfiguration.API.RoutingRules.Abstractions;
using SiteConfiguration.API.RoutingRules.Controllers;
using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Test.RoutingRule
{
    public class RoutingRuleControllerTest
    {

        #region Private readonly properties
        private readonly Mock<IRoutingRuleManager> routingBusiness;
        private readonly Mock<ILogger<RoutingRuleController>> logger;
        private readonly RoutingRuleController controller;
        private readonly Mock<HttpContext> context;
        private readonly Mock<HttpRequest> request;
        Dictionary<string, string> headers = new Dictionary<string, string>();
        #endregion

        #region Constructor
        public RoutingRuleControllerTest()
        {
            routingBusiness = new Mock<IRoutingRuleManager>();
            logger = new Mock<ILogger<RoutingRuleController>>();
            controller = new RoutingRuleController(logger.Object, routingBusiness.Object);
            request = new Mock<HttpRequest>();
            context = new Mock<HttpContext>();

        }
        #endregion

        #region Test Cases for GetAllRoutingRules

        [Fact]
        public async Task GetAllRoutingRules_GetAll_ReturnOkResult()
        {
            //Arrange
            var page = 10;
            var pageSize = 10;
            string searchString = "";
            SetValues();

            routingBusiness.Setup(x => x.GetAllRoutingRule(page, pageSize, searchString)).Returns(FakeDataToGetAllRoutingRules);

            //Act
            var mockresponse = await controller.GetRoutingRules(page, pageSize, searchString);

            //Assert
            Assert.IsType<OkObjectResult>(mockresponse);
        }
        private IEnumerable<RoutingRulesResult> FakeDataToGetAllRoutingRules()
        {
            var result = new List<RoutingRulesResult>();

            result.Add(new RoutingRulesResult
            {
                RoutingRuleKey = Utility.GetNewGuid(),
                RoutingRuleName = "Test",
                Schedules = "Test Schedules",
                Destinations = "Test Destination",
                TranPriorities = "Test Priority"
            });

            return result;
        }
        [Fact]
        public async Task Task_GetAllRoutingRule_Return_NotFoudResult()
        {
            //Arrange
            var page = 10;
            var pageSize = 10;
            string searchString = "";
            SetValues();

            routingBusiness.Setup(x => x.GetAllRoutingRule(page, pageSize, searchString)).Returns(new List<RoutingRulesResult>());

            //Act
            var mockresponse = await controller.GetRoutingRules(page, pageSize, searchString);

            //Assert
            Assert.IsType<BadRequestObjectResult>(mockresponse);

        }

        [Fact]
        public async Task Task_GetAllRoutingRule_Return_BadRequestResult()
        {
            //Arrange
            var page = 10;
            var pageSize = 10;
            string searchString = "";

            routingBusiness.Setup(x => x.GetAllRoutingRule(page, pageSize, searchString)).Throws(new Exception());

            //Act
            var response = await controller.GetRoutingRules(page, pageSize, searchString) as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }

        #endregion

        #region Test Cases for GetRoutingRuleById

        [Fact]
        public async Task Task_GetRoutingRuleById_Return_OkResult()
        {
            //Arrange
            SetValues();

            routingBusiness.Setup(x => x.GetByID(It.IsAny<Guid>())).Returns(FakeDataTorGetRoutingRuleById);

            //Act
            var mockresponse = await controller.GetRoutingRule();

            //Assert
            Assert.IsType<OkObjectResult>(mockresponse);
        }

        private RoutingRulesById FakeDataTorGetRoutingRuleById()
        {
            var result = new RoutingRulesById()
            {

                RoutingRuleKey = Utility.GetNewGuid(),
                RoutingRuleName = "Test",
                RoutingRuleDestinations = new List<Guid> { new Guid() },
                RoutingRuleScheduleTimings = new List<Guid> { new Guid() },
                RoutingRuleTranPriorities = new List<Guid> { new Guid() }
            };

            return result;
        }

        [Fact]
        public async Task Task_GetRoutingRuleById_Return_NotFoudResult()
        {
            //Arrange
            SetValues();

            RoutingRulesById obj = new RoutingRulesById();
            obj = null;

            routingBusiness.Setup(x => x.GetByID(It.IsAny<Guid>())).Returns(obj);

            //Act
            var mockresponse = await controller.GetRoutingRule();

            //Assert
            Assert.IsType<BadRequestObjectResult>(mockresponse);
        }

        [Fact]
        public async Task Task_GetRoutingRuleById_Return_BadRequestResult()
        {
            //Arrange 
            routingBusiness.Setup(x => x.GetByID(It.IsAny<Guid>()));

            //Act
            var response = await controller.GetRoutingRule() as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }

        #endregion

        #region Test Cases for DeleteRoutingRule

        [Fact]
        public async Task Task_DeleteRoutingRule_Return_OkResult()
        {
            //Arrange           

            SetValues();

            routingBusiness.Setup(x => x.DeleteRoutingRule(It.IsAny<Guid>(), headers)).Returns(new BusinessResponse
            {
                IsSuccess = true
            });
            //Act
            var mockresponse = await controller.Delete();

            //Assert
            Assert.IsType<OkObjectResult>(mockresponse);
        }

        [Fact]
        public async Task Task_DeleteRoutingRule_Return_NotFoudResult()
        {
            //Arrange        
            SetValues();
            routingBusiness.Setup(x => x.DeleteRoutingRule(It.IsAny<Guid>(), headers)).Returns(new BusinessResponse
            {
                IsSuccess = false
            });

            //Act
            var response = await controller.Delete();

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Task_DeleteRoutingRule_Return_BadRequestResult()
        {
            //Arrange                 
            SetValues();

            routingBusiness.Setup(x => x.DeleteRoutingRule(It.IsAny<Guid>(), headers)).Throws(new Exception());

            //Act
            var response = await controller.Delete() as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }

        #endregion

        #region Test Cases for AddRoutingRule

        [Fact]
        public async Task Task_AddRoutingRule_Return_OkResult()
        {
            //Arrange            
            SetValues();

            var data = FakeDataForPostRoutingRule();

            routingBusiness.Setup(x => x.AddRoutingRule(data, headers)).Returns(new BusinessResponse
            {
                IsSuccess = true
            });

            //Act
            var mockresponse = await controller.Post(data);

            //Assert
            Assert.IsType<CreatedAtActionResult>(mockresponse);
        }

        [Fact]
        public async Task Task_AddRoutingRule_Return_NotFoudResult()
        {
            //Arrange            
            SetValues();

            var data = FakeDataForPostRoutingRule();

            routingBusiness.Setup(x => x.AddRoutingRule(data, headers)).Returns(new BusinessResponse
            {
                IsSuccess = false
            });

            //Act
            var mockresponse = await controller.Post(data);

            //Assert
            Assert.IsType<BadRequestObjectResult>(mockresponse);
        }

        [Fact]
        public async Task Task_AddRoutingRule_Return_BadRequestResult()
        {
            //Arrange                 
            SetValues();

            var data = FakeDataForPostRoutingRule();

            routingBusiness.Setup(x => x.AddRoutingRule(data,headers)).Throws(new Exception());

            //Act
            var response = await controller.Post(data) as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }

        #endregion

        #region Test Cases for UpdateRoutingRule

        [Fact]
        public async Task Task_UpdateRoutingRule_Return_OkResult()
        {
            //Arrange            
            SetValues();

            var data = FakeDataForPostRoutingRule();

            routingBusiness.Setup(x => x.UpdateRoutingRule(data, It.IsAny<Guid>(), headers)).Returns(new BusinessResponse
            {
                IsSuccess = true
            });

            //Act
            var mockresponse = await controller.Put(data);

            //Assert
            Assert.IsType<OkObjectResult>(mockresponse);
        }

        [Fact]
        public async Task Task_UpdateRoutingRule_Return_NotFoudResult()
        {
            //Arrange            
            SetValues();

            var data = FakeDataForPostRoutingRule();
            routingBusiness.Setup(x => x.UpdateRoutingRule(data,It.IsAny<Guid>(), headers)).Returns(new BusinessResponse
            {
                IsSuccess = false
            });

            //Act
            var mockresponse = await controller.Put(data);

            //Assert
            Assert.IsType<BadRequestObjectResult>(mockresponse);
        }

        [Fact]
        public async Task Task_UpdateRoutingRule_Return_BadRequestResult()
        {
            //Arrange     
            var data = FakeDataForPostRoutingRule();

            routingBusiness.Setup(x => x.UpdateRoutingRule(data,It.IsAny<Guid>(), headers)).Throws(new Exception());

            //Act
            var response = await controller.Put(data) as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }

        private RoutingRuleRequest FakeDataForPostRoutingRule()
        {
            RoutingRuleRequest r = new RoutingRuleRequest();
            var listRequestRoutingRuleSchedule = new List<RequestRoutingRuleSchedule>
            {
                new RequestRoutingRuleSchedule() { ScheduleKey = Utility.GetNewGuid()}
            };
            var listRequestRoutingRuleDestination = new List<RequestRoutingRuleDestination>
            {
                new RequestRoutingRuleDestination(){DestinationKey = Utility.GetNewGuid()}
            };

            var listRequestRoutingRuleTranPriority = new List<RequestRoutingRuleTranPriority>
            {
                new RequestRoutingRuleTranPriority(){TranPriorityKey = Utility.GetNewGuid()}
            };

            r.RoutingRuleName = "Test";
            r.SearchCriteriaGranularityLevel = 0;
            r.RoutingRuleDestinations = listRequestRoutingRuleDestination;
            r.RoutingRuleSchedules = listRequestRoutingRuleSchedule;
            r.RoutingRuleTranPriority = listRequestRoutingRuleTranPriority;
            return r;
        }

        private void SetValues()
        {
            RouteValueDictionary routeDictionary = new RouteValueDictionary();
            routeDictionary.Add("facilitykey", Utility.GetNewGuid());
            routeDictionary.Add("rountingRuleKey", Utility.GetNewGuid());
            routeDictionary.Add("headers", headers);
            //var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["X-Requested-With"]).Returns("XMLHttpRequest");

            //var context = new Mock<HttpContext>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            controller.ControllerContext = new ControllerContext(new ActionContext(context.Object, new RouteData(routeDictionary), new ControllerActionDescriptor()));
        }

        #endregion
    }
}
