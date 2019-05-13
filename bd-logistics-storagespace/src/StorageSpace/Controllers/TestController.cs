//using AutoMapper;
//using BD.Core.EventBus.Abstractions;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System.Collections.Generic;
//using System.Linq;
//using StorageSpace.API.IntegrationEvents.Events;

//namespace StorageSpace.API.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TestController : ControllerBase
//    {
//        private readonly IEventBus _eventBus;
//        private readonly ILogger<TestController> _logger;
//        private readonly IMapper _mapper;
//        private readonly Configuration.Configuration _configuration;

//        public TestController(IEventBus eventBus, ILogger<TestController> logger, IOptions<Configuration.Configuration> options, IMapper mapper)
//        {
//            _eventBus = eventBus;
//            _logger = logger;
//            _mapper = mapper;
//            _configuration = options.Value;
//        }

//        [HttpGet]
//        [Route("request")]
//        public void SendRequest()
//        {
//            var headers = Request.Headers.Select(a => new KeyValuePair<string, string>(a.Key, a.Value)).ToDictionary(a => a.Key, a => a.Value);
//            var req = new StorageSpaceRequestEvent
//            {
//                TransactionQueueId = "123ABC",
//                FormularyId = 1,
//                FacilityId = 201,
//                ISAId = 301
//            };

//            _eventBus.Publish(_configuration.KafkaRequestTopic, req, headers);
//        }
//    }
//}