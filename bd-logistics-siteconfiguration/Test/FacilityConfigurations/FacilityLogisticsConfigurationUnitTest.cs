using AutoMapper;
using Moq;
using SiteConfiguration.API.AutoMapper;
using SiteConfiguration.API.FacilityConfiguration.Abstractions;
using SiteConfiguration.API.FacilityConfiguration.Business;
using SiteConfiguration.API.FacilityConfiguration.Constants;
using SiteConfiguration.API.FacilityConfiguration.Models;
using SiteConfiguration.API.FacilityConfiguration.RequestResponseModel;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test.FacilityConfigurations
{
    public class FacilityLogisticsConfigurationUnitTest
    {
        #region PrivateFields
        private readonly Mock<IFacilityLogisticsConfigurationRepository> _mockFacilitySpecificConfigurationRepository;
        private SiteConfiguration.API.FacilityConfiguration.Business.FacilityLogisticsConfiguration _facilitySpecificConfigurationLogic;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        #endregion

        public FacilityLogisticsConfigurationUnitTest()
        {
            _mockFacilitySpecificConfigurationRepository = new Mock<IFacilityLogisticsConfigurationRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapFacilityConfiguration());
            });
            _mapper = mockMapper.CreateMapper();
            _facilitySpecificConfigurationLogic = new FacilityLogisticsConfiguration(_mockFacilitySpecificConfigurationRepository.Object, _mapper, _unitOfWorkMock.Object);
        }

        #region Exceptions Logic 
        [Fact]
        public void FacilitySpecificConfigurationLogic_CreateFacilitySpecificConfigurationAsync_ShouldThrowArgumentNulExceptionOnNullInput()
        {
            //Arrange           
            TransactionQueueConfigurationRequest transactionQueueConfigurationRequest = null;
            //Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _facilitySpecificConfigurationLogic.CreateFacilitySpecificConfigurationAsync(transactionQueueConfigurationRequest));
            //Assert
        }

        [Fact]
        public void FacilitySpecificConfigurationLogic_CreateFacilitySpecificConfigurationAsync_ShouldThrowArgumentNulExceptionOnEmptyFacilityKeyInput()
        {
            //Arrange
            TransactionQueueConfigurationRequest transactionQueueConfigurationRequest = new TransactionQueueConfigurationRequest() { FacilityKey = Guid.Empty };
            //Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _facilitySpecificConfigurationLogic.CreateFacilitySpecificConfigurationAsync(transactionQueueConfigurationRequest));
            //Assert
        }

        #endregion

        #region DB Entry
        [Fact]
        public async Task FacilitySpecificConfigurationLogic_CreateFacilitySpecificConfigurationAsync_ShouldAddAndCommitRecordOnProperInput()
        {

            _unitOfWorkMock.Setup(u => u.CommitChangesAsync()).ReturnsAsync(1);

            Guid facilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            Guid tenantKey = Guid.Parse("7ecfe81c-cfbe-42c1-8158-047c899fb862");
            SiteConfiguration.API.FacilityConfiguration.Models.FacilityLogisticsConfig existingfacilitySpecificConfiguration = null;

            _mockFacilitySpecificConfigurationRepository.Setup(f => f.GetAsync(facilityKey)).ReturnsAsync(existingfacilitySpecificConfiguration);
            _mockFacilitySpecificConfigurationRepository.Setup(f => f.Add(It.IsAny<SiteConfiguration.API.FacilityConfiguration.Models.FacilityLogisticsConfig>())).Verifiable();


            TransactionQueueConfigurationRequest facilitySpecificConfigurationToBeAdded = new TransactionQueueConfigurationRequest()
            {
                FacilityKey = facilityKey
            };

            await _facilitySpecificConfigurationLogic.CreateFacilitySpecificConfigurationAsync(facilitySpecificConfigurationToBeAdded);

            // Assert
            _mockFacilitySpecificConfigurationRepository.Verify(f => f.Add(It.IsAny<SiteConfiguration.API.FacilityConfiguration.Models.FacilityLogisticsConfig>()), Times.Once);
            _unitOfWorkMock.Verify(w => w.CommitChangesAsync(), Times.Once);
        }

        #endregion

        #region CreateFacilityExtensionAsync

        #region Exceptions Logic  CreateFacilityExtensionAsync
        [Fact]
        public void FacilitySpecificConfigurationLogic_CreateFacilityExtensionAsync_ShouldThrowArgumentNulExceptionOnNullInput()
        {
            //Arrange           
            FacilityLogisticsConfigurationExtension facilityExtensions = null;
            //Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _facilitySpecificConfigurationLogic.CreateFacilityExtensionAsync(facilityExtensions));
            //Assert
        }

        [Fact]
        public void FacilitySpecificConfigurationLogic_CreateFacilityExtensionAsync_ShouldThrowArgumentNulExceptionOnEmptyFacilityKeyInput()
        {
            //Arrange
            FacilityLogisticsConfigurationExtension facilityExtensions = new FacilityLogisticsConfigurationExtension() { FacilityKey = Guid.Empty };
            //Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _facilitySpecificConfigurationLogic.CreateFacilityExtensionAsync(facilityExtensions));
            //Assert
        }

        #endregion

        #region DB Entry CreateFacilityExtensionAsync
        [Fact]
        public async Task FacilitySpecificConfigurationLogic_CreateFacilityExtensionAsync_ShouldAddAndCommitRecordOnProperInput()
        {

            _unitOfWorkMock.Setup(u => u.CommitChangesAsync()).ReturnsAsync(1);

            Guid facilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            FacilityLogisticsConfig existingfacilitySpecificConfiguration = null;

            _mockFacilitySpecificConfigurationRepository.Setup(f => f.GetAsync(facilityKey)).ReturnsAsync(existingfacilitySpecificConfiguration);
            _mockFacilitySpecificConfigurationRepository.Setup(f => f.Add(It.IsAny<FacilityLogisticsConfig>())).Verifiable();


            FacilityLogisticsConfigurationExtension facilityExtensions = new FacilityLogisticsConfigurationExtension()
            {
                FacilityKey = facilityKey
            };

            await _facilitySpecificConfigurationLogic.CreateFacilityExtensionAsync(facilityExtensions);

            // Assert
            _mockFacilitySpecificConfigurationRepository.Verify(f => f.Add(It.IsAny<FacilityLogisticsConfig>()), Times.Once);
            _unitOfWorkMock.Verify(w => w.CommitChangesAsync(), Times.Once);
        }

        #endregion

        #endregion

        #region UpdateFacilityConfigAsync
        [Fact]
        public void FacilitySpecificConfigurationLogic_UpdateFacilityConfigAsync_ShouldBusinessResponseWithIsSuccessFalseOnNullParameter()
        {
            //Arrange           
            TransactionQueueConfigurationRequest facilityExtensions = null;
            //Act
            var result = _facilitySpecificConfigurationLogic.UpdateFacilityConfigAsync(facilityExtensions).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void FacilitySpecificConfigurationLogic_UpdateFacilityConfigAsync_ShouldReturnBusinessResponseWithIsSuccessFalseWhenFacilityNotExists()
        {
            //Arrange           
            TransactionQueueConfigurationRequest facilityExtensions = new TransactionQueueConfigurationRequest() { FacilityKey = Guid.Parse("f37613a6-2bb9-4282-845e-1c6100dd3f6c") };
            //Act
            FacilityLogisticsConfig facilitykeyNotExist = null;
            _mockFacilitySpecificConfigurationRepository.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync(facilitykeyNotExist);
            var result = _facilitySpecificConfigurationLogic.UpdateFacilityConfigAsync(facilityExtensions).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(BusinessError.RecordNotFound, result.Message);
        }
        [Fact]
        public void FacilitySpecificConfigurationLogic_UpdateFacilityConfigAsync_ShouldBusinessResponseWithIsSuccessFalseOnEmptyFacilityKey()
        {
            //Arrange           
            TransactionQueueConfigurationRequest facilityExtensions = new TransactionQueueConfigurationRequest() { FacilityKey = Guid.Empty };
            //Act
            var result = _facilitySpecificConfigurationLogic.UpdateFacilityConfigAsync(facilityExtensions).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void FacilitySpecificConfigurationLogic_UpdateFacilityConfigAsync_ShouldReturnBusinessResponseWithIsSuccess()
        {
            //Arrange           
            TransactionQueueConfigurationRequest facilityExtensions = new TransactionQueueConfigurationRequest() { FacilityKey = Guid.Parse("f37613a6-2bb9-4282-845e-1c6100dd3f6c") };
            //Act
            FacilityLogisticsConfig facilitykeyNotExist = new FacilityLogisticsConfig();
            _mockFacilitySpecificConfigurationRepository.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync(facilitykeyNotExist);
            _mockFacilitySpecificConfigurationRepository.Setup(f => f.Update(It.IsAny<FacilityLogisticsConfig>())).Verifiable();

            _unitOfWorkMock.Setup(u => u.CommitChangesAsync()).ReturnsAsync(1);

            var result = _facilitySpecificConfigurationLogic.UpdateFacilityConfigAsync(facilityExtensions).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(BusinessResponseMessages.LogisticsConfigurationUpdated, result.Message);

            _mockFacilitySpecificConfigurationRepository.Verify(f => f.Update(It.IsAny<FacilityLogisticsConfig>()), Times.Once);
            _unitOfWorkMock.Verify(w => w.CommitChangesAsync(), Times.Once);
        }
        #endregion

        #region UpdateFacilityExtensionAsync

        #region Exceptions Logic  UpdateFacilityExtensionAsync
        [Fact]
        public void FacilitySpecificConfigurationLogic_UpdateFacilityExtensionAsync_ShouldThrowArgumentNulExceptionOnNullInput()
        {
            //Arrange           
            FacilityLogisticsConfigurationExtension facilityExtensions = null;
            //Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _facilitySpecificConfigurationLogic.UpdateFacilityExtensionAsync(facilityExtensions));
            //Assert
        }

        [Fact]
        public void FacilitySpecificConfigurationLogic_UpdateFacilityExtensionAsync_ShouldThrowArgumentNulExceptionOnEmptyFacilityKeyInput()
        {
            //Arrange
            FacilityLogisticsConfigurationExtension facilityExtensions = new FacilityLogisticsConfigurationExtension() { FacilityKey = Guid.Empty };
            //Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _facilitySpecificConfigurationLogic.UpdateFacilityExtensionAsync(facilityExtensions));
            //Assert
        }

        #endregion

        #region DB Entry UpdateFacilityExtensionAsync
        [Fact]
        public async Task FacilitySpecificConfigurationLogic_UpdateFacilityExtensionAsync_ShouldUpdateAndCommitRecordOnProperInput()
        {

            _unitOfWorkMock.Setup(u => u.CommitChangesAsync()).ReturnsAsync(1);

            Guid facilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            FacilityLogisticsConfig existingfacilitySpecificConfiguration = new FacilityLogisticsConfig() { FacilityKey = facilityKey };

            _mockFacilitySpecificConfigurationRepository.Setup(f => f.GetAsync(facilityKey)).ReturnsAsync(existingfacilitySpecificConfiguration);
            _mockFacilitySpecificConfigurationRepository.Setup(f => f.Update(It.IsAny<FacilityLogisticsConfig>())).Verifiable();


            FacilityLogisticsConfigurationExtension facilityExtensions = new FacilityLogisticsConfigurationExtension()
            {
                FacilityKey = facilityKey
            };

            await _facilitySpecificConfigurationLogic.UpdateFacilityExtensionAsync(facilityExtensions);

            // Assert
            _mockFacilitySpecificConfigurationRepository.Verify(f => f.Update(It.IsAny<FacilityLogisticsConfig>()), Times.Once);
            _unitOfWorkMock.Verify(w => w.CommitChangesAsync(), Times.Once);
        }

        #endregion

        #endregion
    }
}
