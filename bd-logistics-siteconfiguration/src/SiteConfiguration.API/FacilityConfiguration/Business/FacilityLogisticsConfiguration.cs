using AutoMapper;
using SiteConfiguration.API.FacilityConfiguration.Abstractions;
using SiteConfiguration.API.FacilityConfiguration.Constants;
using SiteConfiguration.API.FacilityConfiguration.Models;
using SiteConfiguration.API.FacilityConfiguration.RequestResponseModel;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace SiteConfiguration.API.FacilityConfiguration.Business
{
    /// <summary>
    /// FacilityLogisticsConfiguration business layer
    /// </summary>
    public class FacilityLogisticsConfiguration : IFacilityLogisticsConfiguration
    {

        private readonly IFacilityLogisticsConfigurationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// FacilityLogisticsConfiguration initialisation goes here
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public FacilityLogisticsConfiguration(IFacilityLogisticsConfigurationRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// CreateFacilitySpecificConfigurationAsync
        /// </summary>
        /// <param name="facilityConfiguration"></param>
        /// <returns></returns>
        public async Task CreateFacilitySpecificConfigurationAsync(TransactionQueueConfigurationRequest facilityConfiguration)
        {
            if (facilityConfiguration == null || facilityConfiguration.FacilityKey == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            var model = _mapper.Map<FacilityLogisticsConfig>(facilityConfiguration);
            PopulateAuditData(model);

            _repository.Add(model);
            await _unitOfWork.CommitChangesAsync();
        }

        private static void PopulateAuditData(Models.FacilityLogisticsConfig model)
        {
            // TODO: for time being we are setting actor key in this way, when this will come from some context will set from there.
            var actorKey = new Guid();
            model.CreatedByActorKey = actorKey;
            model.LastModifiedByActorKey = actorKey;
            model.LastModifiedUTCDateTime = DateTime.UtcNow;
            model.CreatedDateTime = DateTimeOffset.Now;
        }

        /// <summary>
        /// Business layer for create request of Facility Extension
        /// </summary>
        /// <param name="facilityLogisticsConfigurationExtension"></param>
        /// <returns></returns>
        public async Task<BusinessResponse> CreateFacilityExtensionAsync(FacilityLogisticsConfigurationExtension facilityLogisticsConfigurationExtension)
        {
            if (facilityLogisticsConfigurationExtension == null || facilityLogisticsConfigurationExtension.FacilityKey == Guid.Empty)
            {
                var message = new BusinessResponse() { IsSuccess = false, Message = BusinessError.InvalidInput };
                return message;
            }

            
            var model = _mapper.Map<FacilityLogisticsConfig>(facilityLogisticsConfigurationExtension);
            PopulateAuditData(model);
            _repository.Add(model);
            await _unitOfWork.CommitChangesAsync();
            var result = new BusinessResponse() { IsSuccess = true, Message = BusinessResponseMessages.LogisticsConfigurationCreated };
            return result;
        }

        /// <summary>
        /// Business object for updating existing facility configuration
        /// </summary>
        /// <param name="facilityConfiguration"></param>
        /// <returns></returns>
        public async Task<BusinessResponse> UpdateFacilityConfigAsync(TransactionQueueConfigurationRequest facilityConfiguration)
        {
            if (facilityConfiguration == null || facilityConfiguration.FacilityKey == Guid.Empty)
            {
                var message = new BusinessResponse() { IsSuccess = false, Message = BusinessError.InvalidInput };
                return message;
            }
            var facilityData = await GetFacilityConfigurationAsync(facilityConfiguration.FacilityKey);
            if (facilityData != null)
            {
                _mapper.Map(facilityConfiguration, facilityData, typeof(TransactionQueueConfigurationRequest), typeof(FacilityLogisticsConfig));
                _repository.Update(facilityData);
                await _unitOfWork.CommitChangesAsync();
                var result = new BusinessResponse() { IsSuccess = true, Message = BusinessResponseMessages.LogisticsConfigurationUpdated };
                return result;
            }
            else
            {
                var message = new BusinessResponse() { IsSuccess = false, Message = BusinessError.RecordNotFound };
                return message;
            }

        }


        /// <summary>
        /// Business layer for updating facility extension on db
        /// </summary>
        /// <param name="facilityLogisticsConfigurationExtension"></param>
        /// <returns></returns>
        public async Task<BusinessResponse> UpdateFacilityExtensionAsync(FacilityLogisticsConfigurationExtension facilityLogisticsConfigurationExtension)
        {
            if (facilityLogisticsConfigurationExtension == null || facilityLogisticsConfigurationExtension.FacilityKey == Guid.Empty)
            {
                var message = new BusinessResponse() { IsSuccess = false, Message = BusinessError.InvalidInput };
                return message;
            }
            var facilityData = await GetFacilityConfigurationAsync(facilityLogisticsConfigurationExtension.FacilityKey);
            if (facilityData != null)
            {
                _mapper.Map(facilityLogisticsConfigurationExtension, facilityData, typeof(FacilityLogisticsConfigurationExtension), typeof(FacilityLogisticsConfig));
                _repository.Update(facilityData);
                await _unitOfWork.CommitChangesAsync();
                var result = new BusinessResponse() { IsSuccess = true, Message = BusinessResponseMessages.LogisticsConfigurationUpdated };
                return result;
            }
            else
            {
                var message = new BusinessResponse() { IsSuccess = false, Message = BusinessError.RecordNotFound };
                return message;
            }

        }

        private async Task<FacilityLogisticsConfig> GetFacilityConfigurationAsync(Guid FacilityKey)
        {
            return await _repository.GetAsync(FacilityKey);
        }
    }
}