using System;
using System.Collections.Generic;
using System.Text;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ManageQueues.Business.Models
{
    public class Facility : Entity
    {
        public int FacilityId { get; set; }

        public List<StorageSpace> StorageSpaces { get; set; }

        public bool ProcessInactiveAsException { get; set; }

        public bool AduIgnoreCritLow { get; set; }

        public bool AduIgnoreStockout { get; set; }
    }
}
