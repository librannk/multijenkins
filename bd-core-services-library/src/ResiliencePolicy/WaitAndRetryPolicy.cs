
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using Polly.Timeout;
using System;
using System.Net;


namespace BD.Core.ResiliencePolicy
{
    public class WaitAndRetryPolicy
    {
        public static void WaitAndRetry(ref IPolicyRegistry<string> registry, int retryCount = 5) =>
               registry.Add("WaitAndRetry",
                               HttpPolicyExtensions
                               .HandleTransientHttpError()
                               .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                               .Or<TimeoutRejectedException>()
                               .WaitAndRetryAsync(retryCount, retryAttempt
                                   => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
    }
}
