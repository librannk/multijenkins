
namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Enum
{
    /// <summary>
    /// DevResponse Enum
    /// </summary>
    public enum DevResponse : uint
    {
        /// <summary>
        ///  enum DevResponse constant OK
        /// </summary>
        OK,
        /// <summary>
        ///  enum DevResponse constant InMotion
        /// </summary>
        InMotion,
        /// <summary>
        ///  enum DevResponse constant SafetyError
        /// </summary>
        SafetyError,
        /// <summary>
        ///  enum DevResponse constant TransactionInProgress
        /// </summary>
        TransactionInProgress,
        /// <summary>
        ///  enum DevResponse constant BufferFull
        /// </summary>
        BufferFull,
        /// <summary>
        ///  enum DevResponse constant NoDataAvailable
        /// </summary>
        NoDataAvailable,
        /// <summary>
        ///  enum DevResponse constant InvalidLocation
        /// </summary>
        InvalidLocation,
        /// <summary>
        ///  enum DevResponse constant SlowMotion
        /// </summary>
        SlowMotion,
        /// <summary>
        ///  enum DevResponse constant NoResponse
        /// </summary>
        NoResponse,
        /// <summary>
        ///  enum DevResponse constant StatusInvalid
        /// </summary>
        StatusInvalid,
        /// <summary>
        ///  enum DevResponse constant InvalidCommand
        /// </summary>
        InvalidCommand,
        /// <summary>
        ///  enum DevResponse constant ControlProgramError
        /// </summary>
        ControlProgramError,
        /// <summary>
        ///  enum DevResponse constant ImbalanceDown
        /// </summary>
        ImbalanceDown,
        /// <summary>
        ///  enum DevResponse constant ImbalanceUp
        /// </summary>
        ImbalanceUp,
        /// <summary>
        ///  enum DevResponse constant ChecksumError
        /// </summary>
        ChecksumError,
        /// <summary>
        ///  enum DevResponse constant CommError
        /// </summary>
        CommError,
        /// <summary>
        ///  enum DevResponse constant DeviceBusy
        /// </summary>
        DeviceBusy,
        /// <summary>
        ///  enum DevResponse constant DeviceNotConnected
        /// </summary>
        DeviceNotConnected,
        /// <summary>
        ///  enum DevResponse constant DeviceNotFound
        /// </summary>
        DeviceNotFound,
        /// <summary>
        ///  enum DevResponse constant InvalidClient
        /// </summary>
        InvalidClient,
        /// <summary>
        ///  enum DevResponse constant InvalidParameter
        /// </summary>
        InvalidParameter,
        /// <summary>
        ///  enum DevResponse constant InternalServerError
        /// </summary>
        InternalServerError,
        /// <summary>
        ///  enum DevResponse constant ExtendedErrorCode
        /// </summary>
        ExtendedErrorCode,
        /// <summary>
        ///  enum DevResponse constant MaxStatusValue
        /// </summary>
        MaxStatusValue,
        /// <summary>
        ///  enum DevResponse constant ManualMode
        /// </summary>
        ManualMode
    }
}
