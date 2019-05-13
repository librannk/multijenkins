using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BD.Core.Context;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SiteConfiguration.API.AutoMapper;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Business;
using SiteConfiguration.API.Schedule.Exceptions;
using Xunit;
using SiteConfiguration.API.Schedule.Models;

namespace Test.Schedules
{
    public class ScheduleBusinessUnitTest
    {
        #region PrivateFields
        private readonly Mock<IScheduleRepository> _mockScheduleRepository;
        private readonly Mock<IRoutingRuleScheduleTimingRepository> _mockRoutingRuleScheduleTimingRepository;
        private ScheduleBusiness _scheduleBusiness;
        private readonly Mock<IUnitOfWork> _unitofwork;
        private readonly IMapper _mapper;
        #endregion

        public ScheduleBusinessUnitTest()
        {
            _mockScheduleRepository = new Mock<IScheduleRepository>();
            _mockRoutingRuleScheduleTimingRepository = new Mock<IRoutingRuleScheduleTimingRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _unitofwork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void GetSchedule_ValidFacilityKey_ShouldReturnMappedScheduleData()
        {
            //Arrange
            var mockFacilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            var data = FakeData();

            _mockScheduleRepository.Setup(x => x.GetSchedules(data.FirstOrDefault().FacilityKey)).ReturnsAsync(data);
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object,_mockRoutingRuleScheduleTimingRepository.Object,_unitofwork.Object,_mapper);

            //Act
            var res = _scheduleBusiness.GetSchedules(mockFacilityKey);

            //Assert
            Assert.Equal(data.First().ScheduleTimingName, res.Result.First().Name);
        }

        [Fact]
        public void GetScheduleByKey_ValidFacilityKey_ShouldReturnMappedScheduleData()
        {
            //Arrange
            var mockScheduleTimingKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            var data = FakeData();

            _mockScheduleRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(data.FirstOrDefault());
            _mockRoutingRuleScheduleTimingRepository.Setup(x => x.GetRoutingRuleScheduleTiming(It.IsAny<Guid>())).ReturnsAsync(true);
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object, _mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);

            //Act
            var res = _scheduleBusiness.GetScheduleByKey(mockScheduleTimingKey);

            //Assert
            Assert.Equal(data.First().ScheduleTimingName, res.Result.Name);
        }

        [Fact]
        public async Task PostSchedule_ValidFacilityKey_ShouldReturnAcceptedCode()
        {
            //Arrange
            var mockFacilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            var requestData = FakeDataForPostSchedule();
            
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object,_mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);

            //Act
            await _scheduleBusiness.AddSchedule(mockFacilityKey, requestData);
            
            //Assert
            _mockScheduleRepository.Verify(mock => mock.AddAsync(It.IsAny<ScheduleTiming>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSchedule_ValidFacilityKey_ShouldReturnOkCode()
        {
            //Arrange
            var mockFacilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            var requestData = FakeDataForPostSchedule();
            var data = FakeData();

            _mockScheduleRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(data.FirstOrDefault());
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object, _mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);

            //Act
            await _scheduleBusiness.UpdateSchedule(mockFacilityKey,mockFacilityKey,requestData);

            //Assert
            _mockScheduleRepository.Verify(mock => mock.Update(It.IsAny<ScheduleTiming>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSchedule_ValidFacilityKey_ShouldReturninvalidScheduleException()
        {
            //Arrange
            var mockFacilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            var requestData = FakeDataForPostSchedule();

            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object, _mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);
            Exception expected = new InvalidScheduleException("",4001);
            try
            {
                //Act
                await _scheduleBusiness.UpdateSchedule(mockFacilityKey, mockFacilityKey, requestData);
            }
            catch (Exception actual)
            {
                Assert.Equal(4001,expected.HResult);
            }

           
        }

        [Fact]
        public async Task UpdateSchedule_ValidFacilityKey_ShouldReturninvalidScheduleExceptionforunmatchId()
        {
            //Arrange
            var mockFirstKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E4");
            var mockSecondKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E9");
            var data = FakeData();
            var requestData = FakeDataForPostSchedule();

            _mockScheduleRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(data.FirstOrDefault());
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object, _mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);
            Exception expected = new InvalidScheduleException("", 4003);
            try
            {
                //Act
                await _scheduleBusiness.UpdateSchedule(mockSecondKey, mockFirstKey, requestData);
            }
            catch (Exception actual)
            {
                Assert.Equal(4003, expected.HResult);
            }


        }

        [Fact]
        public async Task DeleteSchedule_ValidKey_ShouldReturnOkCode()
        {
            //Arrange
            var mockKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            var data = FakeData();

            _mockScheduleRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(data.FirstOrDefault);
            _mockRoutingRuleScheduleTimingRepository.Setup(x => x.GetRoutingRuleScheduleTiming(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object, _mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);

            //Act
            await _scheduleBusiness.DeleteSchedule(mockKey);

            //Assert
            _mockScheduleRepository.Verify(mock => mock.Delete(It.IsAny<ScheduleTiming>()), Times.Once);
        }

        [Fact]
        public async Task DeleteSchedule_ValidKey_ShouldReturnInvalidScheduleException()
        {
            //Arrange
            var mockKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            var data = FakeData();

            _mockScheduleRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(data.FirstOrDefault);
            _mockRoutingRuleScheduleTimingRepository.Setup(x => x.GetRoutingRuleScheduleTiming(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object, _mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);

            Exception expected = new InvalidScheduleException("", 4004);
            try
            {
                //Act
                await _scheduleBusiness.DeleteSchedule(mockKey);
            }
            catch (Exception e)
            {
                Assert.Equal(4004, expected.HResult);
            }
        }

        [Fact]
        public async Task DeleteSchedule_NullResult_ShouldReturnInvalidScheduleException()
        {
            //Arrange
            var mockKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            
            _scheduleBusiness = new ScheduleBusiness(_mockScheduleRepository.Object, _mockRoutingRuleScheduleTimingRepository.Object, _unitofwork.Object, _mapper);

            Exception expected = new InvalidScheduleException("", 4002);
            try
            {
                //Act
                await _scheduleBusiness.DeleteSchedule(mockKey);
            }
            catch (Exception e)
            {
                Assert.Equal(4002, expected.HResult);
            }
        }

        #region private method

        /// <summary>
        /// Mock Schedule request
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ScheduleTiming> FakeData()
        {
            ScheduleTiming s = new ScheduleTiming();
            s.FacilityKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            s.EndMinutes = 825;
            s.ScheduleTimingKey = Guid.Parse("BEC05D78-2F6C-4034-8FB9-ACE3417F83E8");
            s.ScheduleTimingName = "test";
            s.StartMinutes = 885;
            s.FridayFlag = true;
            s.MondayFlag = true;
            s.SaturdayFlag = true;
            s.SundayFlag = true;
            s.ThursdayFlag = true;
            s.TuesdayFlag = false;
            s.WednesdayFlag = false;

            List<ScheduleTiming> sc = new List<ScheduleTiming>();
            sc.Add(s);
            return sc.AsEnumerable();
        }

        /// <summary>
        /// Mock Schedule request
        /// </summary>
        /// <returns></returns>
        private ScheduleRequest FakeDataForPostSchedule()
        {
            ScheduleRequest s = new ScheduleRequest();
            s.Name = "Test";
            s.ScheduleDays = new List<ScheduleDays>(){ScheduleDays.Monday};
            s.StartTime = TimeSpan.FromMinutes(800);
            s.EndTime = TimeSpan.FromMinutes(850);

            return s;
        }

        #endregion
    }
}
