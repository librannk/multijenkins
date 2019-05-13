
using System;
using System.Linq;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.QueueHelper;

namespace TransactionQueue.ManageQueues.Business.Concrete
{
    public class SmartSorting : ISortStrategy
    {
        private readonly IPriorityRules _priorityRules;
        private readonly ITransactionQueueRepository _transactionQueueRepository;
        public SmartSorting(IPriorityRules rules, ITransactionQueueRepository transactionQueue)
        {
            _priorityRules = rules;
            _transactionQueueRepository = transactionQueue;
        }
        public SortedQueue GetSortedQueue(List<Models.TransactionQueue> pendingTransactions, string sortedColumnName, int sortedDirection, int page, int pageSize)
        {
            //instance to hold values of anonymous type
            var sortedQueue = new SortedQueue();

            //fetch data of priority rules
            var priorityRules = _priorityRules.GetPriorityRules();

            //fetch values of smartsort Columns dynamically by priority rules, it can be more than 1 column
            //if smart sort columns are not defined set values of default column as TransactionPriorityOrder
            var listofSmartCol = SortColumns.GetSmartColumn(priorityRules.Result);

            //fetch data after making join pending transaction and priority rules 
            var result = (from pt in pendingTransactions
                              join pr in priorityRules.Result
                                  on pt.TranPriorityId equals pr.TransactionPriorityId
                          //orderby
                          //pt.Status descending,
                          //pt.Type descending,
                          //pr.TransactionPriorityOrder ascending,
                          ////listofSmartCol,
                          //pt.ReceivedDt ascending,
                          //pt.ComponentNumber ascending

                          select new TransactionQueueItems()
                              {
                                  Id = pt.Id,
                                  PriorityName = pr.TransactionPriorityCode,
                                  TransactionPriorityOrder = pr.TransactionPriorityOrder,
                                  Quantity = pt.Quantity,
                                  Item = pt.Description,
                                  Location = pt.Location,
                                  Destination = pt.Destination,
                                  PatientName = pt.PatientName,
                                  Description = pt.Description,
                                  Color = pr.Color,
                                  Status = pt.Status,
                                  Type = pt.Type,
                                  ReceivedDT = pt.ReceivedDt,
                                  ComponentNumber = pt.ComponentNumber,
                                  Rack = StorageSpace.GetStorageSpace(pt.Devices, Models.StorageSpaceItemType.Rack),
                                  Shelf = StorageSpace.GetStorageSpace(pt.Devices, Models.StorageSpaceItemType.Shelf),
                                  Bin = StorageSpace.GetStorageSpace(pt.Devices, Models.StorageSpaceItemType.Bin),
                                  Slot = StorageSpace.GetStorageSpace(pt.Devices, Models.StorageSpaceItemType.Slot)
                          }
                            )
                            .ToList();

            DoSmartSort smartSort = new DoSmartSort();
            var sortedList = smartSort.OrderBySmartSort(result, listofSmartCol);

            if (page >= 1 && pageSize >= 1)
            {
                sortedList = sortedList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            sortedQueue.QueueList = sortedList;
            return sortedQueue;
        }
    }
}
