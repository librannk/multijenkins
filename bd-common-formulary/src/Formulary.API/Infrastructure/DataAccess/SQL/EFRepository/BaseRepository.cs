using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Formulary.API.Infrastructure.DataAccess.SQL.EFRepository
{

    /// <summary>
    /// It's  abstract class which implements all the methods of IRepository
    /// </summary>
    /// <typeparam name="TEntity"> For any type of class which is inherited from BaseEntity</typeparam>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Database context
        /// </summary>
        public ApplicationDBContext Context { get; private set; }

        internal DbSet<TEntity> DbSet { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(ApplicationDBContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Add new entity to DB.
        /// </summary>
        /// <param name="entity">Entity to Add in DB</param>
        public async virtual Task Add(TEntity entity)
        {
           await DbSet.AddAsync(entity);

        }
        /// <summary>
        /// Get Entity by the Id of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="Id">Id field of any Entity</param>
        /// <returns></returns>
        public async Task<TEntity> Get(int Id)
        {
           return await DbSet.FindAsync(Id);
        }

        /// <summary>
        /// Get Entity by the Id of Entity which is previously stored on DB.
        /// </summary>
        /// <param name="MedicationItemKey">Id field of any Entity</param>
        /// <returns></returns>
        public async Task<TEntity> Get(Guid MedicationItemKey)
        {
            return await DbSet.FindAsync(MedicationItemKey);
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
