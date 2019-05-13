using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;
using TransactionQueue.Shared.DataAccess.Mongo;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;

namespace TransactionQueue.ExternalDependencies.Infrastructure.Repository
{
    /// <summary>
    /// This class handles the Facility mongo db operations
    /// </summary>
    public class DestinationMongoRepository : BaseRepository<DBModel.Destination>, IDestinationRepository
    {
        #region Private Fields
        private readonly IMongoCollection<DBModel.Destination> _collection;
        #endregion

        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public DestinationMongoRepository(MongoDbClient dataContext) : base(dataContext)
        {
            _collection = CollectionName<DBModel.Destination>();
        }

        #endregion Constructors

        #region Data Operations

        /// <summary>
        /// Get Destination record from DB based on DestinationCode.
        /// </summary>
        /// <param name="destinationCode">DestinationCode</param>
        /// <returns></returns>
        public async Task<DBModel.Destination> GetDestinationByCode(string destinationCode)
        {
            var result = await _collection.FindAsync(m => m.DestinationCode == destinationCode);
            return result.FirstOrDefault();
        }

        #endregion
    }
}
