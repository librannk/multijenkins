using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionQueue.Ingestion.BusinessLayer.Repository;
using TransactionQueue.Shared.DataAccess.Mongo;
using TransactionQueue.Shared.DataAccess.Mongo.Clients;

namespace TransactionQueue.Ingestion.Infrastructure.Infrastructure.Repository
{
    /// <summary>
    /// This class handles the Facility mongo db operations
    /// </summary>
    public class LastAduXrefMongoRepository : BaseRepository<DBModel.LastAduXref>, ILastAduXrefRepository
    {
        #region Constructors

        /// <summary> Initializes instances </summary>
        /// <param name="dataContext"></param>
        public LastAduXrefMongoRepository(MongoDbClient dataContext) : base(dataContext)
        {
        }

        #endregion Constructors

        #region Data Operations

        /// <summary>
        /// Get all LastAduXref record from DB based.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DBModel.LastAduXref>> GetAllLastAduXrefTransactions()
        {
            var obj = this as ILastAduXrefRepository;
            return (await obj.GetAllAsync());
        }

        #endregion
    }
}
