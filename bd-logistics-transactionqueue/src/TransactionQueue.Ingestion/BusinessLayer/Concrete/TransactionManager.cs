using AutoMapper;
using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models.Enums;
using TransactionQueue.Ingestion.BusinessLayer.Abstraction;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Ingestion.BusinessLayer.Repository;
using TransactionQueue.Ingestion.Common.Constants;
using TransactionQueue.Ingestion.IntegrationEvents.Events;
using TransactionQueue.Shared.Common.Helpers;
using TransactionQueue.Shared.Configuration;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.Ingestion.BusinessLayer.Concrete
{
    /// <summary> This class is responsible for handling the Transaction Queue operations </summary>
    public class TransactionManager : ITransactionManager
    {
        #region Private Fields
        private readonly IEventBus _eventBus;
        private readonly ITransactionQueueRepository _transactionQueueRepository;
        private readonly ITransactionQueueHistoryRepository _transactionQueueHistoryRepository;
        private readonly IFacilityManager _facilityManager;
        private readonly ITransactionPriorityManager _transactionPriorityManager;
        private readonly IFormularyManager _formularyManager;
        private readonly IAduTransactionManager _aduTransactionManager;
        private readonly IDestinationManager _destinationManager;
        private Configuration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        #endregion

        #region Constructors
        /// <summary> Initialize the private fields </summary>
        public TransactionManager(IEventBus eventBus,
            ILogger<TransactionManager> logger,
            IOptions<Configuration> options,
            IMapper mapper,
            IConfiguration configuration,
            ITransactionQueueRepository transactionQueueRepository,
            ITransactionQueueHistoryRepository transactionQueueHistoryRepository,
            IFacilityManager facilityManager,
            ITransactionPriorityManager transactionPriorityManager,
            IFormularyManager formularyManager,
            IAduTransactionManager aduTransactionManager,
            IDestinationManager destinationManager)
        {
            _configuration = options.Value;
            _mapper = mapper;
            _eventBus = eventBus;
            _logger = logger;
            this.configuration = configuration;
            _transactionQueueRepository = transactionQueueRepository;
            _transactionQueueHistoryRepository = transactionQueueHistoryRepository;
            _facilityManager = facilityManager;
            _transactionPriorityManager = transactionPriorityManager;
            _formularyManager = formularyManager;
            _aduTransactionManager = aduTransactionManager;
            _destinationManager = destinationManager;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// This method processes the aggregated incoming request from Aggregator Service, applies business rules and inserts transaction record.
        /// </summary>
        /// <param name="request">Incoming request</param>
        /// <param name="headers">Headers</param>
        public async Task ProcessTransactionRequest(TransactionRequest request, Dictionary<string, string> headers)
        {
            _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.ProcessIncomingRequest, JsonConvert.SerializeObject(request)));

            var status = TransactionStatus.Interim;
            var facilityId = request.Facility.FacilityId;
            FacilityStorageSpace storageSpaceInfo = null;
            var priority = await _transactionPriorityManager.ValidatePriority(request.Facility.FacilityId, request.Priority);
            var facility = await _facilityManager.ValidateFacility(facilityId);
            var destination = await _destinationManager.GetDestinationByCode(request?.Patient?.DeliverToLocation);

            if (facility != null)
            {
                if ((facility.AduIgnoreCritLow == true && string.Equals(request.Priority, Priority.PYXISCRITLOW.ToString(), StringComparison.OrdinalIgnoreCase))
                || (facility.AduIgnoreStockout == true && (string.Equals(request.Priority, Priority.PYXISSTOCKOUT.ToString(), StringComparison.OrdinalIgnoreCase) || string.Equals(request.Priority, Priority.PYXISSTKOUT.ToString(), StringComparison.OrdinalIgnoreCase))))
                {
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.RejectedRequest, request.RequestId));
                    status = TransactionStatus.Ignored;
                }

                storageSpaceInfo = facility.StorageSpaces?.Where(x => x.IsDefault).FirstOrDefault();
            }

            var partNo = 0;
            var multiMedName = string.Empty;
            var transactionList = new List<TransactionQueueModel>();

            var orderCount = Enumerable.Count<Item>(request.Items);
            var isMultiComponent = orderCount > 1 ? true : false;

            foreach (var item in request.Items)
            {
                FacilityFormulary facilityFormulary = null;

                var formulary = await _formularyManager.GetFormularyByItemId(item.ItemId);

                if (formulary != null && formulary.FacilityFormulary != null && formulary.FacilityFormulary.FacilityId == facilityId)
                    facilityFormulary = formulary.FacilityFormulary;

                if (facility != null && facilityFormulary != null &&
                    ((string.Equals(request.Priority, Priority.PYXISCRITLOW.ToString(), StringComparison.OrdinalIgnoreCase) && facilityFormulary.AduIgnoreCritLow != null && (bool)facilityFormulary.AduIgnoreCritLow)
                    || ((string.Equals(request.Priority, Priority.PYXISSTOCKOUT.ToString(), StringComparison.OrdinalIgnoreCase) || string.Equals(request.Priority, Priority.PYXISSTKOUT.ToString(), StringComparison.OrdinalIgnoreCase))
                    && facilityFormulary.AduIgnoreStockout != null && (bool)facilityFormulary.AduIgnoreStockout)))
                {
                    status = TransactionStatus.Ignored;
                }

                //Checks for ADUIgnoreCritLow and ADUIgnoreStockOut prior to ADU Cases
                if (priority != null && destination != null
                    && ((priority.TransactionPriorityCode.ToLower() == Priority.PYXISCRITLOW.ToString().ToLower()
                    && destination.AduIgnoreCritLow.GetValueOrDefault())
                    || ((priority.TransactionPriorityCode.ToLower() == Priority.PYXISSTOCKOUT.ToString().ToLower() || priority.TransactionPriorityCode.ToLower() == Priority.PYXISSTKOUT.ToString().ToLower())
                    && destination.AduIgnoreStockOut.GetValueOrDefault())))
                {
                    status = TransactionStatus.Ignored;
                }

                int.TryParse(item.OrderAmount, out int quantity);
                var newTransaction = new TransactionQueueModel();

                if (status != TransactionStatus.Ignored)
                {
                    var isExceptionTransaction = SetExceptionTransaction(facility, formulary, facilityFormulary, storageSpaceInfo, request, item, quantity, newTransaction);

                    if (!isExceptionTransaction)
                    {
                        SetValidTransaction(storageSpaceInfo.Id, newTransaction, status);
                    }
                }

                partNo += 1;
                SetTransactionDetails(priority, formulary, facility, facilityFormulary, destination, request, item, partNo, orderCount, newTransaction, quantity);

                //ADU Processing variables
                Tuple<bool, bool> admResult = default(Tuple<bool, bool>);
                bool isIgnoredTxn = false, isPartialUpdated = false;

                if (newTransaction.Status != TransactionStatus.Exception
                    && status != TransactionStatus.Ignored
                    && priority.IsAdu.GetValueOrDefault(false))
                {
                    admResult = await _aduTransactionManager.ProcessAduTransaction(request, newTransaction, item, priority, facility);
                    isIgnoredTxn = admResult.Item1;
                    isPartialUpdated = admResult.Item2;

                    //Logging for ADU Processing 
                    if (isIgnoredTxn)
                    {
                        _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.ProcessedAdmTransaction, request.RequestId));
                    }
                }

                ConfigureTimeToLive(priority, newTransaction);

                if (status != TransactionStatus.Ignored && !isIgnoredTxn && !isPartialUpdated)
                {
                    newTransaction.TransactionQueueId = await _transactionQueueRepository.CreateTransaction(newTransaction);
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.TransactionCreated, newTransaction.TransactionQueueId, newTransaction.IncomingRequestId));

                    if (newTransaction.Status == TransactionStatus.Interim)
                    {
                        PublishFormularyLocationRequest(newTransaction, headers);
                    }
                }
                else if (!isPartialUpdated)
                {
                    newTransaction.Status = TransactionStatus.Ignored;
                    newTransaction.TransactionQueueId = await _transactionQueueHistoryRepository.CreateTransaction(newTransaction);
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.IgnoredTransactionCreated, newTransaction.TransactionQueueId, newTransaction.IncomingRequestId));
                }
            }
        }

        /// <summary>
        /// This method is used to update formulary location against transactionQueueId in DB.
        /// </summary>
        /// <param name="transactionQueueId">Update Storage location against this TransactionQueueId</param>
        /// <param name="devices">Storage location to be updated</param>
        /// <returns></returns>
        public async Task<bool> UpdateTransactionWithStorageDetails(string transactionQueueId, List<Device> devices)
        {
            _logger.LogInformation(String.Format(CommonConstants.LoggingMessage.UpdateTransactionWithStorageDetails, transactionQueueId));
            var location = GetLocationInfo(devices.Where(x => x.Type == StorageSpaceType.Carousel.ToString()).SingleOrDefault());
            return await _transactionQueueRepository.UpdateTransactionWithStorageDetails(transactionQueueId, TransactionStatus.Pending, location, devices);
        }

        public async Task<TransactionQueueModel> GetTransactionDetails(string transactionQueueId)
        {
            var result = await _transactionQueueRepository.GetTransactionDetails(transactionQueueId);
            if (result != null)
            {
                return _mapper.Map<TransactionQueueModel>(result);
            }
            return null;
        }
        #endregion

        #region Private Methods

        private void SetTransactionDetails(TransactionPriority priority, Formulary formulary, Facility facility,
            FacilityFormulary facilityFormulary, Destination destination, TransactionRequest incomingRequest,
            Item item, int partNo, int orderCount, TransactionQueueModel newTransaction, int quantity)
        {

            var localNow = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            User usr = new User(UserName.Admin.ToString());

            newTransaction.StatusChangeDT = localNow;
            newTransaction.StatusChangeUtcDateTime = utcNow;

            if (formulary != null)
            {
                newTransaction.Description = (priority != null && priority.UseInterfaceItemName.GetValueOrDefault()) ? item.ItemName : formulary.Description;
                newTransaction.FormularyId = formulary.FormularyId;
            }
            // Apply ADU Round if Qualifies
            if (quantity > 0 && facility != null && priority != null && destination != null
                && facilityFormulary != null && (priority.IsAdu != null && (bool)priority.IsAdu)
                && (facility.AduQtyRounding != null && (bool)facility.AduQtyRounding)
                && (facilityFormulary.AduQtyRounding != null && (bool)facilityFormulary.AduQtyRounding)
                && (destination.AduQtyRounding != null && (bool)destination.AduQtyRounding))
            {
                newTransaction.Quantity = AduQtyRound(quantity);
                newTransaction.QuantityProcessed = newTransaction.Quantity;
                _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.AduQuantity, newTransaction.Quantity, incomingRequest.RequestId));
            }
            else
            {
                newTransaction.Quantity = quantity;
                newTransaction.QuantityProcessed = newTransaction.Quantity;
            }

            newTransaction.Type = TransactionType.Pick;
            newTransaction.OrderId = incomingRequest.Order?.OrderNo;
            newTransaction.ItemId = item.ItemId;

            if (!string.IsNullOrEmpty(item.ComponentStrength))
                newTransaction.Strength = item.ComponentStrength.Trim();

            if (!string.IsNullOrEmpty(item.ComponentStrengthUnits))
                newTransaction.StrengthUnit = item.ComponentStrengthUnits.Trim();

            //set these two to real values for display purposes
            newTransaction.ComponentNumber = partNo;
            newTransaction.NumberOfComponents = orderCount;

            // Patient info
            SetPatientInfo(newTransaction, incomingRequest);

            newTransaction.FacilityId = facility.Id;
            newTransaction.IncomingRequestId = incomingRequest.RequestId;

            if (priority != null)
                newTransaction.TranPriorityId = priority.TransactionPriorityId;

            newTransaction.ReceivedDt = localNow;
            newTransaction.ReceivedUtcDateTime = utcNow;

            if (usr != null)
                newTransaction.RequestedBy = usr.UserName;

            if (!string.IsNullOrEmpty(incomingRequest.Patient?.DeliverToLocation))
                newTransaction.Destination = incomingRequest.Patient.DeliverToLocation.Trim();

            if (!string.IsNullOrEmpty(newTransaction.Exception)
                && newTransaction.Exception.Length > 80)
                newTransaction.Exception = newTransaction.Exception.Substring(0, 80);

            if (!string.IsNullOrEmpty(item.Concentration))
            {
                newTransaction.Concentration = item.Concentration.Trim();
            }
            if (!string.IsNullOrEmpty(item.TotalDose))
            {
                newTransaction.TotalDose = item.TotalDose.Trim();
            }
            if (!string.IsNullOrEmpty(item.DispenseAmount))
            {
                newTransaction.DispenseAmount = item.DispenseAmount.Trim();
            }
        }

        private bool SetExceptionTransaction(Facility facility, Formulary formulary, FacilityFormulary facilityFormulary, FacilityStorageSpace storageSpaceInfo, TransactionRequest request, Item item, int quantity, TransactionQueueModel newTransaction)
        {
            var isItemAccepted = (facilityFormulary != null && facilityFormulary.Approved) ? true : false;
            var isActiveFormulary = (formulary != null && !formulary.IsActive.GetValueOrDefault()) ? false : true;

            if (formulary == null)
            {
                newTransaction.Description = item.ItemName;
                SetTransactionException(newTransaction, CommonConstants.TransactionException.UnknownItemId);
                if (!int.TryParse(item.OrderAmount.Trim(), out quantity))
                {
                    newTransaction.Exception += Environment.NewLine + CommonConstants.TransactionException.InvalidQuantity;
                }
                newTransaction.Quantity = quantity;
                return true;
            }

            if (!isActiveFormulary)
            {
                SetTransactionException(newTransaction, CommonConstants.TransactionException.InactiveFormularyItem);
                return true;
            }

            if (facilityFormulary == null)
            {
                SetTransactionException(newTransaction, CommonConstants.TransactionException.FormularyNotMapped);
                return true;
            }

            if (!isItemAccepted)
            {
                SetTransactionException(newTransaction, CommonConstants.TransactionException.NotApprovedFormularyItem);
                return true;
            }

            if (quantity <= 0)
            {
                SetTransactionException(newTransaction, CommonConstants.TransactionException.InvalidUnitQuantity);
                return true;
            }

            if (storageSpaceInfo == null)
            {
                SetTransactionException(newTransaction, CommonConstants.TransactionException.UnassignedLocation);
                return true;
            }

            return false;
        }

        private void SetValidTransaction(int isaId, TransactionQueueModel newTransaction, TransactionStatus status)
        {
            newTransaction.IsaId = isaId;
            newTransaction.Status = status;
        }

        private void SetPatientInfo(TransactionQueueModel tranQNew, TransactionRequest request)
        {
            var item = request.Patient;
            if (!string.IsNullOrEmpty(item.LastName))
                tranQNew.PatientName = item.LastName;

            if (!string.IsNullOrEmpty(item.FirstName))
                tranQNew.PatientName += ", " + item.FirstName.Trim();

            if (!string.IsNullOrEmpty(item.MiddleName))
                tranQNew.PatientName += " " + item.MiddleName.Trim();

            if (!string.IsNullOrEmpty(item.AccountNumber))
                tranQNew.PatientAcctNumber = item.AccountNumber.Trim();

            if (!string.IsNullOrEmpty(item.Mrn))
                tranQNew.Mrn = item.Mrn.Trim();

            if (!string.IsNullOrEmpty(request.ADM?.StationName))
                tranQNew.PatientStation = request.ADM.StationName.Trim();

            if (!string.IsNullOrEmpty(item.Room))
                tranQNew.PatientRoom = item.Room.Trim();

            if (!string.IsNullOrEmpty(item.Bed))
                tranQNew.PatientBed = item.Bed.Trim();

            if (!string.IsNullOrEmpty(request.Order?.OrderingDrInstructions))
                tranQNew.Comments = request.Order.OrderingDrInstructions.Trim();
        }

        private void SetTransactionException(TransactionQueueModel request, string exceptionMessage)
        {
            _logger.LogInformation(exceptionMessage);
            request.Status = TransactionStatus.Exception;
            request.Exception = exceptionMessage;
        }

        private void PublishFormularyLocationRequest(TransactionQueueModel transactionQueue, Dictionary<string, string> headers)
        {
            var eventMessage = new FormularyLocationRequestEvent
            {
                TransactionQueueId = transactionQueue.TransactionQueueId,
                FormularyId = transactionQueue.FormularyId,
                FacilityId = transactionQueue.FacilityId,
                ISAId = transactionQueue.IsaId,
                Headers = headers
            };

            _eventBus.Publish(_configuration.KafkaFormularyLocationRequestTopic, eventMessage, eventMessage.Headers);
            _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.DataPublishedForFormularyLocation, JsonConvert.SerializeObject(eventMessage)));
        }

        private string GetLocationInfo(Device storageSpace)
        {
            if (storageSpace != null && storageSpace.StorageSpaces != null
                && storageSpace.StorageSpaces.Count > 0)
            {
                int tmpNum;
                StringBuilder locationSb = new StringBuilder();

                locationSb.Append(storageSpace.ShortDescription);
                var racks = storageSpace.StorageSpaces.Where(x => x.ItemType == StorageSpaceItemType.Rack);
                if (racks != null && racks.Count() > 0)
                {
                    var rack = racks.FirstOrDefault();
                    if (storageSpace.IsStatic || Convert.ToInt32(storageSpace.Attribute?.MaxRack) > 1
                    || rack.Number > 1)
                    {
                        tmpNum = rack.Number;
                        locationSb.Append("-" + tmpNum.ToString("00"));
                    }
                }

                var shelfs = storageSpace.StorageSpaces.Where(x => x.ItemType == StorageSpaceItemType.Shelf);
                if (shelfs != null && shelfs.Count() > 0)
                {
                    tmpNum = shelfs.FirstOrDefault().Number;
                    locationSb.Append("-" + tmpNum.ToString("00"));
                }

                var bins = storageSpace.StorageSpaces.Where(x => x.ItemType == StorageSpaceItemType.Bin);
                if (bins != null && bins.Count() > 0)
                {
                    tmpNum = bins.FirstOrDefault().Number;
                    locationSb.Append("-" + tmpNum.ToString("00"));
                }

                var slots = storageSpace.StorageSpaces.Where(x => x.ItemType == StorageSpaceItemType.Slot);
                if (slots != null && slots.Count() > 0)
                {
                    tmpNum = slots.FirstOrDefault().Number;
                    locationSb.Append("-" + tmpNum.ToString("00"));
                }
                return locationSb.ToString();
            }
            return null;
        }

        private static int AduQtyRound(int quantity)
        {
            var last = quantity.ToString(CultureInfo.InvariantCulture).Substring(quantity.ToString(CultureInfo.InvariantCulture).Length - 1, 1);

            if (quantity.ToString(CultureInfo.InvariantCulture).Length == 1)
            {
                quantity = quantity <= 6 ? 5 : 10;
            }
            else
            {
                if (int.Parse(last) < 3)
                {
                    while (int.Parse(last) != 0)
                    {
                        quantity -= 1;
                        last = quantity.ToString(CultureInfo.InvariantCulture).Substring(quantity.ToString(CultureInfo.InvariantCulture).Length - 1, 1);
                    }
                }
                else if (int.Parse(last) > 6)
                {
                    while (int.Parse(last) != 0)
                    {
                        quantity += 1;
                        last = quantity.ToString(CultureInfo.InvariantCulture).Substring(quantity.ToString(CultureInfo.InvariantCulture).Length - 1, 1);
                    }
                }
                else if (int.Parse(last) <= 6
                         && int.Parse(last) >= 5)
                {
                    while (int.Parse(last) != 5)
                    {
                        quantity -= 1;
                        last = quantity.ToString(CultureInfo.InvariantCulture).Substring(quantity.ToString(CultureInfo.InvariantCulture).Length - 1, 1);
                    }
                }
                else if (int.Parse(last) >= 3
                         && int.Parse(last) <= 5)
                {
                    while (int.Parse(last) != 5)
                    {
                        quantity += 1;
                        last = quantity.ToString(CultureInfo.InvariantCulture).Substring(quantity.ToString(CultureInfo.InvariantCulture).Length - 1, 1);
                    }
                }
            }

            if (quantity < 5)
                quantity = 5;

            return quantity;
        }

        private void ConfigureTimeToLive(TransactionPriority priority, TransactionQueueModel newTransaction)
        {
            if (priority.TransactionPriorityCode == Priority.STAT.ToString())
            {
                newTransaction.TimeToLive = Convert.ToInt32(configuration.GetValue<string>(CommonConstants.TimeToLive.Stat));
            }
            else if (newTransaction.Destination == TransactionType.BatchParent.ToString())
            {
                newTransaction.TimeToLive = Convert.ToInt32(configuration.GetValue<string>(CommonConstants.TimeToLive.BatchPicks));
            }
            else if (newTransaction.Type == TransactionType.Pick)
            {
                newTransaction.TimeToLive = Convert.ToInt32(configuration.GetValue<string>(CommonConstants.TimeToLive.Pick));
            }
            else if (newTransaction.Type == TransactionType.CycleCount)
            {
                newTransaction.TimeToLive = Convert.ToInt32(configuration.GetValue<string>(CommonConstants.TimeToLive.CycleCount));
            }
            else
            {
                newTransaction.TimeToLive = Convert.ToInt32(configuration.GetValue<string>(CommonConstants.TimeToLive.Other));
            }
        }

        #endregion
    }
}