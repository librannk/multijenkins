using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUsageApp.Infrastructure.EFRepository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Facility
    {
        /// <summary>
        /// Database context
        /// </summary>
        public DbContext _context { get; set; }

        internal DbSet<TEntity> DbSet { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(DbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Add new entity to DB.
        /// </summary>
        /// <param name="entity">Entity to Add in DB</param>
        public  virtual void Add(TEntity entity)
        {

            DbSet.Add(entity);

        }
        /// <summary>
        /// Get Entity by the Id of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="Id">Id field of any Entity</param>
        /// <returns></returns>
        public TEntity Get(int Id)
        {
            return DbSet.Find(Id);
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
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
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
