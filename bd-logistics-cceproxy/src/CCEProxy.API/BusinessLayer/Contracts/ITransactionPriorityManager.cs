using CCEProxy.API.Entity;
using System.Threading.Tasks;

namespace CCEProxy.API.BusinessLayer.Contracts
{
    /// <summary> This interface is responsible for handling the TransactionPriority Queue operation </summary>
    public interface ITransactionPriorityManager
    {
        /// <summary> This method is used for filtering the request and save the data into database </summary>
        /// <param name="transactionPriorityRequest"> Request</param>
        Task ProcessTransactionPriorityRequest(TransactionPriority transactionPriorityRequest);
    }
}
