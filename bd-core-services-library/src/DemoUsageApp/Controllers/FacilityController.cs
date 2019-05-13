using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DemoUsageApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/{facilitykey:Guid}/Facility")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private IFacilityRepository _facilityRepository;
        private readonly IExecutionContextAccessor _contextAccessor;

        public FacilityController(IFacilityRepository facilityRepository, IExecutionContextAccessor contextAccessor)
        {
            _facilityRepository = facilityRepository;
            _contextAccessor = contextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> GetFacility()
        {
            var result = await _facilityRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("CheckFacilityKey")]
        public IActionResult CheckFacilityKey()
        {
            return Ok(_contextAccessor.Current.Facility.FacilityKey);
        }
    }
   


}