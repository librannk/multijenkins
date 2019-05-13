using PactConsumerTemplate.API.Controllers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PactConsumerTemplate.PactTest
{
    public class CustomerApiConsumerTests : IClassFixture<ConsumerApiPact>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        public CustomerApiConsumerTests(ConsumerApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
            _mockProviderService.ClearInteractions();
        }

        [Fact]
        public void GetCustomerById_WithNoAuthorizationToken_ShouldFail()
        {
            int customerId = 20;
            //Arrange
            _mockProviderService.Given(String.Format("there is a customer with id '{0}'", customerId))
                .UponReceiving(String.Format("a request to retrieve customer with id '{0}' with no authorization", customerId))
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = String.Format("/template/api/v1/customer/{0}", customerId)
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 401
                });

            var consumer = new CustomerController(_mockProviderServiceBaseUri);

            //Act //Assert
            Assert.ThrowsAny<Exception>(() => consumer.GetCustomerById(customerId));

            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void GetCustomerById_WhenTheCustomerExists_ReturnsCustomer()
        {
            int customerId = 20;
            var testAuthToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjFBMkUyMzVDQjFCNEUxMUQzMUFFQkMwQUQyNkVFODA2Q0EzNzRFMDUiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJHaTRqWExHMDRSMHhycndLMG03b0JzbzNUZ1UifQ.eyJuYmYiOjE1NTQxMTIwMTgsImV4cCI6MTU1NDExNTYxOCwiaXNzIjoiaHR0cDovL2t1YmU4Lndlc3R1cy5jbG91ZGFwcC5henVyZS5jb206MzEwMTAiLCJhdWQiOlsiaHR0cDovL2t1YmU4Lndlc3R1cy5jbG91ZGFwcC5henVyZS5jb206MzEwMTAvcmVzb3VyY2VzIiwia2Fma2FDb25zdW1lciJdLCJjbGllbnRfaWQiOiJrYWZrYUNsaWVudCIsInN1YiI6IjEiLCJhdXRoX3RpbWUiOjE1NTQxMTIwMTgsImlkcCI6ImxvY2FsIiwic2NvcGUiOlsia2Fma2FDb25zdW1lciJdLCJhbXIiOlsicHdkIl19.YP9CNDK6REs8d9ga3D9phegOtNXv8h2q82cJXUrv53klUJw4a38HORgCiLVItJRTJPBGHPrJQFzOMoebOeOgDZWbnqSOhXBbxjlijY9_tdKPhh__0F5QPfHOcR39X_lmnfyiIiXPcMEc1h_FtdDLQ6km4GtLYx1HBHGvHMR_zuwyEzcSdy8byms3Cf1_U9wnROLCegVwEorTcdWU9atgg9apph42oXe3O1kS_1P6FUkJeTKlVoFfQsLHSrPMt4FB5HcKA68TXgQYaheFeQFcVmJbPrtIt0hKrbQXLul13JwVyFzAVcEBdDoRaANzaoxKep2khdkxtlfhLQWa9z3HLw";
            _mockProviderService.Given(String.Format("there is a customer with id '{0}'", customerId))
                .UponReceiving(String.Format("a request to retrieve customer with id '{0}'", customerId))
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = String.Format("/template/api/v1/customer/{0}", customerId),
                    Headers = new Dictionary<string, object>
                    {
                        { "Authorization", $"Bearer {testAuthToken}" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        name = "Testing string",
                        id = 20
                    }
                });

            var consumer = new CustomerController(_mockProviderServiceBaseUri, testAuthToken);

            //Act
            var result = consumer.GetCustomerById(customerId);

            //Assert
            Assert.Equal(customerId, result.Id);

            _mockProviderService.VerifyInteractions();
        }
    }
}
