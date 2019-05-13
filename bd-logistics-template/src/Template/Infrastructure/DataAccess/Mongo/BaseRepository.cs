using BD.Template.API.Infrastructure.DataAccess.Mongo.Clients;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Contracts;
using BD.Template.API.Infrastructure.DataAccess.Mongo.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BD.Template.API.Infrastructure.DataAccess.Mongo
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
        protected IMongoDatabase _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mongoClient"></param>
        protected BaseRepository(MongoDbClient mongoClient)
        {
            _context = mongoClient.GetContext();
        }

        /// <summary>
        /// Function to get the collection name
        /// </summary>
        /// <typeparam name="TDoc"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> CollectionName<TDoc>()
        {
            var collectionName = typeof(TDoc).Name;
            return _context.GetCollection<T>(collectionName);
        }

        #region SyncMethods

        /// <summary>
        /// To get the document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T IBaseRepository<T>.GetById(string id)
        {
            var collection = CollectionName<T>();
            var result = collection.Find<T>(m => m.Id == id);
            return result.First();
        }

        /// <summary>
        /// To get all the documents from a collection
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> IBaseRepository<T>.GetAll()
        {
            var collection = CollectionName<T>();
            var result = collection.Find(Builders<T>.Filter.Empty);
            return result.ToList();
        }

        /// <summary>
        /// To insert the document in a specific collection
        /// </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Insert(T entity)
        {
            var collection = CollectionName<T>();
            collection.InsertOne(entity);
        }

        /// <summary>
        /// To update the document
        /// </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Update(T entity)
        {
            var collection = CollectionName<T>();
            collection.ReplaceOne(e => e.Id == entity.Id, entity);
        }

        /// <summary>
        /// To delete the document
        /// </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Delete(T entity)
        {
            var collection = CollectionName<T>();
            var filter = Builders<T>.Filter.Eq(nameof(entity.Id), entity.Id);
            collection.DeleteOne(filter);
        }

        /// <summary>
        /// To delete the document
        /// </summary>
        void IBaseRepository<T>.DeleteAll()
        {
            var collection = CollectionName<T>();
            collection.DeleteMany(Builders<T>.Filter.Empty);
        }

        #endregion

        #region AsyncMethods

        /// <summary>
        /// Async method to get all the document from a collection
        /// </summary>
        /// <returns></returns>
        async Task<List<T>> IBaseRepository<T>.GetAllAsync()
        {
            var collection = CollectionName<T>();
            return await collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// To async insert the document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.InsertAsync(T entity)
        {
            var collection = CollectionName<T>();
            await collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// To async update the document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.UpdateAsync(T entity)
        {
            var collection = CollectionName<T>();
            await collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        /// <summary>
        /// To async delete the document from a collection
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task IBaseRepository<T>.DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(nameof(entity.Id), entity.Id);
            var collection = CollectionName<T>();
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// To async delete all the document from a collection
        /// </summary>
        /// <returns></returns>
        async Task IBaseRepository<T>.DeleteAllAsync()
        {
            var collection = CollectionName<T>();
            await collection.DeleteManyAsync(Builders<T>.Filter.Empty);
        }

        #endregion

    }
}
