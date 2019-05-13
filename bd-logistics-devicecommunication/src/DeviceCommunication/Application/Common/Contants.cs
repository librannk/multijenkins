namespace Logistics.Services.DeviceCommunicationAPI.Application.Common
{
    /// <summary>
    /// This class contains all contant messages
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class ProcessTransactionMediator
        {
            #region ProcessTransactionMediator 

            /// <summary>
            /// Use In Execute Method/// </summary>
            public const string EventRecieveFail = "Event not recieved successfully";

            /// <summary>
            /// Use In Execute Method
            /// </summary>
            public const string EventRecieveSuccess = "Event recieved successfully";

            /// <summary>
            /// Use In Execute Method
            /// </summary>
            public const string EventRecievedAtMediator = "Process transaction queue event into ProcessTransactionMediator";

            #endregion
        }

        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class ProcessTransactionQueueEventHandler
        {
            #region ProcessTransactionQueueEventHandler

            /// <summary>
            /// Use In Handle Method
            /// </summary>
            public const string DataReceivedFromTransactionQueue = "Data received from Service Bus - Transaction Queue : {0}";

            #endregion
        }

        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class CarousalProcess
        {
            #region CarousalProcess

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string UnableToConnectToCarousel = "Unable to connect to Carousel";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string CarouselMoveSucccessfully = "Carousel move succcessfully";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string Response = "Response : {0}";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string WhiteTB1470_2300CarouselMoveSucccessfully = "WhiteTB1470_2300 carousel move succcessfully";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string MoveWhiteTB1470_2300 = "Move WhiteTB1470_2300 : {0}";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string ClearBuffer = "ClearBuffer : {0}";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string Move = "Move : {0}";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string CarouselConnected = "Carousel connected";
            
            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string SlotObjectCreated = "Location storage space Slot is created using BuildStorageSpaceSlot method ";

            /// <summary>
            /// Use In MoveCarousel Method
            /// </summary>
            public const string CarouselCreated = "Carousel is created successfully using carousel manager";

            /// <summary>
            /// Use In MoveCarousel Method
            /// </summary>
            public const string CarouselMoved = "Carousel moved successfully using carousel manager";

            /// <summary>
            ///Use In MoveCarousel Method
            /// </summary>
            public const string ConnectionReset = "Connection reset";

            #endregion
        }

        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class CustomExceptionFilter
        {
            #region CustomExceptionFilter

            /// <summary>
            ///Use In OnException Method
            /// </summary>
            public const string CustomErrorMessage = "Hear Customer message will code depending on code";

            /// <summary>
            ///Use In OnException Method
            /// </summary>
            public const string CustomCode = "Hear Customer code will code depending on code";

            #endregion
        }

        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class TelegramAppend
        {

            #region TelegramAppend

            /// <summary>
            /// Use in SetCharacterAttributes method
            /// </summary>
            public const string CarWhiteTermChars = "\x0003";

            /// <summary>
            /// Use in SetCharacterAttributes method
            /// </summary>
            public const string CarWhiteBinBaseAddr = "0011";

            /// <summary>
            ///  Use in SetCharacterAttributes method
            /// </summary>
            public const string CarWhiteAlphaBaseAddr = "0012";

            /// <summary>
            ///  Use in ClearBuffer method
            /// </summary>
            public const char CarWhiteTelegramAppend1 = '\x0006';

            /// <summary>
            ///  Use in ClearDisplay method
            /// </summary>
            public const char CarWhiteTelegramAppend2 = '\x0002';

            /// <summary>
            ///  Use in ClearDisplay method
            /// </summary>
            public const string CarWhiteTelegramAppend0 = "0000";

            /// <summary>
            /// 
            /// </summary>
            public const char CarWhiteTelegramAppend3 = '\x0003';

            /// <summary>
            /// Use in MoveCarousel method
            /// </summary>
            public const char CarWhiteTelegramAppend6 = '\x0006';

            /// <summary>
            /// Use in ClearDisplay method
            /// </summary>
            public const string CarWhiteTelegramAppend7 = "{cl}";

            /// <summary>
            /// Use in ClearDisplay method
            /// </summary>
            public const string CarWhiteTelegramAppend8 = "{W}";

            /// <summary>
            /// Use in MoveCarousel method
            /// </summary>
            public const string CarWhiteTelegramAppend9 = "3";

            /// <summary>
            /// Use in MoveCarousel method
            /// </summary>
            public const string CarWhiteTelegramAppend10 = "G";

            /// <summary>
            /// Use in MoveCarousel method
            /// </summary>
            public const string CarWhiteTelegramAppend11 = "s";

            /// <summary>
            /// Use in MoveCarousel method
            /// </summary>
            public const string CarWhiteTelegramAppend12 = "000";

            /// <summary>
            ///  Use in CarWhiteTB1470 method
            /// </summary>
            public const string CarWhiteTelegramAppendSS = "SS";


            #endregion
        }

        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class CarouselManager
        {
            #region CarouselManager

            /// <summary>
            /// Use in CreateCarousel method
            /// </summary>
            public const string ControllerTypeNotFound = "Controller for this storage space is not found";

            /// <summary>
            ///Use In CreateCarousel Method
            /// </summary>
            public const string TransactionQueueCarouselIsCreatedAndCarouselInstantiated = "TransactionQueueCarousel is created and Carousel instantiated";
            
            #endregion
        }

        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class Carousel
        {
            #region Carousel
            /// <summary>
            /// Use in write telegram message on connected device successfully
            /// </summary>
            public const string DeviceWriteSuccess = "Data is written on device successfully.";
            /// <summary>
            /// Use in write telegram message on connected device failed
            /// </summary>
            public const string DeviceWriteFail = "Data could not write on device.";
            /// <summary>
            /// Use in read response from  connected device
            /// </summary>
            public const string DeviceNoResponse = "No response from device";
            /// <summary>
            /// Use in read response from connected device
            /// </summary>
            public const string DeviceOKResponse = "Response recieved from device successfully";
            #endregion
        }

        /// <summary>
        /// Class specific constants
        /// </summary>
        public static class Swagger
        {
            #region Swagger

            /// <summary> Url for swagger endpoint </summary>
            public const string SwaggerEndPoint = "/swagger/v1/swagger.json";

            /// <summary> Title </summary>
            public const string Title = "DeviceCommunication.API";

            /// <summary> Version </summary>
            public const string Version = "v1";

            /// <summary> RouteTemplate </summary>
            public const string RouteTemplate = "devicecomm/swagger/{documentName}/swagger.json";

            /// <summary> RoutePrefix </summary>
            public const string RoutePrefix = "devicecomm";

            #endregion
        }

        /// <summary>
        /// Class specific constants
        ///</summary>
        public static class Configuration
        {
            /// <summary> Settings </summary>
            public const string CarouselTimeout = "Settings:CarouselTimeout";
        }

        ///<summary>
        ///Logging Messages
        ///</summary>
        public static class LoggingMessage
        {
            /// <summary> RequestReceived </summary>
            public const string RequestReceived = "Received request : {0}";

            /// <summary> ResponseReceived </summary>
            public const string ResponseGenerated = "Generated response : {0}";
        }
        
        ///<summary>
        ///Error Messages
        ///</summary>
        public static class ErrorMessage
        {
            /// <summary> IPNull </summary>
            public const string IPNull = "IP is null";
            
            /// <summary> IPNull </summary>
            public const string InvalidIP = "Invalid IP address";

            /// <summary> InvalidDisplayIP </summary>
            public const string InvalidDisplayIP = "Invalid DisplayIPAddress";

            /// <summary> InvalidDisplayPort </summary>
            public const string InvalidDisplayPort = "DisplayPort should be between 0 and 65535";

            /// <summary> PortNull </summary>
            public const string PortNull = "Port is null";


        }
        
    }
}
