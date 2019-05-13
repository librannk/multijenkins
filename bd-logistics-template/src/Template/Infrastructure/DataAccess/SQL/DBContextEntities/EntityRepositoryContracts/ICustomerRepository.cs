using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using BD.Template.API.Infrastructure.DataAccess.SQL.EFRepository;

namespace BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts
{
    /// <summary>
    /// ICustomerRepository interface represent the all member of IRepository of type Customer.
    /// ICustomerRepository provide the extensibility for new operation other than IRepository.
    /// </summary> 
    public interface ICustomerRepository : IRepository<Customer>
    {

    }
}
