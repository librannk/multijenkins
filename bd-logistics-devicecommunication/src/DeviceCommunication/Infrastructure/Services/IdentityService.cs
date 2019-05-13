using System.Net.Http;

namespace Logistics.Services.DeviceCommunication.API.Infrastructure.Services
{

    /// <summary>
    /// class IdentityService
    /// </summary>
    public class IdentityService
    {
        /// <summary>
        /// IdentityService constructor
        /// </summary>
        /// <param name="client"></param>
        public IdentityService(HttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// read HttpClient object
        /// </summary>
        public HttpClient Client { get; }
    }
}
