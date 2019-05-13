using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.Common.Constants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using AutoMapper;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models.Enums;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Concrete
{
    /// <summary>
    /// This class is responsible for handling Priority related operations.
    /// </summary>
    public class TransactionPriorityManager : ITransactionPriorityManager
    {
        #region Private Fields
        private readonly ITransactionPriorityRepository _transactionPriorityRepository;
        private readonly ILogger<TransactionPriorityManager> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        /// <summary>
        /// Intializes the private fields.
        /// </summary>
        /// <param name="transactionPriorityDAL">transactionPriorityDAL</param>
        /// <param name="logger">logger</param>
        public TransactionPriorityManager(ITransactionPriorityRepository transactionPriorityRepository,
            ILogger<TransactionPriorityManager> logger,
            IMapper mapper)
        {
            _transactionPriorityRepository = transactionPriorityRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get TransactionPriority record from DB based on transactionPriorityId.
        /// </summary>
        /// <param name="transactionPriorityId">TransactionPriorityId</param>
        /// <returns></returns>
        public async Task<TransactionPriority> GetTransactionPriority(int transactionPriorityId)
        {
            var result = await _transactionPriorityRepository.GetTransactionPriority(transactionPriorityId);
            if (result != null)
            {
                return _mapper.Map<TransactionPriority>(result);
            }
            return null;
        }

        /// <summary>
        /// Get TransactionPriority record from DB based on transactionPriorityCode.
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public async Task<TransactionPriority> GetTransactionPriority(Priority priority, int facilityId)
        {
            var result = await _transactionPriorityRepository.GetTransactionPriority(priority, facilityId);
            if (result != null)
            {
                return _mapper.Map<TransactionPriority>(result);
            }
            return null;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// This method is used to store a Priority in DB.
        /// </summary>
        /// <param name="priority">Priority to be inserted/updated.</param>
        public async Task<bool> ProcessTransactionPriorityRequest(Models.TransactionPriority priority)
        {
            _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.TransactionPriorityDataReceivedFromFacility, JsonConvert.SerializeObject(priority)));
            var result = await _transactionPriorityRepository
                .GetPriorityByFacilityIdAndPriorityCode(priority.FacilityId, priority.TransactionPriorityCode);

            if (result == null)
            {
                return await _transactionPriorityRepository.InsertTransactionPriority(priority);
            }
            else
            {
                return await _transactionPriorityRepository.UpdateTransactionPriority(priority);
            }
        }

        /// <summary>
        /// This method is used to validate priority.
        /// </summary>
        /// <param name="facilityId">FacilityId</param>
        /// <param name="priorityCode">PriorityCode</param>
        /// <returns></returns>
        public async Task<TransactionPriority> ValidatePriority(int facilityId, string priorityCode)
        {
            var result = await _transactionPriorityRepository.GetPriorityByFacilityIdAndPriorityCode(facilityId, priorityCode);
            if (result != null)
            {
                return _mapper.Map<TransactionPriority>(result);
            }

            return null;
        }

        #endregion
    }
}
