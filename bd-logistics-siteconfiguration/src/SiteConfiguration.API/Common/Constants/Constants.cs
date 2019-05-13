using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Common.Constants
{
    public static class Constants
    {
        public const string Empty_List = "List Contains No Result";

        public const string GetScheduleRequestReceived = "Get Schedule Request received with Facility Id : {0}";

        public const string GetPrinterRequestReceived = "Get Printer Request received with Facility Key : {0}";

        public const string GetPrinterModelRequestReceived = "Get Printer Model Request received";

        public const string GetPrinterByKeyRequestReceived = "Get Printer Request received with Printer Key: {0}";

        public const string PutPrinterRequestReceived = "Put Printer Request received with Printer Key: {0}";

        public const string PostScheduleRequestReceived = "Post Schedule Request received with Facility Id : {0}";

        public const string DeleteScheduleRequestReceived = "Post Schedule Request received";

        public const string PostPrinterRequestReceived = "Post Printer Request received with Facility Id : {0}";

        public const string ModelValidationFailed = "Model Validation Failed";

        public const string RecordsNotFound = "records not found";

        public const string InvalidIpAddress = "The entered IP Address is invalid.Please enter valid IP4 address";

        public const string InvalidMacAddress = "The entered MAC Address is invalid.Please enter valid MAC address";

        public const string InvalidDimensions = "Should have upto 15 numeric characters upto thousand of an inch";

    }

    public static class ErrorCode
    {
        public const short ResourceAlreadyExists = 4000;

        public const short ResourceAssociated = 4001;

        public const short ResourceNotFound = 4002;

        public const short DuplicateResourceNameExists = 4003;

        public const short InvalidInput = 4004;
    }
}
