using BD.Template.API.Infrastructure.DataAccess.Mongo;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Clients;
using BD.Template.API.Infrastructure.DBModel;
using BD.Template.API.Infrastructure.Repository.Interfaces;

namespace Logistics.Services.Template.API.Infrastructure.Repository
{
    /// <summary>
    /// mongo repository class
    /// </summary>
    public class MongoRepository: BaseRepository<User>, IMongoRepository
    {


        /// <summary>
        /// MongoRepository
        /// </summary>
        /// <param name="dataContext"></param>
        public MongoRepository(MongoDbClient dataContext) : base(dataContext)
        {
        }

       
    }
}
