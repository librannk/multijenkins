using System.Threading.Tasks;
using BD.Core.EventBus.Events;
using Microsoft.Extensions.Logging;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events;


namespace Logistics.Services.DeviceCommunication.API.BusinessLayer
{
    /// <summary>
    /// ProcessTransactionMediator receive data from handler and process business logic
    /// </summary>
    public class ProcessTransactionMediator : IMediator
    {
        #region  Fields

        private readonly ILogger<ProcessTransactionMediator> _logger;
        private readonly ICarouselProcess _carouselProcess;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="carouselProcess"></param>
        public ProcessTransactionMediator(ILogger<ProcessTransactionMediator> logger,
             ICarouselProcess carouselProcess)
        {
            _logger = logger;
            _carouselProcess = carouselProcess;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute event coming from transaction queue service via subscriber
        /// </summary>
        /// <param name="event">Event of ProcessTransactionQueueIntegrationEvent type coming from subscriber</param>
        public async Task Execute(Event @event)
        {
            _logger.LogInformation(Constants.ProcessTransactionMediator.EventRecievedAtMediator);

            var transactionData = ((ProcessTransactionQueueIntegrationEvent)@event).TransactionData;
            if (transactionData?.Devices != null && transactionData.Devices.Count > 0)
            {
                _logger.LogInformation(Constants.ProcessTransactionMediator.EventRecieveSuccess);
                await _carouselProcess.MoveCarousel(transactionData);
            }
            else
            {
                _logger.LogInformation(Constants.ProcessTransactionMediator.EventRecieveFail);
            }
        }

        #endregion
    }
}

