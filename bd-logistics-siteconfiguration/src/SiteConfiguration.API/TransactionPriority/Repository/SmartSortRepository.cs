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
    public class SmartSortRepository : BaseRepository<SmartSortColumn>, ISmartSortRepository
    {

        public SmartSortRepository(Infrastructure.DataAccess.SQL.DBContextEntities.ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SmartSortColumn>> GetAllSmartSortAsync(bool isActive)
        {
            return (await DbSet.Where(s => s.ActiveFlag == isActive).ToListAsync());
        }



    }
    
}
