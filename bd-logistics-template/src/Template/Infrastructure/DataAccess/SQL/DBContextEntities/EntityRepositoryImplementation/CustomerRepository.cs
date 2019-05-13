using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using BD.Template.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{
    /// <summary>
    /// CustomerRepository class implements the all member of ICustomerRepository of type Customer.
    /// </summary> 
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
