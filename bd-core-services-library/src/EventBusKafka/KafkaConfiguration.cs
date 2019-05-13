namespace BD.Core.EventBusKafka
{
    public class EventBusConfiguration
    {
        public KafkaConfiguration KafkaConfiguration { get; set; }
        public EventHubConfiguration EventHubConfiguration { get; set; }
        public int MaxPoolErrorCount { get; set; } = 100;
        public int Acknowledgement { get; set; } = 1;
        public bool PoolFromBeginning { get; set; }
        public bool AutoCommit { get; set; }
        public int PollTimeOutInSec { get; set; } = 2;
        public bool ValidateToken { get; set; }
        public string TokenServerUri { get; set; }
        public bool IsEventHub { get; set; }
    }

    public class KafkaConfiguration
    {
        public string Endpoint { get; set; }
    }

    public class EventHubConfiguration
    {
        public string Endpoint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BrokerVersionFallback { get; set; }
    }
}
