using StorageSpace.API.BusinessLayer.Contracts;
using StorageSpace.API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StorageSpace.API.Infrastructure.Repository.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace StorageSpace.API.BusinessLayer.Concrete
{
    /// <summary>
    /// ISAManager
    /// </summary>
    public class ISAManager : IISAManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IISADetailRepository _isaDetailRepository;

        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<ISAManager> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isaDetailRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ISAManager(IISADetailRepository isaDetailRepository, IMapper mapper, ILogger<ISAManager> logger)
        {
            _isaDetailRepository = isaDetailRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// to get all isa by facility id 
        /// </summary>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        public async Task<List<ISARequest>> GetISA(Guid facilityId)
        {
            _logger.LogInformation(
                $"Request received from Service to fetch GetISA with facilityKey: {facilityId}");

            return await _isaDetailRepository.GetISA(facilityId);
        }

        /// <summary>
        /// to get ISA by isa id and facility id
        /// </summary>
        /// <param name="isaId"></param>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        public async Task<ISA> GetISAById(string isaId, Guid facilityId)
        {
            _logger.LogInformation(
                $"Request received from Service to fetch GetISAById with isaId and facilityKey : {isaId}, {facilityId} respectively");
            var itemStorageSpace = await _isaDetailRepository.GetISAById(isaId, facilityId);

            return _mapper.Map<ISA>(itemStorageSpace);
        }

    }
}
