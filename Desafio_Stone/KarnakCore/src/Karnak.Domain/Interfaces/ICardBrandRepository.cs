using Karnak.Domain.Models;

namespace Karnak.Domain.Interfaces
{
    public interface ICardBrandRepository : IRepository<CardBrand>
    {
        CardBrand GetByName(string name);
    }
}