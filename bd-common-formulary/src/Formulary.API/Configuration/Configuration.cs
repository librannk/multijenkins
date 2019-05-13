using BD.Core.EventBusKafka;
using BD.Core.EventBusKafka.Extensions;

namespace Formulary.API.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class Configuration : EventBusConfiguration
    {
        public string KafkaRequestTopic { get; set; }
    }

}
