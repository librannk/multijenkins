using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PactConsumerTemplate.PactTest
{
    public class ConsumerApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; }
        public IMockProviderService MockProviderService { get; }

        public int MockServerPort => 9222;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        public ConsumerApiPact()
        {
            PactBuilder = new PactBuilder(new PactConfig
            {
                SpecificationVersion = "2.0.0",
                //LogDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}",
                //PactDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}"
                LogDir = $"D:/Demo/Pact/pact_log",
                PactDir = $"D:/Demo/Pact/pacts"
            })
                .ServiceConsumer("Customer API Consumer")
                .HasPactWith("Customer API");

            MockProviderService = PactBuilder.MockService(MockServerPort, false, IPAddress.Any);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}
