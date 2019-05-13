using AutoMapper;
using BD.Core.Context;
using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SiteConfiguration.API.AutoMapper;
using SiteConfiguration.API.Common;
using SiteConfiguration.API.Configuration;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.RoutingRules.Abstractions;
using SiteConfiguration.API.RoutingRules.Business;
using SiteConfiguration.API.RoutingRules.Models;
using SiteConfiguration.API.RoutingRules.RequestReponceModel;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RoutingRuleBusinessUnitTest
{
    public class RoutingRuleBusinessTest
    {
        #region PrivateFields
        private readonly Mock<IRoutingRuleRepository> _mockRoutingRuleRepository;
        private readonly Mock<IScheduleBusiness> _mockScheduleBusiness;
        private RoutingRuleManager _routingRuleManager;
        private IUnitOfWork _unitofwork;
        private IMapper _mapper;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly Mock<ILogger<RoutingRuleManager>> _logger;
        private Mock<IEventBus> _eventbus;
        private readonly Mock<IOptions<Configuration>> _option;

        #endregion

        public RoutingRuleBusinessTest()
        {
            _mockRoutingRuleRepository = new Mock<IRoutingRuleRepository>();
            _mockScheduleBusiness = new Mock<IScheduleBusiness>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapRoutingRule());
            });
            _mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<RoutingRuleManager>>();
            _eventbus = new Mock<IEventBus>();
            _option = new Mock<IOptions<Configuration>>();
            _option.SetupGet(x => x.Value).Returns(new Configuration()
            {
                RoutingRule = "xyz",
                TransactionPriority = "xyz"                
            });

            _routingRuleManager = new RoutingRuleManager(_mockRoutingRuleRepository.Object, _executionContextAccessor, _unitofwork, _logger.Object, _mockScheduleBusiness.Object, _mapper,_eventbus.Object
                ,_option.Object);
        }

        [Fact]
        public void GetAll_ValidArguments_ShouldReturnRoutingRules()
        {
            //Arrange
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var fakeSchedule = new Schedule
            {
                Key = mockGuid
            };
            var scheduleList = new List<Schedule>();
            scheduleList.Add(fakeSchedule);
            _mockScheduleBusiness.Setup(x => x.GetSchedules(It.IsAny<Guid>())).ReturnsAsync(scheduleList);

            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = mockGuid,
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = "test",
            };
            var routingList = new List<RoutingRule>();
            routingList.Add(fakeRoutingRule);
            routingList.AsEnumerable();
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRules(mockGuid, 0, 0, "")).ReturnsAsync(routingList);
            //Act
            var response = _routingRuleManager.GetAllRoutingRule(mockGuid);
            //Assert
            Assert.Equal(mockRoutingRuleGuid, response.First().RoutingRuleId);
        }

        [Fact]
        public void GetAll_ValidArguments_NoRecord_Exist()
        {
            //Arrange
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var fakeSchedule = new Schedule
            {
                Key = mockGuid
            };
            var scheduleList = new List<Schedule>();
            scheduleList.Add(fakeSchedule);
            _mockScheduleBusiness.Setup(x => x.GetSchedules(It.IsAny<Guid>())).ReturnsAsync(scheduleList);

            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = mockGuid,
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = "test",
            };
            var routingList = new List<RoutingRule>();
            routingList.Add(fakeRoutingRule);
            routingList.AsEnumerable();
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRules(mockGuid, 0, 0, "")).ReturnsAsync(routingList);
            //Act
            var response = _routingRuleManager.GetAllRoutingRule(mockRoutingRuleGuid);
            //Assert            
            Assert.Null(response);
        }

        [Fact]
        public void GetByID_ValidArguments_ShouldReturnRoutingRules()
        {
            //Arrange
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var mockFacilityGuid = Guid.Parse("E767B738-3944-4896-93A0-6F0ABCD16890");
            var mockRuleName = "Test";
            
            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = mockFacilityGuid,
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = mockRuleName,
                SearchCriteriaGranularityLevel = 1,
            };
            
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid, mockFacilityGuid)).ReturnsAsync(fakeRoutingRule);
            //Act
            var response = _routingRuleManager.GetByID(mockRoutingRuleGuid, mockFacilityGuid);
            //Assert            
            Assert.Contains(mockRuleName, response.RoutingRuleName);
        }

        [Fact]
        public void GetByID_ValidArguments_NoRecord_Exist()
        {
            //Arrange
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var mockFacilityGuid = Guid.Parse("E767B738-3944-4896-93A0-6F0ABCD16890");
            var mockRuleName = "Test";

            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = mockFacilityGuid,
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = mockRuleName,
                SearchCriteriaGranularityLevel = 1,
            };

            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid, mockRoutingRuleGuid)).Returns(Task.FromResult<RoutingRule>(null));
            //Act
            var response = _routingRuleManager.GetByID(mockRoutingRuleGuid, mockRoutingRuleGuid);
            //Assert            
            Assert.Null(response);
        }

        [Fact]
        public void AddRoutingRule_ValidArguments_ShouldAddRoutingRules()
        {
            //Arrange
            var mockTenantGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var mockFacilityGuid = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46");
            var mockRoutingRuleGuid = Utility.GetNewGuid();
            Dictionary<string, string> headers = new Dictionary<string, string>();

            var mockScheduleGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var fakeSchedule = new RequestRoutingRuleSchedule
            {
                ScheduleId = mockScheduleGuid              
            };
            var scheduleList = new List<RequestRoutingRuleSchedule>();
            scheduleList.Add(fakeSchedule);

            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = "test new",
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46"),
                RoutingRuleSchedules = scheduleList,
            };

            var fakeRoutingRuleScheduleTiming = new RoutingRuleScheduleTiming {
                        RoutingRuleScheduleTimingKey = Utility.GetNewGuid(),
                        TenantKey = mockTenantGuid, // Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                        ScheduleTimingKey = mockScheduleGuid,
                        RoutingRuleKey = mockRoutingRuleGuid,
                        CreatedByActorKey = routingRuleReq.ActorKey,
                        LastModifiedByActorKey = routingRuleReq.ActorKey,
                        CreatedDateUTCDateTime = DateTimeOffset.Now,
                        LastModifiedUTCDateTime = DateTimeOffset.Now
            };
            var _schedules = new List<RoutingRuleScheduleTiming>();
            _schedules.Add(fakeRoutingRuleScheduleTiming);

            var mockRoutingRule = new RoutingRule
            {
                RoutingRuleName = routingRuleReq.RoutingRuleName.Trim(),
                TenantKey = mockTenantGuid, // Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey),
                FacilityKey = mockFacilityGuid,
                SearchCriteriaGranularityLevel = routingRuleReq.SearchCriteriaGranularityLevel,
                CreatedByActorKey = routingRuleReq.ActorKey,
                LastModifiedByActorKey = routingRuleReq.ActorKey,
                RoutingRuleScheduleTiming = _schedules.Count() > 0 ? _schedules : null,                
                RoutingRuleKey = mockRoutingRuleGuid,
            };

            //_mockRoutingRuleRepository.Setup(x => x.Add(mockRoutingRule)).Returns();
            //Act
            var response = _routingRuleManager.AddRoutingRule(routingRuleReq, mockFacilityGuid,headers);
            //Assert            
            Assert.Contains("Routing Rule Created Successfully", response.Message);
            Assert.Equal(mockRoutingRuleGuid, response.Id);
        }
    }
}
