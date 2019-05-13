
namespace TransactionQueue.API.Common.Constants
{
    /// <summary>
    /// This class contains messages
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// This class contains Logging Messages
        /// </summary>
        public class LoggingMessage
        {
            /// <summary>
            /// constant for ActivateTransactionStarted
            /// </summary>
            public const string ActivateTransactionStarted = "Activate Transaction operation started for transaction Id - {0} ";
            /// <summary>
            /// constant for InvalidRequest
            /// </summary>
            public const string InvalidRequestForTransaction = "Invalid Request for TransactionId : {0} ";
            /// <summary>
            /// constant for ActivatedTransactionMesage
            /// </summary>
            public const string UpdateTransactionStatusMesage = "Transaction status to be updated for transaction Id : {0} ";
            /// <summary>
            /// constant for InvalidStatus
            /// </summary>
            public const string InvalidStatus = "Status should be active or complete for TransactionId = {0}";
            /// <summary>
            /// constant for TransactionIdOrStatusNull
            /// </summary>
            public const string TransactionIdOrStatusNull = "TransactionQueue Id or TransactionStatus should not be empty";
            /// <summary>
            /// constant for DataPublishedDeviceCommunication
            /// </summary>
            public const string DataPublishedDeviceCommunication = "Data successfully published from Transaction Queue to Service Bus for Device Communication: {0}";
            /// <summary>
            /// constant for F10Override request parameter is empty
            /// </summary>
            public const string InvalidRequestForF10Override = "Please provide all required data";
        }

        /// <summary>
        /// This class contains for MessageBus
        /// </summary>
        public class MessageBus
        {
            /// <summary>
            /// constant for Topic
            /// </summary>
            public const string Topic = "MessageBusTopics";
            /// <summary>
            /// constant for Groups
            /// </summary>
            public const string Groups = "MessageBusGroups";
        }

        #region Swagger
        /// <summary>
        /// This class contains Swagger Messages
        /// </summary>
        public static class SwaggerMessage
        { /// <summary> Url for swagger endpoint </summary>
            public const string SwaggerEndpointUrl = "/transactionqueue/swagger/v1/swagger.json";

            /// <summary> RoutePrefix </summary>
            public const string RoutePrefix = "transactionqueue";

            /// <summary> RouteTemplate </summary>
            public const string RouteTemplate = "transactionqueue/swagger/{documentName}/swagger.json";
        }
        #endregion

    }
}
