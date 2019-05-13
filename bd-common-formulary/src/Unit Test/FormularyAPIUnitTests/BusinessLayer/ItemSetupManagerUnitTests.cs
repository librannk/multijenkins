using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Formulary.API.AutoMapper;
using Formulary.API.BusinessLayer.Concrete;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Moq;
using Xunit;

namespace FormularyAPIUnitTests.BusinessLayer
{
    public class ItemSetupManagerUnitTests
    {
        private readonly Mock<IItemRepository> mockItemRepo;
        private readonly ItemSetupManager subject;
        private readonly IMapper mapper;

        public ItemSetupManagerUnitTests()
        {
            mapper = new Mapper(new MapperConfiguration(mapper => { mapper.AddProfile<SystemItemSetUpMapProfile>(); }));
            mockItemRepo = new Mock<IItemRepository>();
            subject = new ItemSetupManager(mockItemRepo.Object, mapper);
        }

        [Fact]
        public async Task GetMedicationItemsTestAsync()
        {
            //Arrange 
            mockItemRepo.Setup(b => b.GetAllMedicationItemsForList())
                .ReturnsAsync(new List<ItemEntity>() { new ItemEntity() { ProductIdentifications = new List<ProductIdentificationEntity>() }, new ItemEntity() { ProductIdentifications = new List<ProductIdentificationEntity>() } });

            //Act
            var result = await subject.GetMedicationItems();

            //Assert
            mockItemRepo.Verify();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
