
namespace CCEProxyUnitTests.EventHandler
{
    using AutoMapper;
    using CCEProxy.API.AutoMapper;
    using CCEProxy.API.BusinessLayer.Contracts;
    using CCEProxy.API.Entity;
    using CCEProxy.API.IntegrationEvents.EventHandling;
    using CCEProxy.API.IntegrationEvents.Events;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// This class contains unit test for FacilityIntegrationEventHandler
    /// </summary>
    public class FacilityIntegrationEventHandlerUnitTest
    {
        #region Private Fields
        private readonly Mock<IFacilityManager> _facilityManger;
        private readonly Mock<ILogger<FacilityIntegrationEventHandler>> _logger;
        private readonly IMapper _mapper;
        private readonly FacilityIntegrationEventHandler FacilityIntegrationEventHandler;
        #endregion

        #region Constructor
        public FacilityIntegrationEventHandlerUnitTest()
        {
            _facilityManger = new Mock<IFacilityManager>();
            _logger = new Mock<ILogger<FacilityIntegrationEventHandler>>();
            var mockMapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new MapProfile())
            );
            _mapper = mockMapper.CreateMapper();
            FacilityIntegrationEventHandler = new FacilityIntegrationEventHandler(_facilityManger.Object,
                _logger.Object,_mapper);
        }
        #endregion

        #region Unit Test Cases
        [Fact]
        public async Task Handle_ValidEvent_ShouldProcessRequest()
        {
            //Arrange
            _facilityManger.Setup(x => x.ProcessFacilityRequest(It.IsAny<Facility>())).Returns(Task.CompletedTask);
            var fakeEvent = new FacilityAddedIntegrationEvent
            {
                FacilityCode = "23",
                FacilityId = 1
            };
            //Act
            await FacilityIntegrationEventHandler.Handle(fakeEvent);

            //Assert
            _facilityManger.Verify(x=> x.ProcessFacilityRequest(It.IsAny<Facility>()), Times.Once);
            
        }

        [Fact]
        public async Task Handle_NullEvent_ShouldNotProcessRequest()
        {
            //Act
            await FacilityIntegrationEventHandler.Handle(null);

            //Assert
            _facilityManger.Verify(x => x.ProcessFacilityRequest(It.IsAny<Facility>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Exception_ShouldNotProcessRequest()
        {
            //Arrange
            _facilityManger.Setup(x => x.ProcessFacilityRequest(It.IsAny<Facility>())).Throws(new Exception());
            var fakeEvent = new Mock<FacilityAddedIntegrationEvent>();
            //Act
            await FacilityIntegrationEventHandler.Handle(fakeEvent.Object);

            //Assert
            _facilityManger.Verify(x => x.ProcessFacilityRequest(It.IsAny<Facility>()), Times.Never);
        }
        #endregion

    }
}
