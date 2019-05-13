using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactConsumerTemplate.API.Models;

namespace PactConsumerTemplate.API.Controllers
{
    [Route("templateconsumer/api/v1/")]
    [ApiController]
    public class CustomerController : ControllerBase, IDisposable
    {

        private readonly HttpClient _httpClient;

        public CustomerController(string baseUri = null, string authToken = null)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://localhost:49982") };

            if (authToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            }
        }

        public void Dispose()
        {
            Dispose(_httpClient);
        }

        public void Dispose(params IDisposable[] disposables)
        {
            foreach (var disposable in disposables.Where(d => d != null))
            {
                disposable.Dispose();
            }
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        private static void RaiseResponseError(HttpRequestMessage failedRequest, HttpResponseMessage failedResponse)
        {
            throw new HttpRequestException(
                String.Format("The Customer API request for {0} {1} failed. Response Status: {2}, Response Body: {3}",
                failedRequest.Method.ToString().ToUpperInvariant(),
                failedRequest.RequestUri,
                (int)failedResponse.StatusCode,
                failedResponse.Content.ReadAsStringAsync().Result));
        }

        [HttpGet("customer/{id}")]
        public Customer GetCustomerById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, String.Format("/template/api/v1/customer/{0}", id));
            request.Headers.Add("Accept", "application/json");

            var response = _httpClient.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Customer>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                }

                RaiseResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return null;
        }
    }
}