using System;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ManageQueues.Business.Models
{
    public class SmartSort
    {
        public int SmartSortOrder { get; set; }

        public string SmartSortColumn { get; set; }     

    }
}
