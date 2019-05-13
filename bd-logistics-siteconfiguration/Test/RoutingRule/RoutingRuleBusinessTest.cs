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
        private readonly Mock<IUnitOfWork> _unitofwork;
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
            _unitofwork = new Mock<IUnitOfWork>();
            var tenanentKey = new Guid().ToString();
            var facilityCode = new Guid().ToString();
            var facilityKey = new Guid().ToString();
            _executionContextAccessor = new ExecutionContextAccessor() { Current = new Context() { Tenant = new TenantContext(tenanentKey), Facility = new FacilityContext(facilityKey,facilityCode) } };

            _routingRuleManager = new RoutingRuleManager(_mockRoutingRuleRepository.Object, _executionContextAccessor, _unitofwork.Object, _logger.Object, _mockScheduleBusiness.Object, _mapper,_eventbus.Object
                ,_option.Object);
        }

        [Fact]
        public void GetAll_ValidArguments_ShouldReturnRoutingRules()
        {
            //Arrange
            var mockFacilityKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var fakeSchedule = new ScheduleResponse
            {
                Key = mockGuid
            };
            var scheduleList = new List<ScheduleResponse>();
            scheduleList.Add(fakeSchedule);
            _mockScheduleBusiness.Setup(x => x.GetSchedules(It.IsAny<Guid>())).ReturnsAsync(scheduleList);

            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = mockFacilityKey,
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = "test",
            };
            var routingList = new List<RoutingRule>();
            routingList.Add(fakeRoutingRule);
            routingList.AsEnumerable();
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRules(mockFacilityKey, 0, 0, "")).ReturnsAsync(routingList);
            //Act
            var response = _routingRuleManager.GetAllRoutingRule();
            //Assert
            Assert.Equal(mockRoutingRuleGuid, response.First().RoutingRuleKey);
        }

        [Fact]
        public void GetAll_ValidArguments_NoRecord_Exist()
        {
            //Arrange
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var fakeSchedule = new ScheduleResponse
            {
                Key = mockGuid
            };
            var scheduleList = new List<ScheduleResponse>();
            scheduleList.Add(fakeSchedule);
            _mockScheduleBusiness.Setup(x => x.GetSchedules(It.IsAny<Guid>())).ReturnsAsync(scheduleList);

            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey),
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = "test",
            };
            var routingList = new List<RoutingRule>();
            routingList.Add(fakeRoutingRule);
            routingList.AsEnumerable();
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRules(mockGuid, 0, 0, "")).ReturnsAsync(routingList);
            //Act
            var response = _routingRuleManager.GetAllRoutingRule();
            //Assert            
            Assert.Equal(0,response.Count());
        }

        [Fact]
        public void GetByID_ValidArguments_ShouldReturnRoutingRules()
        {
            //Arrange
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var mockRuleName = "Test";
            
            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey),
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = mockRuleName,
                SearchCriteriaGranularityLevel = 1,
            };
            
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid, Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey))).ReturnsAsync(fakeRoutingRule);
            //Act
            var response = _routingRuleManager.GetByID(mockRoutingRuleGuid);
            //Assert            
            Assert.Contains(mockRuleName, response.RoutingRuleName);
        }

        [Fact]
        public void GetByID_ValidArguments_NoRecord_Exist()
        {
            //Arrange
            var mockGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRuleName = "Test";

            var mockRoutingRuleGuid = new Guid();
            var fakeRoutingRule = new RoutingRule
            {
                FacilityKey = mockFacilityGuid,
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = mockRuleName,
                SearchCriteriaGranularityLevel = 1,
            };

            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid, mockFacilityGuid)).Returns(Task.FromResult<RoutingRule>(null));
            //Act
            var response = _routingRuleManager.GetByID(mockRoutingRuleGuid);
            //Assert            
            Assert.Null(response);
        }

        [Fact]
        public void AddRoutingRule_ValidArguments_ShouldAddRoutingRules()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid = Utility.GetNewGuid();
            Dictionary<string, string> headers = new Dictionary<string, string>();

            var mockScheduleGuid = Guid.Parse("E767B738-3944-4896-93A0-6F074BA16890");         

            var fakeRountingSchedule = new RequestRoutingRuleSchedule
            {
                ScheduleKey = mockScheduleGuid
            };
            var routingRuleScheduleList = new List<RequestRoutingRuleSchedule>();
            routingRuleScheduleList.Add(fakeRountingSchedule);

            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = "test new@@@@45",
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46"),
                RoutingRuleSchedules = routingRuleScheduleList,
            };
        
            //Act
            var response = _routingRuleManager.AddRoutingRule(routingRuleReq, headers);
            //Assert            
            Assert.Contains("Routing Rule Created Successfully", response.Message);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public void AddRoutingRule_InvalidArguments_ShouldReturnError()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid = Utility.GetNewGuid();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = "test new@@@@45",
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46")
            };

            //Act
            var response = _routingRuleManager.AddRoutingRule(routingRuleReq, headers);
            //Assert            
            Assert.Contains("Please select any schedule or destination or transaction priority.", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void AddRoutingRule_SameRoutingRuleName_ShouldReturnError()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid = Utility.GetNewGuid();
            var mockRoutingRuleName = "test";
            Dictionary<string, string> headers = new Dictionary<string, string>();

            var fakeRoutingRule =Task.FromResult<RoutingRule>(new RoutingRule {
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = mockRoutingRuleName,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = mockRoutingRuleName,
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46")
            };
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleName,mockFacilityGuid)).Returns(fakeRoutingRule);

            //Act
            var response = _routingRuleManager.AddRoutingRule(routingRuleReq, headers);
            //Assert            
            Assert.Contains("Routing Rule With Same Name Already Exist.", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void DeleteRoutingRule_InvalidArgument_ShouldReturnError()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid = Utility.GetNewGuid();
            var mockInvalidRuleID = Utility.GetNewGuid();

            var mockRoutingRuleName = "test";
            Dictionary<string, string> headers = new Dictionary<string, string>();

            var fakeRoutingRule = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = mockRoutingRuleName,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockInvalidRuleID, mockFacilityGuid)).Returns(Task.FromResult<RoutingRule>(null));

            //Act
            var response = _routingRuleManager.DeleteRoutingRule(mockInvalidRuleID, headers);
            //Assert            
            Assert.Contains("RountingRuleKey doesn't exist in system", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void DeleteRoutingRule_ValidArgument_ShouldDeleteRoutingRule()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid = Utility.GetNewGuid();
            var mockInvalidRuleID = Utility.GetNewGuid();

            var mockRoutingRuleName = "test";
            Dictionary<string, string> headers = new Dictionary<string, string>();

            var fakeRoutingRule = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid,
                RoutingRuleName = mockRoutingRuleName,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });            
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockInvalidRuleID, mockFacilityGuid)).Returns(fakeRoutingRule);

            //Act
            var response = _routingRuleManager.DeleteRoutingRule(mockInvalidRuleID, headers);
            //Assert            
            Assert.Contains("Routing Rule Deleted Successfully", response.Message);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public void UpdateRoutingRule_SameRoutingRuleName_ShouldReturnError()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid1 = Utility.GetNewGuid();
            var mockRoutingRuleName1 = "test";
            var mockRoutingRuleGuid2= Utility.GetNewGuid();
            var mockRoutingRuleName2= "test new";

            var mockScheduleGuid1 = Utility.GetNewGuid();
            var mockScheduleGuid2 = Utility.GetNewGuid();

            Dictionary<string, string> headers = new Dictionary<string, string>();

            var fakeRoutingRule1 = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid1,
                RoutingRuleName = mockRoutingRuleName1,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            var fakeRoutingRule2 = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid2,
                RoutingRuleName = mockRoutingRuleName2,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            var fakeSchedule1 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid1,
                ScheduleTimingName = "Test Schedule"
            };

            var fakeSchedule2 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid2,
                ScheduleTimingName = "Test Schedule 2"
            };
            var fakeRoutingRuleSchedule = new RoutingRuleScheduleTiming
            {
                RoutingRuleScheduleTimingKey = Utility.GetNewGuid(),
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
                ScheduleTimingKey = mockScheduleGuid1,
                RoutingRuleKey = mockRoutingRuleGuid2,               
            };

            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = mockRoutingRuleName1,
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46")
            };
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleName1, mockFacilityGuid)).Returns(fakeRoutingRule1);
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid2, mockFacilityGuid)).Returns(fakeRoutingRule2);

            //Act
            var response = _routingRuleManager.UpdateRoutingRule(routingRuleReq,mockRoutingRuleGuid2, headers);
            //Assert            
            Assert.Contains("Routing Rule With Same Name Already Exist.", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void UpdateRoutingRule_RequiredParameterMissing_ShouldReturnError()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid1 = Utility.GetNewGuid();
            var mockRoutingRuleName1 = "test";
            var mockRoutingRuleGuid2 = Utility.GetNewGuid();
            var mockRoutingRuleName2 = "test new";

            var mockScheduleGuid1 = Utility.GetNewGuid();
            var mockScheduleGuid2 = Utility.GetNewGuid();

            Dictionary<string, string> headers = new Dictionary<string, string>();

            var fakeRoutingRule1 = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid1,
                RoutingRuleName = mockRoutingRuleName1,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            var fakeRoutingRule2 = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid2,
                RoutingRuleName = mockRoutingRuleName2,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            var fakeSchedule1 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid1,
                ScheduleTimingName = "Test Schedule"
            };

            var fakeSchedule2 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid2,
                ScheduleTimingName = "Test Schedule 2"
            };
            var fakeRoutingRuleSchedule = new RoutingRuleScheduleTiming
            {
                RoutingRuleScheduleTimingKey = Utility.GetNewGuid(),
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
                ScheduleTimingKey = mockScheduleGuid1,
                RoutingRuleKey = mockRoutingRuleGuid2,
            };

            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = mockRoutingRuleName2,
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46")
            };
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleName1, mockFacilityGuid)).Returns(fakeRoutingRule1);
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid2, mockFacilityGuid)).Returns(fakeRoutingRule2);

            //Act
            var response = _routingRuleManager.UpdateRoutingRule(routingRuleReq, mockRoutingRuleGuid2, headers);
            //Assert            
            Assert.Contains("Please select any schedule or destination or transaction priority.", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void UpdateRoutingRule_RoutingRuleNotFound_ShouldReturnError()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid1 = Utility.GetNewGuid();
            var mockRoutingRuleName1 = "test";
            var mockRoutingRuleGuid2 = Utility.GetNewGuid();
            var mockRoutingRuleName2 = "test new";

            var mockScheduleGuid1 = Utility.GetNewGuid();
            var mockScheduleGuid2 = Utility.GetNewGuid();

            Dictionary<string, string> headers = new Dictionary<string, string>();

            var fakeRoutingRule1 = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid1,
                RoutingRuleName = mockRoutingRuleName1,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            var fakeSchedule1 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid1,
                ScheduleTimingName = "Test Schedule"
            };

            var fakeSchedule2 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid2,
                ScheduleTimingName = "Test Schedule 2"
            };
            var fakeRoutingRuleSchedule = new RoutingRuleScheduleTiming
            {
                RoutingRuleScheduleTimingKey = Utility.GetNewGuid(),
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
                ScheduleTimingKey = mockScheduleGuid1,
                RoutingRuleKey = mockRoutingRuleGuid2,
            };

            var fakeRountingSchedule = new RequestRoutingRuleSchedule
            {
                ScheduleKey = mockScheduleGuid2
            };
            var routingRuleScheduleList = new List<RequestRoutingRuleSchedule>();
            routingRuleScheduleList.Add(fakeRountingSchedule);


            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = mockRoutingRuleName2,
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46"),
                RoutingRuleSchedules = routingRuleScheduleList,
            };
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleName1, mockFacilityGuid)).Returns(fakeRoutingRule1);
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid2, mockFacilityGuid)).Returns(Task.FromResult<RoutingRule>(null));

            //Act
            var response = _routingRuleManager.UpdateRoutingRule(routingRuleReq, mockRoutingRuleGuid2, headers);
            //Assert            
            Assert.Contains("Routing Rule with RountingRuleKey :" + mockRoutingRuleGuid2 + " not exist.", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void UpdateRoutingRule_ValidArgument_ShouldUpdateRoutingRule()
        {
            //Arrange
            var mockFacilityGuid = Utility.ParseStringToGuid(_executionContextAccessor.Current.Facility.FacilityKey);
            var mockRoutingRuleGuid1 = Utility.GetNewGuid();
            var mockRoutingRuleName1 = "test";
            var mockRoutingRuleGuid2 = Utility.GetNewGuid();
            var mockRoutingRuleName2 = "test new";

            var mockScheduleGuid1 = Utility.GetNewGuid();
            var mockScheduleGuid2 = Utility.GetNewGuid();

            Dictionary<string, string> headers = new Dictionary<string, string>();

            var fakeSchedule1 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid1,
                ScheduleTimingName = "Test Schedule"
            };

            var fakeSchedule2 = new ScheduleTiming
            {
                ScheduleTimingKey = mockScheduleGuid2,
                ScheduleTimingName = "Test Schedule 2"
            };
            var fakeRoutingRuleSchedule = new RoutingRuleScheduleTiming
            {
                RoutingRuleScheduleTimingKey = Utility.GetNewGuid(),
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
                ScheduleTimingKey = mockScheduleGuid1,
                RoutingRuleKey = mockRoutingRuleGuid2,
            };

            var fakeCollectionRoutingRuleSchedule = new List<RoutingRuleScheduleTiming>();
            fakeCollectionRoutingRuleSchedule.Add(fakeRoutingRuleSchedule);

            var fakeRoutingRule1 = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid1,
                RoutingRuleName = mockRoutingRuleName1,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
            });

            var fakeRoutingRule2 = Task.FromResult<RoutingRule>(new RoutingRule
            {
                RoutingRuleKey = mockRoutingRuleGuid2,
                RoutingRuleName = mockRoutingRuleName2,
                FacilityKey = mockFacilityGuid,
                TenantKey = Utility.ParseStringToGuid(_executionContextAccessor.Current.Tenant.TenantKey),
                RoutingRuleScheduleTiming = fakeCollectionRoutingRuleSchedule
            });


            var fakeRountingSchedule = new RequestRoutingRuleSchedule
            {
                ScheduleKey = mockScheduleGuid2
            };
            var routingRuleScheduleList = new List<RequestRoutingRuleSchedule>();
            routingRuleScheduleList.Add(fakeRountingSchedule);


            var routingRuleReq = new RoutingRuleRequest
            {
                RoutingRuleName = mockRoutingRuleName2,
                SearchCriteriaGranularityLevel = 1,
                ActorKey = Guid.Parse("6DD7B978-2BAE-4C0B-9ED2-0A79FB689A46"),
                RoutingRuleSchedules = routingRuleScheduleList,
            };
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleName1, mockFacilityGuid)).Returns(fakeRoutingRule1);
            _mockRoutingRuleRepository.Setup(x => x.GetRoutingRule(mockRoutingRuleGuid2, mockFacilityGuid)).Returns(fakeRoutingRule2);

            //Act
            var response = _routingRuleManager.UpdateRoutingRule(routingRuleReq, mockRoutingRuleGuid2, headers);
            //Assert            
            Assert.Contains("Routing Rule Updated Successfully", response.Message);
            Assert.True(response.IsSuccess);
        }

    }
}
