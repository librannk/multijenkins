using TransactionQueue.Shared.DataAccess.Mongo.Clients;
using TransactionQueue.Shared.DataAccess.Mongo.Contracts;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TransactionQueue.Shared.DataAccess.Mongo
{
    /// <summary>
    /// Base repository for CRUD operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        /// <summary>
        /// Context variable
        /// </summary>
        protected IMongoDatabase _Context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mongoClient"></param>
        public BaseRepository(MongoDbClient mongoClient)
        {
            _Context = mongoClient.GetContext();
        }

        /// <summary>
        /// Function to get the collection name
        /// </summary>
        /// <typeparam name="TDoc"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> CollectionName<TDoc>()
        {
            var collectionName = typeof(TDoc).Name;
            return _Context.GetCollection<T>(collectionName);
        }

        #region SyncMethods

        /// <summary>
        /// To get the document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T IBaseRepository<T>.GetById(string id)
        {
            var _collection = CollectionName<T>();
            var result = _collection.Find<T>(m => m.Id == id);
            return result.First();
        }

        /// <summary>
        /// To get all the documents from a collection
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> IBaseRepository<T>.GetAll()
        {
            var _collection = CollectionName<T>();
            var result = _collection.Find(Builders<T>.Filter.Empty);
            return result.ToList();
        }

        /// <summary>
        /// To insert the document in a specific collection
        /// </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Insert(T entity)
        {
            var _collection = CollectionName<T>();
            _collection.InsertOne(entity);
        }

        /// <summary>
        /// To update the document
        /// </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Update(T entity)
        {
            var _collection = CollectionName<T>();
            _collection.ReplaceOne(e => e.Id == entity.Id, entity);
        }

        /// <summary>
        /// To delete the document
        /// </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Delete(T entity)
        {
            var _collection = CollectionName<T>();
            var filter = Builders<T>.Filter.Eq(nameof(entity.Id), entity.Id);
            _collection.DeleteOne(filter);
        }

        /// <summary>
        /// To delete the document
        /// </summary>
        void IBaseRepository<T>.DeleteAll()
        {
            var _collection = CollectionName<T>();
            _collection.DeleteMany(Builders<T>.Filter.Empty);
        }

        #endregion

        #region AsyncMethods

        /// <summary>
        /// Async method to get all the document from a collection
        /// </summary>
        /// <returns></returns>
        async Task<List<T>> IBaseRepository<T>.GetAllAsync()
        {
            var _collection = CollectionName<T>();
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// Async method to get document by Id
        /// </summary>
        /// <returns></returns>
        async Task<T> IBaseRepository<T>.GetByIdAsync(string id)
        {
            var _collection = CollectionName<T>();
            var result = await _collection.FindAsync<T>(m => m.Id == id);
            return result.First();
        }

        /// <summary>
        /// To async insert the document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.InsertAsync(T entity)
        {
            var _collection = CollectionName<T>();
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// To async update the document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.UpdateAsync(T entity)
        {
            var _collection = CollectionName<T>();
            await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        /// <summary>
        /// To async delete the document from a collection
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(nameof(entity.Id), entity.Id);
            var _collection = CollectionName<T>();
            await _collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// To async delete all the document from a collection
        /// </summary>
        /// <returns></returns>
        async Task IBaseRepository<T>.DeleteAllAsync()
        {
            var _collection = CollectionName<T>();
            await _collection.DeleteManyAsync(Builders<T>.Filter.Empty);
        }

        #endregion
    }
}