
namespace TransactionQueue.Ingestion.Common.Constants
{
    /// <summary>
    /// This class contains messages
    /// </summary>
    public static class CommonConstants
    {
        /// <summary>
        /// This class contains TransactionException messages
        /// </summary>
        public class TransactionException
        {
            /// <summary>
            /// constant for UnknownItemId
            /// </summary>
            public const string UnknownItemId = "Unknown Item";
            /// <summary>
            /// constant for InvalidQuantity
            /// </summary>
            public const string InvalidQuantity = "INVALID QUANTITY";
            /// <summary>
            /// constant for InactiveFormularyItem
            /// </summary>
            public const string InactiveFormularyItem = "Formulary not active for Item";
            /// <summary>
            /// constant for FormularyNotMapped
            /// </summary>
            public const string FormularyNotMapped = "Formulary not mapped with Facility";
            /// <summary>
            /// constant for NotApprovedFormularyItem
            /// </summary>
            public const string NotApprovedFormularyItem = "Formulary not approved for Facility";
            /// <summary>
            /// constant for InvalidUnitQuantity
            /// </summary>
            public const string InvalidUnitQuantity = "Invalid Quantity";
            /// <summary>
            /// constant for UnassignedLocation
            /// </summary>
            public const string UnassignedLocation = "Unassigned Location for Facility";
        }

        /// <summary>
        /// This class contains Logging Messages
        /// </summary>
        public class LoggingMessage
        {
            /// <summary>
            /// constant for RejectedRequest
            /// </summary>
            public const string RejectedRequest = "Transaction request rejected due to invalid priority and Request Id is {0}";
            /// <summary>
            /// constant for ProcessIncomingRequest
            /// </summary>
            public const string ProcessIncomingRequest = "ProcessTrasactionRequest from Transaction API with data : {0} ";
            /// <summary>
            /// constant for ErrorWhileProcessingRequest
            /// </summary>
            public const string ErrorWhileProcessingRequest = "Error while processing request. Error - {0}";
            /// <summary>
            /// constant for InvalidRequest
            /// </summary>
            public const string InvalidRequest = "Invalid Request. ";
            /// <summary>
            /// constant for ZeroQuantityException
            /// </summary>
            public const string ZeroQuantityException = "Blank or zero Quantity; cannot calculate quantity";
            /// <summary>
            /// constant for ExceptionInOtherComponent
            /// </summary>
            public const string ExceptionInOtherComponent = "Exception in other component(s).";
            /// <summary>
            /// constant for PublishFormularyLocationEventBus
            /// </summary>
            public const string PublishFormularyLocationEventBus = "DummyFormularyLocationEventBus publishing to EVENT-BUS";
            /// <summary>
            /// constant for UpdateTransactionWithStorageDetails
            /// </summary>
            public const string UpdateTransactionWithStorageDetails = "Start updating transaction queue storage location for transaction Id : {0}";
            /// <summary>
            /// constant for IVCasesError
            /// </summary>
            public const string IVCasesError = "IV cases are not handled in release 1";
            /// <summary>
            /// constant for TransactionCreated
            /// </summary>
            public const string TransactionCreated = "Transaction created and Id is : {0} for Request Id : {1} ";
            /// <summary>
            /// constant for IgnoredTransactionCreated
            /// </summary>
            public const string IgnoredTransactionCreated = "Adm ignored transaction created and Id is : {0} for Request Id : {1} ";
            /// <summary>
            /// constant for ErrorInformularyLocationEventHandler
            /// </summary>
            public const string ErrorInformularyLocationEventHandler = "Error in FormularyLocationIntegrationEventHandler - Error message - {0}";
            /// <summary>
            /// constant for InvalidStatus
            /// </summary>
            public const string InvalidStatus = "Status should be active or complete for TransactionId = {0}";
            /// <summary>
            /// constant for MultiComponentException
            /// </summary>
            public const string MultiComponentException = "MULTI-COMPONENT EXCEPTION";
            /// <summary>
            /// constant for DataReceivedFromFormularyLocation
            /// </summary>
            public const string DataReceivedFromFormularyLocation = "Data received from Service Bus - Formulary Location : {0}";
            /// <summary>
            /// constant for DataReceivedFromCCEProxy
            /// </summary>
            public const string DataReceivedFromCCEProxy = "Data received from Service Bus - CCE Proxy : {0}";
            /// <summary>
            /// constant for TransactionIdOrStatusNull
            /// </summary>
            public const string TransactionIdOrStatusNull = "TransactionQueue Id or TransactionStatus should not be empty";
            /// <summary>
            /// constant for TransactionIdNull
            /// </summary>
            public const string TransactionIdNull = "TransactionQueue Id is null";
            /// <summary>
            /// constant for DataPublishedForFormularyLocation
            /// </summary>
            public const string DataPublishedForFormularyLocation = "Data successfully published from Transaction Queue to Service Bus for FormularyLocation: {0}";
            /// <summary>
            /// constant for AduQuantity
            /// </summary>
            public const string AduQuantity = "Updated Adm Quantity - {0} for Request Id : {1} ";
            /// <summary>
            /// constant for AduQuantity
            /// </summary>
            public const string ProcessedAdmTransaction = "Processed Adm Transaction for Request Id : {0}";
            /// <summary>
            /// constant for UpdatedQuantityForReplenishmentOrder
            /// </summary>
            public const string UpdatedQuantityForReplenishmentOrder = "Updated quantity-{0} for Transaction Id : {1}";
            /// <summary>
            /// constant for UpdatedQuantityPriorityAndRequestId
            /// </summary>
            public const string UpdatedQuantityPriorityAndRequestIdForAduTransaction = "Updated quantity - {0}, priority - {1} and RequestId - {2} for Adm Transaction";
            /// <summary>
            /// constant for PyxisLoadTransactionExists
            /// </summary>
            public const string PyxisLoadTransactionExists = "Pyxisload transaction exists for RequestId - {0} for Adm transaction";
            /// <summary>
            /// constant for PyxisRefillTransactionExists
            /// </summary>
            public const string PyxisRefillTransactionExists = "PyxisRefill transaction exists in db for requested priority - PyxisRefill for Adm transaction. RequestId- {0}";
        }

        /// <summary>
        /// This class contains for Destination
        /// </summary>
        public class Destination
        {
            /// <summary>
            /// constant for Batch Pick
            /// </summary>
            public const string BatchPick = "BATCH PICK";
        }

        /// <summary>
        /// This class contains for TimeToLive
        /// </summary>
        public static class TimeToLive
        {
            /// <summary>
            /// constant for Stat
            /// </summary>
            public const string Stat = "TimeToLive:Stat";
            /// <summary>
            /// constant for BatchPicks
            /// </summary>
            public const string BatchPicks = "TimeToLive:BatchPicks";
            /// <summary>
            /// constant for Pick
            /// </summary>
            public const string Pick = "TimeToLive:Pick";
            /// <summary>
            /// constant for CycleCount
            /// </summary>
            public const string CycleCount = "TimeToLive:CycleCount";
            /// <summary>
            /// constant for Other
            /// </summary>
            public const string Other = "TimeToLive:Other";
        }
    }
}
