
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BD.Template.API.Controllers;
using BD.Template.API.Infrastructure.DBModel;
using BD.Template.API.IntegrationEvents.Events;
using BD.Template.API.Infrastructure.Repository.Interfaces;
using BD.Core.EventBus.Abstractions;

namespace Logistics.Services.Template.API.IntegrationEvents.EventHandling
{
    /// <summary>
    /// class Template Added Event Handler
    /// </summary>
    public class TemplateAddedEventHandler :
        IEventHandler<TemplateAddedIntegrationEvent>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IKafkaResponseRepository _kafkaResponseRepository;
        private readonly ILogger<TemplateController> _logger;

        /// <summary>
        /// default constructor
        /// </summary>
        public TemplateAddedEventHandler()
        {
        }

        /// <summary>
        /// parameterized constructor to inject dependencies
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="kafkaResponseRepository"></param>
        /// <param name="logger"></param>
        public TemplateAddedEventHandler(IHttpContextAccessor httpContextAccessor, IKafkaResponseRepository kafkaResponseRepository,
            ILogger<TemplateController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _kafkaResponseRepository = kafkaResponseRepository;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(TemplateAddedIntegrationEvent @event)
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;

                if (context != null)
                {
                    foreach (var keyValuePair in @event.Headers)
                    {
                        context.Request.Headers[keyValuePair.Key] = keyValuePair.Value;
                    }
                }
                KafkaResponse kafkaResponse = new KafkaResponse
                {
                ResponseType = "Subscriber",
                EventMessage = @event.Message,
                Names = @event.Names.ToArray(),
                Quantity = @event.Quantity,
                TranQType = @event.TranQType,
                ConnectionResetMinutes = @event.ConnectionResetMinutes,
                CreationDate = DateTime.Now,
                Topic = "bddev"
                };

                await _kafkaResponseRepository.InsertAsync(kafkaResponse);
                //@event.Headers
                // YOOO
                //Event sampleEvent = new Event();
                //_eventBus.Publish(sampleEvent);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
