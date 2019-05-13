using BD.Core.EventBusKafka;

namespace TransactionQueue.Shared.Configuration
{
    /// <summary> Configuration </summary>
    public class Configuration : EventBusConfiguration
    {
        /// <summary> KafkaDeviceTopic </summary>
        public string KafkaDeviceTopic { get; set; }
        /// <summary> KafkaAggregatorTopic </summary>
        public string KafkaAggregatorTopic { get; set; }
        /// <summary> KafkaFormularyLocationResponseTopic </summary>
        public string KafkaFormularyLocationResponseTopic { get; set; }
        /// <summary>
        /// KafkaFormularyLocationRequestTopic
        /// </summary>
        public string KafkaFormularyLocationRequestTopic { get; set; }

    }
  
}
