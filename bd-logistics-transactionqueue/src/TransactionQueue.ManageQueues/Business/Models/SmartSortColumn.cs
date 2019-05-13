using System;

namespace TransactionQueue.ManageQueues.Business.Models
{
    public class SmartSortColumn
    {
        public string ColumnName { get; set; }

        public string FriendlyName { get; set; }

        public int CreatedBy { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public bool Active { get; set; }

        public byte[] LastModifiedBinaryValue { get; set; }

        public DateTime LastModifiedUTCDateTime { get; set; }

        public DateTime CreateUTCDateTime { get; set; }
    }
}
