using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using StorageSpace.API.BusinessLayer.Contracts;
using StorageSpace.API.IntegrationEvents.Events;
using static StorageSpace.API.Common.Constant;

namespace StorageSpace.API.IntegrationEvents.EventHandling
{
    /// <summary> StorageSpaceRequestEventHandler will handle StorageSpaceRequestEvents </summary>
    public class StorageSpaceRequestEventHandler : IEventHandler<StorageSpaceRequestEvent>
    {
        #region Private Fields
        private readonly ILogger<StorageSpaceRequestEventHandler> _logger;
        private readonly IStorageSpaceManager _StorageSpaceManager;
        #endregion

        #region Constructors
        /// <summary> StorageSpaceRequestEventHandler constructor </summary>
        /// <param name="logger"></param>
        /// <param name="StorageSpaceManager"></param>
        public StorageSpaceRequestEventHandler(ILogger<StorageSpaceRequestEventHandler> logger, IStorageSpaceManager StorageSpaceManager)
        {
            _logger = logger;
            _StorageSpaceManager = StorageSpaceManager;
        }
        #endregion

        #region Public Methods
        /// <summary> Subscribes to a specific topic </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(StorageSpaceRequestEvent @event)
        {
            _logger.LogInformation(ReceivedOnStorageSpaceRequestEventHandler);
            try
            {
                await _StorageSpaceManager.GetStorageSpaces(@event);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.GetType().Name);
                _logger.LogError(ex.Message);
            }
        }
        #endregion
    }
}
