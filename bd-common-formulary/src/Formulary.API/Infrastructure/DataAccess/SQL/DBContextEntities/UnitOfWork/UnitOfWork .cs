using System;
using System.Threading.Tasks;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork
{
    /// <summary>
    /// UnitOfWork is class has functionality to commit the changes to DB.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDBContext _appDBContext;

        /// <summary>
        /// Constructor is used to initialize this class
        /// </summary>
        /// <param name="appDBContext"></param>
        public UnitOfWork(ApplicationDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        /// <summary>
        /// Implementation to perform multiple operation in single commit.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitChanges()
        {
            return await _appDBContext.SaveChangesAsync();
        }

        private bool disposed = false;
        /// <summary>
        /// Method to dispose this class
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _appDBContext.Dispose();
                }
            }
            this.disposed = true;
        }
        /// <summary>
        /// Dispose the ApplicationDBContext object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
