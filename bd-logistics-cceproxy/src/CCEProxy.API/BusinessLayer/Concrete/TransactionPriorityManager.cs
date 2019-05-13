using CCEProxy.API.BusinessLayer.Contracts;
using CCEProxy.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CCEProxy.API.Entity;
using static CCEProxy.API.Common.Constants.Constants;
using System.Threading.Tasks;

namespace CCEProxy.API.BusinessLayer.Concrete
{
    /// <summary> TransactionPriorityManager </summary>
    public class TransactionPriorityManager : ITransactionPriorityManager
    {
        #region Private Fields
        private readonly IRequestRepository _requestRepository;
        private readonly ILogger _logger;
        #endregion

        #region Constructors
        /// <summary> Initialize the private fields </summary>
        public TransactionPriorityManager(IRequestRepository requestRepository, ILogger<TransactionPriorityManager> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;

        }
        #endregion
        /// <summary>
        /// This method processes the facility request from Facility Service and insert the data into database.
        /// <param name="transactionPriorityRequest">facilityRequest</param>
        /// </summary>
        public async Task ProcessTransactionPriorityRequest(TransactionPriority transactionPriorityRequest)
        {
            _logger.LogInformation(LoggingMessage.ProcessTransactionPriorityRequest, JsonConvert.SerializeObject(transactionPriorityRequest));
            await _requestRepository.AddTransactionPriorityRequest(transactionPriorityRequest);
        }
    }
}
