
namespace CCEProxy.API.Common.Constants
{
    /// <summary>
    /// This class contains messages
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// This class contains IncomingRequestStatus messages
        /// </summary>
        public static class IncomingRequestStatus
        {
            /// <summary>
            /// constant for Rejected
            /// </summary>
            public const string Rejected = "Rejected";
            /// <summary>
            /// constant for Received
            /// </summary>
            public const string Received = "Received";
            /// <summary>
            /// constant for Accepted
            /// </summary>
            public const string Accepted = "Accepted";
            /// <summary>
            /// constant for Exception
            /// </summary>
            public const string Exception = "Exception:";
            /// <summary>
            /// constant for Ignored
            /// </summary>
            public const string Ignored = "Ignored";
        }

        /// <summary>
        /// This class contains IncomingRequestStatusMessage
        /// </summary>
        public static class IncomingRequestStatusMessage
        {
            /// <summary>
            /// constant for NoItemPresent
            /// </summary>
            public const string NoItemPresent = "No Item Present";
        }

        /// <summary>
        /// This class contains RequestPriority Messages
        /// </summary>
        public static class RequestPriority
        {
            /// <summary>
            /// constant for PyxisCritLow
            /// </summary>
            public const string PyxisCritLow = "PYXISCRITLOW";
            /// <summary>
            /// constant for PyxisStockOut
            /// </summary>
            public const string PyxisStockOut = "PYXISSTOCKOUT";
            /// <summary>
            /// constant for PyxisStckOut
            /// </summary>
            public const string PyxisStckOut = "PYXISSTKOUT";
            /// <summary>
            /// constant for PyxisRefill
            /// </summary>
            public const string PyxisRefill = "PYXISREFILL";
        }

        /// <summary>
        /// This class contains LoggingMessage Messages
        /// </summary>
        public static class LoggingMessage
        {
            /// <summary>
            /// constant for Facility
            /// </summary>
            public const string Facility = "Facility:";
            /// <summary>
            /// constant for RequestReceived
            /// </summary>
            public const string RequestReceived = "Incoming Request received : {0}";
            /// <summary>
            /// constant for ModelStateInvalid
            /// </summary>
            public const string ModelStateInvalid = "Incoming Request is not valid";
            /// <summary>
            /// constant for ModelStateValid
            /// </summary>
            public const string ModelStateValid = "Incoming Request validation successful";
            /// <summary>
            /// constant for PriorityInvalid
            /// </summary>
            public const string PriorityInvalid = "Priority is not valid with Facility";
            /// <summary>
            /// constant for PriorityIgnored
            /// </summary>
            public const string PriorityIgnored = "Priority is ignored";
            /// <summary>
            /// constant for FacilityInvalid
            /// </summary>
            public const string FacilityInvalid = "Facility is not valid";
            /// <summary>
            /// constant for DataPublished
            /// </summary>
            public const string DataPublished = "Data successfully published from CCE Proxy API to Service Bus : {0}";
            /// <summary>
            /// constant for RejectedId
            /// </summary>
            public const string RejectedId = ": Rejected IncomingRequestId with ";
            /// <summary>
            /// constant for Priority
            /// </summary>
            public const string Priority = "Priority =";
            /// <summary>
            /// constant for DataValid
            /// </summary>
            public const string DataValid = "Valid Request";
            /// <summary>
            /// constant for NoItemPresent
            /// </summary>
            public const string NoItemPresent = "No Item Present";
            /// <summary>
            /// constant for ItemIdNull
            /// </summary>
            public const string ItemIdNull = "Item id is null";
            /// <summary>
            /// constant for DataReceivedFromFacility
            /// </summary>
            public const string DataReceivedFromFacility = "Data Receieved from service bus - Facility : {0}";
            /// <summary>
            /// constant for InvalidRequest
            /// </summary>
            public const string InvalidRequest = "Invalid Request. ";
            /// <summary>
            /// constant for ErrorWhileProcessingRequest
            /// </summary>
            public const string ErrorWhileProcessingRequest = "Error while processing Facility Request. Error - ";
            /// <summary>
            /// constant for ProcessFacilityRequest
            /// </summary>
            public const string ProcessFacilityRequest = "ProcessFacilityRequest from Facility API with data :";
            /// <summary>
            /// constant for DataReceivedFromTransactionPriority
            /// </summary>
            public const string DataReceivedFromTransactionPriority = "Data Receieved from service bus - Transaction Priority : {0}";
            /// <summary>
            /// constant for ProcessTransactionPriorityRequest
            /// </summary>
            public const string ProcessTransactionPriorityRequest = "ProcessTransactionPriorityRequest from Facility API with data :";
        }

        /// <summary>
        /// This class contains User details
        /// </summary>
        public static class User
        {
            /// <summary>
            /// constant for CreatedBy
            /// </summary>
            public const string CreatedBy = "System";
            /// <summary>
            /// constant for ModifiedBy
            /// </summary>
            public const string ModifiedBy = "System";
        }
        #region Swagger
        /// <summary>
        /// This class contains Swagger Messages
        /// </summary>
        public static class SwaggerMessage
        { /// <summary> Url for swagger endpoint </summary>
            public const string SwaggerEndpointUrl = "/cceproxy/swagger/v1/swagger.json";

            /// <summary> RoutePrefix </summary>
            public const string RoutePrefix = "cceproxy";

            /// <summary> RouteTemplate </summary>
            public const string RouteTemplate = "cceproxy/swagger/{documentName}/swagger.json";
        }
        #endregion

        #region Health
        /// <summary>
        /// This class contains Health Messages
        /// </summary>
        public static class HealthMessage
        {
            /// <summary> HealthCheckReady </summary>
            public const string HealthCheckReady = "/health/ready";

            /// <summary> HealthCheckLive </summary>
            public const string HealthCheckLive = "/health/live";
        }
        #endregion
    }
}
