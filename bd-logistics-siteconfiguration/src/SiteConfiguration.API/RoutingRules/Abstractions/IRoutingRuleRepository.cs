using SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository;
using SiteConfiguration.API.RoutingRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.Abstractions
{
    /// <summary>
    /// Routing Rule Interface
    /// </summary>
    public interface IRoutingRuleRepository :IRepository<RoutingRule>
    {
        /// <summary>
        /// Get routing rules from database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RoutingRule>> GetRoutingRules(Guid facility,int page=0,int pageSize=0,string searchString="");

        /// <summary>
        /// get routing rule by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RoutingRule> GetRoutingRule(Guid id, Guid facilityId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        Task<RoutingRule> GetRoutingRule(string name, Guid facilityId);
        /// <summary>
        /// add new routing rule to database
        /// </summary>
        /// <param name="routingRule"></param>
        /// <returns></returns>
        Task AddRoutingRule(RoutingRule routingRule);

        /// <summary>
        /// update existing routing rule in database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="routingRule"></param>
        /// <returns></returns>
        void UpdateRoutingRule(RoutingRule routingRule);

        /// <summary>
        /// delete routing rule from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteRoutingRule(RoutingRule routingRule);
    }
}
