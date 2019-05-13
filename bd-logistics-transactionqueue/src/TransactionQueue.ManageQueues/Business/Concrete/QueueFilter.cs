
using System;
using System.Linq;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.QueueHelper;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.ViewModel;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.ManageQueues.Business.Concrete
{
    public class QueueFilter : IQueueFilter
    {
        ISortStrategy colsortStrategy;
        ISortStrategy smartsortStrategy;
        private readonly IPriorityRules _priorityRules;
        private readonly ITransactionQueueRepository _transactionQueueRepository;
        public QueueFilter(IPriorityRules priorityRules, ITransactionQueueRepository transactionQueueRepository)
        {
            _priorityRules = priorityRules;
            _transactionQueueRepository = transactionQueueRepository;
        }
        public SortedQueue GetAllSortedTransaction(List<Models.TransactionQueue> pendingTransactions, string sortedColumnName, int sortedDirection, int page, int pageSize)
        {
            colsortStrategy = new ColumnSorting(_priorityRules, _transactionQueueRepository);
            smartsortStrategy = new SmartSorting(_priorityRules, _transactionQueueRepository);

            if (!string.IsNullOrEmpty(sortedColumnName))
            {
                return colsortStrategy.GetSortedQueue(pendingTransactions, sortedColumnName, sortedDirection, page, pageSize);
            }
            else
            {
                return smartsortStrategy.GetSortedQueue(pendingTransactions, sortedColumnName, sortedDirection, page, pageSize);
            }
        }

        public TransactionQueueStatusWiseResponse GetSortedTransaction(List<Models.TransactionQueue> activeAndPendingTransaction, bool isStaled, List<Models.TransactionQueue> holdTransaction, string sortedColumnName, int sortedDirection, int page, int pageSize)
        {
            bool IsRemoved = false;
            TransactionQueueStatusWiseResponse response = new TransactionQueueStatusWiseResponse() {
                ActiveTransaction = new TransactionQueueItems() { },
                LockedTransactions = new List<TransactionQueueItems>(),
                RestockReturnTransactions = new List<TransactionQueueItems>()};
            #region If there is AN Active Transaction

            var priorityRules = _priorityRules.GetPriorityRules();
            int? ComponentNumber=0; string PriorityCode=null; int PriorityOrder=0; DateTime? ReceivedDt=null; string Color=null; int Rack=0; int Shelf = 0; int Bin = 0; int Slot = 0;

            //Fetch frist transaction which is Active
            var activeTransaction = activeAndPendingTransaction.Where(x => x.Status == TransactionQueueStatus.Active.ToString()).FirstOrDefault();

            if (activeTransaction != null)
            {
                #region set already Active Transaction
                var activeList = activeAndPendingTransaction.Where(x => x.Id == activeTransaction.Id).ToList();
                // Rack = StorageSpace.GetStorageSpace(pt.Devices, Models.StorageSpaceItemType.Rack),
                var all = (from al in activeList
                           join pt in priorityRules.Result
                               on al.TranPriorityId equals pt.TransactionPriorityId
                           select new
                           {
                               al.TranPriorityId, 
                               pt.Color,
                               al.ReceivedDt,
                               al.ComponentNumber,
                               pt.TransactionPriorityCode,
                               pt.TransactionPriorityOrder,
                               al.Devices
                           }).ToList();

                foreach (var item in all)
                {
                    ComponentNumber = item.ComponentNumber;
                    PriorityCode = item.TransactionPriorityCode;
                    PriorityOrder = item.TransactionPriorityOrder;
                    ReceivedDt = item.ReceivedDt;
                    Color = item.Color;
                    Rack = StorageSpace.GetStorageSpace(item.Devices, Models.StorageSpaceItemType.Rack);
                    Shelf = StorageSpace.GetStorageSpace(item.Devices, Models.StorageSpaceItemType.Shelf);
                    Bin = StorageSpace.GetStorageSpace(item.Devices, Models.StorageSpaceItemType.Bin);
                    Slot = StorageSpace.GetStorageSpace(item.Devices, Models.StorageSpaceItemType.Slot);
                }

                response.ActiveTransaction.Id = activeTransaction.Id;
                response.ActiveTransaction.PriorityName = PriorityCode;
                response.ActiveTransaction.Quantity = activeTransaction.Quantity;
                response.ActiveTransaction.Item = activeTransaction.Description;
                response.ActiveTransaction.Location = activeTransaction.Location;
                response.ActiveTransaction.Destination = activeTransaction.Description;
                response.ActiveTransaction.PatientName = activeTransaction.PatientName;
                response.ActiveTransaction.Description = activeTransaction.Description;
                response.ActiveTransaction.Color = Color;
                response.ActiveTransaction.Status = activeTransaction.Status;
                response.ActiveTransaction.Type = activeTransaction.Type;
                response.ActiveTransaction.ReceivedDT = DateTime.Now;
                response.ActiveTransaction.TransactionPriorityOrder = 0;
                response.ActiveTransaction.ComponentNumber = ComponentNumber;
                response.ActiveTransaction.Rack = Rack;
                response.ActiveTransaction.Shelf = Shelf;
                response.ActiveTransaction.Bin = Bin;
                response.ActiveTransaction.Slot = Slot;

                #endregion set already Active Transaction

                //Remove Already Active Transaction from Pending
                IsRemoved = activeAndPendingTransaction.Remove(activeTransaction);
                if (IsRemoved)
                {
                    //Pending minus Hold
                    activeAndPendingTransaction.RemoveAll(x => holdTransaction.Any(y => y.Id == x.Id));

                     //Get All Pending Sorted Transaction
                     var allPendingSortedTransaction = GetAllSortedTransaction(activeAndPendingTransaction, sortedColumnName, sortedDirection, 0, 0);

                    //Assign All Pending into Pending Item List
                    if (page >= 1 && pageSize >= 1)
                    {
                        response.PendingTransactions = allPendingSortedTransaction.QueueList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    } else
                    {
                        response.PendingTransactions = allPendingSortedTransaction.QueueList;
                    }

                    //Get All Pending Sorted Transaction
                    var allHoldSortedTransaction = GetAllSortedTransaction(holdTransaction, sortedColumnName, sortedDirection, 0, 0);

                    //Assign All Hold into Hold Item List
                    if (page >= 1 && pageSize >= 1)
                    {
                        response.HoldTransactions = allHoldSortedTransaction.QueueList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    }
                    else
                    {
                        response.HoldTransactions = allHoldSortedTransaction.QueueList;
                    }
                  
                }
            }
            #endregion If there is AN Active Transaction

            #region If there is NO Active Transaction
            else
            {
                //Pending minus Hold
                activeAndPendingTransaction.RemoveAll(x => holdTransaction.Any(y => y.Id == x.Id));

                //Get All Pending Sorted Transaction
                var allPendingSortedTransaction = GetAllSortedTransaction(activeAndPendingTransaction, sortedColumnName, sortedDirection,0,0);

                //Fetch top 1 from Pending List which will be an ActiveTransaction
                var OnlyActivetransaction = allPendingSortedTransaction.QueueList.FirstOrDefault();

                if (OnlyActivetransaction != null)
                {
                    //Update top 1 from Pending Transaction into Active State into DB
                    _transactionQueueRepository.UpdateTransactionsAsync(OnlyActivetransaction.Id);

                    //Remove Active (top 1 from Pending) from pending List
                    IsRemoved = allPendingSortedTransaction.QueueList.Remove(OnlyActivetransaction);

                    if (IsRemoved)
                    {
                        //Assign Fetch top 1 from Pending Transaction into Active Item Object
                        OnlyActivetransaction.Status = TransactionQueueStatus.Active.ToString();
                        response.ActiveTransaction = OnlyActivetransaction;

                        //Assign All Pending into Pending Item List
                        if (page >= 1 && pageSize >= 1)
                        {
                            response.PendingTransactions = allPendingSortedTransaction.QueueList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                        }
                        else
                        {
                            response.PendingTransactions = allPendingSortedTransaction.QueueList;
                        }
                    }
                }
                //Get All Pending Sorted Transaction
                var allHoldSortedTransaction = GetAllSortedTransaction(holdTransaction, sortedColumnName, sortedDirection, 0, 0);

                //Assign All Hold into Hold Item List
                if (page >= 1 && pageSize >= 1)
                {
                    response.HoldTransactions = allHoldSortedTransaction.QueueList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    response.HoldTransactions = allHoldSortedTransaction.QueueList;
                }

            }
            #endregion If there is no Active Transaction

            #region If there is ANY Stale Transaction
            //Assign Stale flag if it is true
            response.IsStaled = isStaled;
            #endregion If there is Stale Transaction
            
            //return all response
            return response;
        }
    }
}


   