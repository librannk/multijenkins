using System;

namespace BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork
{
    /// <summary>
    /// UnitOfWork is class has functionality to commit the changes to DB.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _appDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appDbContext"></param>
        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        /// <summary>
        /// Implementation to perform multiple operation in single commit.
        /// </summary>
        /// <returns></returns>
        public int CommitChanges()
        {
            return _appDbContext.SaveChanges();
        }

        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            _disposed = true;
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
