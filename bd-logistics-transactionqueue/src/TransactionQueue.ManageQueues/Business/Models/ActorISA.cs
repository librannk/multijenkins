using System;
using System.Collections.Generic;
using System.Text;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ManageQueues.Business.Models
{
    public class ActorISA : Entity
    {
        public int ActorId { get; set; }

        public List<ActiveISA> ActiveISA { get; set; }
    }

    public class ActiveISA
    {
        public int ISAId { get; set; }
    }
}
