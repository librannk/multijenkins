using Microsoft.Extensions.Logging;
using Moq;
using SiteConfiguration.API.Printers.Abstractions;
using SiteConfiguration.API.Printers.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SiteConfiguration.API.Printers.Models.BuisnessContract;
using System.Linq;
using BD.Core.Context;
using SiteConfiguration.API.Printers.Exceptions;
using SiteConfiguration.API;
using SiteConfiguration.API.Common.Constants;

namespace Test.Printers
{
    /// <summary>
    /// This Class Contains Unit Test for PrinterController
    /// </summary>
    public class PrinterControllerUnitTest
    {
        #region Private Fields
        private readonly Mock<IPrinterBusiness> _printerBusiness;
        private readonly Mock<ILogger<PrinterController>> _logger;
        private readonly PrinterController printerController;
        private readonly Mock<IExecutionContextAccessor> _executionContextAccessor;

        #endregion

        #region Constructor
        public PrinterControllerUnitTest()
        {
            _printerBusiness = new Mock<IPrinterBusiness>();
            _logger = new Mock<ILogger<PrinterController>>();
            _executionContextAccessor = new Mock<IExecutionContextAccessor>();
            Context context = new Context();
            context.Facility = new FacilityContext("BF521211-CEAF-4DCA-82C7-40446D4C46ED", null);

            _executionContextAccessor.Setup(x => x.Current).Returns(context);

            printerController = new PrinterController(_printerBusiness.Object, _logger.Object, _executionContextAccessor.Object);

            RouteValueDictionary routeDictionary = new RouteValueDictionary();
            routeDictionary.Add("Key", Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED"));
            printerController.ControllerContext = new ControllerContext
            {
                RouteData = new RouteData(routeDictionary)
            };
        }
        #endregion

        #region Test cases

        #region GetAllPrinter TestCases
        [Fact]
        public async Task GetAllPrinters_PrintersFound_ShouldReturnOkStatus()
        {
            //Arrange
            var fakePrinters = new List<PrinterResponse>();
            fakePrinters.Add(new PrinterResponse
            {
                Name = "Printer1"
            });
            _printerBusiness.Setup(x => x.GetPrintersByFacility(It.IsAny<Guid>())).ReturnsAsync(fakePrinters);

            //Act
            var response = await printerController.GetAllPrinters() as OkObjectResult;

            //Assert
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetAllPrinters_PrintersFound_ShouldReturnPrinters()
        {
            //Arrange
            var fakePrinters = new List<PrinterResponse>();
            var mockPrinter = new PrinterResponse
            {
                Name = "Printer1"
            };
            fakePrinters.Add(mockPrinter);
            _printerBusiness.Setup(x => x.GetPrintersByFacility(It.IsAny<Guid>())).ReturnsAsync(fakePrinters);

            //Act
            var response = await printerController.GetAllPrinters() as OkObjectResult;
            var list = response.Value as IList<PrinterResponse>;
            //Assert
            Assert.Equal(mockPrinter.Name, list.First().Name);
        }

        [Fact]
        public async Task GetAllPrinters_PrintersNotFound_ShouldReturnNotFoundStatus()
        {

            //Act
            var response = await printerController.GetAllPrinters() as NotFoundResult;

            //Assert
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task GetAllPrinters_Exception_ShouldReturn500StatusCode()
        {
            //Arrange
            _printerBusiness.Setup(x => x.GetPrintersByFacility(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            //Act
            var response = await printerController.GetAllPrinters() as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }
        #endregion

        #region GetPrinterByKey TestCases
        [Fact]
        public async Task GetPrinterByKey_PrinterFound_ShouldReturnOkStatus()
        {
            //Arrange
            var fakePrinter = new PrinterResponse
            {
                Name = "Printer1"
            };

            _printerBusiness.Setup(x => x.GetPrinterByKey(It.IsAny<Guid>())).ReturnsAsync(fakePrinter);

            //Act
            var response = await printerController.GetPrinterByKey() as OkObjectResult;

            //Assert
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetPrinterByKey_PrinterFound_ShouldReturnPrinters()
        {
            //Arrange
            var mockPrinter = new PrinterResponse
            {
                Name = "Printer1"
            };

            _printerBusiness.Setup(x => x.GetPrinterByKey(It.IsAny<Guid>())).ReturnsAsync(mockPrinter);

            //Act
            var response = await printerController.GetPrinterByKey() as OkObjectResult;
            var responsePrinter = response.Value as PrinterResponse;
            //Assert
            Assert.Equal(mockPrinter.Name, responsePrinter.Name);
        }

        [Fact]
        public async Task GetPrinterByKey_PrinterNotFound_ShouldReturnNotFoundStatus()
        {
            //Arrange
            var guid = Guid.Parse("599E303F-99DE-4A9C-9B6A-00B8FA904DEF");

            //Act
            var response = await printerController.GetPrinterByKey() as NotFoundResult;

            //Assert
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task GetPrinterByKey_Exception_ShouldReturn500StatusCode()
        {
            //Arrange
            var guid = Guid.Parse("599E303F-99DE-4A9C-9B6A-00B8FA904DEF");
            _printerBusiness.Setup(x => x.GetPrinterByKey(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            //Act
            var response = await printerController.GetPrinterByKey() as StatusCodeResult;

            //Arrange
            Assert.Equal(500, response.StatusCode);

        }
        #endregion

        #region AddPrinter TestCases
        [Fact]
        public async Task AddPrinter_ValidRequest_ShouldReturnStatusCode201()
        {
            //Arrange
            var fakePrinterRequest = new PrinterRequest();

            //Act
            var response = await printerController.AddPrinter(fakePrinterRequest) as StatusCodeResult;

            //Assert
            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public async Task AddPrinter_Exception_ShouldReturnStatusCode500()
        {
            //Arrange
            _printerBusiness.Setup(x => x.AddPrinter(It.IsAny<PrinterRequest>(), It.IsAny<Guid>())).Throws(new Exception());
            var fakePrinterRequest = new PrinterRequest();

            //Act
            var response = await printerController.AddPrinter(fakePrinterRequest) as StatusCodeResult;

            //Assert
            Assert.Equal(500, response.StatusCode);
        }
        #endregion

        #region UpdatePrinter TestCases
        [Fact]
        public async Task UpdatePrinter_ValidRequest_ShouldReturnOkStatus()
        {
            //Arrange
            var fakePrinterRequest = new PrinterRequest();

            //Act
            var response = await printerController.UpdatePrinter(fakePrinterRequest) as OkResult;

            //Assert
            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public async Task UpdatePrinter_PrinterException_ShouldReturnBadRequest()
        {
            //Arrange
            var fakePrinterRequest = new PrinterRequest();
            _printerBusiness.Setup(x => x.UpdatePrinter(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<PrinterRequest>())).ThrowsAsync(new InvalidPrinterException(Resource.ResourceManager.GetString($"E{ErrorCode.ResourceNotFound}"), ErrorCode.ResourceNotFound));

            //Act
            var response = await printerController.UpdatePrinter(fakePrinterRequest) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, response.StatusCode);

        }

        [Fact]
        public async Task UpdatePrinter_Exception_ShouldReturn500()
        {
            //Arrange
            var fakePrinterRequest = new PrinterRequest();
            _printerBusiness.Setup(x => x.UpdatePrinter(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<PrinterRequest>())).ThrowsAsync(new Exception());

            //Act
            var response = await printerController.UpdatePrinter(fakePrinterRequest) as StatusCodeResult;

            //Assert
            Assert.Equal(500, response.StatusCode);

        }
        #endregion
        #endregion
    }
}
