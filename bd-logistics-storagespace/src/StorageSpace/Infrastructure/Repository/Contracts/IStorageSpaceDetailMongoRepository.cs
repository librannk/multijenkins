using System.Threading.Tasks;
using StorageSpace.API.Model;
using StorageSpace.API.Model.Request;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Contracts;

namespace StorageSpace.API.Infrastructure.Repository.Contracts
{
    /// <summary> IStorageSpaceDetailMongoRepository </summary>
    public interface IStorageSpaceDetailMongoRepository : IBaseRepository<FormularyLocationDetail>
    {
        /// <summary> Gets formulary locations based on the request </summary>
        /// <param name="request">GetStorageSpaceRequest</param>
        /// <returns></returns>
        Task<FormularyLocationDetail> GetStorageSpaces(GetStorageSpaceRequest request);
    }
}
