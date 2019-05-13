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
    /// <summary> Eventhandler to update formulary facility data </summary>
    public class FormularyFacilityUpdatedIntegrationEventHandler : IEventHandler<FormularyFacilityUpdatedIntegrationEvent>
    {
        #region Private Fields
        private readonly ILogger<FormularyFacilityUpdatedIntegrationEventHandler> _logger;
        private readonly IFormularyManager _formularyManager;
        #endregion

        #region Constructors
        /// <summary> Initializes an instace </summary>
        public FormularyFacilityUpdatedIntegrationEventHandler()
        {
        }

        /// <summary>
        /// Initializes instances - IFormularyManager and logger
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="formularyManager"></param>
        public FormularyFacilityUpdatedIntegrationEventHandler(ILogger<FormularyFacilityUpdatedIntegrationEventHandler> logger, IFormularyManager formularyManager)
        {
            _logger = logger;
            _formularyManager = formularyManager;
        }

        #endregion

        #region Public Methods
        /// <summary> Subscribes to Event-Bus </summary>
        /// <param name="event"> data received from event bus</param>
        /// <returns></returns>
        public async Task Handle(FormularyFacilityUpdatedIntegrationEvent @event)
        {
            try
            {
                if (@event != null && @event.FormularyId > 0 && @event.FacilityId > 0)
                {
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.DataReceivedFromFormularyFacility, JsonConvert.SerializeObject(@event)));
                    var formularyRequestModel = new BusinessLayer.Models.Formulary
                    {
                        FormularyId = @event.FormularyId,
                        FacilityFormulary = new BusinessLayer.Models.FacilityFormulary
                        {
                            FacilityId = @event.FacilityId,
                            AduIgnoreCritLow = @event.AduIgnoreCritLow,
                            AduIgnoreStockout = @event.AduIgnoreStockout,
                            Approved = @event.Approved
                        }
                    };
                    await _formularyManager.UpdateFormularyFacility(formularyRequestModel);
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
