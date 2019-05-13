using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.Shared.Models;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.ManageQueues.Business.Concrete
{
    public class TransactionQueueBussiness : ITransactionQueueBussiness
    {
        private readonly ITransactionQueueRepository _transactionQueueRepository;
        public const string F10_Scan_code = "KB-F10";
        public TransactionQueueBussiness(ITransactionQueueRepository transactionQueueRepository)
        {
            _transactionQueueRepository = transactionQueueRepository;
        }
        /// <summary>
        /// This method is used to get all the pending transactions for selected ISAs
        /// </summary>
        /// <param name="isaIds"></param>
        /// <returns></returns>
        public async Task<Tuple<List<Business.Models.TransactionQueue>, bool>> GetTransactions(string activeTQId, List<int?> activeISA, string transactionType)
        {
            var transactions = await _transactionQueueRepository.GetTransactions(activeTQId, activeISA, transactionType);
            return transactions;
        }

        public async Task<List<int?>> GetActiveISA(int actorId)
        {
            var activeISA = await _transactionQueueRepository.GetActiveISA(actorId);

            return activeISA;
        }

        public Task<List<Business.Models.TransactionQueue>> GetHoldTransactions(List<int?> activeISA, string transactionType)
        {
            var holdTransactions = _transactionQueueRepository.GetHoldTransactions(activeISA, transactionType);
            return holdTransactions;
        }

        public async Task<BusinessResponse> PickNow(string activeTransactionQueueKey, int actorKey, PickNow pickNow, Dictionary<string, string> headers)
        {
            List<int?> isaIds = await GetIsaIds(actorKey);
            if (isaIds == null || !isaIds.Any())
            {
                return new BusinessResponse() { IsSuccess = false, Message = "No active Isa found.", StatusCode = 404 };
            }
            var tqStatus = await _transactionQueueRepository.GetTransactionStatus(activeTransactionQueueKey);
            if (tqStatus== TransactionQueueStatus.Pending.ToString())
                return new BusinessResponse() { IsSuccess = false, Message = "Transaction is no longer active.", StatusCode = 400 };

            BusinessResponse objBusinessResponse = await CheckForExistingTransaction(actorKey, pickNow, isaIds);
            if (objBusinessResponse.IsSuccess)
            {
                long updateResult = await UpdateTransactionQueueStatus(activeTransactionQueueKey, pickNow.TransactionQueueKeyToActivate, isaIds);
                return updateResult > 0 ? new BusinessResponse() { IsSuccess = true, Message = "Transaction is activated.", StatusCode = 200 } : new BusinessResponse() { IsSuccess = false, Message = "Transaction is not activated.", StatusCode = 404 };
            }
            return objBusinessResponse;

        }

        /// <summary>
        /// Activate the transaction status 
        /// </summary>
        /// <param name="activeTransactionQueueKey">active transaction key</param>
        /// <param name="transactionQueueKeyToActivate">transaction key to activate</param>
        /// <param name="isaIds">Active ISA Ids</param>
        /// <returns></returns>

        private async Task<long> UpdateTransactionQueueStatus(string activeTransactionQueueKey, string transactionQueueKeyToActivate, List<int?> isaIds)
        {
            return await _transactionQueueRepository.UpdateTransactionQueueStatus(activeTransactionQueueKey, transactionQueueKeyToActivate, isaIds);
        }

        /// <summary>
        /// Get ISA Ids
        /// </summary>
        /// <param name="actorKey">actor key</param>
        /// <returns></returns>
        private async Task<List<int?>> GetIsaIds(int actorKey)
        {
            return (await _transactionQueueRepository.GetActiveISA(actorKey));
        }

        /// <summary>
        /// Check transaction is exist or not
        /// </summary>
        /// <param name="actorKey">actorKey</param>
        /// <param name="pickNow">pickNow object </param>
        /// <param name="isaIds">ISA Ids</param>
        /// <returns></returns>
        private async Task<BusinessResponse> CheckForExistingTransaction(int actorKey, PickNow pickNow, List<int?> isaIds)
        {

            var pendingTransactions = await _transactionQueueRepository.GetPendingTransactions(isaIds);
            var selectedItem = pendingTransactions?.FirstOrDefault(item => item.Id == pickNow.TransactionQueueKeyToActivate);

            if (selectedItem == null)
            {
                return new BusinessResponse { IsSuccess = false, Message = "This item has been deleted by another device", StatusCode = 404 };

            }

            return new BusinessResponse { IsSuccess = true };
        }

        public BusinessResponse MarkCompleteTransaction(string activeTransactionQueueKey,int actorKey, TQRequestObjectForComplete tQRequestObjectForComplete)
        {
            bool f10Override = false;
            var message = new BusinessResponse();
            if(string.IsNullOrEmpty(activeTransactionQueueKey.Trim()))
            {
                message = new BusinessResponse() { IsSuccess = false, Message = "Please provide active transaction queue key." };
                return message;
            }
            if (actorKey <= 0)
            {
                message = new BusinessResponse() { IsSuccess = false, Message = "Please provide valid actorKey." };
                return message;
            }
            if (tQRequestObjectForComplete == null)
            {                
                message = new BusinessResponse() { IsSuccess = false, Message = "Model is not Valid." };
                return message;
            }
            var chkTransactionIsActive = _transactionQueueRepository.CheckTransactionIsActive(activeTransactionQueueKey);
            if (chkTransactionIsActive == null)
            {
                message = new BusinessResponse() { IsSuccess = false, Message = "Given transaction is no longer active." };
                return message;
            }

            var activeISA = this.GetActiveISA(actorKey).GetAwaiter().GetResult();
            if (activeISA.Count > 0)
            {
                if (!activeISA.Contains(chkTransactionIsActive.IsaId))
                {
                    message = new BusinessResponse() { IsSuccess = false, Message = "Active ISA is no longer controlled by current user." };
                    return message;
                }
            }
            else
            {
                message = new BusinessResponse() { IsSuccess = false, Message = "No active ISA found for current user" };
                return message;
            }

            if (string.IsNullOrEmpty(tQRequestObjectForComplete.scanCode))
            {
                message = new BusinessResponse() { IsSuccess = false, Message = "NO SCAN VALUE PROVIDED" };
                return message;
            }
            if(tQRequestObjectForComplete.scanCode.ToLower() == F10_Scan_code.ToString().ToLower())
            {
                f10Override = true;
                // To Do check current user is authrorize for overriding the scan process
            }
            if (string.IsNullOrEmpty(tQRequestObjectForComplete.workFlowStep.ToString()))
            {
                
                   var statusUpdate = _transactionQueueRepository.UpdateTQStatus(activeTransactionQueueKey,
                       TransactionQueueStatus.Active.ToString(), TransactionQueueStatus.Pending.ToString());
            }
            
            // Active ISA Check TO DO

            if (tQRequestObjectForComplete.tqType.ToString().ToLower() == TransactionQueueType.Pick.ToString().ToLower())
            {
                if(tQRequestObjectForComplete.workFlowStep.ToString().ToLower()== WorkFlowStep.PICK_BIN.ToString().ToLower())
                {
                    // TO DO Actual Scan BIN
                }

                if (tQRequestObjectForComplete.workFlowStep.ToString().ToLower() == WorkFlowStep.PICK_ITEM.ToString().ToLower())
                {
                    // TO DO Actual Scan ITEM
                }
                if (tQRequestObjectForComplete.workFlowStep.ToString().ToLower() == WorkFlowStep.PICK_LABEL.ToString().ToLower())
                {
                    //To DO check Authorization
                    // TO Do Verify Quantity
                    if (f10Override)
                    {
                        var result = _transactionQueueRepository.MarkComplete(activeTransactionQueueKey);
                        if (result)
                        {
                            message = new BusinessResponse() { IsSuccess = true, Message = "Transaction Completed" };
                            return message;

                        }
                        else
                        {
                            message = new BusinessResponse() { IsSuccess = false, Message = "Transaction Not Completed" };
                            return message;

                        }
                    }
                }
                // TO DO Contion for other use cases.
            }
            message = new BusinessResponse() { IsSuccess = false, Message = "No condition meet" };

            return message;
        }
    }
}
