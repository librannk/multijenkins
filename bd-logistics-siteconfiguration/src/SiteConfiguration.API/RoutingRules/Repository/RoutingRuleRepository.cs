using Microsoft.EntityFrameworkCore;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.RoutingRules.Abstractions;
using SiteConfiguration.API.RoutingRules.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.Repository
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// RoutingRepository class implements the all member of IRoutingRuleRepository of type RoutingRule.
    /// </summary>
    public class RoutingRuleRepository : BaseRepository<RoutingRule>, IRoutingRuleRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RoutingRuleRepository(ApplicationDbContext context) : base(context)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoutingRule>> GetRoutingRules(Guid facilityID, int page = 0, int pageSize = 0, string searchString = "")
        {
            var result = DbSet.Include<RoutingRule>("RoutingRuleDestination")
                            .Include<RoutingRule>("RoutingRuleScheduleTiming")
                            .Include<RoutingRule>("RoutingRuleTranPriority").Where(x => x.FacilityKey.Equals(facilityID));
            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.RoutingRuleName.Contains(searchString));
            }
            if (page >= 1 && pageSize >= 1)
            {
                result = result.Skip((page - 1) * pageSize).Take(pageSize);
            }
            return result.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RoutingRule> GetRoutingRule(Guid id, Guid facilityId)
        {
            var result = DbSet.Include<RoutingRule>("RoutingRuleDestination")
                            .Include<RoutingRule>("RoutingRuleScheduleTiming")
                            .Include<RoutingRule>("RoutingRuleTranPriority")
                            .Where(x => x.RoutingRuleKey == id && x.FacilityKey == facilityId).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<RoutingRule> GetRoutingRule(string name, Guid facilityId)
        {
            var result = DbSet.Where(x => x.RoutingRuleName.Trim().ToLower() == name.Trim().ToLower() && x.FacilityKey == facilityId).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingRule"></param>
        /// <returns></returns>
        public async Task AddRoutingRule(RoutingRule routingRule)
        {
            await DbSet.AddAsync(routingRule);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingRule"></param>
        /// <returns></returns>
        public void UpdateRoutingRule(RoutingRule routingRule)
        {
            DbSet.Update(routingRule);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteRoutingRule(RoutingRule routingRule)
        {
            DbSet.Remove(routingRule);
        }
    }
}
