
using Serilog.Formatting.Display;

namespace BD.Core.Logging.Models
{
    internal static class Model
    {
        public const string LEVEL = "level";
        public const string LEVEL_NAME = "levelName";
        public const string DATE_TIME = "dateTime";
        public const string EPOCH_TIMESTAMP_NS = "epochTimestampNS";
        public const string METHOD = "method";
        public const string MESSAGE = "message";
        public const string STACKTRACE = "stackTrace";
        public const string DATA = "data";

        //Middleware Properties
        public const string USER = "user";
        public const string DURATION_NS = "durationNS";
        public const string STATUS_CODE = "statusCode";
        public const string CORRELATION_CURRENT_ID = "correlationCurrentId";
        public const string CORRELATION_PARENT_ID = "correlationParentId";
        public const string CORRELATION_ORIGIN_ID = "correlationOriginId";

        //Scope Property
        public const string SCOPE = "Scope";

        // ------------- Tracing specific headers ---------------------

        /// <summary>
        /// The request identifier
        /// </summary>
        public const string REQUEST_ID = "x-request-id";

        /// <summary>
        /// The b3 trace identifier
        /// </summary>
        public const string B3_TRACE_ID = "x-b3-traceid";

        /// <summary>
        /// The b3 span identifier
        /// </summary>
        public const string B3_SPAN_ID = "x-b3-spanid";

        /// <summary>
        /// The b3 parent span identifier
        /// </summary>
        public const string B3_PARENT_SPAN_ID = "x-b3-parentspanid";

        /// <summary>
        /// The b3 sampled
        /// </summary>
        public const string B3_SAMPLED = "x-b3-sampled";

        /// <summary>
        /// The b3 flags
        /// </summary>
        public const string B3_FLAGS = "x-b3-flags";

        /// <summary>
        /// The ot span context
        /// </summary>
        public const string OT_SPAN_CONTEXT = "x-ot-span-context";

        /// <summary>
        /// 
        /// </summary>
        public const string UBER = "uber-trace-id";

        /// <summary>
        /// Trace Id
        /// </summary>
        public const string Trace_Id = "TraceId";
    }

    internal static class CustomOutputProperties
    {
        public const string CUSTOM_PROPERTIES = "CustomProperties";
        public const string CONTAINER_PROPERTIES = "ContainerProperties";
    }
}