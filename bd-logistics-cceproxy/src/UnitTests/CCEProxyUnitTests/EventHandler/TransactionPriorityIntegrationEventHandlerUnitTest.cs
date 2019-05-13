using AutoMapper;
using CCEProxy.API.AutoMapper;
using CCEProxy.API.BusinessLayer.Contracts;
using CCEProxy.API.Entity;
using CCEProxy.API.IntegrationEvents.EventHandling;
using CCEProxy.API.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CCEProxyUnitTests.EventHandler
{

    /// <summary>
    /// This class contains unit test for transactionPriorityIntegrationEventHandler
    /// </summary>

    public class TransactionPriorityIntegrationEventHandlerUnitTest
    {
        #region Private Fields
        private readonly Mock<ILogger<TransactionPriorityIntegrationEventHandler>> _logger;
        private readonly Mock<ITransactionPriorityManager> _transactionPriorityManager;
        private readonly IMapper _mapper;
        private readonly TransactionPriorityIntegrationEventHandler transactionPriorityIntegrationEventHandler;
        #endregion

        #region Constructors
        public TransactionPriorityIntegrationEventHandlerUnitTest()
        {
            _logger = new Mock<ILogger<TransactionPriorityIntegrationEventHandler>>();
            _transactionPriorityManager = new Mock<ITransactionPriorityManager>();
            var mockMapper = new MapperConfiguration(cfg =>
               cfg.AddProfile(new MapProfile())
            );
            _mapper = mockMapper.CreateMapper();
            transactionPriorityIntegrationEventHandler = new TransactionPriorityIntegrationEventHandler(
                _transactionPriorityManager.Object,
                _logger.Object,
                _mapper
                );
        }
        #endregion

        #region Test Cases
        [Fact]
        public async Task Handle_ValidEvent_ShouldProcessRequest()
        {
            //Arrange
            _transactionPriorityManager.Setup(x => x.ProcessTransactionPriorityRequest(It.IsAny<TransactionPriority>())).Returns(Task.CompletedTask);
            var fakeEvent = new TransactionPriorityAddedIntegrationEvent
            {
                FacilityId = 1,
                PriorityCode = "PATIENTPICK",
                Active = true,
                TransactionPriorityId = 100
            };

            //Act
            await transactionPriorityIntegrationEventHandler.Handle(fakeEvent);

            //Assert
            _transactionPriorityManager.Verify(x => x.ProcessTransactionPriorityRequest(It.IsAny<TransactionPriority>()), Times.Once);

        }

        [Fact]
        public async Task Handle_NullEvent_ShouldNotProcessRequest()
        {
            //Act
            await transactionPriorityIntegrationEventHandler.Handle(null);

            //Assert
            _transactionPriorityManager.Verify(x => x.ProcessTransactionPriorityRequest(It.IsAny<TransactionPriority>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Exception_ShouldNotProcessRequest()
        {
            //Arrange
            _transactionPriorityManager.Setup(x => x.ProcessTransactionPriorityRequest(It.IsAny<TransactionPriority>())).Throws(new Exception());
            var fakeEvent = new Mock<TransactionPriorityAddedIntegrationEvent>();

            //Act
            await transactionPriorityIntegrationEventHandler.Handle(fakeEvent.Object);

            //Assert
            _transactionPriorityManager.Verify(x => x.ProcessTransactionPriorityRequest(It.IsAny<TransactionPriority>()), Times.Never);
        }
        #endregion
    }
}