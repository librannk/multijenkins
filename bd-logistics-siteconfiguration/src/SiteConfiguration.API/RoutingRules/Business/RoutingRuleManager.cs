using AutoMapper;
using BD.Core.Context;
using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SiteConfiguration.API.Common;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.IntegrationEvents.Events;
using SiteConfiguration.API.RoutingRules.Abstractions;
using SiteConfiguration.API.RoutingRules.Models;
using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using SiteConfiguration.API.Schedule.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.RoutingRules.Business
{
    public class RoutingRuleManager : IRoutingRuleManager
    {
        private readonly IRoutingRuleRepository _routingRulesRepository;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScheduleBusiness _scheduleBusiness;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly Configuration.Configuration _configuration;
        private readonly ILogger<RoutingRuleManager> _logger;

        public RoutingRuleManager(IRoutingRuleRepository routingRulesRepository, IExecutionContextAccessor executionContextAccessor
            , IUnitOfWork unitOfWork, ILogger<RoutingRuleManager> logger, IScheduleBusiness scheduleBusiness,
            IMapper mapper, IEventBus eventBus,
             IOptions<Configuration.Configuration> options)
        {
            _routingRulesRepository = routingRulesRepository;
            _scheduleBusiness = scheduleBusiness;
            _executionContextAccessor = executionContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventBus = eventBus;
            _configuration = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// to add routing rule in database
        /// </summary>
        /// <param name="routingRule"></param>
        /// <param name="facilityID"></param>
        /// <returns></returns>
        public BusinessResponse AddRoutingRule(RoutingRuleRequest routingRule, Dictionary<string, string> headers)
        {
            try
            {                
                var facilityID = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);

                var rule = _routingRulesRepository.GetRoutingRule(routingRule.RoutingRuleName, facilityID).GetAwaiter().GetResult();
                if (rule != null)
                {
                    var message = new BusinessResponse() { IsSuccess = false, Message = "Routing Rule With Same Name Already Exist." };
                    return message;
                }

                if (routingRule.RoutingRuleDestinations == null && routingRule.RoutingRuleSchedules == null && routingRule.RoutingRuleTranPriority == null)
                {
                    var message = new BusinessResponse() { IsSuccess = false, Message = "Please select any schedule or destination or transaction priority." };
                    return message;
                }

                var newID = Utility.GetNewGuid();
                var ruleId = Guid.Empty;
                RoutingRule UpdateRule = new RoutingRule();

                //common methods for add/Update operations..
                List<RoutingRuleScheduleTiming> _schedules = AddOrUpdateSchedules(routingRule, newID, ruleId, UpdateRule);
                List<RoutingRuleDestination> _destination = AddOrUpdateDestination(routingRule, newID, ruleId, UpdateRule);
                List<RoutingRuleTranPriority> _tranPrio = AddOrUpdateTranPriority(routingRule, newID, ruleId, UpdateRule);

                _routingRulesRepository.Add(new RoutingRule
                {
                    RoutingRuleName = routingRule.RoutingRuleName.Trim(),
                    TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                    FacilityKey = facilityID,
                    SearchCriteriaGranularityLevel = routingRule.SearchCriteriaGranularityLevel,
                    CreatedByActorKey = routingRule.ActorKey,
                    LastModifiedByActorKey = routingRule.ActorKey,
                    RoutingRuleScheduleTiming = _schedules.Any() ? _schedules : null,
                    RoutingRuleDestination = _destination.Any() ? _destination : null,
                    RoutingRuleTranPriority = _tranPrio.Any() ? _tranPrio : null,
                    RoutingRuleKey = newID,
                });

                _unitOfWork.CommitChanges();
                SendEvent(GetByID(newID), "Add", headers);
                var result = new BusinessResponse() { IsSuccess = true, Message = "Routing Rule Created Successfully", Id = newID };
                return result;
            }
            catch (Exception ex)
            {
                var exception = new BusinessResponse() { IsSuccess = false, Message = ex.Message };
                return exception;
            }
        }

        [ExcludeFromCodeCoverage]
        private List<RoutingRuleTranPriority> AddOrUpdateTranPriority(RoutingRuleRequest routingRule, Guid newID, Guid ruleId, RoutingRule rule)
        {
            var _tranPrio = new List<RoutingRuleTranPriority>();
            if (routingRule.RoutingRuleTranPriority != null && routingRule.RoutingRuleTranPriority.Any())
            {
                foreach (var item in routingRule.RoutingRuleTranPriority)
                {
                    if (ruleId == Guid.Empty) // case Add new tranPriority
                    {
                        _tranPrio.Add(new RoutingRuleTranPriority
                        {
                            RoutingRuleTranPriorityKey = Utility.GetNewGuid(),
                            TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                            TranPriorityKey = item.TranPriorityKey,
                            RoutingRuleKey = newID,
                            CreatedByActorKey = routingRule.ActorKey,
                            LastModifiedByActorKey = routingRule.ActorKey,
                            CreatedDateTime = DateTimeOffset.Now,
                            LastModifiedUTCDateTime = DateTime.UtcNow
                        });
                    }
                    else if (newID == Guid.Empty) // Case Update destination
                    {
                        rule.RoutingRuleTranPriority.Add(new RoutingRuleTranPriority
                        {
                            RoutingRuleTranPriorityKey = Utility.GetNewGuid(),
                            TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                            TranPriorityKey = item.TranPriorityKey,
                            RoutingRuleKey = ruleId,
                            CreatedByActorKey = routingRule.ActorKey,
                            LastModifiedByActorKey = routingRule.ActorKey,
                            CreatedDateTime = DateTimeOffset.Now,
                            LastModifiedUTCDateTime = DateTime.UtcNow
                        });
                    }
                }
            }

            return _tranPrio;
        }

        [ExcludeFromCodeCoverage]
        private List<RoutingRuleDestination> AddOrUpdateDestination(RoutingRuleRequest routingRule, Guid newID, Guid ruleId, RoutingRule rule)
        {
            var _destination = new List<RoutingRuleDestination>();
            if (routingRule.RoutingRuleDestinations != null && routingRule.RoutingRuleDestinations.Any())
            {
                foreach (var item in routingRule.RoutingRuleDestinations)
                {
                    if (ruleId == Guid.Empty) // case Add new destination
                    {
                        _destination.Add(new RoutingRuleDestination
                        {
                            RoutingRuleDestinationKey = Utility.GetNewGuid(),
                            TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                            DestinationKey = item.DestinationKey,
                            RoutingRuleKey = newID,
                            CreatedByActorKey = routingRule.ActorKey,
                            LastModifiedByActorKey = routingRule.ActorKey,
                            CreatedDateTime = DateTimeOffset.Now,
                            LastModifiedUTCDateTime = DateTime.UtcNow
                        });
                    }
                    else if (newID == Guid.Empty) // Case Update destination
                    {
                        rule.RoutingRuleDestination.Add(new RoutingRuleDestination
                        {
                            RoutingRuleDestinationKey = Utility.GetNewGuid(),
                            TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                            DestinationKey = item.DestinationKey,
                            RoutingRuleKey = ruleId,
                            CreatedByActorKey = routingRule.ActorKey,
                            LastModifiedByActorKey = routingRule.ActorKey,
                            CreatedDateTime = DateTimeOffset.Now,
                            LastModifiedUTCDateTime = DateTime.UtcNow
                        });
                    }
                }
            }
            return _destination;
        }

        [ExcludeFromCodeCoverage]
        private List<RoutingRuleScheduleTiming> AddOrUpdateSchedules(RoutingRuleRequest routingRule, Guid newID, Guid ruleId, RoutingRule rule)
        {
            var _schedules = new List<RoutingRuleScheduleTiming>();
            if (routingRule.RoutingRuleSchedules != null && routingRule.RoutingRuleSchedules.Any())
            {
                foreach (var item in routingRule.RoutingRuleSchedules)
                {
                    if (ruleId == Guid.Empty) // case Add new Schedule
                    {
                        _schedules.Add(new RoutingRuleScheduleTiming
                        {
                            RoutingRuleScheduleTimingKey = Utility.GetNewGuid(),
                            TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                            ScheduleTimingKey = item.ScheduleKey,
                            RoutingRuleKey = newID,
                            CreatedByActorKey = routingRule.ActorKey,
                            LastModifiedByActorKey = routingRule.ActorKey,
                            CreatedDateTime = DateTimeOffset.Now,
                            LastModifiedUTCDateTime = DateTime.UtcNow
                        });
                    }
                    else if (newID == Guid.Empty) // Case Update Schedule
                    {
                        rule.RoutingRuleScheduleTiming.Add(new RoutingRuleScheduleTiming
                        {
                            RoutingRuleScheduleTimingKey = Utility.GetNewGuid(),
                            TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                            ScheduleTimingKey = item.ScheduleKey,
                            RoutingRuleKey = ruleId,
                            CreatedByActorKey = routingRule.ActorKey,
                            LastModifiedByActorKey = routingRule.ActorKey,
                            CreatedDateTime = DateTimeOffset.Now,
                            LastModifiedUTCDateTime = DateTime.UtcNow
                        });
                    }
                }
            }

            return _schedules;
        }

        [ExcludeFromCodeCoverage]
        private void SendEvent(RoutingRulesById routingRulesById, string type, Dictionary<string, string> headers)
        {
            var eventMessage = new RoutingRuleEvent
            {
                Message = routingRulesById,
                EventType = type
            };
            try
            {
                _eventBus.Publish(_configuration.RoutingRule, eventMessage, headers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        /// <summary>
        /// to update routing rule in database
        /// </summary>
        /// <param name="routingRule"></param>
        /// <param name="facilityID"></param>
        /// <param name="ruleId"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public BusinessResponse UpdateRoutingRule(RoutingRuleRequest routingRule, Guid ruleId, Dictionary<string, string> headers)
        {
            try
            {
                var facilityID = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);

                var ruleExist = _routingRulesRepository.GetRoutingRule(routingRule.RoutingRuleName, facilityID).GetAwaiter().GetResult();
                if (ruleExist != null && ruleExist.RoutingRuleKey != ruleId)
                {
                    var message = new BusinessResponse() { IsSuccess = false, Message = "Routing Rule With Same Name Already Exist." };
                    return message;
                }

                if (routingRule.RoutingRuleDestinations == null && routingRule.RoutingRuleSchedules == null && routingRule.RoutingRuleTranPriority == null)
                {
                    var message = new BusinessResponse() { IsSuccess = false, Message = "Please select any schedule or destination or transaction priority." };
                    return message;
                }

                var rule = _routingRulesRepository.GetRoutingRule(ruleId, facilityID).GetAwaiter().GetResult();
                if (rule == null)
                {
                    var message = new BusinessResponse() { IsSuccess = false, Message = "Routing Rule with RountingRuleKey :" + ruleId + " not exist." };
                    return message;
                }

                if (rule.RoutingRuleTranPriority.Any())
                    rule.RoutingRuleTranPriority.Clear();
                if (rule.RoutingRuleScheduleTiming.Any())
                    rule.RoutingRuleScheduleTiming.Clear();
                if (rule.RoutingRuleDestination.Any())
                    rule.RoutingRuleDestination.Clear();

                var newGuid = Guid.Empty;

                //common methods for add/Update operations..
                AddOrUpdateSchedules(routingRule, newGuid, ruleId, rule);
                AddOrUpdateDestination(routingRule, newGuid, ruleId, rule);
                AddOrUpdateTranPriority(routingRule, newGuid, ruleId, rule);


                rule.RoutingRuleName = routingRule.RoutingRuleName.Trim();
                rule.SearchCriteriaGranularityLevel = routingRule.SearchCriteriaGranularityLevel;
                rule.LastModifiedByActorKey = routingRule.ActorKey;
                rule.LastModifiedUTCDateTime = DateTime.UtcNow;
                _routingRulesRepository.UpdateRoutingRule(rule);

                _unitOfWork.CommitChanges();
                SendEvent(GetByID(ruleId), "Update", headers);
                var result = new BusinessResponse() { IsSuccess = true, Message = "Routing Rule Updated Successfully" };
                return result;
            }
            catch (Exception ex)
            {
                var exception = new BusinessResponse() { IsSuccess = false, Message = ex.Message };
                return exception;
            }
        }

        /// <summary>
        /// to delete routing rule from database..
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="facilityID"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public BusinessResponse DeleteRoutingRule(Guid ruleId, Dictionary<string, string> headers)
        {
            try
            {
                var facilityID = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);

                var rule = _routingRulesRepository.GetRoutingRule(ruleId, facilityID).GetAwaiter().GetResult();
                if (rule == null)
                {
                    var exception = new BusinessResponse() { IsSuccess = false, Message = "RountingRuleKey doesn't exist in system" };
                    return exception;
                }
                _routingRulesRepository.DeleteRoutingRule(rule);

                _unitOfWork.CommitChanges();
                SendEvent(GetByID(ruleId), "Delete", headers);
                var result = new BusinessResponse() { IsSuccess = true, Message = "Routing Rule Deleted Successfully" };
                return result;
            }
            catch (Exception ex)
            {
                var exception = new BusinessResponse() { IsSuccess = false, Message = ex.Message };
                return exception;
            }
        }

        /// <summary>
        /// to get routing rule by Id.
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="facilityID"></param>
        /// <returns></returns>
        public RoutingRulesById GetByID(Guid ruleId)
        {
            var facilityID = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);

            RoutingRule rule = _routingRulesRepository.GetRoutingRule(ruleId, facilityID).GetAwaiter().GetResult();
            return _mapper.Map<RoutingRulesById>(rule);
        }

        /// <summary>
        /// to get all routing rule from database..
        /// </summary>
        /// <param name="facilityID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<RoutingRulesResult> GetAllRoutingRule(int page = 0, int pageSize = 0, string searchString = "")
        {
            var facilityID = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);

            IEnumerable<Schedule.Models.ScheduleResponse> schedule = _scheduleBusiness.GetSchedules(facilityID).GetAwaiter().GetResult();

            IEnumerable<RoutingRule> rule = _routingRulesRepository.GetRoutingRules(facilityID, page, pageSize, searchString).GetAwaiter().GetResult();
            var result = new List<RoutingRulesResult>();
            foreach (var item in rule)
            {
                var sheduleIdByRule = item.RoutingRuleScheduleTiming.Select(x => x).ToList();
                // TO DO Destination
                // To Do TransactionPriority

                var perRuleShedule = (from s in schedule
                                      join rs in sheduleIdByRule
                                      on s.Key equals rs.ScheduleTimingKey
                                      select
                                          $"{s.Name},{String.Join(",", s.ScheduleDays.Select(w => w).ToArray())},{s.StartTime},{s.EndTime};"
                                        ).ToList();
                //var perTraPri = (from s in item.RoutingRuleTranPriority TO DO
                //                        select
                //                            $"{s.TranPriorityKeyNavigation.PriorityCode}"
                //                    ).ToList();

                result.Add(new RoutingRulesResult
                {
                    RoutingRuleName = item.RoutingRuleName,
                    RoutingRuleKey = item.RoutingRuleKey,
                    Schedules = String.Join(",", perRuleShedule.Select(w => w).ToArray()),
                    TranPriorities = "",//String.Join(",", perTraPri.Select(w => w).ToArray()) ,
                    Destinations = ""
                });

            }
            return result;
        }
    }
}
