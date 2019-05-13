//using Microsoft.Extensions.Logging;
//using Moq;
//using System.Threading.Tasks;
//using TransactionQueue.API.Application.Entities;
//using TransactionQueue.API.BussinessLayer.Concrete;
//using TransactionQueue.API.DataLayer.Abstraction;
//using Xunit;

//namespace TransactionQueue.UnitTest.EventHandlers
//{
//    /// <summary>
//    /// This class contains unit test for FormularyUpdatedIntegrationEventHandlerUnitTest
//    /// </summary>

//    public class FormularyUpdatedIntegrationEventHandlerUnitTest
//    {
//        /// <summary>
//        /// This event should get data from data bus and store that in DB
//        /// </summary>

//        [Fact]
//        public void ProcessFormularyRequest_ShouldCallInsertFormulary()
//        {
//            //Arrange
//            var fakeLog = new Mock<ILogger<FormularyManager>>();
//            var fakeFormularyDAL = new Mock<IFormularyDAL>();
//            fakeFormularyDAL.Setup(x => x.InsertFormulary(It.IsAny<Formulary>()));
//            FormularyManager manager = new FormularyManager(fakeFormularyDAL.Object, fakeLog.Object);
//            Formulary request = new Formulary();

//            //Act
//            manager.ProcessFormularyRequest(request);

//            //Assert
//            fakeFormularyDAL.Verify(x => x.InsertFormulary(It.IsAny<Formulary>()), Times.Once);
//        }

//        [Fact]
//        public async Task ProcessFormularyRequest_ShouldCallUpdateFormulary()
//        {
//            //Arrange
//            var fakeLog = new Mock<ILogger<FormularyManager>>();
//            var fakeFormularyDAL = new Mock<IFormularyDAL>();
//            Formulary data = new Formulary
//            {
//                FormularyId = 1,
//                IsActive = true
//            };
//            fakeFormularyDAL.Setup(x => x.GetFormularyById(data.FormularyId)).ReturnsAsync(data);
//            fakeFormularyDAL.Setup(x => x.UpdateFormulary(data));
//            FormularyManager manager = new FormularyManager(fakeFormularyDAL.Object, fakeLog.Object);

//            //Act
//            manager.ProcessFormularyRequest(data);

//            //Assert
//            fakeFormularyDAL.Verify(x => x.UpdateFormulary(It.IsAny<Formulary>()), Times.Once);
//        }
//    }
//}
