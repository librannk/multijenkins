using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulary.API.Common
{
    /// <summary>
    /// Contains all api's constants
    /// </summary>
    public static class Constants
    {
        /// <summary> EVENT_BUS_CONFIGURATION_IS_EVENT_HUB </summary>
        public const string EVENT_BUS_CONFIGURATION_IS_EVENT_HUB = "EventBusConfiguration:IsEventHub";

        /// <summary> EVENT_BUS_CONFIGURATION_VALIDATE_TOKEN </summary>
        public const string EVENT_BUS_CONFIGURATION_VALIDATE_TOKEN = "EventBusConfiguration:ValidateToken";

        /// <summary> EVENT_BUS_CONFIGURATION_TOKEN_SERVER_URL </summary>
        public const string EVENT_BUS_CONFIGURATION_TOKEN_SERVER_URL = "EventBusConfiguration:TokenServerURL";

        /// <summary> EVENT_BUS_CONFIGURATION_ENABLE_AUTO_COMMIT </summary>
        public const string EVENT_BUS_CONFIGURATION_ENABLE_AUTO_COMMIT = "EventBusConfiguration:EnableAutoCommit";

        /// <summary> EVENT_BUS_KAFKA_CONFIGURATION_ENDPOINT </summary>
        public const string EVENT_BUS_KAFKA_CONFIGURATION_ENDPOINT = "EventBusConfiguration:KafkaConfiguration:Endpoint";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_ENDPOINT </summary>
        public const string EVENT_BUS_HUB_CONFIGURATION_ENDPOINT = "EventBusConfiguration:EventHubConfiguration:Endpoint";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_USERNAME </summary>
        public const string EVENT_BUS_HUB_CONFIGURATION_USERNAME = "EventBusConfiguration:EventHubConfiguration:Username";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_PASSWORD </summary>
        public const string EVENT_BUS_HUB_CONFIGURATION_PASSWORD = "EventBusConfiguration:EventHubConfiguration:Password";

        /// <summary> EVENT_BUS_HUB_CONFIGURATION_BROKER_VERSION_FALLBACK </summary>
        public const string EVENT_BUS_HUB_CONFIGURATION_BROKER_VERSION_FALLBACK = "EventBusConfiguration:EventHubConfiguration:BrokerVersionFallback";

        /// <summary>
        /// Kafka configuration
        /// </summary>
        public const string KAFKA_CONFIGURATION = "KafkaConfiguration";
        /// <summary>
        /// Kafka End point
        /// </summary>
        public const string KAFKA_ENDPOINT = "KafkaEndpoint";
        /// <summary>
        /// Validate token
        /// </summary>
        public const string VALIDATE_TOKEN = "ValidateToken";
        /// <summary>
        /// Token Server URL
        /// </summary>
        public const string TOKEN_SERVER_URL = "TokenServerURL";
        /// <summary>
        /// Trace Endpoints
        /// </summary>
        public const string SERI_LOG_TRACE_END_POINTS = "Serilog:TraceEndpoints";
        /// <summary>
        /// SERILOG Trace 
        /// </summary>
        public const string SERILOG_TRACE = "Serilog:Trace";
        /// <summary>
        /// This key has topic that is used to publish formulary detials to CCEProxy service
        /// </summary>
        public const string Topic_Publish_Formulary_Update = "KafkaConfiguration:KafkaFormularyUpdate";

        /// <summary>
        /// This key has topic that is used to publish Facility detials to CCEProxy service
        /// </summary>
        public const string TOPIC_PUBLISH_FACILITY_UPDATE = "KafkaConfiguration:KafkaFacilityUpdate";

        /// <summary>
        /// This key has topic that is used to publish NDC detials to CCEProxy service
        /// </summary>
        public const string TOPIC_PUBLISH_NDC_UPDATE = "KafkaConfiguration:KafkaNDCUpdate";

        /// <summary>
        /// This key has topic that is used to publish transaction details to CCEProxy service
        /// </summary>
        public const string TOPIC_PUBLISH_TRANSACTIONDETAILS_TO_CCEProxy_SERVICE = "KafkaConfiguration:KafkaTransactionPriorityTopic";

        /// <summary>
        /// Message to save data into the database
        /// </summary>
        public const string Entity_Saved= "Entity has been saved and entity object  {0}:";
        /// <summary>
        /// Message to publish data on event bus
        /// </summary>
        public const string Data_Published= "data published to Event Hub and data object :{1}";
        /// <summary>
        /// Formulary error message
        /// </summary>
        public const string Formulary_Error_Msg = "There is some problem on saving formualry details, possible error is {0}";
        /// <summary>
        /// NDC error message
        /// </summary>
        public const string NDC_ERROR_MSG = "There is some problem on saving NDC details, possible error is {0}";
        /// <summary>
        /// NDC error message
        /// </summary>
        public const string FACILITY_ERROR_MSG = "There is some problem on saving facility details, possible error is {0}";
        /// <summary>
        /// Swagger route template
        /// </summary>
        public const string ROUTE_TEMPLATE = "formulary/swagger/{documentName}/swagger.json";
        /// <summary>
        /// Swagger End Point
        /// </summary>
        public const string SWAGGER_ENDPOINT = "/formulary/swagger/v1/swagger.json";
        /// <summary>
        /// Api name
        /// </summary>
        public const string API_NAME = "Formulary.API";
        /// <summary>
        /// Api version
        /// </summary>
        public const string API_VERSION = "v1";
        /// <summary>
        /// Helth check api integration
        /// </summary>
        public const string HEALTH_CHECK = "/healthCheck";
        /// <summary>
        /// Facility.API is listening
        /// </summary>
        public const string FORMULARY_API_LISTENING ="Formulary.API is listening.......!";
        /// <summary>
        /// IncomingRequest
        /// </summary>
        public const string IncomingRequest = "IncomingRequest is :{0}";

    }
    /// <summary>
    /// Class have all db's constants
    /// </summary>
    public class DBConstants
    {
        /// <summary>
        /// Schema detials
        /// </summary>
        public const string SCHEMA = "dbo";

       /// <summary>
       /// Db table Collection name
       /// </summary>
        public class Tables
        {
            /// <summary>
            /// Table is used to save facility, NDC association details
            /// </summary>
            public const string FACILITY_NDC_ASSOC= "CS_Formulary_FacililtyNDC";
           
          
        }
    }

    public class Admin
    {
        /// <summary>
        /// LastUpdated
        /// </summary>
        public const int LastUpdatedBy = 1;
        /// <summary>
        /// Created
        /// </summary>
        public const int CreatedBy = 1;
    }
    
    public class NotSuccess
    {
        /// <summary>
        /// RequestNotValid
        /// </summary>
        public const string RequestNotValid = "Request is Not Valid";
        /// <summary>
        /// RecordNotInserted
        /// </summary>
        public const string RecordNotInserted = "Trying to duplicate ItemId Insertion";
        /// <summary>
        /// DbInsertionIssue
        /// </summary>
        public const string DbInsertionIssue = "DB Insertion Issue";
        /// <summary>
        /// UpdateDbIssue
        /// </summary>
        public const string UpdateDbIssue = "Trying to duplicate ItemId Insertion or Formulary Id does not Exist";
    }
}
