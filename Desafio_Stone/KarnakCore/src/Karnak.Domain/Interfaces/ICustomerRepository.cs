using Karnak.Domain.Models;

namespace Karnak.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);

        Customer GetByName(string name);
    }
}