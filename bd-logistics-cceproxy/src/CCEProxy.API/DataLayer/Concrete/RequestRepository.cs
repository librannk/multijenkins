using CCEProxy.Repository.Contracts;
using CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces;
using System.Threading.Tasks;
using CCEProxy.API.Entity;
using AutoMapper;

namespace CCEProxy.API.DataLayer.Concrete
{
    /// <summary> Implementation of interface. Handles incoming object data operations. </summary>
    public class RequestRepository : IRequestRepository
    {
        #region properties
        private readonly IIncomingRequestRepository _incomingRequestRepository;
        private readonly ITransactionPriorityRepository _transactionPriorityRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Contructors
        /// <param name="incomingRequestRepository"></param>
        /// <param name="facilityRepository"></param>
        /// <param name="transactionPriorityRepository"></param>
        /// <param name="mapper"></param>
        public RequestRepository(IIncomingRequestRepository incomingRequestRepository
            , ITransactionPriorityRepository transactionPriorityRepository,
            IFacilityRepository facilityRepository,
            IMapper mapper)
        {
            _incomingRequestRepository = incomingRequestRepository;
            _transactionPriorityRepository = transactionPriorityRepository;
            _facilityRepository = facilityRepository;
            _mapper = mapper;
        }
        #endregion

        #region Adding Incoming Request
        /// <summary>
        /// Insert IncomingRequest to db.
        /// </summary>
        /// <param name="incomingRequest"></param>
        /// <returns>IncomingRequestId</returns>
        public async Task<string> AddIncomingRequest(IncomingRequest incomingRequest)
        {
            var insertIncomingRequest = _mapper.Map<Infrastructure.DataAccess.DBModel.IncomingRequest>(incomingRequest);
            await _incomingRequestRepository.InsertAsync(insertIncomingRequest);
            return insertIncomingRequest.Id;
        }
        #endregion

        #region Updating the incoming Request
        /// <summary>
        /// Update IncomingRequestStatus in db.
        /// </summary>
        /// <param name="incomingRequestId"></param>
        /// <param name="status"></param>
        /// <param name="statusMessage"></param>
        /// <returns>true/false</returns>
        public async Task UpdateIncomingRequest(string incomingRequestId, string status, string statusMessage)
        {
            var incomingRequest = await _incomingRequestRepository.GetByIdAsync(incomingRequestId);
            incomingRequest.Status = status;
            incomingRequest.StatusMessage = statusMessage;
            _incomingRequestRepository.Update(incomingRequest);
        }

        #endregion

        #region Getting Transaction Priority by FacilityId and PriorityCode.
        /// <summary>
        /// Get TransactionPriority by FacilityId and PriorityCode.
        /// This will include TransactionPriorityDetails
        /// </summary>
        /// <param name="facilityId"></param>
        /// <param name="priorityCode"></param>
        /// <returns></returns>
        public async Task<TransactionPriority> GetTransactionPriority(int facilityId, string priorityCode)
        {
            TransactionPriority transactionPriority = null;
            var transactionPriorityDb = await _transactionPriorityRepository.GetTransactionPriority(facilityId, priorityCode);

            if (transactionPriorityDb != null)
            {
                transactionPriority = _mapper.Map<TransactionPriority>(transactionPriorityDb);
                return transactionPriority;
            }
            return transactionPriority;
        }
        #endregion

        #region Get Facility by FacilityCode
        /// <summary>
        /// Get Facility by FacilityCode This will include Facility Details
        /// </summary>
        /// <param name="facilityCode"> </param>
        /// <returns></returns>
        public async Task<Facility> GetFacility(string facilityCode)
        {
            Facility facility = null;
            var facilityDb = await _facilityRepository.GetFacilityByCode(facilityCode);
            if (facilityDb != null)
            {
                facility = _mapper.Map<Facility>(facilityDb);

                return facility;
            }
            return facility;
        }
        #endregion

        #region Adding Facility Request
        /// <summary>
        /// Insert FacilityRequest to db.
        /// </summary>
        /// <param name="facility"></param>
        public async Task AddFacilityRequest(Facility facility)
        {
            var insertFacilityRequest = _mapper.Map<Infrastructure.DataAccess.DBModel.Facility>(facility);

            await _facilityRepository.InsertAsync(insertFacilityRequest);
        }
        #endregion

        #region Adding TransactionPriority Request
        /// <summary>
        /// Insert TransactionPriority to db.
        /// </summary>
        /// <param name="transactionPriority"></param>
        public async Task AddTransactionPriorityRequest(TransactionPriority transactionPriority)
        {
            var insertTransactionPriorityRequest = _mapper.Map<Infrastructure.DataAccess.DBModel.TransactionPriority>(transactionPriority);
            await _transactionPriorityRepository.InsertAsync(insertTransactionPriorityRequest);
        }
        #endregion
    }
}
