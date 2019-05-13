using BD.Template.API.Infrastructure.DataAccess.Mongo;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Clients;
using BD.Template.API.Infrastructure.DBModel;
using BD.Template.API.Infrastructure.Repository.Interfaces;

namespace BD.Template.API.Infrastructure.Repository
{
    /// <summary>
    /// base class of Kafka Response Repository
    /// </summary>
    public class KafkaResponseRepository : BaseRepository<KafkaResponse>, IKafkaResponseRepository
    {


        /// <summary>
        /// KafkaResponseRepository
        /// </summary>
        /// <param name="dataContext"></param>
        public KafkaResponseRepository(MongoDbClient dataContext) : base(dataContext)
        { }
    }
}
