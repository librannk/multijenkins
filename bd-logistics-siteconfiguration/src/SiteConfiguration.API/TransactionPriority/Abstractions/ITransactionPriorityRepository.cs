using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace SiteConfiguration.API.TransactionPriority.Abstractions
{ 

    public interface ITransactionPriorityRepository : IRepository<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>
    {
        Task UpdatePriorityOrderUpAsync(int priorityOrder);
        Task UpdatePriorityOrderDownAsync(int priorityOrder, int currentPriority);
        Task<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>> GetAllTransactionPriorityAsync(bool isActive, int offset, int limit, Guid facilityId);
        Task<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>> GetAllSerachedTransactionPriorityAsync(string transactionPriorityDescription, int offset, int limit, Guid facilityId);
        Models.TransactionPriority GetByTransactionPriorityAndFacilityKey(Guid transactionpriorityId, Guid facilityId);

    }
}
