using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.Models;


namespace SiteConfiguration.API.TransactionPriority.Repository
{
    [ExcludeFromCodeCoverage]
    public class TransactionPriorityRepository : BaseRepository<Models.TransactionPriority>, ITransactionPriorityRepository
    {

        public TransactionPriorityRepository(Infrastructure.DataAccess.SQL.DBContextEntities.ApplicationDbContext context) : base(context)
        {
        }


        public async Task UpdatePriorityOrderUpAsync(int priorityOrder)
        {
            DbSet.Where(p => p.PriorityOrder >= priorityOrder)?.ToList()?.ForEach(p => p.PriorityOrder = p.PriorityOrder + 1);

        }

        public async Task UpdatePriorityOrderDownAsync(int priorityOrder, int currentPriority)
        {
            DbSet.Where(p => p.PriorityOrder <= priorityOrder && p.PriorityOrder >= currentPriority)?.ToList()?.ForEach(p => p.PriorityOrder = p.PriorityOrder - 1);

        }



        public async Task<IEnumerable<Models.TransactionPriority>> GetAllTransactionPriorityAsync(bool isActive, int offset, int limit, Guid facilitykey)
        {

            if (offset == 0 && limit == 0)
            {
                return await DbSet.Include(e => e.SmartSort)
                     .ThenInclude(e => e.SmartSortColumnKeyNavigation)?
                     .Where(s => s.ActiveFlag == isActive && s.FacilityKey == facilitykey)?
                     .ToListAsync();
            }

            return await DbSet.Include(e => e.SmartSort)
           .ThenInclude(e => e.SmartSortColumnKeyNavigation)?
           .Where(s => s.ActiveFlag == isActive && s.FacilityKey == facilitykey)?.Skip(offset)?.Take(limit)?
           .ToListAsync();
        }

        public async Task<IEnumerable<Models.TransactionPriority>> GetAllSerachedTransactionPriorityAsync(string transactionPriorityDescription, int offset, int limit, Guid facilitykey)
        {

            if (offset == 0 && limit == 0)
            {
                return await DbSet.Include(e => e.SmartSort)
               .ThenInclude(e => e.SmartSortColumnKeyNavigation)?
               .Where(s => s.FacilityKey == facilitykey && s.PriorityName.Contains(transactionPriorityDescription) )?
               .ToListAsync();
            }

            return await DbSet.Include(e => e.SmartSort)
           .ThenInclude(e => e.SmartSortColumnKeyNavigation)?
           .Where(s => s.FacilityKey == facilitykey && s.PriorityName.Contains(transactionPriorityDescription))?.Skip(offset)?.Take(limit)?
           .ToListAsync();
        }

        public Models.TransactionPriority GetByTransactionPriorityAndFacilityKey(Guid transactionpriorityId, Guid facilityId)
        {
            return DbSet.Where(p => p.Id == transactionpriorityId && p.FacilityKey == facilityId)?.FirstOrDefault();
        }


    }

}
