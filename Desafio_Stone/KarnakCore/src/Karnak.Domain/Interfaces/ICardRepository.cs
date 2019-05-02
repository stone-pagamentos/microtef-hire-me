using Karnak.Domain.Models;

namespace Karnak.Domain.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        Card GetByCardNumber(string cardNumber);        
    }
}