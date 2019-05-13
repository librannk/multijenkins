using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.ManageQueues.Business.Models
{
    public class Computer
    {
        /// <summary> ComputerId </summary>
        public int ComputerId { get; set; }

        /// <summary> PrinterId </summary>
        public int PrinterId { get; set; }

        /// <summary> IsOutsideComputer </summary>
        public bool IsOutsideComputer { get; set; }
    }
}
