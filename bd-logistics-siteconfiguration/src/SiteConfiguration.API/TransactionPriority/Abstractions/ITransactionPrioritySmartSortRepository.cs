using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.TransactionPriority.Models;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;

namespace SiteConfiguration.API.TransactionPriority.Abstractions
{
    public  interface ITransactionPrioritySmartSortRepository : IRepository<SmartSort>
    {
        Task<IEnumerable<TransactionPrioritySmartSort>> GetSmartSortForTransactionPriorityAsync(Guid transPriorityKey);
        Task PutSmartSortForTransactionPriorityAsync(Guid transPriorityKey, List<SmartSort> listTransactionPrioritySmartSortPut);

        Task PostSmartSortForTransactionPriorityAsync(Guid transPriorityKey, List<SmartSort> listTransactionPrioritySmartSortPut);
        Task PutSmartSortForTransactionPriorityIdSmartSortIdAsync(Guid transPriorityKey, Guid smartSortId, SmartSort transactionPrioritySmartSortPut);
    }
}
