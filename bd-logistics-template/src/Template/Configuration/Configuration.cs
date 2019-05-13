using BD.Core.EventBusKafka;

namespace BD.Template.API.Configuration
{
    public class Configuration : EventBusConfiguration
    {
        /// <summary> KafkaRequestTopic </summary>
        public string KafkaRequestTopic { get; set; }

        /// <summary> KafkaResponseTopic </summary>
        public string KafkaResponseTopic { get; set; }
    }
}
