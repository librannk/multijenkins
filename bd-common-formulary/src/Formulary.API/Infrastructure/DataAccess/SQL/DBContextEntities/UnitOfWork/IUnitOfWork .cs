using System;
using System.Threading.Tasks;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork
{
    /// <summary>
    /// IUnitOfWork is an interface.Provide a abstract method to perform multiple operation in single unit.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// An abstract method to perform multiple operation in single commit.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitChanges();

    }

}
