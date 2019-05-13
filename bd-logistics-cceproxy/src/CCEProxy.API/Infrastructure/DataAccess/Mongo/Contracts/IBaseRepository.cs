using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CCEProxy.API.Infrastructure.DataAccess.Mongo.Contracts
{
    /// <summary>
    /// Base repository for CRUD operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// To get the document with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(string id);

        /// <summary>
        /// To get all the documents
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// To insert the new document
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// To update the document
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// To delete the document
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// To delete all the documents
        /// </summary>
        void DeleteAll();


        /// <summary>
        /// To get the document with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(string id);
        /// <summary>
        /// Async method to get all the documents from a collection
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Async method to insert the documents
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Async method to update the document
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// To delete a single document from a collection
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// To async delete all the documents from a collection
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();
    }
}
