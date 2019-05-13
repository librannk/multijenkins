


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.ManageQueues.Business.Models;

namespace TransactionQueue.ManageQueues.UnitTest.TestData
{
    public class TestDataGeneratorTransaction
    {
        public List<TransactionQueue.ManageQueues.Business.Models.TransactionQueue> GetPendingTransaction()
        {
            List<TransactionQueue.ManageQueues.Business.Models.TransactionQueue> pendingTransaction = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
                new ManageQueues.Business.Models.TransactionQueue(){ Id= Guid.NewGuid().ToString(), Status="Pending", Type="Pick", Destination="Rack1", PatientName="John", Description="DO Check1", TranPriorityId=1 },
                new ManageQueues.Business.Models.TransactionQueue(){ Id= Guid.NewGuid().ToString(), Status="Pending", Type="Pick", Destination="Rack2", PatientName="Mark", Description="DO Check2", TranPriorityId= 2},
                new ManageQueues.Business.Models.TransactionQueue(){ Id= Guid.NewGuid().ToString(), Status="Pending", Type="Pick", Destination="Rack3", PatientName="Sasha", Description="DO Check3", TranPriorityId=3 },
                new ManageQueues.Business.Models.TransactionQueue(){ Id= Guid.NewGuid().ToString(), Status="Active", Type="Pick", Destination="Rack4", PatientName="Zest", Description="DO Check4", TranPriorityId=4 }
            };
            return pendingTransaction;
        }
        public List<TransactionQueue.ManageQueues.Business.Models.TransactionQueue> GetActiveTransaction()
        {
            List<TransactionQueue.ManageQueues.Business.Models.TransactionQueue> pendingTransaction = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
                new ManageQueues.Business.Models.TransactionQueue(){ Id= Guid.NewGuid().ToString(), Status="Active", Type="Pick", Destination="Rack4", PatientName="Zest", Description="DO Check4", TranPriorityId=4 }
            };
            return pendingTransaction;
        }
        public List<TransactionQueue.ManageQueues.Business.Models.TransactionQueue> GetHoldTransaction()
        {
            List<TransactionQueue.ManageQueues.Business.Models.TransactionQueue> pendingTransaction = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
                new ManageQueues.Business.Models.TransactionQueue(){ Id= Guid.NewGuid().ToString(), Status="Hold", Type="Pick", Destination="Rack1", PatientName="John", Description="DO Check1", TranPriorityId=1 },
                new ManageQueues.Business.Models.TransactionQueue(){ Id= Guid.NewGuid().ToString(), Status="Hold", Type="Pick", Destination="Rack2", PatientName="Mark", Description="DO Check2", TranPriorityId= 2}
            };
            return pendingTransaction;
        }
        public async Task<List<TransactionQueue.ManageQueues.Business.Models.TransactionPriority>> GetTransactionPriority()
        {
            List<TransactionQueue.ManageQueues.Business.Models.TransactionPriority> transactionPriorities = new List<ManageQueues.Business.Models.TransactionPriority>()
            {
                new ManageQueues.Business.Models.TransactionPriority(){ Id= Guid.NewGuid().ToString(), TransactionPriorityId=1, TransactionPriorityCode ="PICK", TransactionPriorityOrder=1, Color="red" },
                new ManageQueues.Business.Models.TransactionPriority(){ Id= Guid.NewGuid().ToString(), TransactionPriorityId=2, TransactionPriorityCode ="MANUALSTAT", TransactionPriorityOrder=2, Color="blue"},
                new ManageQueues.Business.Models.TransactionPriority(){ Id= Guid.NewGuid().ToString(), TransactionPriorityId=3, TransactionPriorityCode ="AUTOPICK", TransactionPriorityOrder=3, Color="green"},
                new ManageQueues.Business.Models.TransactionPriority(){ Id= Guid.NewGuid().ToString(), TransactionPriorityId=4, TransactionPriorityCode ="AUTOPICK", TransactionPriorityOrder=4, Color="Yellow", }
            };
            return transactionPriorities;
        }
    }
}
