using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities
{
    /// <summary>
    /// ApplicationDBContext is class  which represent DB in memory.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {

        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

    }
}
