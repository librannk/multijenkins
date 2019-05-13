using System.Linq;
using System.Threading.Tasks;
using StorageSpace.API.Model;
using StorageSpace.API.Model.Request;
using StorageSpace.API.Infrastructure.DataAccess.Mongo;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Clients;
using StorageSpace.API.Infrastructure.Repository.Contracts;
using BD.Core.ElasticClient.Mongo;

namespace StorageSpace.API.Infrastructure.Repository.Concrete
{
    /// <summary> StorageSpaceMongoRepository </summary>
    internal class StorageSpaceDetailMongoRepository : BaseRepository<FormularyLocationDetail>, IStorageSpaceDetailMongoRepository
    {
        public StorageSpaceDetailMongoRepository(MongoDbClient mongoClient)
            : base(mongoClient)
        {

        }

        public async Task<FormularyLocationDetail> GetStorageSpaces(GetStorageSpaceRequest request)
        {
            var collections = await (this as IStorageSpaceDetailMongoRepository).GetAllAsync();

            return collections.FirstOrDefault(collection => collection.FormularyId == request.FormularyId);
        }
    }
}
