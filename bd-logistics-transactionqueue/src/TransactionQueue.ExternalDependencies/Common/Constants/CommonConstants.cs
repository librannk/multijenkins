using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.ExternalDependencies.Common.Constants
{
    /// <summary>
    /// This class contains messages
    /// </summary>
    public static class CommonConstants
    {
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
            /// constant for ErrorWhileProcessingRequest
            /// </summary>
            public const string ErrorWhileProcessingRequest = "Error while processing request. Error - {0}";
            /// <summary>
            /// constant for InvalidRequest
            /// </summary>
            public const string InvalidRequest = "Invalid Request. ";
            /// <summary>
            /// constant for PyxisLoad
            /// </summary>
            public const string PyxisLoad = "PYXISLOAD";
            /// <summary>
            /// constant for DataReceivedFromFacility
            /// </summary>
            public const string DataReceivedFromFacility = "Data Receieved from facility service. Data : {0} ";

            /// <summary>
            /// constant for DataReceivedFromFormulary
            /// </summary>
            public const string DataReceivedFromFormulary = "Data Receieved from formulary service. Data : {0} ";

            /// <summary>
            /// constant for StartDeletingFormulary
            /// </summary>
            public const string StartDeletingFormulary = "Start deleting formulary : {0}.";

            /// <summary>
            /// constant for FormularyDeleted
            /// </summary>
            public const string FormularyDeleted = "Formulary : { 0 } deleted successfully.";

            /// <summary>
            /// constant for TransactionPriorityDataReceivedFromFacility
            /// </summary>
            public const string TransactionPriorityDataReceivedFromFacility = "Trasanction priority data receieved from facility service. Data : {0} ";
            /// <summary>
            /// constant for DataReceivedFromFormularyFacility
            /// </summary>
            public const string DataReceivedFromFormularyFacility = "FormularyFacility data receieved from formulary service. Data : {0} ";
        }
    }
}
