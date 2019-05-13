using BD.Core.EventBusKafka;

namespace Logistics.Services.DeviceCommunication.API.Configuration
{
    /// <summary>
    /// Configuration Class
    /// </summary>
    public class Configuration : EventBusConfiguration
    {
        /// <summary>
        /// read-write KafkaTopic property
        /// </summary>
        public string KafkaTopic { get; set;}
    }
}
