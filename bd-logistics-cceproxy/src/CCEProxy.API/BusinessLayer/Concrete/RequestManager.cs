using Microsoft.Extensions.Logging;
using System;
using CCEProxy.API.BusinessLayer.Contracts;
using CCEProxy.Repository.Contracts;
using CCEProxy.API.IntegrationEvents.Events;
using System.Collections.Generic;
using static CCEProxy.API.Common.Constants.Constants;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using CCEProxy.API.Entity;
using BD.Core.EventBus.Abstractions;

namespace CCEProxy.API.BusinessLayer.Concrete
{
    /// <summary>
    /// This class is responsible for handling Incoming request.
    /// </summary>
    public class RequestManager : IRequestManager
    {
        #region properties declaration

        private readonly IRequestRepository _requestRepository;
        private readonly ILogger<RequestManager> _logger;
        private readonly IEventBus _eventBus;
        private readonly Configuration.Configuration _configuration;

        #endregion

        /// <summary>
        /// Constructor to inject the repository object
        /// </summary>
        /// <param name="requestRepository"></param>
        /// <param name="eventBus"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        #region constructor
        public RequestManager(IOptions<Configuration.Configuration> options, 
            IEventBus eventBus, 
            IRequestRepository requestRepository, 
            ILogger<RequestManager> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
            _eventBus = eventBus;
            _configuration = options.Value;
        }
        #endregion

        #region Public Function to Process Incoming Request
        /// <summary>
        /// Apply validations on the incoming object.
        /// If validation passes, return aggregated object of incoming request 
        /// </summary>
        /// <param name="incomingRequest"></param>
        /// <param name="incomingRequestId"></param>
        /// <param name="headers"></param>
        public async Task<string> ProcessIncomingRequest(IncomingRequest incomingRequest, string incomingRequestId, Dictionary<string, string> headers)
        {
            string responseMessage = string.Empty;

            #region Step1. Get Facility based on Facility Code coming from Incoming Request
            var facility = await _requestRepository.GetFacility(incomingRequest.Facility.FacilityCode);

            string facilityResponse = CheckFacility(facility, incomingRequestId);
            if (!string.IsNullOrEmpty(facilityResponse))
            {
                await _requestRepository.UpdateIncomingRequest(incomingRequestId, IncomingRequestStatus.Rejected, LoggingMessage.FacilityInvalid);
                return facilityResponse;
            }
            #endregion

            #region Step2. Get Transaction Priority based on Facility Id from Facility and PriorityCode from incoming request
            var transactionPriority = await _requestRepository.GetTransactionPriority(facility.Id, incomingRequest.Priority);

            string transactionResponse = CheckTransactionPriority(incomingRequestId, transactionPriority, incomingRequest.Priority);
            if (!string.IsNullOrEmpty(transactionResponse))
            {
                await _requestRepository.UpdateIncomingRequest(incomingRequestId, IncomingRequestStatus.Rejected, LoggingMessage.PriorityInvalid);
                return transactionResponse;
            }
            #endregion

            #region Step3. Aggregate all the Data and update the status and status message

            incomingRequest.Facility.FacilityId = facility.Id;
            incomingRequest.RequestId = incomingRequestId;
            incomingRequest.Items = incomingRequest.Items.Where(x => x.ItemId != null);
            await _requestRepository.UpdateIncomingRequest(incomingRequestId, IncomingRequestStatus.Accepted, LoggingMessage.DataValid);

            #endregion Step3

            PublishIncomingRequestData(incomingRequest, headers);
            return responseMessage;
        }

        #endregion

        /// <summary>
        /// Check for Facility.
        /// If validation fails, return response message
        /// </summary>
        /// <param name="facility"></param>
        /// <param name="incomingRequestId"></param>
        private string CheckFacility(Facility facility, string incomingRequestId)
        {
            string responseMessage = string.Empty;
            if (facility == null || facility.Id == 0)
            {
                responseMessage = LoggingMessage.FacilityInvalid;
                _logger.LogInformation($"{incomingRequestId}" + IncomingRequestStatus.Rejected + $"{facility?.FacilityCode}" + LoggingMessage.FacilityInvalid);
                return responseMessage;
            }
            return responseMessage;
        }

        /// <summary>
        /// Check for TransactionPriority.
        /// If validation fails, return response message
        /// </summary>
        /// <param name="transactionPriority"></param>
        /// <param name="incomingRequestId"></param>
        /// <param name="priorityCode"></param>
        private string CheckTransactionPriority(string incomingRequestId, TransactionPriority transactionPriority, string priorityCode )
        {
            string responseMessage = string.Empty;
            if (transactionPriority == null)
            {
                responseMessage = LoggingMessage.PriorityInvalid;
                _logger.LogInformation($"{ incomingRequestId}" + IncomingRequestStatus.Rejected + $"{priorityCode}" + LoggingMessage.PriorityInvalid);
                return responseMessage;
            }

            return responseMessage;
        }
        #region Publish IncomingRequest
        /// <summary>
        /// Publishing the incoming request data
        /// </summary>
        private void PublishIncomingRequestData(IncomingRequest incomingRequest, Dictionary<string, string> headers)
        {
            var eventMessage = new TransactionQueueAddedIntegrationEvent
            {
                Message = incomingRequest,
                Headers = headers
            };

            _eventBus.Publish(_configuration.KafkaCCEProxyTopic, eventMessage, headers);

            _logger.LogInformation(String.Format(LoggingMessage.DataPublished, JsonConvert.SerializeObject(eventMessage)));
        }
        #endregion

        #region public function to insert incoming Request
        /// <summary>
        /// Insert the incoming request data with the incoming status and status Message
        /// </summary>
        public async Task<string> InsertIncomingRequest(IncomingRequest incomingRequest)
        {
            #region Insert record in db

            string incomingRequestId =  await _requestRepository.AddIncomingRequest(incomingRequest);

            #endregion Insert record in db

            return incomingRequestId;
        }
        #endregion
    }
}