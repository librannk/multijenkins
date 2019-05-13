using BD.Core.EventBus.Abstractions;
using Facility.API.BusinessLayer.Contracts;
using Facility.API.Configuration;
using Facility.API.Constants;
using Facility.API.Controllers;
using Facility.API.Model;
using Facility.API.Model.InternalModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BD.Core.EventBus.Events;
using Xunit;
using OkObjectResult = Microsoft.AspNetCore.Mvc.OkObjectResult;

namespace Facility.API.Tests.Controller
{
    /// <summary>
    /// Tests for FacilityController
    /// </summary>
    public class FacilityControllerUnitTests
    {
        /// <summary>
        /// The subject
        /// </summary>
        private readonly FacilityController _subject;

        /// <summary>
        /// The mock facility manager
        /// </summary>
        private readonly Mock<IFacilityManager> _mockFacilityManager;

        /// <summary>
        /// The mock HTTP context accessor.
        /// </summary>
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        /// <summary>
        /// The mock event bus
        /// </summary>
        private readonly Mock<IEventBus> _mockEventBus;


        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityControllerUnitTests"/> class.
        /// </summary>
        public FacilityControllerUnitTests()
        {
            var mockLogger = new Mock<ILogger<FacilityController>>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockEventBus = new Mock<IEventBus>();
            _mockFacilityManager = new Mock<IFacilityManager>();
            var messageBusTopics = new Mock<IOptions<MessageBusTopics>>();
            _mockHttpContextAccessor.SetupGet(b => b.HttpContext.Request.Headers).Returns(new HeaderDictionary()
                {{"key1", "value1"}, {"key2", "value2"}});
            messageBusTopics.Setup(b => b.Value).Returns(() => new MessageBusTopics()
            { KafkaFacilityDetailsTopic = "busTopic" });
            _subject = new FacilityController(mockLogger.Object, _mockEventBus.Object,
                _mockFacilityManager.Object, messageBusTopics.Object, _mockHttpContextAccessor.Object);
        }

        /// <summary>
        /// Defines the test method GetFacilities.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetFacilitiesListTest()
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager.Setup(b => b.GetAllFacilities(true, "", 0, 0))
                .ReturnsAsync(new BusinessResult<IList<FacilityList>>(new List<FacilityList>(),
                    CreateUpdateResultEnum.Success, 10));

            //Act
            var result = await _subject.GetFacilitiesList(true);
            var okResult = result as OkObjectResult;

            //Assert
            _mockFacilityManager.Verify();
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        /// <summary>
        /// Defines the test method GetFacilityById.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetFacilityByIdTest()
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager.Setup(b => b.GetFacilityByKeyAsync(It.IsAny<Guid>())).ReturnsAsync(new Model.Facility());

            //Act
            var result = await _subject.GetFacilityByKey(Guid.NewGuid());
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            _mockFacilityManager.Verify();
        }

        /// <summary>
        /// Defines the test method GetFacilityById negative tests with empty Guid.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetFacilityByIdNegativeEmptyGuid()
        {
            //Arrange
            _mockFacilityManager.Reset();

            //Act
            var result = await _subject.GetFacilityByKey(Guid.Empty);
            var badRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        /// <summary>
        /// Defines the test method GetFacilityById negative tests with no result found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetFacilityByIdNegativeNotFound()
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager.Setup(b => b.GetFacilityByKeyAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await _subject.GetFacilityByKey(Guid.NewGuid());
            var statusCodeResult = result as ObjectResult;

            //Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(404, statusCodeResult.StatusCode);
            _mockFacilityManager.Verify();
        }

        /// <summary>
        /// Positive test for create facility.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateFacilityTest()
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager
                .Setup(b => b.Add(It.IsAny<NewFacilityRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<Model.Facility>(new Model.Facility(), CreateUpdateResultEnum.Success));
            _mockEventBus.Setup(b =>
                b.Publish(It.IsAny<string>(), It.IsAny<Event>(), It.IsAny<Dictionary<string, string>>()));

            //Act
            var result = await _subject.AddFacility(new NewFacilityRequest { FacilityCode = "FC001" });
            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            _mockFacilityManager.Verify();
            _mockEventBus.Verify();
            _mockHttpContextAccessor.Verify();
        }

        /// <summary>
        /// Positive test for create facility.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateFacilityNegativeAlreadyExistTest()
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager
                .Setup(b => b.Add(It.IsAny<NewFacilityRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<Model.Facility>(new Model.Facility(),
                        CreateUpdateResultEnum.ErrorAlreadyExists));
            //Act
            var result = await _subject.AddFacility(new NewFacilityRequest { FacilityCode = "FC001" });
            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(400, createdResult.StatusCode);
            _mockFacilityManager.Verify();
        }

        /// <summary>
        /// Positive test for create facility.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateFacilityNegativeValidationFailedTest()
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager
                .Setup(b => b.Add(It.IsAny<NewFacilityRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<Model.Facility>(new Model.Facility(),
                        CreateUpdateResultEnum.ValidationFailed));

            //Act
            var result = await _subject.AddFacility(new NewFacilityRequest { FacilityCode = "FC001" });
            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(400, createdResult.StatusCode);
            _mockFacilityManager.Verify();
        }

        /// <summary>
        /// Positive test for create facility.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateFacilityNegativeBadRequestTest()
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager
                .Setup(b => b.Add(It.IsAny<NewFacilityRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<Model.Facility>(new Model.Facility(),
                        CreateUpdateResultEnum.ValidationFailed));
            var badRequest = new NewFacilityRequest();
            //Act
            var result = await _subject.AddFacility(badRequest);
            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(400, createdResult.StatusCode);
            _mockFacilityManager.Verify();
        }

        /// <summary>
        /// Test method for update facility.
        /// </summary>
        /// <returns>Task.</returns>
        [Theory]
        [InlineData("22BFE608-AAFC-4BE3-9951-00536B214DFE", 200)]
        [InlineData("32BFE608-AAFC-4BE3-9951-00536B214DFE", 400)]
        [InlineData("42BFE608-AAFC-4BE3-9951-00536B214DFE", 400)]
        public async Task UpdateFacilityTest(string facilityKey, int statusCode)
        {
            //Arrange
            _mockFacilityManager.Reset();
            _mockFacilityManager.Setup(b =>
                    b.Update(Guid.Parse("22BFE608-AAFC-4BE3-9951-00536B214DFE"), It.IsAny<UpdateFacilityRequest>()))
                .ReturnsAsync(
                    new BusinessResult<Model.Facility>(new Model.Facility(), CreateUpdateResultEnum.Success));

            _mockFacilityManager.Setup(b =>
                    b.Update(Guid.Parse("32BFE608-AAFC-4BE3-9951-00536B214DFE"), It.IsAny<UpdateFacilityRequest>()))
                .ReturnsAsync(new BusinessResult<Model.Facility>(null, CreateUpdateResultEnum.NotFound));

            _mockFacilityManager.Setup(b =>
                    b.Update(Guid.Parse("42BFE608-AAFC-4BE3-9951-00536B214DFE"), It.IsAny<UpdateFacilityRequest>()))
                .ReturnsAsync(new BusinessResult<Model.Facility>(null, CreateUpdateResultEnum.ValidationFailed));

            //Act
            var result = await _subject.UpdateFacility(Guid.Parse(facilityKey), new UpdateFacilityRequest());
            var updateResult = result as ObjectResult;

            //Assert
            Assert.NotNull(updateResult);
            Assert.Equal(statusCode, updateResult.StatusCode);
            _mockFacilityManager.Verify();
        }
    }
}
