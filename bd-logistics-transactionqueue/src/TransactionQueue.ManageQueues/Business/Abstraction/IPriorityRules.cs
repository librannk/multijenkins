
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.ManageQueues.Business.Models;

namespace TransactionQueue.ManageQueues.Business.Abstraction
{
    public interface IPriorityRules
    {
        Task<List<TransactionPriority>> GetPriorityRules();
    }
}