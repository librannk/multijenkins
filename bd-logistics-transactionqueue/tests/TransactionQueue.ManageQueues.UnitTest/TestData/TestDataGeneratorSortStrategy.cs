
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.ManageQueues.Infrastructure.DBEntity;

namespace TransactionQueue.ManageQueues.UnitTest.TestData
{
    public class TestDataGeneratorSortStrategy
    {
        public List<ManageQueues.Business.Models.TransactionQueue> GetTransactionQueueEntity()
        {
            List<ManageQueues.Business.Models.TransactionQueue> transactionQueueItems = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "407849", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 15, Type = "A", Status = "P", PatientName = "Bill", Location = "FallingRack1", Destination = "M-3W", ReceivedDt= DateTime.Now },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703804", Description = "KHEPARIN 25,000 units/500mL", TranPriorityId = 17, Type = "P", Status = "A", PatientName = "Mark", Location = "RollingRack2", Destination = "A_CCSN", ReceivedDt=DateTime.Now.AddDays(+1) },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703763", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 16, Type = "R", Status = "H", PatientName = "Joe", Location = "SillingRack3", Destination = "K_ED", ReceivedDt=DateTime.Now.AddDays(+2) }
            };
            return transactionQueueItems;
        }
        public List<ManageQueues.Business.Models.TransactionQueue> GetTransactionQueueEntityBySmartColumn()
        {
            List<ManageQueues.Business.Models.TransactionQueue> transactionQueueItems = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "407849", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 15, Type = "P", Status = "A", PatientName = "Bill", Location = "FallingRack1", Destination = "M-3W", ReceivedDt= DateTime.Now },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703804", Description = "KHEPARIN 25,000 units/500mL", TranPriorityId = 17, Type = "P", Status = "A", PatientName = "Mark", Location = "RollingRack2", Destination = "A_CCSN", ReceivedDt=DateTime.Now.AddDays(+1) },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703763", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 16, Type = "P", Status = "A", PatientName = "Joe", Location = "SillingRack3", Destination = "K_ED", ReceivedDt=DateTime.Now.AddDays(+2) }
            };
            return transactionQueueItems;
        }
        public List<ManageQueues.Business.Models.TransactionQueue> GetTransactionQueueEntitybyPatient()
        {
            List<ManageQueues.Business.Models.TransactionQueue> transactionQueueItems = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "407849", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 15, Type = "P", Status = "P", PatientName = "Bill", Location = "Falling Rack-08-03-01-01", Destination = "M-3W", ReceivedDt= DateTime.Now },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703804", Description = "KHEPARIN 25,000 units/500mL", TranPriorityId = 17, Type = "P", Status = "P", PatientName = "Mark", Location = "Rolling Rack-08-03-01-01", Destination = "MMM_CCSN", ReceivedDt=DateTime.Now.AddDays(+1) },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703763", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 16, Type = "P", Status = "P", PatientName = "Joe", Location = "Carousel Rack-08-03-01-01", Destination = "MMM_ED", ReceivedDt=DateTime.Now.AddDays(+2) }
            };
            return transactionQueueItems;
        }
        public List<ManageQueues.Business.Models.TransactionQueue> GetTransactionQueueEntitybyReceivedDT()
        {
            List<ManageQueues.Business.Models.TransactionQueue> transactionQueueItems = new List<ManageQueues.Business.Models.TransactionQueue>()
            {
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "407849", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 15, Type = "P", Status = "P", PatientName = "B", Location = "Falling Rack-08-03-01-01", Destination = "M-3W", ReceivedDt= DateTime.Today },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703804", Description = "KHEPARIN 25,000 units/500mL", TranPriorityId = 17, Type = "P", Status = "P", PatientName = "B", Location = "Rolling Rack-08-03-01-01", Destination = "MMM_CCSN", ReceivedDt=DateTime.Today.AddDays(+1) },
            new ManageQueues.Business.Models.TransactionQueue { IncomingRequestId = "703763", Description = "KEYS- PCA/EPIDURAL", TranPriorityId = 16, Type = "P", Status = "P", PatientName = "B", Location = "Carousel Rack-08-03-01-01", Destination = "MMM_ED", ReceivedDt=DateTime.Today.AddDays(+2) }
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
        public async Task<List<TransactionPriority>> GetTransactionPriorityEntityBySmartColumn()
        {
            List<TransactionPriority> transactionPriorityItems = new List<TransactionPriority>()
            {
            new TransactionPriority { TransactionPriorityId = 15, TransactionPriorityCode = "PYXISREFILL", TransactionPriorityName = "Pyxis Replenishment", TransactionPriorityOrder = 6, SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder = 1, SmartSortColumn ="Location" } },  },
            new TransactionPriority { TransactionPriorityId = 16, TransactionPriorityCode = "OMNIFILL-1", TransactionPriorityName = "OMNICELL REFILL", TransactionPriorityOrder = 7, SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder = 2, SmartSortColumn ="PatientName" } },  },
            new TransactionPriority { TransactionPriorityId = 17, TransactionPriorityCode = "OMNIFILL-2", TransactionPriorityName = "OMNICELL REFILL", TransactionPriorityOrder = 8 , SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder=3, SmartSortColumn="Destination"}  },  }
            };
            return transactionPriorityItems;
        }
        public async Task<List<TransactionPriority>> GetTransactionPriorityEntityByPatientName()
        {
            List<TransactionPriority> transactionPriorityItems = new List<TransactionPriority>()
            {
            new TransactionPriority { TransactionPriorityId = 15, TransactionPriorityCode = "PYXISREFILL", TransactionPriorityName = "Pyxis Replenishment", TransactionPriorityOrder = 6, SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder = 1, SmartSortColumn ="Location" } },  },
            new TransactionPriority { TransactionPriorityId = 16, TransactionPriorityCode = "OMNIFILL-1", TransactionPriorityName = "OMNICELL REFILL", TransactionPriorityOrder = 7, SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder = 2, SmartSortColumn ="PatientName" } },  },
            new TransactionPriority { TransactionPriorityId = 17, TransactionPriorityCode = "OMNIFILL-2", TransactionPriorityName = "OMNICELL REFILL", TransactionPriorityOrder = 8 , SmartSortData = new List<SmartSort>(){ new SmartSort() {SmartSortOrder=3, SmartSortColumn="Destination"}  },  }
            };
            return transactionPriorityItems;
        }
        public async Task<List<TransactionPriority>> GetTransactionPriorityEntityByDestination()
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
