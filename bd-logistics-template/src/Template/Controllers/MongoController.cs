using BD.Template.API.Infrastructure.DBModel;
using BD.Template.API.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;

namespace BD.Template.API.Controllers
{
    /// <summary>
    /// Mongo controller
    /// </summary>
    [Route("/api/v1/template/[controller]")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    public class SampleMongoController : ControllerBase
    {
        private readonly IMongoRepository _mongoRepository;

        /// <summary>
        /// Constructor with IMongoRepository DI
        /// </summary>
        /// <param name="mongoRepository"></param>
        public SampleMongoController(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }


        /// <summary>
        /// To get the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public ActionResult<User> GetUser(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var query = _mongoRepository.GetById(userId);
                return Ok(query);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// To get all the user
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var query = _mongoRepository.GetAll();
            return Ok(query);
        }

        /// <summary>
        /// To insert the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public ActionResult<ObjectId> InsertUser(User user)
        {
            _mongoRepository.Insert(user);
            return Ok(user.Id);
        }

        /// <summary>
        /// To update the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("user")]
        public ActionResult<bool> UpdateUser(User user)
        {
            _mongoRepository.Update(user);
            return Ok(true);
        }

        /// <summary>
        /// To delete the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete("user")]
        public ActionResult<bool> DeleteUser(User user)
        {
            _mongoRepository.Delete(user);
            return Ok(true);
        }
    }
}