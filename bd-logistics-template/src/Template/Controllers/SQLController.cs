using Caching;
using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD.Template.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/api/v1/template/[controller]")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class SampleSqlController : ControllerBase
    {
        /// <summary>
        /// _customerRepository property represents the Customer Repository.
        /// </summary>
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// _unitOfWork represents IUnitOfWork object to commit multiple or single operation as single unit.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// represents the IDistributed cache object to get/set cache
        /// </summary>
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="cache"></param>
        public SampleSqlController(ICustomerRepository customer, IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _cache = cache;
            _customerRepository = customer;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <returns></returns>

        [HttpGet("customers")]
        [ServiceFilter(typeof(HateoasFilter.HateoasFilter))]
        public async Task<ActionResult<Customer>> Get()
        {
            string cacheKey = "GetAllCustomer";
            var cachedItem = await _cache.GetAsync<IEnumerable<Customer>>(cacheKey);
            if (cachedItem != null)
            {
                return Ok(cachedItem);
            }
            else
            {
                var data = _customerRepository.GetAll();
                await _cache.PutAsync(cacheKey, data, 10);
                return Ok(data);
            }

        }

        /// <summary>
        /// Get a particular customer by their object.
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>

        [HttpGet("customer/{customerid}")]
        public Customer Get(int customerid)
        {
            return _customerRepository.GetAll().Where(x => x.Id == customerid).FirstOrDefault();
        }


        /// <summary>
        /// Create new customer in DB
        /// </summary>
        /// <param name="customer"></param>

        // POST api/values
        [HttpPost("customer")]
        public void Post([FromBody] Customer customer)
        {
            _customerRepository.Add(customer);
            _unitOfWork.CommitChanges();
        }

        /// <summary>
        /// Update a customer in DB.
        /// </summary>
        /// <param name="customer"></param>
        [HttpPut("customer")]
        public async void Put([FromBody] Customer customer)
        {
            _customerRepository.Update(customer);
            _unitOfWork.CommitChanges();
            await _cache.RemoveAsync("GetAllCustomer");
        }

        /// <summary>
        /// Delete a particular customer by their Id in DB.
        /// </summary>
        /// <param name="customerid"></param>

        [HttpDelete("customer/{customerid}")]
        public void Delete(int customerid)
        {
            var customer = _customerRepository.GetAll().Where(x => x.Id == customerid).FirstOrDefault();
            _customerRepository.Delete(customer);
            _unitOfWork.CommitChanges();

        }

    }
}