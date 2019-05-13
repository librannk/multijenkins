
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Business.Models;

namespace TransactionQueue.ManageQueues.UnitTest.TestData
{
    public class TestDataGeneratorQueueFilter
    {
        public List<ManageQueues.Business.Models.TransactionQueue> GetTransactionQueueEntityByColumn()
        {
            List<ManageQueues.Business.Models.TransactionQueue> transactionQueueItems = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "407849", Description = "AKEYS- PCA/EPIDURAL", TranPriorityId = 15, Type = "P", Status = "A", PatientName = "Bill", Location = "FallingRack1", Destination = "M-3W", ReceivedDt= DateTime.Now },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703804", Description = "CKHEPARIN 25,000 units/500mL", TranPriorityId = 17, Type = "P", Status = "A", PatientName = "Mark", Location = "RollingRack2", Destination = "A_CCSN", ReceivedDt=DateTime.Now.AddDays(+1) },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703763", Description = "BKEYS- PCA/EPIDURAL", TranPriorityId = 16, Type = "P", Status = "A", PatientName = "Joe", Location = "SillingRack3", Destination = "K_ED", ReceivedDt=DateTime.Now.AddDays(+2) }
            };
            return transactionQueueItems;
        }
        public async Task<List<TransactionPriority>> GetTransactionPriorityEntity()
        {
            List<TransactionPriority> transactionPriorityItems = new List<TransactionPriority>()
            {
            new TransactionPriority { TransactionPriorityId = 15, TransactionPriorityCode = "PYXISREFILL", TransactionPriorityName = "Pyxis Replenishment", TransactionPriorityOrder = 6, SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder = 1, SmartSortColumn ="Location" } },  },
            new TransactionPriority { TransactionPriorityId = 16, TransactionPriorityCode = "OMNIFILL-1", TransactionPriorityName = "OMNICELL REFILL", TransactionPriorityOrder = 7, SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder = 2, SmartSortColumn ="PatientName" } },  },
            new TransactionPriority { TransactionPriorityId = 17, TransactionPriorityCode = "OMNIFILL-2", TransactionPriorityName = "OMNICELL REFILL", TransactionPriorityOrder = 8 , SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder=3, SmartSortColumn="Destination"}  },  }
            };
            return transactionPriorityItems;
        }
    }
}
