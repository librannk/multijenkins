using System.Net.Http;

namespace TransactionQueue.API.Infrastructure.Services
{
    /// <summary> IdentityService </summary>
    public class IdentityService
    {
        #region Auto-Properties
        /// <summary> Client </summary>
        public HttpClient Client { get; }
        #endregion

        #region Constructors
        /// <summary> Initializes instances </summary>
        /// <param name="client"></param>
        public IdentityService(HttpClient client)
        {
            Client = client;
        }
        #endregion
    }
}
