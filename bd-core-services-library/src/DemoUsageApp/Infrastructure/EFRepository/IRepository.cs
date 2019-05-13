using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUsageApp.Infrastructure.EFRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get Entity by the Id of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="Id">Id field of any Entity</param>
        /// <returns></returns>
        TEntity Get(int Id);

        /// <summary>
        /// Add new entity to DB.
        /// </summary>
        /// <param name="entity">Entity to Add in DB</param>
        void Add(TEntity entity);

        /// <summary>
        /// Update Entity to DB 
        /// </summary>
        /// <param name="entity">Entity to Update</param>
        void Update(TEntity entity);

        /// <summary>
        /// To delete a particular entity on DB.
        /// </summary>
        /// <param name="entity">Entity to Delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Get all entity from DB
        /// </summary>
        /// <returns>Retruns List of Entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();


    }
}
