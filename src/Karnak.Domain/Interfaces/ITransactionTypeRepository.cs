using Karnak.Domain.Models;

namespace Karnak.Domain.Interfaces
{
    public interface ITransactionTypeRepository : IRepository<TransactionType>
    {
        TransactionType GetByName(string name);
    }
}