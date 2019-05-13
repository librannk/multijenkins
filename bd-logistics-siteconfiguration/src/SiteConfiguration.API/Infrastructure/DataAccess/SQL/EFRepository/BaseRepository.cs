using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiteConfiguration.API.Printers.Models.BuisnessContract;

namespace SiteConfiguration.API.Infrastructure.DataAccess.SQL.EFRepository
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
        internal DbContext Context { get; set; }
        internal DbSet<TEntity> DbSet { get; set; }
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
        public async virtual void Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);

        }

        /// <summary>
        /// Add new entity Async to DB .
        /// </summary>
        /// <param name="entity">Entity to Add in DB</param>
        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);

        }

        /// <summary>
        /// Get Entity by the Id of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="id">Id field of any Entity</param>
        /// <returns></returns>
        public TEntity Get(Guid id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get Entity by the Key of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="key">Key field of any Entity</param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(Guid key)
        {
            return await DbSet.FindAsync(key);
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
        public  virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Get all entity from DB
        /// </summary>
        /// <returns>Retruns List of Entities.</returns>
        public virtual async  Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => DbSet);
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
