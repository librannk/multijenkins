using System.Net.Http;

namespace BD.Core.ResiliencePolicy
{
    /// <summary>
    /// Class IdentityService
    /// </summary>
    public class HttpClientFactory
    {
        /// <summary>
        /// DI for initialization
        /// </summary>
        public HttpClientFactory()
        {
           
        }

        /// <summary>
        /// acheiving DI for initialization
        /// </summary>
        /// <param name="client"></param>
        public HttpClientFactory(HttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// getter property of HttpClient type
        /// </summary>
        public HttpClient Client { get; }
    }
}
