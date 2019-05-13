using System.Net.Http;

namespace CCEProxy.API.Infrastructure.Services
{

    /// <summary>
    /// Identity Service class
    /// </summary>
    public class IdentityService
    {
        /// <summary>
        /// Constructor class
        /// </summary>
        /// <param name="client"></param>
        public IdentityService(HttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// HttpClient property
        /// </summary>
        public HttpClient Client { get; }
    }
}
