using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.TransactionPriority.Abstractions
{
    public interface ISmartSortManager
    {
        Task<List<RequestResponseModel.SmartSortResponse>> GetAllSmartSort(bool isActive = true);
    }
}
