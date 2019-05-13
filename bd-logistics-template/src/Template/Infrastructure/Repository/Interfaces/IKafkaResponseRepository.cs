
using BD.Template.API.Infrastructure.DBModel;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Contracts;

namespace BD.Template.API.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Separate Kafka response repository which is responsible to create mongo connection
    /// </summary>
    public interface IKafkaResponseRepository : IBaseRepository<KafkaResponse>
    {
    }
}
