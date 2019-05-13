using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;

namespace SiteConfiguration.API.TransactionPriority.Abstractions
{
    public interface ITransactionPriorityManager
    {
        Task<BusinessResponse> AddTransactionPriority(TransactionPriorityPost transactionPriorityPost, Guid facilityID, Dictionary<string, string> headers);
        Models.TransactionPriority GetTransactionPriorityById(string id);
        Task<BusinessResponse> UpdateTransactionPriorityAsync(string id, TransactionPriorityPut transactionPriorityPut, Guid facilityID, Dictionary<string, string> headers);
        Task<IEnumerable<TransactionPriorityGet>> GetAllTransactionPriorityASync(int offset, int limit, bool isActive, string facilityID);
        Task<IEnumerable<TransactionPriorityGet>> GetAllSerachedTransactionPriorityAsync(string transactionPriorityDescription, int offset, int limit, string facilityID);
        Task<IEnumerable<TransactionPrioritySmartSort>> GetSmartSortForTransactionPriority(string transactionPriorityKey, string facilityID);
       Task<BusinessResponse> PutSmartSortForTransactionPriorityAsync(string transactionPriorityKey, string facilityID, List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut, Dictionary<string, string> headers);
        Task<BusinessResponse> PostSmartSortForTransactionPriorityAsync(string transactionPriorityKey, string facilityID, List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut, Dictionary<string, string> headers);
  
    }
}
