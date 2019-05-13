using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.Abstractions
{
    public interface IRoutingRuleManager
    {
        /// <summary>
        /// Add Routing Rule
        /// </summary>
        /// <param name="routingRule"></param>
        /// <param name="facilityID"></param>
        /// <returns></returns>
        BusinessResponse AddRoutingRule(RoutingRuleRequest routingRule, Dictionary<string, string> headers);

        /// <summary>
        /// Update Routing Rule
        /// </summary>
        /// <param name="routingRule"></param>
        /// <param name="facilityID"></param>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        BusinessResponse UpdateRoutingRule(RoutingRuleRequest routingRule, Guid ruleId, Dictionary<string, string> headers);

        /// <summary>
        /// Delete Routing Rule
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        BusinessResponse DeleteRoutingRule(Guid ruleId, Dictionary<string, string> headers);

        /// <summary>
        /// Get Routing Rule by ID
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        RoutingRulesById GetByID(Guid ruleId);

        /// <summary>
        /// Get All Routing Rule
        /// </summary>
        /// <param name="facilityID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        IEnumerable<RoutingRulesResult> GetAllRoutingRule(int page = 0, int pageSize = 0, string searchString = "");
    }
}
