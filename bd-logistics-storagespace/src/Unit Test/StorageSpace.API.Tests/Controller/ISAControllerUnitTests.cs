using BD.Core.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;
using StorageSpace.API.BusinessLayer.Contracts;
using StorageSpace.API.Controllers;
using StorageSpace.API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StorageSpace.API.Tests.Controller
{
    /// <summary>
    /// ISAControllerUnitTests.
    /// </summary>
    public class ISAControllerUnitTests
    {
        /// <summary>
        /// The subject test
        /// </summary>
        private ISAController subject_test;

        /// <summary>
        /// The mock isa manager
        /// </summary>
        private readonly Mock<IISAManager> mockISAManager;
        private readonly Mock<IExecutionContextAccessor> mockExecutionContext;

        /// <summary>
        /// The mock logger
        /// </summary>
        private readonly Mock<ILogger<ISAController>> mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ISAControllerUnitTests"/> class.
        /// </summary>
        public ISAControllerUnitTests()
        {
            mockLogger = new Mock<ILogger<ISAController>>();
            mockISAManager = new Mock<IISAManager>();
            mockExecutionContext = new Mock<IExecutionContextAccessor>();
            mockExecutionContext.SetupProperty(b => b.Current,
                new Context { Facility = new FacilityContext("BF521211-CEAF-4DCA-82C7-40446D4C46ED", "") });
            subject_test = new ISAController(mockISAManager.Object, mockLogger.Object, mockExecutionContext.Object);
        }

        /// <summary>
        /// Defines the test method GetISATest.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISATest()
        {
            //Arrange
            mockISAManager.Reset();
            mockISAManager.Setup(b => b.GetISA(It.IsAny<Guid>())).ReturnsAsync(new List<ISARequest>()
            {
                new ISARequest(){ IsaId = "123", Description = "testDescription1", IsaType = "isaType1", Active = true },
                new ISARequest(){ IsaId = "124", Description = "testDescription2", IsaType = "isaType2", Active = false}
            });

            //Act
            var result = await subject_test.GetISA();
            var okResult = result as OkObjectResult;

            //Assert
            mockISAManager.Verify();
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        /// <summary>
        /// Defines the test method GetISATestExceptionTest.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISATestExceptionTest()
        {
            //Arrange
            mockISAManager.Reset();
            mockISAManager.Setup(b => b.GetISA(It.IsAny<Guid>())).Throws(new Exception());

            //Act
            var result = await subject_test.GetISA();
            var statusCodeResult = result as StatusCodeResult;

            //Assert
            mockISAManager.Verify();
            Assert.NotNull(statusCodeResult);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        /// <summary>
        /// Defines the test method GetISAByIdPositiveTest.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISAByIdPositiveTest()
        {

            var id = "1234";
            var guid = Guid.NewGuid();
            var createdDate = DateTime.Now;
            var modifiedDate = DateTime.Now;
            var dummyISA = GetMockISA(id, guid);

            //Arrange
            mockISAManager.Reset();
            mockISAManager.Setup(b => b.GetISAById(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(dummyISA);

            //Act
            var result = await subject_test.GetISAById("5cc18d9c2a88d222b06a060d");
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            mockISAManager.Verify();
        }

        /// <summary>
        /// Defines the test method GetISAByIdNegativeNotFound.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISAByIdNegativeNotFound()
        {
            //Arrange
            mockISAManager.Reset();
            mockISAManager.Setup(b => b.GetISAById(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await subject_test.GetISAById("5cc18d9c2a88d222b06a060d");
            var statusCodeResult = result as ObjectResult;

            //Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(404, statusCodeResult.StatusCode);
            mockISAManager.Verify();
        }

        /// <summary>
        /// Defines the test method GetISAByIdNegativeEmptyGuid.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISAByIdNegativeWrongIsaIdTest()
        {

            //Arrange
            mockISAManager.Reset();

            //Act
            var result = await subject_test.GetISAById("random");
            var badRequestResult = result as ObjectResult;

            //Assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        /// <summary>
        /// Gets the mock isa.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>ISA.</returns>
        private ISA GetMockISA(string id, Guid guid)
        {
            return new ISA()
            {
                IsaId = id,
                Description = "testDescription",
                ActiveFlag = true,
                ShortDescription = "testshortDescription",
                TransactionQueueLockExpiration = 3,
                ReqRestockLotInfoFlag = true,
                StaticFlag = true,
                ReturnStatusFlag = true,
                Carousel = new ObjectId("5cc18d382a88d222b06a060c"),
                DisconnectOnIdleFlag = true,
                DeviceNumber = 1234,
                IPAddress = "123.23.4.4",
                Port = 2344,
                ConnectionResetMinutes = 34,
                MaxRack = 20,
                MaxBin = 20,
                DefaultBinWidth = 20,
                MaxShelves = 20,
                ShelfWidth = 20,
                DefaultBinDividersH = 20,
                DefaultBinDividersV = 20,
                MaxDisplayColumns = 20,
                DisplayColumnsPerInch = 20,
                DisplayArrowDirection = "testleft",
                DisplayAttachedFlag = true,
                DisplayTypeKey = "4",
                DisplayIPAddress = "123.43.23.22",
                DisplayPort = 23,
                RestrictControlFlag = true
            };

        }
    }
}
