using Karnak.Domain.Models;

namespace Karnak.Domain.Interfaces
{
    public interface ITransactionStatusRepository : IRepository<TransactionStatus>
    {
        TransactionStatus GetByName(string name);
    }
}