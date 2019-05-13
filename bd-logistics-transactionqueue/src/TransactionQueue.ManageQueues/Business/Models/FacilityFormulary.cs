using System;
using System.Collections.Generic;
using System.Text;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.ManageQueues.Business.Models
{
    public class Formulary : Entity
    {
        public int ItemId { get; set; }

        public int FormularyId { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public Facility FacilityFormulary { get; set; }
    }
}
