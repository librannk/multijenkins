using BD.Core.ElasticClient.Mongo;
using CCEProxy.API.Infrastructure.DataAccess.Mongo.Clients;
using CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces;

namespace CCEProxy.API.Infrastructure.DataAccess.Repository
{
    /// <summary>
    /// This class handles the IncomingRequest db operations
    /// </summary>
    public class IncomingRequestRepository : BaseRepository<DBModel.IncomingRequest>, IIncomingRequestRepository
    {
        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public IncomingRequestRepository(ElasticDbContext dataContext) : base(dataContext)
        {
        }

        #endregion Constructors
    }
}
