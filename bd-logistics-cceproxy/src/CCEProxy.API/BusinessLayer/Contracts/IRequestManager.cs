using CCEProxy.API.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCEProxy.API.BusinessLayer.Contracts
{
    /// <summary>
    /// This interface is responsible for handling incoming object and inserting it in DB.
    /// </summary>
    public interface IRequestManager
    {
        /// <summary>
        /// Apply validations on the incoming object.
        /// If validation passes, create an aggregated object based on facility, formulary and incoming request and return it.
        /// </summary>
        Task<string> ProcessIncomingRequest(IncomingRequest incomingRequest, string incomingRequestId, Dictionary<string, string> headers);

        /// <summary>
        /// Insert the incoming request when the model state fails and update the status
        /// </summary>
        Task<string> InsertIncomingRequest(IncomingRequest incomingRequest);
    }
}
