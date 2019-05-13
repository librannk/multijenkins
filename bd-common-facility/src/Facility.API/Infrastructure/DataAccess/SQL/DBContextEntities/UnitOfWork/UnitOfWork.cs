using System;
using System.Threading.Tasks;

namespace Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork
{
    /// <summary>
    /// UnitOfWork is class has functionality to commit the changes to DB.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private readonly FacilityDbContext _appDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="appDbContext">The application database context.</param>
        public UnitOfWork(FacilityDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        /// <summary>
        /// Implementation to perform multiple operation in single commit.
        /// </summary>
        /// <returns></returns>
        public Task<int> CommitChangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }

        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _appDbContext.Dispose();
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
