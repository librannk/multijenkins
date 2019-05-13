
using System.Linq;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Common.Constants;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.QueueHelper;
using TransactionQueue.ManageQueues.Infrastructure.DBEntity;

namespace TransactionQueue.ManageQueues.Business.Concrete
{
    public class ColumnSorting : ISortStrategy
    {
        private readonly IPriorityRules _priorityRules;
        private readonly ITransactionQueueRepository _transactionQueueRepository;
        public ColumnSorting(IPriorityRules priorityRules,
            ITransactionQueueRepository transactionQueueRepository)
        {
            _priorityRules = priorityRules;
            _transactionQueueRepository = transactionQueueRepository;
        }
        public SortedQueue GetSortedQueue(List<Models.TransactionQueue> pendingTransactions, string sortedColumnName, int sortedDirection, int page, int pageSize)
        {
            //fetch column name dynamically
            var ColumnName = typeof(TransactionQueueItems).GetProperty(sortedColumnName);
            //var ColumnNameBySortDir = SortColumns.GetDefaultColumn(sortedColumnName, sortedDirection);

            //instance to hold values of anonymous type
            var sortedQueue = new SortedQueue();

            //fetch data of priority rules
            var priorityRules = _priorityRules.GetPriorityRules();

            //fetch data by doing join
            var sortedList = (from pt in pendingTransactions
                              join pr in priorityRules.Result
                              on pt.TranPriorityId equals pr.TransactionPriorityId
                              //orderby
                              //pt.Status descending,
                              //pt.Type descending,
                              ////ColumnNameBySortDir,
                              //pt.ReceivedDT ascending

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

            if (sortedDirection == 1)
            {
                sortedList = sortedList.OrderByDescending(pt => pt.Status)
                              .ThenByDescending(pt => pt.Type)
                              .ThenByDescending(col => ColumnName.GetValue(col, null))
                              .ThenBy(pr => pr.ReceivedDT)
                              .ToList();
            }
            else
                sortedList = sortedList.OrderByDescending(pt => pt.Status)
                                             .ThenByDescending(pt => pt.Type)
                                             .ThenBy(col => ColumnName.GetValue(col, null))
                                             .ThenBy(pr => pr.ReceivedDT)
                                             .ToList();

            if (page >= 1 && pageSize >= 1)
            {
                sortedList = sortedList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            sortedQueue.QueueList = sortedList;
            return sortedQueue;
        }
    }
}