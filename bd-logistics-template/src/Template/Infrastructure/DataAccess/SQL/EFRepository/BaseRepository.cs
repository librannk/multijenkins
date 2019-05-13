﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BD.Template.API.Infrastructure.DataAccess.SQL.EFRepository
{

    /// <summary>
    /// It's  abstract class which implements all the methods of IRepository
    /// </summary>
    /// <typeparam name="TEntity"> For any type of class which is inherited from BaseEntity</typeparam>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public DbContext Context { get; set; }
        internal DbSet<TEntity> DbSet { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Add new entity to DB.
        /// </summary>
        /// <param name="entity">Entity to Add in DB</param>
        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);

        }
        /// <summary>
        /// Get Entity by the Id of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="id">Id field of any Entity</param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// To delete a particular entity on DB.
        /// </summary>
        /// <param name="entity">Entity to Delete</param>
        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);

        }
        /// <summary>
        /// Get all entity from DB
        /// </summary>
        /// <returns>Retruns List of Entities.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Update Entity to DB 
        /// </summary>
        /// <param name="entity">Entity to Update</param>
        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);

        }
    }
}
