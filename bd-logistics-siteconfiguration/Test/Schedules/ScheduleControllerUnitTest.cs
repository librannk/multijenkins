using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Core.BaseModels;
using BD.Core.ResiliencePolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Controllers;
using SiteConfiguration.API.Schedule.Exceptions;
using SiteConfiguration.API.Schedule.Models;
using Xunit;

namespace Test.Schedules
{
    public class ScheduleControllerUnitTest
    {
        #region Private readonly properties
        private readonly Mock<IScheduleBusiness> scheduleBusiness;
        private readonly Mock<ILogger<ScheduleController>> logger;
        private readonly ScheduleController controller;
        private readonly HttpClientFactory _factory;
        #endregion

        #region Constructor
        public ScheduleControllerUnitTest()
        {
            scheduleBusiness = new Mock<IScheduleBusiness>();
            logger = new Mock<ILogger<ScheduleController>>();
            controller = new ScheduleController(scheduleBusiness.Object, logger.Object,null);

            RouteValueDictionary routeDictionary = new RouteValueDictionary();
            routeDictionary.Add("FacilityKey", Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED"));
            routeDictionary.Add("key", Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46EE"));
            controller.ControllerContext = new ControllerContext
            {
                RouteData = new RouteData(routeDictionary)
            };
        }
        #endregion

        [Fact]
        public async Task GetAllSchedules_SchedulesFound_ShouldReturnOkStatus()
        {
            //Arrange
            var data = FakeData();

            scheduleBusiness.Setup(x => x.GetSchedules(It.IsAny<Guid>())).ReturnsAsync(data);

            //Act
            var mockresponse = await controller.GetAllSchedules() as OkObjectResult;
            var list = mockresponse.Value as IList<ScheduleResponse>;

            //Assert
            Assert.Equal(data.First().Name, list.First().Name);
        }

        [Fact]
        public async Task GetAllSchedules_SchedulesNotFound_ShouldNotFoundStatus()
        {
            //Act
            var mockresponse = await controller.GetAllSchedules() as NotFoundResult;

            //Assert
            Assert.Equal(404, mockresponse.StatusCode);
        }

        [Fact]
        public async Task GetAllSchedules_Exception_ShouldReturn500StatusCode()
        {
            //Arrange
            scheduleBusiness.Setup(x => x.GetSchedules(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            //Act
            var response = await controller.GetAllSchedules() as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }

        [Fact]
        public async Task GetSchedulebyId_SchedulesFound_ShouldReturnOkStatus()
        {
            //Arrange
            var data = FakeDataForGetById();

           scheduleBusiness.Setup(x => x.GetScheduleByKey(It.IsAny<Guid>())).ReturnsAsync(data.FirstOrDefault());

            //Act
            var mockresponse = await controller.GetScheduleByKey() as OkObjectResult;
            var schedule = mockresponse.Value as ScheduleResponse;

            //Assert
            Assert.Equal(data.First().Name, schedule.Name);
        }

        [Fact]
        public async Task GetSchedulebyId_SchedulesNotFound_ShouldReturnNotFoundStatus()
        {
            //Arrange
            var data = FakeData();

            //Act
            var mockresponse = await controller.GetScheduleByKey() as StatusCodeResult;

            //Assert
            Assert.Equal(404,mockresponse.StatusCode);
        }

        [Fact]
        public async Task GetSchedulebyId_Exception_ShouldReturnException()
        {
            //Arrange
            var data = FakeData();

            scheduleBusiness.Setup(x => x.GetScheduleByKey(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            //Act
            var mockresponse = await controller.GetScheduleByKey() as StatusCodeResult;

            //Assert
            Assert.Equal(500, mockresponse.StatusCode);
        }

        [Fact]
        public async Task PostSchedules_SchedulesAccepted_ShouldReturnAcceptedStatus()
        {
            //Arrange
            var data = FakeDataScheduleResponse();

            //Act
            var mockresponse = await controller.AddSchedule(data.First()) as StatusCodeResult;

            //Assert
            Assert.Equal(201, mockresponse.StatusCode);
        }

        [Fact]
        public async Task PostSchedules_InvalidScheduleException_ShouldReturnBadRequest()
        {
            //Arrange
            var data = FakeDataScheduleResponse();
            scheduleBusiness.Setup(x => x.AddSchedule(It.IsAny<Guid>(), data.First())).ThrowsAsync(new InvalidScheduleException("",404));

            //Act
            var mockresponse = await controller.AddSchedule(data.First()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, mockresponse.StatusCode);
        }

        [Fact]
        public async Task PostSchedules_ArgumentNullException_ShouldReturnBadRequest()
        {
            //Arrange
            var data = FakeDataScheduleResponse();
            scheduleBusiness.Setup(x => x.AddSchedule(It.IsAny<Guid>(),data.First())).ThrowsAsync(new ArgumentNullException(""));

            //Act
            var mockresponse = await controller.AddSchedule(data.First()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, mockresponse.StatusCode);
        }

        [Fact]
        public async Task PostSchedules_Exception_ShouldReturn500StatusCode()
        {
            //Arrange
            var data = FakeDataScheduleResponse();
            scheduleBusiness.Setup(x => x.AddSchedule(It.IsAny<Guid>(), data.First())).ThrowsAsync(new Exception());

            //Act
            var mockresponse = await controller.AddSchedule(data.First()) as StatusCodeResult;

            //Assert
            Assert.Equal(500, mockresponse.StatusCode);
        }

        [Fact]
        public async Task UpdateSchedules_ScheduleUpdated_ShouldReturnOkStatusCode()
        {
            //Arrange
            var data = FakeDataScheduleResponse();

            //Act
            var mockresponse = await controller.UpdateSchedule(data.First()) as StatusCodeResult;

            //Assert
            Assert.Equal(200, mockresponse.StatusCode);
        }

        [Fact]
        public async Task UpdateSchedules_Exception_ShouldReturn500StatusCode()
        {
            //Arrange
            var data = FakeDataScheduleResponse();
            scheduleBusiness.Setup(x => x.UpdateSchedule(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<ScheduleRequest>())).ThrowsAsync(new Exception());

            //Act
            var mockresponse = await controller.UpdateSchedule(data.First()) as StatusCodeResult;

            //Assert
            Assert.Equal(500, mockresponse.StatusCode);
        }

        [Fact]
        public async Task DeleteSchedules_ScheduleDeleted_ShouldReturnokStatus()
        {
            //Act
            var mockresponse = await controller.DeleteSchedule() as StatusCodeResult;

            //Assert
            Assert.Equal(200, mockresponse.StatusCode);
        }

        [Fact]
        public async Task DeleteSchedules_Exception_ShouldReturn500StatusCode()
        {
            //Arrange
            scheduleBusiness.Setup(x => x.DeleteSchedule(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            //Act
            var mockresponse = await controller.DeleteSchedule() as StatusCodeResult;

            //Assert
            Assert.Equal(500, mockresponse.StatusCode);
        }

        /// <summary>
        /// Mock Schedule request
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ScheduleResponse> FakeData()
        {
            ScheduleResponse s = new ScheduleResponse();
            s.Key = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            s.EndTime = new TimeSpan();
            s.StartTime = new TimeSpan();
            s.Name = "Test";
            s.ScheduleDays = new List<string>() {"Monday", "Tuesday"};

            ScheduleResponse s1 = new ScheduleResponse();
            s.Key = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E9");
            s.EndTime = new TimeSpan();
            s.StartTime = new TimeSpan();
            s.Name = "Test1";
            s.ScheduleDays = new List<string>() { "Monday", "Tuesday" };

            List<ScheduleResponse> sc = new List<ScheduleResponse>();
            sc.Add(s);
            sc.Add(s1);
            return sc.AsEnumerable();
        }

        private IEnumerable<ScheduleResponseByKey> FakeDataForGetById()
        {
            ScheduleResponseByKey s = new ScheduleResponseByKey();
            s.Key = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            s.EndTime = new TimeSpan();
            s.StartTime = new TimeSpan();
            s.Name = "Test";
            s.ScheduleDays = new List<string>() { "Monday", "Tuesday" };
            s.isAssociatedWithRoutingRuleFlag = true;

            ScheduleResponseByKey s1 = new ScheduleResponseByKey();
            s.Key = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E9");
            s.EndTime = new TimeSpan();
            s.StartTime = new TimeSpan();
            s.Name = "Test1";
            s.ScheduleDays = new List<string>() { "Monday", "Tuesday" };
            s.isAssociatedWithRoutingRuleFlag = false;

            List<ScheduleResponseByKey> sc = new List<ScheduleResponseByKey>();
            sc.Add(s);
            sc.Add(s1);
            return sc.AsEnumerable();
        }

        /// <summary>
        /// Mock Schedule request
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ScheduleRequest> FakeDataScheduleResponse()
        {
            ScheduleRequest s = new ScheduleRequest();
            s.EndTime = new TimeSpan();
            s.StartTime = new TimeSpan();
            s.Name = "Test";
            s.ScheduleDays = new List<ScheduleDays>() { ScheduleDays.Monday,ScheduleDays.Friday };

            List<ScheduleRequest> sc = new List<ScheduleRequest>();
            sc.Add(s);
            return sc.AsEnumerable();
        }
    }
}
