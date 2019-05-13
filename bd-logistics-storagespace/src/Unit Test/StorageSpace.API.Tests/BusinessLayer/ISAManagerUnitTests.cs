using Microsoft.Extensions.Logging;
using Moq;
using StorageSpace.API.BusinessLayer.Concrete;
using StorageSpace.API.BusinessLayer.Contracts;
using StorageSpace.API.Infrastructure.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Xunit;
using System.Threading.Tasks;
using StorageSpace.API.Model;
using MongoDB.Bson;

namespace StorageSpace.API.Tests.BusinessLayer
{
    /// <summary>
    /// ISAManagerUnitTests.
    /// </summary>
    public class ISAManagerUnitTests 
    {
        /// <summary>
        /// The subject test
        /// </summary>
        private IISAManager subject_test;
        /// <summary>
        /// The mock repo
        /// </summary>
        private readonly Mock<IISADetailRepository> mockRepo;
        /// <summary>
        /// The mock logger
        /// </summary>
        private Mock<ILogger<ISAManager>> mockLogger;
        /// <summary>
        /// The mock mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ISAManagerUnitTests"/> class.
        /// </summary>
        public ISAManagerUnitTests()
        {
            mockRepo = new Mock<IISADetailRepository>();
            mockLogger = new Mock<ILogger<ISAManager>>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new API.AutoMapper.MapProfile());
            });
            _mapper = mockMapper.CreateMapper();

            subject_test = new ISAManager(mockRepo.Object, _mapper, mockLogger.Object);
        }

        /// <summary>
        /// Defines the test method GetISA.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISA()
        {
            //Arrange
            mockRepo.Reset();
            mockRepo.Setup(b => b.GetISA(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<ISARequest>()
                {
                    new ISARequest(){ IsaId = "123", Description = "testDescription1", IsaType = "isaType1", Active = true },
                    new ISARequest(){ IsaId = "124", Description = "testDescription2", IsaType = "isaType2", Active = false}
                }));

            //Act
            var result = await subject_test.GetISA(It.IsAny<Guid>());

            //Assert
            mockRepo.Verify();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("testDescription2", result[1].Description);
        }

        /// <summary>
        /// Defines the test method GetISAById.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISAById()
        {
            var id = "1234";
            var guid = Guid.NewGuid();
            var createdDate = DateTime.Now;
            var modifiedDate = DateTime.Now;
            var dummyItemStorageSpace = GetMockItemStorageSpace(id, guid, createdDate, modifiedDate);
            var dummyResponse = GetMockISA(dummyItemStorageSpace);
            //Arrange
            mockRepo.Reset();
            mockRepo.Setup(b => b.GetISAById(It.IsAny<String>(),It.IsAny<Guid>()))
                .ReturnsAsync(dummyItemStorageSpace);

            //Act
            var result = await subject_test.GetISAById(id,guid);

            //Assert
            mockRepo.Verify();
            Assert.NotNull(result);
        }

        /// <summary>
        /// Defines the test method GetISAByIdUnitNegativeNotFoundTest.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetISAByIdUnitNegativeNotFoundTest()
        {
            var guid = Guid.NewGuid();
            //Arrange
            mockRepo.Reset();
            mockRepo.Setup(b => b.GetISAById(It.IsAny<string>(),It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await subject_test.GetISAById(It.IsAny<string>(),guid);

            //Assert
            mockRepo.Verify();
            Assert.Null(result);
        }

        /// <summary>
        /// Gets the mock item storage space.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="createdDate">The created date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <returns>ItemStorageSpace.</returns>
        private ItemStorageSpace GetMockItemStorageSpace(string id,Guid guid, DateTime createdDate, DateTime modifiedDate)
        {
            return new ItemStorageSpace()
            {
                Id = id,
                CreatedByActorKey = 4,
                CreatedDateUTCDateTime = createdDate,
                LastModifiedByActorKey = 4,
                LastModifiedUTCDateTime = modifiedDate,
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

        /// <summary>
        /// Gets the mock isa.
        /// </summary>
        /// <param name="itemStorageSpace">The item storage space.</param>
        /// <returns>ISA.</returns>
        private ISA GetMockISA(ItemStorageSpace itemStorageSpace)
        {
            return _mapper.Map<ISA>(itemStorageSpace);
        }
    }
}
