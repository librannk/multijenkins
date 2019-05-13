using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Clients;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Contracts;
using StorageSpace.API.Infrastructure.DataAccess.Mongo.Entities;
using BD.Core.ElasticClient.Mongo;

namespace StorageSpace.API.Infrastructure.DataAccess.Mongo
{
    /// <summary> Base repository for CRUD operations </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        /// <summary> Context variable </summary>
        protected IMongoDatabase _context;

        /// <summary>  </summary>
        /// <param name="mongoClient"></param>
        protected BaseRepository(MongoDbClient mongoClient)
        {
            _context = mongoClient.GetContext();
        }

        /// <summary> Gets the collection name </summary>
        /// <typeparam name="TDoc"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> CollectionName<TDoc>() => _context.GetCollection<T>(typeof(TDoc).Name);

        #region SyncMethods
        /// <summary> To get the document by id </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T IBaseRepository<T>.GetById(string id) => CollectionName<T>().Find(m => m.Id == id).FirstOrDefault();

        /// <summary> To get all the documents from a collection </summary>
        /// <returns></returns>
        IEnumerable<T> IBaseRepository<T>.GetAll() => CollectionName<T>().Find(Builders<T>.Filter.Empty).ToEnumerable();

        /// <summary> To insert the document in a specific collection </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Insert(T entity) => CollectionName<T>().InsertOne(entity);

        /// <summary> To update the document </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Update(T entity) => CollectionName<T>().ReplaceOne(e => e.Id == entity.Id, entity);

        /// <summary> To delete the document </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Delete(T entity)
        {
            var filter = Builders<T>.Filter.Eq(nameof(entity.Id), entity.Id);
            CollectionName<T>().DeleteOne(filter);
        }

        /// <summary> To delete the document </summary>
        void IBaseRepository<T>.DeleteAll() => CollectionName<T>().DeleteMany(Builders<T>.Filter.Empty);
        #endregion

        #region AsyncMethod
        /// <summary> Async method to get all the document from a collection </summary>
        /// <returns></returns>
        async Task<List<T>> IBaseRepository<T>.GetAllAsync() => await CollectionName<T>().Find(Builders<T>.Filter.Empty).ToListAsync();

        /// <summary>
        /// Gets the entity by Id asynchronously.
        /// </summary>
        /// <returns></returns>
        async Task<T> IBaseRepository<T>.GetAsync(string id)
        {
            var collection = CollectionName<T>();
            var result = await collection.FindAsync<T>(m => m.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        /// <summary> To async insert the document </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.InsertAsync(T entity) => await CollectionName<T>().InsertOneAsync(entity);

        /// <summary> To async update the document </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.UpdateAsync(T entity) => await CollectionName<T>().ReplaceOneAsync(e => e.Id == entity.Id, entity);

        /// <summary> To async delete the document from a collection </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(nameof(entity.Id), entity.Id);
            await CollectionName<T>().DeleteOneAsync(filter);
        }

        /// <summary> To async delete all the document from a collection </summary>
        /// <returns></returns>
        async Task IBaseRepository<T>.DeleteAllAsync() => await CollectionName<T>().DeleteManyAsync(Builders<T>.Filter.Empty);
        #endregion
    }
}
