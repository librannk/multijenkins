//using Microsoft.Extensions.Logging;
//using Moq;
//using Xunit;
//using System.Threading.Tasks;
//using TransactionQueue.API.Application.Entities;
//using TransactionQueue.API.BussinessLayer.Concrete;
//using TransactionQueue.API.DataLayer.Abstraction;

//namespace TransactionQueue.UnitTest.BussinessLayer
//{
//    public class FormularyManagerTest
//    {
//        #region Private Fields
//        private readonly Mock<ILogger<FormularyManager>> _logger;
//        private readonly Mock<IFormularyDAL> _mockFormularyDAL;
//        private FormularyManager _formularyManager;
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Initializes the private fields
//        /// </summary>
//        public FormularyManagerTest()
//        {
//            _logger = new Mock<ILogger<FormularyManager>>();
//            _mockFormularyDAL = new Mock<IFormularyDAL>();
//        }

//        #endregion

//        #region Test Methods

//        [Fact]
//        public async Task ProcessFormularyRequest_ShouldCallUpdateFormulary()
//        {
//            //Arrange
//            var data = CreateRequest();

//            _mockFormularyDAL.Setup(x => x.GetFormularyById(data.FormularyId)).ReturnsAsync(data);
//            _mockFormularyDAL.Setup(x => x.UpdateFormulary(data));
//            _formularyManager = new FormularyManager(_mockFormularyDAL.Object, _logger.Object);

//            //Act
//            _formularyManager.ProcessFormularyRequest(data);

//            //Assert
//            _mockFormularyDAL.Verify(x => x.UpdateFormulary(data), Times.Once);
//        }

//        #endregion

//        #region private method

//        /// <summary>
//        /// Mock Formulary request
//        /// </summary>
//        /// <returns></returns>
//        private Formulary CreateRequest()
//        {
//            Formulary model = new Formulary();
//            model.FormularyId = 1;
//            model.IsActive = true;
//            model.Description = "Description";
//            return model;
//        }
//        #endregion
//    }
//}
