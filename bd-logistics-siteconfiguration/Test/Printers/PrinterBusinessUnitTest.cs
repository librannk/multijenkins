using AutoMapper;
using Moq;
using SiteConfiguration.API.AutoMapper;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.Printers.Abstractions;
using SiteConfiguration.API.Printers.Business;
using SiteConfiguration.API.Printers.Models.BuisnessContract;
using SiteConfiguration.API.Printers.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Printers
{
    /// <summary>
    /// This class contains unit test for PrinterBusiness class.
    /// </summary>
    public class PrinterBusinessUnitTest
    {
        #region Private Fields
        private readonly Mock<IPrinterRepository> _printerRepository;
        private readonly Mock<IPrinterModelRepository> _printerModelRepository;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly PrinterBusiness printerBusiness;
        #endregion

        #region Constructor
        public PrinterBusinessUnitTest()
        {
            _printerRepository = new Mock<IPrinterRepository>();
            _printerModelRepository = new Mock<IPrinterModelRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _unitOfWork = new Mock<IUnitOfWork>();
            printerBusiness = new PrinterBusiness(_printerRepository.Object, _printerModelRepository.Object, _mapper, _unitOfWork.Object);
        }
        #endregion

        #region Test Cases
        [Fact]
        public async Task GetPrintersByFacility_ValidFacilityKey_ShouldReturnPrinters()
        {
            //Arrange
            var guid = Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED");
            var fakePrinter = new SiteConfiguration.API.Printers.Models.Data.Printer
            {
                PrinterKey = guid
            };


            var mockList = new List<SiteConfiguration.API.Printers.Models.Data.Printer>();
            mockList.Add(fakePrinter);

            _printerRepository.Setup(x => x.GetPrintersByFacility(It.IsAny<Guid>())).ReturnsAsync(mockList);

            //Act
            var response = await printerBusiness.GetPrintersByFacility(guid);

            //Assert
            Assert.Equal(guid, response.First().Key);

        }

        [Fact]
        public async Task GetPrinterByKey_ValidPrinterKey_ShouldReturnPrinter()
        {
            //Arrange
            var guid = Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED");
            var mockPrinterName = "xyz";
            var fakePrinter = new SiteConfiguration.API.Printers.Models.Data.Printer
            {
                PrinterName = mockPrinterName
            };

            _printerRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(fakePrinter);

            //Act
            var response = await printerBusiness.GetPrinterByKey(guid);
            //Assert
            Assert.Equal(mockPrinterName, response.Name);

        }

        [Fact]
        public async Task AddPrinterKey_ValidRequest_ShouldCommit()
        {
            //Arrange
            var guid = Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED");
            var fakeRequest = new PrinterRequest();
            var fakeprintermodel = new SiteConfiguration.API.Printers.Models.Data.PrinterModel
            {
                PrinterModelKey = Guid.NewGuid(),
                DescriptionText = "testdescription"
            };

            _printerModelRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(fakeprintermodel);

            //Act
            await printerBusiness.AddPrinter(fakeRequest, guid);

            //Assert
            _unitOfWork.Verify(x => x.CommitChanges(), Times.Once);

        }

        [Fact]
        public async Task UpdatePrinter_ValidRequest_ShouldCommit()
        {
            //Arrange
            var guid = Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED");
            var fakePrinter = new SiteConfiguration.API.Printers.Models.Data.Printer
            {
                PrinterKey = guid,
                PrinterModelKey = guid
            };

            var fakePrinterModel = new SiteConfiguration.API.Printers.Models.Data.PrinterModel
            {
                PrinterModelKey = guid
            };

            var fakePrinterRequest = new PrinterRequest();

            _printerRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(fakePrinter);
            _printerModelRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(fakePrinterModel);

            //Act
            await printerBusiness.UpdatePrinter(guid, guid, fakePrinterRequest);

            //Assert
            _unitOfWork.Verify(x => x.CommitChanges(), Times.Once);
        }

        [Fact]
        public async Task UpdatePrinter_InvalidRequest_ShouldNotCommit()
        {
            //Arrange
            var guid = Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED");
            var fakePrinterRequest = new PrinterRequest();

            //Act
            try
            {
                await printerBusiness.UpdatePrinter(guid, guid, fakePrinterRequest);
            }
            //Assert

            catch (Exception ex)
            {
                _unitOfWork.Verify(x => x.CommitChanges(), Times.Never);
            }

        }

        [Fact]
        public async Task GetPrinterModelsByFacility_ValidFacilityKey_ShouldReturnPrinterModels()
        {
            //Arrange
            var guid = Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED");
            var fakePrinterModels = new SiteConfiguration.API.Printers.Models.Data.PrinterModel
            {
                PrinterModelKey = guid,
                DescriptionText = "testdescription"
            };


            var mockList = new List<SiteConfiguration.API.Printers.Models.Data.PrinterModel>();
            mockList.Add(fakePrinterModels);

            _printerModelRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(mockList);

            //Act
            var response = await printerBusiness.GetPrinterModels();

            //Assert
            Assert.Equal(guid, response.First().Id);

        }
        #endregion

    }
}
