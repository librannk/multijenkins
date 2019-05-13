using CCEProxy.API.Entity;
using System.Threading.Tasks;

namespace CCEProxy.Repository.Contracts
{
    /// <summary> This interface is responsible for handling the incoming object data operations 
    /// </summary>

    public interface IRequestRepository
    {
        /// <summary>
        /// Insert IncomingRequest to db.
        /// </summary>
        Task<string> AddIncomingRequest(IncomingRequest incomingRequest);

        /// <summary>
        /// Update IncomingRequestStatus in db.
        /// </summary>
        Task UpdateIncomingRequest(string incomingRequestId, string status, string statusMessage);

        /// <summary>
        /// Get TransactionPriority by FacilityId and PriorityCode.
        /// This will include Transaction priority information
        /// </summary>
        Task<TransactionPriority> GetTransactionPriority(int facilityId, string priorityCode);
        /// <summary>
        /// Get Facility by FacilityCode 
        /// </summary>
        Task<Facility> GetFacility(string facilityCode);
        /// <summary>
        /// Add Facility in database
        /// </summary>
        Task AddFacilityRequest(Facility facility);
        /// <summary>
        /// Add Transaction Prioirty in database
        /// </summary>
        Task AddTransactionPriorityRequest(TransactionPriority transactionPriority);

    }
}
