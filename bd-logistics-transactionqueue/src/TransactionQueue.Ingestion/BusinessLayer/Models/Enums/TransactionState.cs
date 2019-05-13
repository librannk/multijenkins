
namespace TransactionQueue.Ingestion.BusinessLayer.Models.Enums
{
    /// <summary> TransactionStatus </summary>
    public enum TransactionStatus
    {
        /// <summary> Pending </summary>
        Pending = 1,

        /// <summary> Active </summary>
        Active = 2,

        /// <summary> Exception </summary>
        Exception = 3,

        /// <summary> Interim </summary>
        Interim = 4,

        /// <summary> Complete </summary>
        Complete = 5,

        /// <summary> Hold </summary>
        Hold = 6,

        /// <summary> Ignored </summary>
        Ignored = 7
    }
}
