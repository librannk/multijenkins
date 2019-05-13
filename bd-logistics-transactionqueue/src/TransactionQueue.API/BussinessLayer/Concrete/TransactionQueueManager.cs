using AutoMapper;
using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.API.Application.BussinessLayer.Abstraction;
using TransactionQueue.API.Application.Entities;
using TransactionQueue.API.Common.Constants;
using TransactionQueue.API.Infrastructure.Repository.Interfaces;
using TransactionQueue.API.IntegrationEvents.Events;
using TransactionQueue.Ingestion.BusinessLayer.Abstraction;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;

namespace TransactionQueue.API.Application.BussinessLayer.Concrete
{
    /// <summary> This class is responsible for handling the Transaction Queue operations </summary>
    public class TransactionQueueManager : ITransactionQueueManager
    {
        #region Private Fields
        private readonly IEventBus _eventBus;
        private Shared.Configuration.Configuration _configuration;
        private readonly ILogger<TransactionQueueManager> _logger;
        private readonly IMapper _mapper;
        private readonly ITransactionQueueMongoRepository _transactionQueueMongoRepository;
        private readonly ITransactionManager _transactionManager;
        #endregion

        #region Constructors
        /// <summary> Initialize the private fields </summary>
        public TransactionQueueManager(IEventBus eventBus,
            IOptions<Shared.Configuration.Configuration> options,
            ILogger<TransactionQueueManager> logger,
            IMapper mapper,
            ITransactionQueueMongoRepository transactionQueueMongoRepository,
            ITransactionManager transactionManager)
        {
            _configuration = options.Value;
            _eventBus = eventBus;
            _logger = logger;
            _mapper = mapper;
            _transactionQueueMongoRepository = transactionQueueMongoRepository;
            _transactionManager = transactionManager;
        }
        #endregion

        #region Public Methods
        /// <summary> This method is used to update the transaction </summary>
        /// <param name="transactionQueueId">update transaction status against transactionQueueId </param>
        /// <param name="status">status updated against transaction </param>
        /// <param name="headers"> headers</param>
        public async Task<TransactionQueueModel> UpdateTransactionStatus(string transactionQueueId, TransactionStatus status, Dictionary<string, string> headers)
        {
            _logger.LogInformation(string.Format(Constants.LoggingMessage.UpdateTransactionStatusMesage, transactionQueueId));
            return await UpdateTransactionQueueStatus(transactionQueueId, status, headers);
        }

        #endregion

        #region Private Methods

        private void PublishDeviceCommunicationRequest(TransactionQueueModel transactionQueue, Dictionary<string, string> headers)
        {
            var eventMessage = new ProcessTransactionQueueIntegrationEvent()
            {
                TransactionData = new TransactionData
                {
                    Quantity = transactionQueue.Quantity.Value,
                    Type = transactionQueue.Type,
                    Devices = transactionQueue.Devices.ToList()
                },
                Headers = headers
            };

            _eventBus.Publish(_configuration.KafkaDeviceTopic, eventMessage, headers);
            _logger.LogInformation(string.Format(Constants.LoggingMessage.DataPublishedDeviceCommunication, JsonConvert.SerializeObject(eventMessage)));
        }

        private async Task<TransactionQueueModel> UpdateTransactionQueueStatus(string transactionQueueId, TransactionStatus status, Dictionary<string, string> headers)
        {
            var transaction = await _transactionManager.GetTransactionDetails(transactionQueueId);

            if (transaction != null)
            {
                if (status == TransactionStatus.Complete)
                {
                    return await UpdateTransactionStatusWhenStatusIsComplete(transaction, status, headers);
                }

                if (status == TransactionStatus.Active)
                {
                    return await UpdateTransactionStatusWhenStatusIsActive(transaction, status, headers);
                }
            }

            return transaction;
        }

        private async Task<TransactionQueueModel> UpdateTransactionStatusWhenStatusIsActive(TransactionQueueModel transaction, TransactionStatus status, Dictionary<string, string> headers)
        {
            if (transaction.Status == TransactionStatus.Pending)
            {
                var transactionQueueModel = _mapper.Map<TransactionQueueModel>(await _transactionQueueMongoRepository.UpdateTransactionStatus(transaction.TransactionQueueId, status));
                PublishDeviceCommunicationRequest(transactionQueueModel, headers);
                return transactionQueueModel;
            }
            else if (transaction.Status == TransactionStatus.Active)
            {
                return transaction;
            }
            return null;
        }

        private async Task<TransactionQueueModel> UpdateTransactionStatusWhenStatusIsComplete(TransactionQueueModel transaction, TransactionStatus status, Dictionary<string, string> headers)
        {
            if (transaction.Status == TransactionStatus.Active)
            {
                var transactionQueueModel = _mapper.Map<TransactionQueueModel>(await _transactionQueueMongoRepository.UpdateTransactionStatus(transaction.TransactionQueueId, status));
                PublishDeviceCommunicationRequest(transactionQueueModel, headers);
                return transaction;
            }
            else if (transaction.Status == TransactionStatus.Complete)
            {
                return transaction;
            }
            return null;
        }

        #endregion
    }
}

