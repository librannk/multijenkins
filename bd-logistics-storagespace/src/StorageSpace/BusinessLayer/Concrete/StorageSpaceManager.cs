using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using StorageSpace.API.BusinessLayer.Contracts;
using StorageSpace.API.Model.Request;
using StorageSpace.API.Infrastructure.Repository.Contracts;
using StorageSpace.API.IntegrationEvents.Events;
using static StorageSpace.API.Common.Constant;

namespace StorageSpace.API.BusinessLayer.Concrete
{
    /// <summary> StorageSpaceManager </summary>
    public class StorageSpaceManager : IStorageSpaceManager
    {
        #region Private Fields
        private readonly IStorageSpaceDetailMongoRepository _StorageSpaceDetailMongoRepository;
        private readonly ILogger<StorageSpaceManager> _logger;
        private readonly IEventBus _eventBus;
        private readonly Configuration.Configuration _configuration;
        #endregion

        #region Constructors
        /// <summary> StorageSpaceManager constructor </summary>
        /// <param name="StorageSpaceDetailMongoRepository">IStorageSpaceDetailMongoRepository</param>
        /// <param name="logger">ILogger</param>
        /// <param name="eventBus">IEventBus</param>
        /// <param name="options">IOptions</param>
        public StorageSpaceManager(IStorageSpaceDetailMongoRepository StorageSpaceDetailMongoRepository,
            ILogger<StorageSpaceManager> logger, IEventBus eventBus, IOptions<Configuration.Configuration> options)
        {
            _StorageSpaceDetailMongoRepository = StorageSpaceDetailMongoRepository;
            _logger = logger;
            _eventBus = eventBus;
            _configuration = options.Value;
        }
        #endregion

        #region Public Methods
        /// <summary>  </summary>
        /// <param name="requestEvent">StorageSpaceRequestEvent</param>
        /// <returns></returns>
        public async Task GetStorageSpaces(StorageSpaceRequestEvent requestEvent)
        {
            if (!requestEvent.IsValid)
                InvalidStorageSpaceRequestEvent(requestEvent);

            var StorageSpaces = await _StorageSpaceDetailMongoRepository.GetStorageSpaces(new GetStorageSpaceRequest
            {
                FacilityId = requestEvent.FacilityId,
                FormularyId = requestEvent.FormularyId,
                ISAId = requestEvent.ISAId
            });
            var response = new StorageSpaceResponseEvent
            {
                TransactionQueueId = requestEvent.TransactionQueueId,
                FormularyId = StorageSpaces.FormularyId,
                MedId = StorageSpaces.ItemId,
                Devices = StorageSpaces.Devices,
                Headers = requestEvent.Headers
            };

            _logger.LogInformation(PublishEventType);
            _logger.LogInformation($"{nameof(StorageSpaceResponseEvent.TransactionQueueId)}: {response.TransactionQueueId};");
            _logger.LogInformation($"{CorrelationParentId}: {response.Headers.FirstOrDefault(_ => _.Key.ToLower() == CorrelationParentId.ToLower()).Value}");
            _eventBus.Publish(_configuration.KafkaResponseTopic, response, requestEvent.Headers);
            _logger.LogInformation($"{FollowingDataSuccessfullyPublishedToEventBus}: {response}");
        }
        #endregion

        #region Private Methods
        private void InvalidStorageSpaceRequestEvent(StorageSpaceRequestEvent request)
        {
            var propertiesWithInvalidValues = request.GetRuleViolations().Select(a => a.PropertyName);
            var properties = request.GetType().GetProperties().Where(_ => propertiesWithInvalidValues.Contains(_.Name));
            foreach (var property in properties)
            {
                _logger.LogError($"{property.Name} = {property.GetValue(request, null)};");
            }

            // following line throws an ApplicationException
            request.OnValidate();
        }
        #endregion
    }
}
