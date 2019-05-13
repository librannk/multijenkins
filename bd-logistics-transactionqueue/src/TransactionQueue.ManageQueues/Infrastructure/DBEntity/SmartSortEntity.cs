using System;

namespace TransactionQueue.ManageQueues.Infrastructure.DBEntity
{
    public class SmartSortEntity
    {
        public int TranPriorityId { get; set; }

        public int SmartSortColumnId { get; set; }

        public int SmartSortOrder { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public byte[] LastModifiedBinaryValue { get; set; }

        public DateTime LastModifiedUTCDateTime { get; set; }

        public SmartSortColumnEntity SmartSortColumn { get; set; }

    }
}
