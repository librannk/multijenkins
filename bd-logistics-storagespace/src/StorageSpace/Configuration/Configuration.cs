using BD.Core.EventBusKafka;

namespace StorageSpace.API.Configuration
{
    /// <summary> Configuration </summary>
    public class Configuration : EventBusConfiguration
    {
        /// <summary> KafkaRequestTopic </summary>
        public string KafkaRequestTopic { get; set; }

        /// <summary> KafkaResponseTopic </summary>
        public string KafkaResponseTopic { get; set; }
    }
}
