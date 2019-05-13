namespace Facility.API.Configuration
{
    /// <summary>
    /// Kafka Configuration settings for facility.
    /// </summary>
    public class MessageBusTopics
    {
        /// <summary>
        /// Topic to sent facility update details
        /// </summary>
        /// <value>Topic to sent facility update details.</value>
        public string KafkaFacilityDetailsTopic { get; set; }
    }

}
