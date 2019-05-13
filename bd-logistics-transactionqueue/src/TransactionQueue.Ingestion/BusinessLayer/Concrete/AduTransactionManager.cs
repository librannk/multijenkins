using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models.Enums;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Ingestion.BusinessLayer.Repository;
using TransactionQueue.Ingestion.Common.Constants;
using TransactionQueue.Ingestion.Infrastructure.DBModel;
using TransactionQueue.Shared.Common.Helpers;
using AutoMapper;

namespace TransactionQueue.Ingestion.BusinessLayer.Abstraction
{
    /// <summary>
    /// This class is responsible for handling AduTransaction related operations.
    /// </summary>
    public class AduTransactionManager : IAduTransactionManager
    {
        #region Private Fields
        private readonly ITransactionQueueRepository _transactionQueueRepository;
        private readonly ITransactionPriorityManager _transactionPriorityManager;
        private readonly ILastAduXrefRepository _lastAduXrefRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AduTransactionManager> _logger;
        #endregion

        #region Constructors
        /// <summary> Initialize the private fields </summary>
        public AduTransactionManager(ITransactionQueueRepository transactionQueueRepository,
            ILogger<AduTransactionManager> logger,
            IMapper mapper,
            ITransactionPriorityManager transactionPriorityManager,
            ILastAduXrefRepository lastAduXrefRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _transactionQueueRepository = transactionQueueRepository;
            _transactionPriorityManager = transactionPriorityManager;
            _lastAduXrefRepository = lastAduXrefRepository;
        }

        #endregion

        /// <summary>
        /// This method is used for processing Adu transaction
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transactionQueueModel"></param>
        /// <param name="item"></param>
        /// <param name="priority"></param>
        /// <param name="facility"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, bool>> ProcessAduTransaction(TransactionRequest request, TransactionQueueModel transactionQueueModel, Item item,
            TransactionPriority priority, ExternalDependencies.BusinessLayer.Models.Facility facility)
        {
            var result = new Tuple<bool, bool>(false,false);

            //PyxisLoad duplicate check
            if (await IsPyxisLoadTransactionExists(request.Patient.DeliverToLocation, facility.Id,
                transactionQueueModel.FormularyId))
            {
                _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.PyxisLoadTransactionExists, request.RequestId));

                //Not updated any existing txn and current txn should be ignored
                result = new Tuple<bool, bool>(true, false);
                return result;
            }

            //If the current transaction is not of type PYXISLOAD
            if (priority.IsAdu.GetValueOrDefault(false) && !StringHelper.IsEqual(priority.TransactionPriorityCode, Priority.PYXISLOAD.ToString()))
            {
                //Refill duplicate check
                if (StringHelper.IsEqual(priority.TransactionPriorityCode, Priority.PYXISREFILL.ToString()) &&
                    await IsPyxisRefillTransactionExists(request.Patient.DeliverToLocation, facility.Id,
                        transactionQueueModel.FormularyId))
                {
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.PyxisRefillTransactionExists, request.RequestId));

                    //Not updated any existing txn and current txn should be ignored
                    result = new Tuple<bool, bool>(true, false);
                    return result;
                }

                //Get the critlow, stockout or stkout transaction from Database
                var tranQClSo = await GetCriticalLowOrStockOutTransaction(request, item);

                //If the current transaction is refill
                if (StringHelper.IsEqual(priority.TransactionPriorityCode, Priority.PYXISREFILL.ToString()) && tranQClSo != null)
                {
                    result = await UpdateQuantityFromReplenishmentOrder(transactionQueueModel.Quantity.Value, tranQClSo);
                    return result;
                }

                //If the current transaction is critlow, stockout or stkout
                if (priority != null && IsCriticalLowOrStockoutPriority(priority.TransactionPriorityCode))
                {
                    //Get the refill transaction from Database
                    var tranQRefillDetail = await GetRefillTransaction(request, item);

                    //If there is not transation of type Refill present in Database
                    if (tranQRefillDetail != null)
                    {
                        await ConvertReplenishmentToCriticalLowOrStockOut(tranQRefillDetail, request, priority, transactionQueueModel.Quantity.Value);
                        _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.UpdatedQuantityPriorityAndRequestIdForAduTransaction, transactionQueueModel.Quantity.Value, priority.TransactionPriorityCode, request.RequestId));

                        //Updated one existing txn
                        result = new Tuple<bool, bool>(true, true);
                        return result;
                    }

                    if (tranQClSo != null)
                    {
                        var priorityResult = await _transactionPriorityManager.GetTransactionPriority(tranQClSo.TranPriorityId);

                        //Case when Critical Low exists in DB
                        if (IsCriticalLowOrStockoutPriority(priority.TransactionPriorityCode)
                            && StringHelper.IsEqual(priorityResult.TransactionPriorityCode, Priority.PYXISCRITLOW.ToString()))
                        {
                            tranQClSo.Quantity = transactionQueueModel.Quantity.Value;
                            tranQClSo.QuantityProcessed = tranQClSo.Quantity;

                            tranQClSo.TranPriorityId = priority.TransactionPriorityId;
                            tranQClSo.StatusChangeDT = DateTime.Now;
                            tranQClSo.StatusChangeUtcDateTime = DateTime.UtcNow;
                            tranQClSo.IncomingRequestId = request.RequestId;
                            await _transactionQueueRepository.UpdateTransaction(tranQClSo);

                            //Updated one existing txn
                            result = new Tuple<bool, bool>(true, true);
                        }
                        //Case when Stockout Low exists in DB
                        else if (IsCriticalLowOrStockoutPriority(priority.TransactionPriorityCode)
                                 && StringHelper.IsEqual(priorityResult.TransactionPriorityCode,
                                     Priority.PYXISSTOCKOUT.ToString()))
                        {
                            tranQClSo.Quantity = transactionQueueModel.Quantity.Value;
                            tranQClSo.QuantityProcessed = tranQClSo.Quantity;

                            tranQClSo.StatusChangeDT = DateTime.Now;
                            tranQClSo.StatusChangeUtcDateTime = DateTime.UtcNow;
                            tranQClSo.IncomingRequestId = request.RequestId;
                            await _transactionQueueRepository.UpdateTransaction(tranQClSo);

                            //Updated one existing txn
                            result = new Tuple<bool, bool>(true, true);
                        }
                        else
                        {
                            //Not updated any existing txn and current txn should be ignored
                            result = new Tuple<bool, bool>(true, false);
                        }

                        return result;
                    }

                    return await EligibleforAduDupDelete(transactionQueueModel, facility, request);
                }
            }

            return result;
        }

        private async Task<bool> IsPyxisRefillTransactionExists(string deliverToLocation, int facilityId, int formularyId)
        {
            TransactionPriority priority = await _transactionPriorityManager.GetTransactionPriority(Priority.PYXISREFILL, facilityId);
            if (priority != null)
            {
                var transactionQueueModels = await _transactionQueueRepository.GetAllTransactions(tq =>
                {
                    if ((tq.Status == TransactionStatus.Pending.ToString()
                         || tq.Status == TransactionStatus.Hold.ToString())
                        && tq.Destination == deliverToLocation
                        && tq.FormularyId == formularyId
                        && tq.FacilityId == facilityId
                        && tq.TranPriorityId == priority.TransactionPriorityId)
                        return true;
                    else
                        return false;
                });

                if (transactionQueueModels != null && transactionQueueModels.Any())
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> IsPyxisLoadTransactionExists(string deliverToLocation, int facilityId, int formularyId)
        {
            TransactionPriority priority = await _transactionPriorityManager.GetTransactionPriority(Priority.PYXISLOAD, facilityId);
            if (priority != null)
            {
                var transactionQueueModels = await _transactionQueueRepository.GetAllTransactions(tq =>
                {
                    if ((tq.Status == TransactionStatus.Pending.ToString()
                         || tq.Status == TransactionStatus.Hold.ToString())
                        && tq.Destination == deliverToLocation
                        && tq.FormularyId == formularyId
                        && tq.FacilityId == facilityId
                        && tq.TranPriorityId == priority.TransactionPriorityId)
                        return true;
                    else
                        return false;
                });

                if (transactionQueueModels != null && transactionQueueModels.Any())
                {
                    return true;
                }
            }

            return false;
        }

        private async Task ConvertReplenishmentToCriticalLowOrStockOut(TransactionQueueModel tranQRefillDetail,
            TransactionRequest request, TransactionPriority priority, int quantity)
        {
            tranQRefillDetail.TranPriorityId = priority.TransactionPriorityId;
            tranQRefillDetail.Type = TransactionType.Pick;
            tranQRefillDetail.Quantity = quantity;
            tranQRefillDetail.QuantityProcessed = quantity;

            tranQRefillDetail.StatusChangeDT = DateTime.Now;
            tranQRefillDetail.StatusChangeUtcDateTime = DateTime.UtcNow;
            tranQRefillDetail.IncomingRequestId = request.RequestId;
            await _transactionQueueRepository.UpdateTransaction(tranQRefillDetail);
        }

        private async Task<Tuple<bool,bool>> UpdateQuantityFromReplenishmentOrder(int quantity, TransactionQueueModel tranQClSo)
        {
            var priority = await _transactionPriorityManager.GetTransactionPriority(tranQClSo.TranPriorityId);
            if (StringHelper.IsEqual(priority.TransactionPriorityCode, Priority.PYXISCRITLOW.ToString()))
            {
                _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.UpdatedQuantityForReplenishmentOrder, quantity, tranQClSo.TransactionQueueId));
                tranQClSo.Quantity = quantity;
                tranQClSo.QuantityProcessed = quantity;

                tranQClSo.StatusChangeDT = DateTime.Now;
                tranQClSo.StatusChangeUtcDateTime = DateTime.UtcNow;
                await _transactionQueueRepository.UpdateTransaction(tranQClSo);

                //Updated one existing txn
                return new Tuple<bool, bool>(true, true);
            }
            else
            {
                //Not updated any existing txn and current txn should be ignored
                return new Tuple<bool, bool>(true, false);
            }            
        }

        /// <summary>
        /// Get CriticalLow or StockOut Transactions
        /// </summary>
        /// <param name="request"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task<TransactionQueueModel> GetCriticalLowOrStockOutTransaction(TransactionRequest request, Item item)
        {
            List<Infrastructure.DBModel.TransactionQueue> transactionQueueModels = await _transactionQueueRepository.GetAllTransactions();
            TransactionQueueModel tranQClSo = null;

            if (transactionQueueModels != null && transactionQueueModels.Any())
            {
                var result = (from tq in transactionQueueModels
                              where (tq.Status == TransactionStatus.Pending.ToString()
                                     || tq.Status == TransactionStatus.Hold.ToString())
                                    && tq.Destination == request.Patient.DeliverToLocation
                                    && tq.ItemId == item.ItemId
                                    && tq.FacilityId == request.Facility.FacilityId
                              select tq);

                if (result != null && result.Any())
                {
                    var lstTransactionQueueModel = new List<TransactionQueueModel>();

                    foreach (var transaction in result)
                    {
                        TransactionPriority priority =
                            await _transactionPriorityManager.GetTransactionPriority(transaction.TranPriorityId);
                        if (priority != null && priority.IsAdu.GetValueOrDefault(false) &&
                            IsCriticalLowOrStockoutPriority(priority.TransactionPriorityCode))
                        {
                            lstTransactionQueueModel.Add(_mapper.Map<TransactionQueueModel>(transaction));
                        }
                    }

                    if (lstTransactionQueueModel.Any())
                    {
                        tranQClSo = (from tq in lstTransactionQueueModel
                                     orderby tq.ReceivedDt descending
                                     select tq).FirstOrDefault();
                    }
                }
            }

            return tranQClSo;
        }

        /// <summary>
        /// Get CriticalLow or StockOut Transactions
        /// </summary>
        /// <param name="request"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task<TransactionQueueModel> GetRefillTransaction(TransactionRequest request, Item item)
        {
            List<Infrastructure.DBModel.TransactionQueue> transactionQueueModels = await _transactionQueueRepository.GetAllTransactions();
            TransactionQueueModel tranQClSo = null;

            if (transactionQueueModels != null && transactionQueueModels.Any())
            {
                var result = (from tq in transactionQueueModels
                              where (tq.Status == TransactionStatus.Pending.ToString()
                                    || tq.Status == TransactionStatus.Hold.ToString())
                                    && (tq.Type == TransactionType.Batch.ToString()
                                    || (tq.Type == TransactionType.Pick.ToString() && tq.Destination != CommonConstants.Destination.BatchPick))
                                    && tq.Destination == request.Patient.DeliverToLocation
                                    && tq.ItemId == item.ItemId
                                    && tq.FacilityId == request.Facility.FacilityId
                              select tq);

                if (result != null && result.Any())
                {
                    var lstTransactionQueueModel = new List<TransactionQueueModel>();

                    foreach (var transaction in result)
                    {
                        TransactionPriority priority =
                            await _transactionPriorityManager.GetTransactionPriority(transaction.TranPriorityId);
                        if (priority != null && priority.IsAdu.GetValueOrDefault(false) &&
                            StringHelper.IsEqual(priority.TransactionPriorityCode, Priority.PYXISREFILL.ToString()))
                        {
                            lstTransactionQueueModel.Add(_mapper.Map<TransactionQueueModel>(transaction));
                        }
                    }

                    if (lstTransactionQueueModel.Any())
                    {
                        tranQClSo = (from tq in lstTransactionQueueModel
                                     orderby tq.ReceivedDt descending
                                     select tq).FirstOrDefault();
                    }
                }
            }

            return tranQClSo;
        }

        private async Task<Tuple<bool,bool>> EligibleforAduDupDelete(TransactionQueueModel transactionQueueModel,
           ExternalDependencies.BusinessLayer.Models.Facility facility, TransactionRequest request)
        {
            var lastAduXrefs = await _lastAduXrefRepository.GetAllLastAduXrefTransactions();
            LastAduXref lastAduXref = lastAduXrefs?.FirstOrDefault(la => la.FacilityId == facility.Id
                                && la.FormularyId == transactionQueueModel.FormularyId
                                && la.Destination == request?.Patient?.DeliverToLocation);

            var transactions = await _transactionQueueRepository.GetAllTransactions();
            var AduItemAlreadyinQueue = transactions?.FirstOrDefault(tqa => tqa.FacilityId == facility.Id
                             && tqa.FormularyId == transactionQueueModel.FormularyId
                             && tqa.Destination == request?.Patient?.DeliverToLocation
                             && (tqa.Status == TransactionStatus.Pending.ToString() || tqa.Status == TransactionStatus.Hold.ToString()));

            bool eligibleforAduDupCheck = ((lastAduXref != null && lastAduXref.LastAduTransUtcDateTime != null)
                                        || (AduItemAlreadyinQueue != null && AduItemAlreadyinQueue.ReceivedUtcDateTime != null))
                                        && facility.AduDupeTimeDelay != null
                                        && facility.AduDupeTimeDelay > 0;

            if (eligibleforAduDupCheck)
            {
                bool eligibleforAduDupDelete =
                                (lastAduXref != null
                                    && ((DateTime)lastAduXref.LastAduTransUtcDateTime).AddMinutes((int)facility.AduDupeTimeDelay) > DateTime.UtcNow)
                                || (AduItemAlreadyinQueue != null
                                    && ((DateTime)AduItemAlreadyinQueue.ReceivedUtcDateTime).AddMinutes((int)facility.AduDupeTimeDelay) > DateTime.UtcNow);

                if (eligibleforAduDupDelete)
                {
                    _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.ProcessedAdmTransaction, request.RequestId));

                    //Not updated any existing txn and current txn should be ignored
                    return new Tuple<bool, bool>(true, false);
                }
            }

            //Normal processing
            //Not updated any existing txn and current txn should also not be ignored
            return new Tuple<bool, bool>(false, false);
        }

        private bool IsCriticalLowOrStockoutPriority(string priorityCode)
        {
            return StringHelper.IsEqual(priorityCode, Priority.PYXISCRITLOW.ToString())
                        || StringHelper.IsEqual(priorityCode, Priority.PYXISSTOCKOUT.ToString())
                        || StringHelper.IsEqual(priorityCode, Priority.PYXISSTKOUT.ToString());
        }
    }
}
