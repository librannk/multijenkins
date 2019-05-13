﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facility.API.Infrastructure.DataAccess.SQL.EFRepository
{
    /// <summary>
    /// Interface creating contract between different Entities.
    /// </summary>
    /// <typeparam name="TEntity"> For any type of Class</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get Entity by the Id of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="id">Id field of any Entity</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Add new entity to DB.
        /// </summary>
        /// <param name="entity">Entity to Add in DB</param>
        Task AddAsync(TEntity entity);

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
        /// <returns>Returns List of Entities.</returns>
        Task<List<TEntity>> GetAllAsync();
    }
}
