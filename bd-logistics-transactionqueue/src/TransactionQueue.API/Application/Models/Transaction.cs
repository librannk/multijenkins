using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;

namespace TransactionQueue.API.Application.Models
{
    /// <summary>
    /// It contains information of the transaction
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// To store value of Status
        /// </summary>
        public TransactionStatus? Status { get; set; }
    }
}