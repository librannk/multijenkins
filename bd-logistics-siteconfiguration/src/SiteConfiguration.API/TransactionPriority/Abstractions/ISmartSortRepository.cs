using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.TransactionPriority.Models;


namespace SiteConfiguration.API.TransactionPriority.Abstractions
{
    public interface ISmartSortRepository : IRepository<SmartSortColumn>
    {
        Task<IEnumerable<SmartSortColumn>> GetAllSmartSortAsync(bool isActive);
    }
}
