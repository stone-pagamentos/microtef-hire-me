using Karnak.Domain.Models;

namespace Karnak.Domain.Interfaces
{
    public interface ICardTypeRepository : IRepository<CardType>
    {
        CardType GetByName(string name);
    }
}