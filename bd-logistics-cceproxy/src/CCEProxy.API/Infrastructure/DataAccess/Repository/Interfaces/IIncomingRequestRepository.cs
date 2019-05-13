using CCEProxy.API.Infrastructure.DataAccess.Mongo.Contracts;

namespace CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces
{
    /// <summary>
    /// Incoming Request Repository
    /// </summary>
    public interface IIncomingRequestRepository : IBaseRepository<DBModel.IncomingRequest>
    {
    }
}
