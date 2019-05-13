using AutoMapper;
using Formulary.API.AutoMapper;
using Formulary.API.BusinessLayer.Concrete;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Formulary.API.Model;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FormularyAPIUnitTests.BusinessLayer
{
    public class SystemItemSetUpManagerUnitTests
    {
        /// <summary>
        /// The mock item repository
        /// </summary>
        private readonly Mock<IItemRepository> _mockItemRepository;

        /// <summary>
        /// The mock mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The mock system item set up manager
        /// </summary>
        private readonly ISystemItemSetUpManager _subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemItemSetUpManagerUnitTests"/> class.
        /// </summary>
        public SystemItemSetUpManagerUnitTests()
        {
            _mapper = new Mapper(new MapperConfiguration(mapper => { mapper.AddProfile<SystemItemSetUpMapProfile>(); }));
            _mockItemRepository = new Mock<IItemRepository>();
            var mockLogger = new Mock<ILogger<SystemItemSetUpManager>>();
            _subject = new SystemItemSetUpManager(_mockItemRepository.Object, _mapper);
        }

        /// <summary>
        /// Defines the test method UpdateSystemItemSetUp.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task UpdateSystemItemSetUp()
        {
            //Arrange
            var itemkey = Guid.NewGuid();
            var item = GetMockItem(itemkey);


            _mockItemRepository.Reset();
            _mockItemRepository.Setup(b => b.GetItemById(itemkey))
                .ReturnsAsync(item);
            _mockItemRepository.Setup(b => b.UpdateSystemItemSetUp(item))
                .ReturnsAsync(true);

            //Act
            var result = await _subject.UpdateSystemItemSetUp(itemkey, new SystemItemSetupRequest()
            {
                Description = "test Description"
            });


            //Assert
            _mockItemRepository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.Success, result.OperationResult);
        }


        /// <summary>
        /// Update item test Negative case Not found
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task UpdateSystemItemSetUpNegativeNotFoundTest()
        {
            //Arrange
            var itemkey = Guid.NewGuid();

            _mockItemRepository.Reset();
            _mockItemRepository.Setup(b => b.GetItemById(itemkey))
                .ReturnsAsync(() => null);


            //Act
            var result = await _subject.UpdateSystemItemSetUp(itemkey, new SystemItemSetupRequest() { Description = "test Description" });


            //Assert
            _mockItemRepository.Verify(b => b.GetItemById(itemkey));
            _mockItemRepository.VerifyNoOtherCalls();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.NotFound, result.OperationResult);
            Assert.Null(result.Object);
        }

        /// <summary>
        /// Update item test Negative - Validation failed at Db
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task UpdateSystemItemSetUpNegativeValidationFailedTest()
        {
            //Arrange
            var itemkey = Guid.NewGuid();
            var item = GetMockItem(itemkey);

            _mockItemRepository.Reset();
            _mockItemRepository.Setup(b => b.GetItemById(itemkey))
                .ReturnsAsync(item);
            _mockItemRepository.Setup(b => b.UpdateSystemItemSetUp(item))
                .ReturnsAsync(false);

            //Act
            var result = await _subject.UpdateSystemItemSetUp(itemkey, new SystemItemSetupRequest() { Description = "test Description" });


            //Assert
            _mockItemRepository.Verify(b => b.GetItemById(itemkey));
            _mockItemRepository.Verify(b => b.UpdateSystemItemSetUp(It.IsAny<ItemEntity>()));
            _mockItemRepository.VerifyNoOtherCalls();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.ValidationFailed, result.OperationResult);
            Assert.Null(result.Object);
        }

        /// <summary>
        /// Gets the dummy facility.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="createdDate">The created date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <returns>FacilityEntity.</returns>
        private ItemEntity GetMockItem(Guid guid)
        {
            return new ItemEntity()
            {
                ItemKey = guid,
                ItemID = "string",
                AlternateItemID = "string",
                MedicationItem = new MedicationItemEntity()
                {
                    MedicationItemKey = guid,
                    ActiveFlag = true,
                    MedicationClassKey = null,
                    Description = "test Description",
                    GLAccountKey = guid,
                    DispenseFormLookupKey = guid,
                    DispenseUnitLookupKey = guid,
                    StrengthAmount = 0,
                    ConcentrationVolumeAmount = 0,
                    TotalVolumeAmount = 0,
                    StrengthUnitOfMeasureKey = guid,
                    ConcentrationVolumeUnitOfMeasureKey = guid,
                    ConcentrationVolumeExternalUnitOfMeasureKey = guid,
                    TotalVolumeUnitOfMeasureKey = guid,
                    ChargeCode = "chargecode",
                    HighRiskFlag = true,
                    LASAFlag = true,
                    DrugFlag = true,
                    FreezerFlag = true,
                    ChemotherapyFlag = true,
                    RefrigeratedFlag = true,
                    NonFormularyFlag = true,
                    HazToxicFlag = true,
                    HazAerosolFlag = true,
                    HazOxidizerFlag = true,
                    HazChemicalFlag = true,
                    HazAcidFlag = true,
                    HazBaseFlag = true,
                    ChemoAgentFlag = true,
                    BioHazFlag = true,
                }
            };

        }

        /// <summary>
        /// Gets the mock formulary request.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>MedicationItem.</returns>
        private SystemItemSetupRequest GetMockFormularyRequest(ItemEntity item)
        {
            return _mapper.Map<ItemEntity, SystemItemSetupRequest>(item);
        }
    }
}
