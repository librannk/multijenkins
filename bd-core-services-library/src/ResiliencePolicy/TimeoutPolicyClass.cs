using Polly;
using Polly.Registry;
using Polly.Timeout;
using System;
using System.Net.Http;

namespace BD.Core.ResiliencePolicy
{
    public class TimeoutPolicyClass
    {
        public static void Timeout(ref IPolicyRegistry<string> registry, int seconds = 2) =>
                             registry.Add("TimeOut", Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(seconds), TimeoutStrategy.Pessimistic));
    }
}
