using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using AutoMapper;
using BD.Core.Context;
using Microsoft.AspNetCore.Mvc;
using SiteConfiguration.API.Common.Constants;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Exceptions;
using SiteConfiguration.API.Schedule.Models;

namespace SiteConfiguration.API.Schedule.Business
{
    /// <summary>
    /// Business Layer for schedules
    /// </summary>
    public class ScheduleBusiness : IScheduleBusiness
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRoutingRuleScheduleTimingRepository _routingRuleScheduleTimingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Only constructor, params are automatically injected
        /// </summary>
        /// <param name="scheduleRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public ScheduleBusiness(IScheduleRepository scheduleRepository, IRoutingRuleScheduleTimingRepository routingRuleScheduleTimingRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _routingRuleScheduleTimingRepository = routingRuleScheduleTimingRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// get list of schedule from database
        /// </summary>
        /// <param name="facilityKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ScheduleResponse>> GetSchedules(Guid facilityKey)
        {
            var schedules = await _scheduleRepository.GetSchedules(facilityKey);
            return _mapper.Map<IEnumerable<ScheduleResponse>>(schedules);
        }

        /// <summary>
        /// get schedule by Key from database 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Schedule</returns>
        public async Task<ScheduleResponseByKey> GetScheduleByKey(Guid key)
        {
            var schedule = await _scheduleRepository.GetAsync(key);
            var model = _mapper.Map<ScheduleResponseByKey>(schedule);
            if (await _routingRuleScheduleTimingRepository.GetRoutingRuleScheduleTiming(model.Key))
            //if (await _scheduleRepository.GetRoutingRuleScheduleTiming(model.Key))
            {
                model.isAssociatedWithRoutingRuleFlag = true;
            }
            else
            {
                model.isAssociatedWithRoutingRuleFlag = false;
            }
            return model;
        }

        /// <summary>
        /// add a schedule to database
        /// </summary>
        /// <param name="facilityKey"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task AddSchedule(Guid facilityKey, ScheduleRequest schedule)
        {
            if (schedule == null || facilityKey == Guid.Empty)
            {
                throw new ArgumentNullException($"{facilityKey} is not valid");
            }

            //Validate schedule does not exists within the given facility
            if (_scheduleRepository.GetScheduleByName(facilityKey, schedule.Name))
            {
                throw new InvalidScheduleException(Resource.ResourceManager.GetString($"E{ErrorCode.ResourceAlreadyExists}"), ErrorCode.ResourceAlreadyExists);
            }

            var dataModel = _mapper.Map<ScheduleTiming>(schedule);
            dataModel.ScheduleTimingKey = Guid.NewGuid();
            dataModel.FacilityKey = facilityKey;

            await _scheduleRepository.AddAsync(dataModel);
            _unitOfWork.CommitChanges();

        }

        /// <summary>
        /// add a schedule to database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="facilityKey"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task UpdateSchedule(Guid key, Guid facilityKey, ScheduleRequest schedule)
        {
            // Validate that attempting Update does not duplicate Schedule Name
            // if GetScheduleTimingByName returns null, means that the name is not yet taken = Valid Name
            // however, the name can be already taken, if current schedule's name has not yet been changed
            // hence, compare scheduleIds
            var dataModel = await _scheduleRepository.GetAsync(key);
            var scheduleDetail = _scheduleRepository.GetScheduleByName(schedule.Name);

            //// if after looking for the model with the Id, it's still null, then the item is not found
            if (dataModel == null)
            {
                throw new InvalidScheduleException(Resource.ResourceManager.GetString($"E{ErrorCode.ResourceNotFound}"), ErrorCode.ResourceNotFound);
            }

            // The ScheduleTimingId should be the same now, if not, the name is already taken
            if (scheduleDetail != null && scheduleDetail.ScheduleTimingKey != key)
            {
                throw new InvalidScheduleException(Resource.ResourceManager.GetString($"E{ErrorCode.DuplicateResourceNameExists}"), ErrorCode.DuplicateResourceNameExists);
            }
            _mapper.Map<ScheduleRequest, ScheduleTiming>(schedule, dataModel);

            _scheduleRepository.Update(dataModel);
            _unitOfWork.CommitChanges();
        }

        /// <summary>
        /// delete schedule from database
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task DeleteSchedule(Guid key)
        {
            var schedules = _scheduleRepository.Get(key);
            if (schedules == null)
            {
                throw new InvalidScheduleException(Resource.ResourceManager.GetString($"E{ErrorCode.ResourceNotFound}"), ErrorCode.ResourceNotFound);
            }
            if (await _routingRuleScheduleTimingRepository.GetRoutingRuleScheduleTiming(schedules.ScheduleTimingKey))
            {
                throw new InvalidScheduleException(Resource.ResourceManager.GetString($"E{ErrorCode.ResourceAssociated}"), ErrorCode.ResourceAssociated);
            }

            _scheduleRepository.Delete(schedules);
            _unitOfWork.CommitChanges();
        }
    }
}
