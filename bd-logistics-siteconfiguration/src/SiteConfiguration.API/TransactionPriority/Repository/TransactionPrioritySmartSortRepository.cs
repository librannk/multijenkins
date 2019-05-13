using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.Models;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;

namespace SiteConfiguration.API.TransactionPriority.Repository
{
    [ExcludeFromCodeCoverage]
    public class TransactionPrioritySmartSortRepository : BaseRepository<SmartSort>, ITransactionPrioritySmartSortRepository
    {

        public TransactionPrioritySmartSortRepository(Infrastructure.DataAccess.SQL.DBContextEntities.ApplicationDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<TransactionPrioritySmartSort>> GetSmartSortForTransactionPriorityAsync(Guid transPriorityKey)
        {
            return await DbSet?.Where(s => s.TransPriorityKey == transPriorityKey)?.Select(x => new TransactionPrioritySmartSort() { SmartSortColumnKey = x.SmartSortColumnKey.ToString(), SmartSortName = x.SmartSortColumnKeyNavigation.ColumnNameText, SmartSortOrder = x.SmartSortOrder })?.ToListAsync();
        }

        public async Task PutSmartSortForTransactionPriorityAsync(Guid transPriorityKey, List<SmartSort> listTransactionPrioritySmartSortPut)
        {
            DbSet.RemoveRange(DbSet.Where(t => t.TransPriorityKey == transPriorityKey));
            await DbSet.AddRangeAsync(listTransactionPrioritySmartSortPut);
        }


        public async Task PutSmartSortForTransactionPriorityIdSmartSortIdAsync(Guid transPriorityKey,Guid smartSortId,SmartSort transactionPrioritySmartSortPut)
        {
            var smartSort = DbSet?.Where(s => s.TransPriorityKey == transPriorityKey && s.SmartSortColumnKey == smartSortId).FirstOrDefault();
            smartSort.SmartSortOrder = transactionPrioritySmartSortPut.SmartSortOrder;
            smartSort.LastModifiedByActorKey = transactionPrioritySmartSortPut.LastModifiedByActorKey;
            smartSort.LastModifiedUTCDateTime = transactionPrioritySmartSortPut.LastModifiedUTCDateTime;
            DbSet.Update(smartSort);
            
        }

        public async Task PostSmartSortForTransactionPriorityAsync(Guid transPriorityKey, List<SmartSort> listTransactionPrioritySmartSortPut)
        {
            await DbSet.AddRangeAsync(listTransactionPrioritySmartSortPut);
        }
    }
}
