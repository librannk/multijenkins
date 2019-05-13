////using Microsoft.AspNetCore.Http;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Mvc.Controllers;
////using Microsoft.AspNetCore.Routing;
////using Microsoft.Extensions.Logging;
////using Microsoft.Extensions.Options;
////using Moq;
////using System.Collections.Generic;
////using TransactionQueue.UnitTest.Common;
////using Xunit;
////using System.Net;
////using System.Threading.Tasks;
////using TransactionQueue.API.Controllers;
////using BD.Core.EventBus.Abstractions;
////using TransactionQueue.API.Application.BussinessLayer.Abstraction;
////using TransactionQueue.API.Configuration;
////using TransactionQueue.API.Application.Models.Enums;
////using TransactionQueue.API.Infrastructure.DBModel;
////using TransactionQueue.ManageQueues.Business.Abstraction;

////namespace TransactionQueue.UnitTest.Controllers
////{
////    /// <summary>
////    /// This class contains  unit-tests for TransactionQueue controller </summary>
////    /// </summary>
////    public class TransactionQueueControllerUnitTest
////    {
////        #region Test Methods

////        /// <summary>
////        /// Validates the storage location of the activate transaction
////        /// </summary>
////        [Fact]
////        public async Task ActivateTransaction_ShouldReturnStorageLocation()
////        {
////            var transaction = new Transaction
////            {
////                Status = TransactionStatus.Active
////            };

//            var transactionQueueId = "ac12ds324o2901234";
//            var logger = new Mock<ILogger<TransactionQueueController>>();
//            var eventBus = new Mock<IEventBus>();
//            var transactionQueueBussiness = new Mock<ITransactionQueueBussiness>();
//            var transactionQueueManager = new Mock<ITransactionQueueManager>();
//            var queueFilter = new Mock<IQueueFilter>();
//            transactionQueueManager.Setup(x => x.UpdateTransactionStatus(transactionQueueId, transaction.Status.Value, new Dictionary<string, string>()))
//                .ReturnsAsync(TransactionQueueChildObject.TransactionQueueModel);

////            var options = new Mock<IOptions<Configuration>>();
////            options.Setup(x => x.Value).Returns(new Configuration
////            {
////                KafkaDeviceTopic = "DeviceMessage"
////            });

//            var controller = new TransactionQueueController(logger.Object, transactionQueueManager.Object, transactionQueueBussiness.Object, queueFilter.Object);
//            var request = new Mock<HttpRequest>();
//            request.SetupGet(x => x.Headers["X-Requested-With"]).Returns("XMLHttpRequest");

////            var context = new Mock<HttpContext>();
////            context.SetupGet(x => x.Request).Returns(request.Object);

////            controller.ControllerContext = new ControllerContext(new ActionContext(context.Object, new RouteData(), new ControllerActionDescriptor()));
////            var response = controller.UpdateTransactionStatus(transactionQueueId, transaction);
////            var result = await response as OkResult;
////            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
////        }

////        #endregion
////    }
////}
