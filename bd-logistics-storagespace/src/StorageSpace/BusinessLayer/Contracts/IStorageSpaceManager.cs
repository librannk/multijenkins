using System.Threading.Tasks;
using StorageSpace.API.IntegrationEvents.Events;

namespace StorageSpace.API.BusinessLayer.Contracts
{
    /// <summary> IStorageSpaceManager </summary>
    public interface IStorageSpaceManager
    {
        #region Methods
        /// <summary> GetStorageSpaces </summary>
        /// <param name="request">GetStorageSpaceRequest</param>
        /// <returns></returns>
        Task GetStorageSpaces(StorageSpaceRequestEvent request);
        #endregion
    }
}
