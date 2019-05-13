using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.Common.Constants;
using TransactionQueue.ExternalDependencies.IntegrationEvents.Events;

namespace TransactionQueue.ExternalDependencies.IntegrationEvents.EventHandling
{
    /// <summary> Eventhandler to store formulary </summary>
    public class FormularyUpdatedIntegrationEventHandler : IEventHandler<FormularyUpdatedIntegrationEvent>
    {
        #region Private Fields
        private readonly ILogger<FormularyUpdatedIntegrationEventHandler> _logger;
        private readonly IFormularyManager _formularyManager;
        #endregion

        #region Constructors
        /// <summary> Initializes an instace </summary>
        public FormularyUpdatedIntegrationEventHandler()
        {
        }

        /// <summary>
        /// Initializes instances - IFormularyManager and logger
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="formularyManager"></param>
        public FormularyUpdatedIntegrationEventHandler(ILogger<FormularyUpdatedIntegrationEventHandler> logger, IFormularyManager formularyManager)
        {
            _logger = logger;
            _formularyManager = formularyManager;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus </summary>
        /// <param name="event"> data received from event bus</param>
        /// <returns></returns>
        public async Task Handle(FormularyUpdatedIntegrationEvent @event)
        {
            try
            {
                if (@event != null)
                {
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.DataReceivedFromFormulary, JsonConvert.SerializeObject(@event)));
                    var formularyRequestModel = new BusinessLayer.Models.Formulary
                    {
                        FormularyId = @event.FormularyId,
                        ItemId = @event.ItemId,
                        Description = @event.Description,
                        IsActive = @event.IsActive,
                    };
                    await _formularyManager.ProcessFormularyRequest(formularyRequestModel);
                }
                else
                {
                    _logger.LogError(CommonConstants.LoggingMessage.InvalidRequest);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format(CommonConstants.LoggingMessage.ErrorWhileProcessingRequest, ex.Message));
            }
        }
        #endregion
    }
}
