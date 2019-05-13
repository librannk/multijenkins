using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBus.Events;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.Common;
using Formulary.API.Controllers;
using Formulary.API.Model;
using Formulary.API.Model.InternalModel;
using Formulary.API.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FormularyAPIUnitTests.Controller
{
    public class FormularyControllerUnitTests
    {

        /// <summary>
        /// The subject
        /// </summary>
        private readonly FormularyController _subject;

        /// <summary>
        /// The mock facility manager
        /// </summary>

        private readonly Mock<IConfiguration> _mockConfiguration;

        /// <summary>
        /// The mock logger
        /// </summary>
        private readonly Mock<ILogger<FormularyController>> _mockLogger;

        /// <summary>
        /// The mock formulary manager
        /// </summary>
        private readonly Mock<IFormularyManager> _mockFormularyManager;

        /// <summary>
        /// The mock event bus
        /// </summary>
        private readonly Mock<IEventBus> _mockEventBus;

        /// <summary>
        /// The mock system item set up manager
        /// </summary>
        private readonly Mock<ISystemItemSetUpManager> _mockSystemItemSetUpManager;

        /// <summary>
        /// The mock item setup manager
        /// </summary>
        private readonly Mock<IItemSetupManager> _mockItemSetupManager;
        /// <summary>
        /// the mock http context
        /// </summary>
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormularyControllerUnitTests"/> class.
        /// </summary>
        public FormularyControllerUnitTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<FormularyController>>();
            _mockFormularyManager = new Mock<IFormularyManager>();
            _mockEventBus = new Mock<IEventBus>();
            _mockSystemItemSetUpManager = new Mock<ISystemItemSetUpManager>();
            _mockItemSetupManager = new Mock<IItemSetupManager>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupGet(b => b.HttpContext.Request.Headers).Returns(new HeaderDictionary()
                {{"key1", "value1"}, {"key2", "value2"}});
            _subject = new FormularyController(_mockConfiguration.Object, _mockLogger.Object,
                _mockFormularyManager.Object, _mockEventBus.Object, _mockSystemItemSetUpManager.Object,
                _mockItemSetupManager.Object, _mockHttpContextAccessor.Object);
            _mockHttpContextAccessor.SetupGet(b => b.HttpContext.Request.Headers).Returns(new HeaderDictionary()
                {{"key1", "value1"}, {"key2", "value2"}});

        }

        /// <summary>
        /// Defines the test method for update formulary..
        /// </summary>
        /// <param name="itemkey">The itemkey.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns>Task.</returns>
        [Theory]
        [InlineData("22BFE608-AAFC-4BE3-9951-00536B214DFE", 200)]
        [InlineData("32BFE608-AAFC-4BE3-9951-00536B214DFE", 404)]
        [InlineData("42BFE608-AAFC-4BE3-9951-00536B214DFE", 400)]
        public async Task UpdateFormularyTest(string itemkey, int statusCode)
        {
            //Arrange
            _mockSystemItemSetUpManager.Reset();
            _mockSystemItemSetUpManager.Setup(b =>
                    b.UpdateSystemItemSetUp(Guid.Parse("22BFE608-AAFC-4BE3-9951-00536B214DFE"), It.IsAny<SystemItemSetupRequest>()))
                .ReturnsAsync(
                    new BusinessResult<SystemItemSetupRequest>(new SystemItemSetupRequest(), CreateUpdateResultEnum.Success));

            _mockSystemItemSetUpManager.Setup(b =>
                    b.UpdateSystemItemSetUp(Guid.Parse("32BFE608-AAFC-4BE3-9951-00536B214DFE"), It.IsAny<SystemItemSetupRequest>()))
                .ReturnsAsync(new BusinessResult<SystemItemSetupRequest>(null, CreateUpdateResultEnum.NotFound));

            _mockSystemItemSetUpManager.Setup(b =>
                    b.UpdateSystemItemSetUp(Guid.Parse("42BFE608-AAFC-4BE3-9951-00536B214DFE"), It.IsAny<SystemItemSetupRequest>()))
                .ReturnsAsync(new BusinessResult<SystemItemSetupRequest>(null, CreateUpdateResultEnum.ValidationFailed));

            //Act
            var result = await _subject.UpdateItemSetUpById(Guid.Parse(itemkey), new SystemItemSetupRequest());
            var updateResult = result as ObjectResult;

            //Assert
            Assert.NotNull(updateResult);
            Assert.Equal(statusCode, updateResult.StatusCode);
            _mockSystemItemSetUpManager.Verify();
        }
        /// <summary>
        /// Defines the test method GetFacilities.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetGetItemsTest()
        {
            //Arrange
            _mockItemSetupManager.Reset();
            _mockItemSetupManager.Setup(b => b.GetMedicationItems())
                .ReturnsAsync(new List<MedicationItemList>());

            //Act
            var result = await _subject.GetItems();
            var okResult = result as OkObjectResult;

            //Assert
            _mockFormularyManager.Verify();
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Positive test for create systemformulary.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateSystemFormularyTest()
        {
            //Arrange
            _mockFormularyManager.Reset();
            _mockFormularyManager
                .Setup(b => b.AddSystemItem(It.IsAny<SystemItemSetupRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<SystemItemSetupRequest>(new SystemItemSetupRequest(), CreateUpdateResultEnum.Success));
            _mockEventBus.Setup(b =>
                b.Publish(It.IsAny<string>(), It.IsAny<Event>(), It.IsAny<Dictionary<string, string>>()));

            //Act
            var result = await _subject.AddSystemItem(new SystemItemSetupRequest
            {
                ItemId = "FC001",
                BioHazFlag = true,
                ChemoAgentFlag = true,
                DrugFlag = true,
                FreezerFlag = true,
                HazAcidFlag = true,
                HazAerosolFlag = true,
                HazBaseFlag = true,
                ChemotherapyFlag = true,
                HazChemicalFlag = true,
                HazOxidizerFlag = true,
                HighRiskFlag = true,
                RefrigeratedFlag = true,
                HazToxicFlag = true,
                LASAFlag = true,
                NonFormularyFlag = true
            });

            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            _mockFormularyManager.Verify();
            _mockEventBus.Verify();
            _mockHttpContextAccessor.Verify();
        }

        /// <summary>
        /// Positive test for create systemformulary.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateFacilityNegativeAlreadyExistTest()
        {
            //Arrange
            _mockFormularyManager.Reset();
            _mockFormularyManager
                .Setup(b => b.AddSystemItem(It.IsAny<SystemItemSetupRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<SystemItemSetupRequest>(new SystemItemSetupRequest(),
                        CreateUpdateResultEnum.ErrorAlreadyExists));
            //Act
            var result = await _subject.AddSystemItem(new SystemItemSetupRequest
            {
                ItemId = "FC001",
                BioHazFlag = true,
                ChemoAgentFlag = true,
                DrugFlag = true,
                FreezerFlag = true,
                HazAcidFlag = true,
                HazAerosolFlag = true,
                HazBaseFlag = true,
                ChemotherapyFlag = true,
                HazChemicalFlag = true,
                HazOxidizerFlag = true,
                HighRiskFlag = true,
                RefrigeratedFlag = true,
                HazToxicFlag = true,
                LASAFlag = true,
                NonFormularyFlag = true
            });
            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(400, createdResult.StatusCode);
            _mockFormularyManager.Verify();
        }

        /// <summary>
        /// Positive test for create systemformulary.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateFacilityNegativeValidationFailedTest()
        {
            //Arrange
            _mockFormularyManager.Reset();
            _mockFormularyManager
                .Setup(b => b.AddSystemItem(It.IsAny<SystemItemSetupRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<SystemItemSetupRequest>(new SystemItemSetupRequest(),
                        CreateUpdateResultEnum.ValidationFailed));

            //Act
            var result = await _subject.AddSystemItem(new SystemItemSetupRequest
            {
                ItemId = "FC001",
                BioHazFlag = true,
                ChemoAgentFlag = true,
                DrugFlag = true,
                FreezerFlag = true,
                HazAcidFlag = true,
                HazAerosolFlag = true,
                HazBaseFlag = true,
                ChemotherapyFlag = true,
                HazChemicalFlag = true,
                HazOxidizerFlag = true,
                HighRiskFlag = true,
                RefrigeratedFlag = true,
                HazToxicFlag = true,
                LASAFlag = true,
                NonFormularyFlag = true
            });
            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(400, createdResult.StatusCode);
            _mockFormularyManager.Verify();
        }

        /// <summary>
        /// Positive test for create systemformulary.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task CreateFacilityNegativeBadRequestTest()
        {
            //Arrange
            _mockFormularyManager.Reset();
            _mockFormularyManager
                .Setup(b => b.AddSystemItem(It.IsAny<SystemItemSetupRequest>()))
                .ReturnsAsync(() =>
                    new BusinessResult<SystemItemSetupRequest>(new SystemItemSetupRequest(),
                        CreateUpdateResultEnum.ValidationFailed));
            var badRequest = new SystemItemSetupRequest();
            //Act
            var result = await _subject.AddSystemItem(badRequest);
            var createdResult = result as ObjectResult;

            //Assert
            Assert.NotNull(createdResult);
            Assert.Equal(400, createdResult.StatusCode);
            _mockFormularyManager.Verify();
        }
    }
}
