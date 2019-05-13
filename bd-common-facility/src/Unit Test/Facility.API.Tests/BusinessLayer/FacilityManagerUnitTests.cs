using AutoMapper;
using Facility.API.Automapper;
using Facility.API.BusinessLayer.Concrete;
using Facility.API.BusinessLayer.Contracts;
using Facility.API.Constants;
using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Facility.API.Model;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Facility.API.Tests.BusinessLayer
{
    /// <summary>
    /// Unit Tests for FacilityManager
    /// </summary>
    public class FacilityManagerUnitTests
    {
        /// <summary>
        /// Automapper Interface for mapping.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The subject Under Test ie FacilityManager instance
        /// </summary>
        private readonly IFacilityManager _subjectUnderTest;
        /// <summary>
        /// The mock repo
        /// </summary>
        private readonly Mock<IFacilityRepository> _mockRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityManagerUnitTests"/> class.
        /// </summary>
        public FacilityManagerUnitTests()
        {
            _mapper = new Mapper(new MapperConfiguration(mapper => { mapper.AddProfile<FacilityMapProfile>(); }));
            _mockRepo = new Mock<IFacilityRepository>();
            var mockLogger = new Mock<ILogger<FacilityManager>>();
            _subjectUnderTest = new FacilityManager(_mockRepo.Object, mockLogger.Object, _mapper);
        }

        /// <summary>
        /// Defines the test method GetFacilitiesList.
        /// </summary>
        /// <returns>Task.</returns>
        [Theory]
        [InlineData(false, 0, 0, 6, 6)]
        [InlineData(true, 0, 0, 6, 6)]
        [InlineData(true, 0, 3, 3, 4)]
        public async Task GetFacilitiesListTest(bool searchInActive, int offset, int limit, int resultCount, int totalCount)
        {
            //Arrange
            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetAllFacilitiesAsync(It.IsAny<bool>(), It.IsAny<string>()))
                .ReturnsAsync(new List<FacilityEntity>()
                {
                    new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC01"},
                    new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC02"},
                    new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC03"},
                    new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC04"},
                    new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC05"},
                    new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC06"}
                });
            _mockRepo.Setup(b => b.GetAllFacilitiesWithPaginationAsync(true, It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() =>
                {
                    var facilities = new List<FacilityEntity>()
                    {
                        new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC01"},
                        new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC02"},
                        new FacilityEntity() {FacilityKey = Guid.NewGuid(), FacilityCode = "FC03"},
                    };
                    return (facilities, 4);
                });

            //Act
            var result = await _subjectUnderTest.GetAllFacilities(searchInActive, "", offset, limit);

            //Assert
            _mockRepo.Verify();
            Assert.NotNull(result);
            Assert.Equal(resultCount, result.Object.Count);
            Assert.Equal(totalCount, result.ResultCount);
        }

        /// <summary>
        /// Test case for GetFacility By Id
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetFacilityByIdTest()
        {
            var guid = Guid.NewGuid();
            var createdDate = DateTimeOffset.Now;
            var modifiedDate = DateTimeOffset.Now;
            var dummyFacility = GetMockFacility(guid, createdDate, modifiedDate);
            var dummyResponse = GetMockFacilityRequest(dummyFacility);
            //Arrange
            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(dummyFacility);

            //Act
            var result = await _subjectUnderTest.GetFacilityByKeyAsync(guid);

            //Assert
            _mockRepo.Verify();
            Assert.NotNull(result);
            Assert.Equal(dummyResponse.FacilityKey, result.FacilityKey);
            Assert.Equal(dummyResponse.FacilityCode, result.FacilityCode);
        }

        /// <summary>
        /// Test for GetFacilityById - Negative test case for not found
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetFacilityByIdUnitNegativeNotFoundTest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await _subjectUnderTest.GetFacilityByKeyAsync(guid);

            //Assert
            _mockRepo.Verify();
            Assert.Null(result);
        }

        /// <summary>
        /// Tests for AddFacility
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task AddFacilityTest()
        {
            //Arrange
            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetFacilityByCode(It.IsAny<string>())).ReturnsAsync(() => null);
            _mockRepo.Setup(b => b.AddFacility(It.IsAny<FacilityEntity>())).ReturnsAsync(() => true);

            //Act
            var result = await _subjectUnderTest.Add(new NewFacilityRequest());

            //Assert
            _mockRepo.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.Success, result.OperationResult);
        }

        /// <summary>
        /// Tests for AddFacility - Existing Facility
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task AddFacilityNegativeFacilityExistsTest()
        {
            //Arrange
            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetFacilityByCode(It.IsAny<string>())).ReturnsAsync(() => new FacilityEntity());
            _mockRepo.Setup(b => b.AddFacility(It.IsAny<FacilityEntity>())).ReturnsAsync(() => true);

            //Act
            var result = await _subjectUnderTest.Add(new NewFacilityRequest() { FacilityCode = "FC01" });

            //Assert
            _mockRepo.Verify(b => b.GetFacilityByCode("FC01"));
            _mockRepo.VerifyNoOtherCalls();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.ErrorAlreadyExists, result.OperationResult);
        }

        /// <summary>
        /// Tests for AddFacility - Negative case Creation Error
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task AddFacilityNegativeCreationErrorTest()
        {
            //Arrange
            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetFacilityByCode(It.IsAny<string>())).ReturnsAsync(() => null);
            _mockRepo.Setup(b => b.AddFacility(It.IsAny<FacilityEntity>())).ReturnsAsync(() => false);

            //Act
            var result = await _subjectUnderTest.Add(new NewFacilityRequest() { FacilityCode = "FC01" });

            //Assert
            _mockRepo.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.ValidationFailed, result.OperationResult);
        }

        /// <summary>
        /// Tests for SearchFacility
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task SearchFacilityTest()
        {
            //Arrange
            _mockRepo.Reset();
            _mockRepo.Setup(b => b.SearchFacilitiesByNameAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(() => new List<FacilityEntity>()
                    {new FacilityEntity(), new FacilityEntity(), new FacilityEntity()});

            //Act
            var result = await _subjectUnderTest.SearchFacility("FC01", false);

            //Assert
            _mockRepo.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task UpdateFacilityTest()
        {
            //Arrange
            var facilityKey = Guid.NewGuid();
            var creationDate = DateTimeOffset.Now;
            var modificationDate = DateTimeOffset.Now;
            var facility = GetMockFacility(facilityKey, creationDate, modificationDate);


            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetAsync(facilityKey))
                .ReturnsAsync(facility);
            _mockRepo.Setup(b => b.UpdateFacility(facility))
                .ReturnsAsync(true);

            //Act
            var result = await _subjectUnderTest.Update(facilityKey, new UpdateFacilityRequest() { FacilityName = "New Name" });


            //Assert
            _mockRepo.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.Success, result.OperationResult);
            Assert.Equal("New Name", result.Object.FacilityName);
        }


        /// <summary>
        /// Update Facility test Negative case Not found
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task UpdateFacilityNegativeNotFoundTest()
        {
            //Arrange
            var facilityKey = Guid.NewGuid();

            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetAsync(facilityKey))
                .ReturnsAsync(() => null);


            //Act
            var result = await _subjectUnderTest.Update(facilityKey, new UpdateFacilityRequest() { FacilityName = "New Name" });


            //Assert
            _mockRepo.Verify(b => b.GetAsync(facilityKey));
            _mockRepo.VerifyNoOtherCalls();
            Assert.NotNull(result);
            Assert.Equal(CreateUpdateResultEnum.NotFound, result.OperationResult);
            Assert.Null(result.Object);
        }

        /// <summary>
        /// Update Facility test Negative - Validation failed at Db
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task UpdateFacilityNegativeValidationFailedTest()
        {
            //Arrange
            var facilityKey = Guid.NewGuid();
            var facility = GetMockFacility(facilityKey, DateTimeOffset.Now, DateTimeOffset.Now);

            _mockRepo.Reset();
            _mockRepo.Setup(b => b.GetAsync(facilityKey))
                .ReturnsAsync(facility);
            _mockRepo.Setup(b => b.UpdateFacility(facility))
                .ReturnsAsync(false);

            //Act
            var result = await _subjectUnderTest.Update(facilityKey, new UpdateFacilityRequest() { FacilityName = "New Name" });


            //Assert
            _mockRepo.Verify(b => b.GetAsync(facilityKey));
            _mockRepo.Verify(b => b.UpdateFacility(It.IsAny<FacilityEntity>()));
            _mockRepo.VerifyNoOtherCalls();
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
        private FacilityEntity GetMockFacility(Guid guid, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        {
            return new FacilityEntity()
            {
                FacilityKey = guid,
                FacilityCode = "FC01",
                ActiveFlag = true,
                CityName = "city",
                DescriptionText = "description",
                StreetAddressText = "street",
                FacilityName = "facility",
                CountryName = "country",
                CreatedByActorKey = "actor",
                CreatedDateTime = createdDate,
                CustomerContactName = "name",
                CustomerContactEmailAddressValue = "email",
                CustomerContactFaxNumberText = "fax",
                CustomerContactPhoneNumberText = "number",
                LastModifiedByActorKey = "modifiedActor",
                LastModifiedDateTime = modifiedDate,
                PostalCode = "11000",
                SiteId = "siteid",
                RxLicenseId = "rxLicense",
                StreetAddress2Text = "address2",
                SubDivisionName = "subdiv",
                TenantKey = guid,
                TimeZoneId = "UTC",
                PharmacyInformationSystemKey = guid
            };

        }

        /// <summary>
        /// Gets the dummy facility request.
        /// </summary>
        /// <param name="facility">The facility.</param>
        /// <returns>Facility.</returns>
        private Model.Facility GetMockFacilityRequest(FacilityEntity facility)
        {
            return _mapper.Map<FacilityEntity, Model.Facility>(facility);
        }
    }
}
