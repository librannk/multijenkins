using BD.Template.API.Infrastructure.DataAccess.Mongo.Contracts;
using BD.Template.API.Infrastructure.DBModel;

namespace BD.Template.API.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// IMongoRepository interface
    /// </summary>
    public interface IMongoRepository:IBaseRepository<User>
    {
    }
}
