using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SiteConfiguration.API.Printers.Abstractions;
using SiteConfiguration.API.Printers.Controllers;
using SiteConfiguration.API.Printers.Models.BuisnessContract;
using Xunit;

namespace Test.Printers
{
    public class PrinterModelControllerUnitTest
    {
        private readonly Mock<IPrinterBusiness> _printerBusiness;
        private readonly Mock<ILogger<PrinterModelController>> _logger;
        private readonly PrinterModelController printerController;

        public PrinterModelControllerUnitTest()
        {
            _printerBusiness = new Mock<IPrinterBusiness>();
            _logger = new Mock<ILogger<PrinterModelController>>();
            printerController = new PrinterModelController(_printerBusiness.Object, _logger.Object);
        }

        [Fact]
        public async Task GetAllPrinterModels_PrinterModelFound_ShouldReturnOkStatus()
        {
            //Arrange
            var fakePrinterModel = new List<PrinterModel>();
            fakePrinterModel.Add(new PrinterModel
            {
                Id = Guid.NewGuid(),
                Label = "Printer1"
            });
            _printerBusiness.Setup(x => x.GetPrinterModels()).ReturnsAsync(fakePrinterModel);

            //Act
            var response = await printerController.GetAllPrinterModels() as OkObjectResult;

            //Assert
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetAllPrinterModels_PrinterModelNotFound_ShouldReturnOkStatus()
        {
            //Arrange
            var fakePrinterModel = new List<PrinterModel>();
            _printerBusiness.Setup(x => x.GetPrinterModels()).ReturnsAsync(fakePrinterModel);

            //Act
            var response = await printerController.GetAllPrinterModels() as OkObjectResult;

            //Assert
            _printerBusiness.Verify(x => x.GetPrinterModels(),Times.Once);
        }

        [Fact]
        public async Task GetAllPrinterModels_PrinterModelNotFound_ThrowException()
        {
            //Arrange
            _printerBusiness.Setup(x => x.GetPrinterModels()).Throws(new Exception());

            //Act
            var response = await printerController.GetAllPrinterModels() as StatusCodeResult;

            //Assert
            Assert.Equal(500, response.StatusCode);
        }
    }
}
