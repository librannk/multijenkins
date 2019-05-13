using Polly;
using Polly.Registry;
using System.Net.Http;

namespace BD.Core.ResiliencePolicy
{
    public class NoOpPolicyClass
    {
        public static void NoOpPolicy(ref IPolicyRegistry<string> registry) =>
                      registry.Add("NoOp", Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>());
    }
}
