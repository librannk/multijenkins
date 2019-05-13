namespace StorageSpace.API.Common
{
    /// <summary> Constants </summary>
    public static class Constant
    {
        #region AppSetting/Main method
        /// <summary> EVENT_BUS_CONFIGURATION </summary>
        public const string EventBusConfiguration = "EventBusConfiguration";

        /// <summary> EVENT_HUB_CONFIGURATION </summary>
        public const string EventHubConfiguration = "EventHubConfiguration";

        /// <summary> BROKER_VERSION_FALLBACK </summary>
        public const string BrokerVersionFallback = "BrokerVersionFallback";

        /// <summary> ENDPOINT </summary>
        public const string Endpoint = "Endpoint";

        /// <summary> USERNAME </summary>
        public const string Username = "Username";

        /// <summary> PASSWORD </summary>
        public const string Password = "Password";

        /// <summary> EVENT_BUS_CONFIGURATION_IS_EVENT_HUB </summary>
        public const string EventBusConfigurationIsEventHub = "EventBusConfiguration:IsEventHub";

        /// <summary> EVENT_BUS_CONFIGURATION_VALIDATE_TOKEN </summary>
        public const string EventBusConfigurationValidateToken = "EventBusConfiguration:ValidateToken";

        /// <summary> EVENT_BUS_CONFIGURATION_TOKEN_SERVER_URL </summary>
        public const string EventBusConfigurationTokenServerUrl = "EventBusConfiguration:TokenServerURL";

        /// <summary> EVENT_BUS_CONFIGURATION_ENABLE_AUTO_COMMIT </summary>
        public const string EventBusConfigurationEnableAutoCommit = "EventBusConfiguration:EnableAutoCommit";

        /// <summary> EVENT_BUS_KAFKA_CONFIGURATION_ENDPOINT </summary>
        public const string EventBusConfigurationKafkaConfigurationEndpoint = "EventBusConfiguration:KafkaConfiguration:Endpoint";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_ENDPOINT </summary>
        public const string EventBusHubConfigurationEndpoint = "EventBusConfiguration:EventHubConfiguration:Endpoint";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_USERNAME </summary>
        public const string EventBusHubConfigurationUsername = "EventBusConfiguration:EventHubConfiguration:Username";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_PASSWORD </summary>
        public const string EventBusHubConfigurationPassword = "EventBusConfiguration:EventHubConfiguration:Password";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_BROKER_VERSION_FALLBACK </summary>
        public const string EventBusHubConfigurationBrokerVersionFallback = "EventBusConfiguration:EventHubConfiguration:BrokerVersionFallback";

        /// <summary> Key to get TraceEndpoints from Serilog section of appSetting.json </summary>
        public const string SerilogTraceEndpoints = "Serilog:TraceEndpoints";

        /// <summary> Key to get KafkaConfiguration of appSetting.json </summary>
        public const string KafkaConfiguration = "KafkaConfiguration";

        /// <summary> Key to get the KafkaEndpoint from KafkaConfiguration section of appSetting.json </summary>
        public const string KafkaConfigurationKafkaEndpoint = "KafkaConfiguration:KafkaEndpoint";

        /// <summary> Key to get the TOPIC from appSetting.json to subscribe to TQ service </summary>
        public const string KafkaRequestTopic = "KafkaRequestTopic";

        /// <summary> Key to get the TOPIC from appSetting.json to publish to TQ service </summary>
        public const string KafkaResponseTopic = "KafkaResponseTopic";

        /// <summary> MESSAGE_BUS_TOPICS </summary>
        public const string MessageBusTopics = "MessageBusTopics";

        /// <summary> Topic that subscribes to EventBus for TQ's request </summary>
        public const string MessageBusTopicsKafkaRequestTopic = "MessageBusTopics:KafkaRequestTopic";

        /// <summary> Topic that publishes to EventBus after TQ's request has been processed </summary>
        public const string MessageBusTopicsKafkaResponseTopic = "MessageBusTopics:KafkaResponseTopic";

        /// <summary> MONGO_DB_DATABASE </summary>
        public const string MongoDbDatabase = "MongoDb:Database";

        /// <summary> MONGO_DB_CONNECTION_STRING </summary>
        public const string MongoDbConnectionString = "MongoDb:ConnectionString";

        /// <summary> CONFIGURATION </summary>
        public const string CONFIGURATION = "configuration";

        /// <summary> APP_SETTINGS </summary>
        public const string Appsettings = "appsettings";

        /// <summary> ASPNETCORE_ENVIRONMENT </summary>
        public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        /// <summary> PRODUCTION </summary>
        public const string Production = "DEVELOPMENT";
        #endregion

        #region Authentication/Authorization
        /// <summary> BEARER </summary>
        public const string Bearer = "Bearer";

        /// <summary> HEADER </summary>
        public const string Header = "header";

        /// <summary> INFO_WHEN_NO_JWT_PRESENT </summary>
        public const string InfoWhenJwtNotPresent = "Please enter JWT with Bearer into field";

        /// <summary> AUTHORIZATION </summary>
        public const string Authorization = "Authorization";

        /// <summary> API_KEY </summary>
        public const string ApiKey = "apiKey";

        /// <summary> IDENTITY_SERVER_AUDIENCE </summary>
        public const string IdentityServerAudience = "IdentityServer:Audience";

        /// <summary> IDENTITY_SERVER_ENDPOINT </summary>
        public const string IdentityServerEndpoint = "IdentityServer:Endpoint";

        #endregion

        #region Event
        /// <summary> INTERNAL_ERROR </summary>
        public const string InternalError = "Some error occured, please try again later.";

        /// <summary> INVALID_VALUE </summary>
        public const string InvalidValue = "Invalid value";

        /// <summary> EXCEPTION_MESSAGE </summary>
        public const string RulesExceptionMessage = "Rules validation failed";

        /// <summary> CORRELATION_PARENT_ID received in header </summary>
        public const string CorrelationParentId = "CorrelationParentId";
        #endregion

        #region EventHandler
        /// <summary>
        /// RECEIVED_ON_FORMULARY_LOCATION_REQUEST_EVENT_HANDLER:
        /// when event is received on subscribing to a topic
        /// </summary>
        public const string ReceivedOnStorageSpaceRequestEventHandler = "StorageSpaceRequestEventHandler has received an @event of type: StorageSpaceRequestEvent";

        /// <summary>
        /// PUBLISHED_BY_FORMULARY_LOCATION_REQUEST_EVENT_HANDLER: 
        /// when event is successfully processed and is ready to be published in the event-bus
        /// </summary>
        public const string PublishEventType = "@event of type: StorageSpaceResponseEvent will be published";

        /// <summary>
        /// PUBLISHED_DATA_SUCCESSFULLY_TO_EVENT_BUS:
        /// when event is successfully published
        /// </summary>
        public const string FollowingDataSuccessfullyPublishedToEventBus = "Response successfully published to event bus by Formulary Location API";
        #endregion

        #region FileExtensions
        /// <summary> For xml </summary>
        public const string XmlUpper = "XML";

        /// <summary> For json </summary>
        public const string JsonLower = "json";
        #endregion

        #region MongoDb
        /// <summary> CLIENT_NOT_INSTANTIATED </summary>
        public const string ClientNotInitiated = "client not instantiated";
        #endregion

        #region Others
        /// <summary> DOT/Period </summary>
        public const string Dot = ".";

        /// <summary> COLON </summary>
        public const string Colon = ":";

        /// <summary> TITLE </summary>
        public const string Title = "StorageSpace.API";

        /// <summary> VERSION </summary>
        public const string Version = "v1";
        #endregion

        #region Swagger
        /// <summary> Url for swagger endpoint </summary>
        public const string SwaggerEndpointUrl = "/StorageSpace/swagger/v1/swagger.json";

        /// <summary> ROUTE_PREFIX </summary>
        public const string RoutePrefix = "StorageSpace";

        /// <summary> ROUTE_TEMPLATE </summary>
        public const string RouteTemplate = "StorageSpace/swagger/{documentName}/swagger.json";
        #endregion

        #region Document Type 
        /// <summary> ISA Type </summary>
        public const string ISAType = "ISA";

        /// <summary> Carousal Type </summary>
        public const string CarousalType = "Carousal";
        #endregion

    }
}
