using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.ManageQueues.Business.Models
{
    public class BusinessResponse
    {
        public bool IsSuccess { get; set; }
        public string  Message { get; set; }
        public int StatusCode { get; set; }
    }
}
