using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.Controllers;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;
using Xunit;

namespace Test.TransactionPriority
{
    public class TransactionPriorityControllerTest
    {
        #region Private Fields
        private readonly Mock<ITransactionPriorityManager> _manager;
        private readonly Mock<ILogger<TransactionPriorityController>> _logger;
        private readonly Mock<IValidator<TransactionPriorityPost>> _entityValidator;
        private readonly TransactionPriorityController _controller;
        #endregion

        #region Constructor

        public TransactionPriorityControllerTest()
        {
            _manager = new Mock<ITransactionPriorityManager>();
            _logger = new Mock<ILogger<TransactionPriorityController>>();
            _entityValidator = new Mock<IValidator<TransactionPriorityPost>>();
            _controller = new TransactionPriorityController(_logger.Object, _manager.Object, _entityValidator.Object);
            //
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["X-Requested-With"]).Returns("XMLHttpRequest");

            var context = new Mock<HttpContext>();
            context.SetupGet(x => x.Request).Returns(request.Object);

           

            //
            RouteValueDictionary routeDictionary = new RouteValueDictionary();
            routeDictionary.Add("facilitykey", Guid.Parse("BF521211-CEAF-4DCA-82C7-40446D4C46ED"));
            _controller.ControllerContext = new ControllerContext(new ActionContext(context.Object, new RouteData(routeDictionary), new ControllerActionDescriptor()));
           
        }
        #endregion

        #region Test Cases


        [Fact]
        public async Task PutTransactionPriority_NoTransactionPriorityUpdated_Test()
        {

            //Arrange
            
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccesss = false };
            _manager.Setup(m => m.UpdateTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<TransactionPriorityPut>(), It.IsAny<Guid>(),It.IsAny<Dictionary<string,string>>())).ReturnsAsync(objBusinessResponse);


            //Act
            BadRequestObjectResult notFoundObjectResult = (BadRequestObjectResult)(await _controller.PutTransactionPriority("",new TransactionPriorityPut()));

            //Assert
            Assert.Equal(400, notFoundObjectResult.StatusCode);

        }

        [Fact]
        public async Task PutTransactionPriority_TransactionPriorityUpdated_Test()
        {

            //Arrange
                      BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccesss = true };
            _manager.Setup(m => m.UpdateTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<TransactionPriorityPut>(), It.IsAny<Guid>(),It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);


            //Act
            OkObjectResult okObjectResult = (OkObjectResult)(await _controller.PutTransactionPriority("", new TransactionPriorityPut()));

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);

        }

        [Fact]
        public async Task PutTransactionPriority_ThrowException_Test()
        {

            //Arrange
            List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut = new List<TransactionPrioritySmartSortPut>();
            _manager.Setup(m => m.UpdateTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<TransactionPriorityPut>(), It.IsAny<Guid>(),It.IsAny<Dictionary<string, string>>())).ThrowsAsync(new Exception());

            //Act
            StatusCodeResult statusCodeResult = (StatusCodeResult)(await _controller.PutTransactionPriority("", new TransactionPriorityPut()));

            //Assert
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task GetTransactionPriority_NoTransactionPriorityFound_Test()
        {

            //Arrange
            IEnumerable<TransactionPriorityGet> listTransactionPriorityGet = null;
            Task<IEnumerable<TransactionPriorityGet>> taskListTransactionPriorityGet = Task.FromResult<IEnumerable<TransactionPriorityGet>>(listTransactionPriorityGet);
            _manager.Setup(m => m.GetAllTransactionPriorityASync(0, 0, false, "707FC9C1-BDEB-417F-8546-028FA9B4CE54")).Returns(taskListTransactionPriorityGet);


            //Act
            NotFoundObjectResult notFoundObjectResult = (NotFoundObjectResult)(await   _controller.GetTransactionPriority(0, 0, true)).Result;

            //Assert
            Assert.Equal(404,notFoundObjectResult.StatusCode);

        }


        [Fact]
        public async Task GetTransactionPriority_TransactionPriorityFound_Test()
        {

            //Arrange
            IEnumerable<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>() { new TransactionPriorityGet() {PriorityCode="TPCode" } };
            _manager.Setup(m => m.GetAllTransactionPriorityASync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(),It.IsAny<string>())).ReturnsAsync(listTransactionPriorityGet);

            //Act
            var enumerTransactionPriorityGetResult = (await _controller.GetTransactionPriority(0, 0, true)).Value;
            List<TransactionPriorityGet> listTransactionPriority = (List<TransactionPriorityGet>)enumerTransactionPriorityGetResult;

            //Assert
             Assert.True(listTransactionPriority.Count==1);

        }


        [Fact]
        public async Task GetTransactionPriority_ThrowException_Test()
        {

            //Arrange
            _manager.Setup(m => m.GetAllTransactionPriorityASync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            //Act
            StatusCodeResult statusCodeResult =(StatusCodeResult) (await _controller.GetTransactionPriority(0, 0, true)).Result;
         
            //Assert
            Assert.Equal(500, statusCodeResult.StatusCode);

        }


        [Fact]
        public async Task GetTransactionPrioritySearch_NoTransactionPriorityFound_Test()
        {

            //Arrange
            IEnumerable<TransactionPriorityGet> listTransactionPriorityGet = null;
            Task<IEnumerable<TransactionPriorityGet>> taskListTransactionPriorityGet = Task.FromResult<IEnumerable<TransactionPriorityGet>>(listTransactionPriorityGet);
            _manager.Setup(m => m.GetAllSerachedTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(taskListTransactionPriorityGet);


            //Act
            NotFoundObjectResult notFoundObjectResult = (NotFoundObjectResult)(await _controller.GetTransactionPrioritySearch("",0, 0)).Result;

            //Assert
            Assert.Equal(404, notFoundObjectResult.StatusCode);

        }


        [Fact]
        public async Task GetTransactionPrioritySearch_TransactionPriorityFound_Test()
        {

            //Arrange
            IEnumerable<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>() { new TransactionPriorityGet() { PriorityCode = "TPCode" } };
            _manager.Setup(m => m.GetAllSerachedTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(listTransactionPriorityGet);

            //Act
            var enumerTransactionPriorityGetResult = (await _controller.GetTransactionPrioritySearch("", 0, 0)).Value;
            List<TransactionPriorityGet> listTransactionPriority = (List<TransactionPriorityGet>)enumerTransactionPriorityGetResult;

            //Assert
            Assert.True(listTransactionPriority.Count == 1);

        }


        [Fact]
        public async Task GetTransactionPrioritySearch_ThrowException_Test()
        {

            //Arrange
            _manager.Setup(m => m.GetAllSerachedTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            //Act
            StatusCodeResult statusCodeResult = (StatusCodeResult)(await _controller.GetTransactionPrioritySearch("", 0, 0)).Result;

            //Assert
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

       
        [Fact]
        public async Task GetTransactionPrioritySmartSorts_NoTransactionPrioritySmartSortFound_Test()
        {

            //Arrange
            IEnumerable<TransactionPrioritySmartSort> listTransactionPrioritySmartSort = null;
            _manager.Setup(m => m.GetSmartSortForTransactionPriority(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(listTransactionPrioritySmartSort);


            //Act
            NotFoundObjectResult notFoundObjectResult = (NotFoundObjectResult)(await _controller.GetTransactionPrioritySmartSorts("")).Result;

            //Assert
            Assert.Equal(404, notFoundObjectResult.StatusCode);

        }

        [Fact]
        public async Task GetTransactionPrioritySmartSorts_TransactionPrioritySmartSortFound_Test()
        {

            //Arrange
            IEnumerable<TransactionPrioritySmartSort> listTransactionPrioritySmartSort = new List<TransactionPrioritySmartSort>() { new TransactionPrioritySmartSort() { SmartSortName="Destination" } };
            _manager.Setup(m => m.GetSmartSortForTransactionPriority(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(listTransactionPrioritySmartSort);

            //Act
            var enumerTransactionPrioritySmartSortResult = (await _controller.GetTransactionPrioritySmartSorts("")).Value;
            List<TransactionPrioritySmartSort> listTransactionPrioritySmartSortResult = (List<TransactionPrioritySmartSort>)enumerTransactionPrioritySmartSortResult;

            //Assert
            Assert.True(listTransactionPrioritySmartSortResult.Count == 1);

        }

        [Fact]
        public async Task GetTransactionPrioritySmartSorts_ThrowException_Test()
        {

            //Arrange
            _manager.Setup(m => m.GetSmartSortForTransactionPriority(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            //Act
            StatusCodeResult statusCodeResult = (StatusCodeResult)(await _controller.GetTransactionPrioritySmartSorts("")).Result;

            //Assert
            Assert.Equal(500, statusCodeResult.StatusCode);

        }


        [Fact]
        public async Task UpdateTransactionPrioritySmartSorts_NoTransactionPrioritySmartSortUpdated_Test()
        {

            //Arrange
            List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut = new List<TransactionPrioritySmartSortPut>();
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccesss = false};// listTransactionPrioritySmartSort = null;
            _manager.Setup(m => m.PutSmartSortForTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<List<TransactionPrioritySmartSortPut>>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);


            //Act
            NotFoundObjectResult notFoundObjectResult = (NotFoundObjectResult)(await _controller.UpdateTransactionPrioritySmartSorts("", listTransactionPrioritySmartSortPut));

            //Assert
            Assert.Equal(404, notFoundObjectResult.StatusCode);

        }

        [Fact]
        public async Task UpdateTransactionPrioritySmartSorts_TransactionPrioritySmartSortFoundUpdated_Test()
        {

            //Arrange
            List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut = new List<TransactionPrioritySmartSortPut>();
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccesss = true };
            _manager.Setup(m => m.PutSmartSortForTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<TransactionPrioritySmartSortPut>>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);


            //Act
            OkObjectResult okObjectResult =(OkObjectResult) (await _controller.UpdateTransactionPrioritySmartSorts("", listTransactionPrioritySmartSortPut));

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);

        }

        [Fact]
        public async Task UpdateTransactionPrioritySmartSorts_ThrowException_Test()
        {

            //Arrange
            List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut = new List<TransactionPrioritySmartSortPut>();
            _manager.Setup(m => m.PutSmartSortForTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<TransactionPrioritySmartSortPut>>(), It.IsAny<Dictionary<string, string>>())).ThrowsAsync(new Exception());

            //Act
            StatusCodeResult statusCodeResult = (StatusCodeResult)(await _controller.UpdateTransactionPrioritySmartSorts("", listTransactionPrioritySmartSortPut));

            //Assert
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task InsertTransactionPrioritySmartSorts_NoTransactionPrioritySmartSortInserted_Test()
        {

            //Arrange
            List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut = new List<TransactionPrioritySmartSortPut>();
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccesss = false };
            _manager.Setup(m => m.PostSmartSortForTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<TransactionPrioritySmartSortPut>>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);


            //Act
            NotFoundObjectResult notFoundObjectResult = (NotFoundObjectResult)(await _controller.InsertTransactionPrioritySmartSorts("", listTransactionPrioritySmartSortPut));

            //Assert
            Assert.Equal(404, notFoundObjectResult.StatusCode);

        }

        [Fact]
        public async Task InsertTransactionPrioritySmartSorts_TransactionPrioritySmartSortFoundInserted_Test()
        {

            //Arrange
            List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut = new List<TransactionPrioritySmartSortPut>();
            BusinessResponse objBusinessResponse = new BusinessResponse() { IsSuccesss = true };
            _manager.Setup(m => m.PostSmartSortForTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<TransactionPrioritySmartSortPut>>(), It.IsAny<Dictionary<string, string>>())).ReturnsAsync(objBusinessResponse);


            //Act
            OkObjectResult okObjectResult = (OkObjectResult)(await _controller.InsertTransactionPrioritySmartSorts("", listTransactionPrioritySmartSortPut));

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);

        }

        [Fact]
        public async Task InsertTransactionPrioritySmartSorts_ThrowException_Test()
        {

            //Arrange
            List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut = new List<TransactionPrioritySmartSortPut>();
            _manager.Setup(m => m.PostSmartSortForTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<TransactionPrioritySmartSortPut>>(), It.IsAny<Dictionary<string, string>>())).ThrowsAsync(new Exception());

            //Act
            StatusCodeResult statusCodeResult = (StatusCodeResult)(await _controller.InsertTransactionPrioritySmartSorts("", listTransactionPrioritySmartSortPut));

            //Assert
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        #endregion
    }
}
