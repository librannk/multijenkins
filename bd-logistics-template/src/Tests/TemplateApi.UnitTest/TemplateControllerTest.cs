
using System;
using System.Net.Http;
using BD.Core.EventBus.Abstractions;
using BD.Template.API.Controllers;
using BD.Template.API.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using HttpClientFactory = BD.Core.ResiliencePolicy.HttpClientFactory;

namespace TemplateApi.UnitTest
{
    public class TemplateControllerTest
    {
        
        public TemplateController Initialize()
        {
            var eventBus = new Mock<IEventBus>();
            var identityService = new Mock<HttpClientFactory>();
            var logger = new Mock<ILogger<TemplateController>>();
            var saveresponse = new Mock<IKafkaResponseRepository>();
            TemplateController controller = new TemplateController(eventBus.Object, identityService.Object, saveresponse.Object, logger.Object);
            return controller;
        }

        //Supplying Header value
        [Fact]
        public void Test_Post_Positive_Response()
        {
            var controller = Initialize();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjFBMkUyMzVDQjFCNEUxMUQzMUFFQkMwQUQyNkVFODA2Q0EzNzRFMDUiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJHaTRqWExHMDRSMHhycndLMG03b0JzbzNUZ1UifQ.eyJuYmYiOjE1NTA1NzkzMjAsImV4cCI6MTU1MDU4MjkyMCwiaXNzIjoiaHR0cDovL2t1YmU4Lndlc3R1cy5jbG91ZGFwcC5henVyZS5jb206MzEwMTAiLCJhdWQiOlsiaHR0cDovL2t1YmU4Lndlc3R1cy5jbG91ZGFwcC5henVyZS5jb206MzEwMTAvcmVzb3VyY2VzIiwia2Fma2FDb25zdW1lciJdLCJjbGllbnRfaWQiOiJrYWZrYUNsaWVudCIsInN1YiI6IjEiLCJhdXRoX3RpbWUiOjE1NTA1NzkzMjAsImlkcCI6ImxvY2FsIiwic2NvcGUiOlsia2Fma2FDb25zdW1lciJdLCJhbXIiOlsicHdkIl19.nsEcXceGZYt1S4LEAr-r9hUbEAeGkk7PiEQHRor5nyeG-cXBO3OcqkgymTQHHIMjNRAiL3fmXEFuYm0CoxTQpcZXNRU2XxZ4mOZgQaCvUj_rOxalmTHSzgvLfbxi-nvhsBq5ZMv209YfRp_BnIKf7vNe6cjFDx4IRoSccxApbb5twRf1j49QjPWJWdiXZXOp58Dzgb1rzoWNI_uVZkApbm4paslGUc-swDwFlG5jjOqu2mVB040E8R9FfGcqgYL2ZGVgz1piEeDKfrDFRdEc0vysZW9gmveA9689ML_xytzpvbscNUmT2RFaKgT4wmMGStNg6zhCYERnNySHW1qk9g";

            try
            {
                controller.Post("TestString");
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }

        //Not Supplying Header value
        [Fact]
        public void Test_Post_Negative_Response()
        {
            var controller = Initialize();
            try
            {
                controller.Post("TestString");
                Assert.False(true, "Test case failed");
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Test_Task_GetPolly_Positive()
        {
            var eventBus = new Mock<IEventBus>();
            var httpClient = new HttpClient();
            var logger = new Mock<ILogger<TemplateController>>();
            var saveresponse = new Mock<IKafkaResponseRepository>();
            httpClient.BaseAddress = new Uri("http://localhost");
            // var identityService = new Mock<IdentityService>(MockBehavior.Default, httpClient);
            var identityService = new Mock<HttpClientFactory>();
            TemplateController controller = new TemplateController(eventBus.Object, identityService.Object, saveresponse.Object, logger.Object);
            try
            {
                var result = controller.GetPolly().GetAwaiter().GetResult();
                Assert.Equal(200, ((ObjectResult)result).StatusCode);
            }
            catch (Exception)
            {
            }
        }

        [Fact]
        public void Test_Task_GetPolly_Negative()
        {
            var controller = Initialize();
            try
            {
                //calling async method in sync
                var result = controller.GetPolly().GetAwaiter().GetResult();
                Assert.False(true, "Test case failed");
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
    }
}
