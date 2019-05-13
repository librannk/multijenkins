using BD.Core.EventBusKafka;

namespace CCEProxy.API.Configuration
{
    /// <summary> Configuration </summary>
    public class Configuration : EventBusConfiguration
    {
        /// <summary>
        /// To hold the value for KafkaCCEProxyTopic
        /// </summary>
        public string KafkaCCEProxyTopic { get; set; }
    }
}
