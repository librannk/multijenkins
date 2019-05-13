using System;
using System.Collections.Generic;
using System.Text;
using TransactionQueue.Shared.Models.Enums;

namespace TransactionQueue.Shared.Models
{
    public class TQRequestObjectForComplete
    {
        public string scanCode { get; set; }
        
        public TransactionQueueType tqType { get; set; } 

        public WorkFlowStep? workFlowStep { get; set; }
    }
}
