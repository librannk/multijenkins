using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoUsageApp.Infrastructure.MongoRepository;

namespace DemoUsageApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/Facility")]
    [ApiController]  
    public class MongoFacilityController : ControllerBase
    {
        private DemoUsageApp.Infrastructure.MongoRepository.IFacilityRepository _facilityRepository;
        private readonly IExecutionContextAccessor _contextAccessor;

        public MongoFacilityController(DemoUsageApp.Infrastructure.MongoRepository.IFacilityRepository facilityRepository,
            IExecutionContextAccessor contextAccessor)
        {
            _facilityRepository = facilityRepository;
            _contextAccessor = contextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> GetFacility(string facilityCode)
        {
            var result = await _facilityRepository.GetFacilityByCode(facilityCode);
            return Ok(result);
        }
        [HttpGet]
        [Route("InsertFacility")]
        public async Task<IActionResult> AddFacility(string facilityKey,
            string facilityCode)
        { 
            DemoUsageApp.Infrastructure.MongoRepository.Facility facility = new Infrastructure.MongoRepository.Facility();
            facility.FacilityKey = facilityKey;
            facility.FacilityCode = facilityCode;
            var result =  _facilityRepository.InsertAsync(facility);
            return Ok(result);
        }
    }
}